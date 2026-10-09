-- =============================================
-- Database: BromoAirlines (SQL Server / T-SQL)
-- Struktur sesuai diagram: hanya tabel, kolom, PK, dan FK
-- Perbaikan: named FK, UNIQUE, CHECK, CASCADE yang benar, re-runnable
-- =============================================
IF DB_ID('BromoAirlines') IS NULL
    CREATE DATABASE BromoAirlines;
GO

USE BromoAirlines;
GO

-- Drop dengan urutan terbalik agar FK tidak menghalangi (re-runnable)
IF OBJECT_ID('dbo.TransaksiDetail', 'U') IS NOT NULL DROP TABLE dbo.TransaksiDetail;
IF OBJECT_ID('dbo.TransaksiHeader', 'U') IS NOT NULL DROP TABLE dbo.TransaksiHeader;
IF OBJECT_ID('dbo.PerubahanStatusJadwalPenerbangan', 'U') IS NOT NULL DROP TABLE dbo.PerubahanStatusJadwalPenerbangan;
IF OBJECT_ID('dbo.JadwalPenerbangan', 'U') IS NOT NULL DROP TABLE dbo.JadwalPenerbangan;
IF OBJECT_ID('dbo.Akun', 'U') IS NOT NULL DROP TABLE dbo.Akun;
IF OBJECT_ID('dbo.KodePromo', 'U') IS NOT NULL DROP TABLE dbo.KodePromo;
IF OBJECT_ID('dbo.StatusPenerbangan', 'U') IS NOT NULL DROP TABLE dbo.StatusPenerbangan;
IF OBJECT_ID('dbo.Maskapai', 'U') IS NOT NULL DROP TABLE dbo.Maskapai;
IF OBJECT_ID('dbo.Bandara', 'U') IS NOT NULL DROP TABLE dbo.Bandara;
IF OBJECT_ID('dbo.Negara', 'U') IS NOT NULL DROP TABLE dbo.Negara;
GO

CREATE TABLE Negara (
    ID            INT IDENTITY(1,1) PRIMARY KEY,
    Nama          VARCHAR(100) NOT NULL,
    IbukotaNegara VARCHAR(100) NOT NULL
);

CREATE TABLE Bandara (
    ID             INT IDENTITY(1,1) PRIMARY KEY,
    Nama           VARCHAR(150) NOT NULL,
    KodeIATA       CHAR(3)      NOT NULL,
    Kota           VARCHAR(100) NOT NULL,
    NegaraID       INT          NOT NULL,
    JumlahTerminal INT          NOT NULL,
    Alamat         VARCHAR(255) NOT NULL,
    CONSTRAINT UQ_Bandara_Nama UNIQUE (Nama),
    CONSTRAINT UQ_Bandara_KodeIATA UNIQUE (KodeIATA),
    CONSTRAINT FK_Bandara_Negara FOREIGN KEY (NegaraID)
        REFERENCES Negara(ID)
        ON DELETE NO ACTION
        ON UPDATE CASCADE
);

CREATE TABLE Maskapai (
    ID         INT IDENTITY(1,1) PRIMARY KEY,
    Nama       VARCHAR(100) NOT NULL,
    Perusahaan VARCHAR(150) NOT NULL,
    JumlahKru  INT          NOT NULL,
    Deskripsi  VARCHAR(500) NOT NULL
);

CREATE TABLE StatusPenerbangan (
    ID   INT IDENTITY(1,1) PRIMARY KEY,
    Nama VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE KodePromo (
    ID               INT IDENTITY(1,1) PRIMARY KEY,
    Kode             VARCHAR(30)   NOT NULL,
    PersentaseDiskon DECIMAL(5,2)  NOT NULL,
    MaksimumDiskon   DECIMAL(18,2) NOT NULL,
    BerlakuSampai    DATE          NOT NULL,
    Deskripsi        VARCHAR(500)  NOT NULL,
    CONSTRAINT UQ_KodePromo_Kode UNIQUE (Kode)
);

CREATE TABLE Akun (
    ID             INT IDENTITY(1,1) PRIMARY KEY,
    Username       VARCHAR(50)  NOT NULL,
    Password       VARCHAR(255) NOT NULL,
    Nama           VARCHAR(100) NOT NULL,
    TanggalLahir   DATE         NOT NULL,
    NomorTelepon   VARCHAR(20)  NOT NULL,
    MerupakanAdmin BIT          NOT NULL,
    CONSTRAINT UQ_Akun_Username UNIQUE (Username)
);

CREATE TABLE JadwalPenerbangan (
    ID                        INT IDENTITY(1,1) PRIMARY KEY,
    KodePenerbangan           VARCHAR(10)   NOT NULL,
    BandaraKeberangkatanID    INT           NOT NULL,
    BandaraTujuanID           INT           NOT NULL,
    MaskapaiID                INT           NOT NULL,
    TanggalWaktuKeberangkatan DATETIME      NOT NULL,
    DurasiPenerbangan         INT           NOT NULL,
    HargaPerTiket             DECIMAL(18,2) NOT NULL,
    CONSTRAINT UQ_Jadwal_Kode UNIQUE (KodePenerbangan),
    CONSTRAINT CK_Jadwal_BandaraBeda CHECK (BandaraKeberangkatanID <> BandaraTujuanID),
    CONSTRAINT FK_Jadwal_Berangkat FOREIGN KEY (BandaraKeberangkatanID)
        REFERENCES Bandara(ID)
        ON DELETE NO ACTION
        ON UPDATE CASCADE,
    CONSTRAINT FK_Jadwal_Tujuan FOREIGN KEY (BandaraTujuanID)
        REFERENCES Bandara(ID)
        ON DELETE NO ACTION
        ON UPDATE CASCADE,
    CONSTRAINT FK_Jadwal_Maskapai FOREIGN KEY (MaskapaiID)
        REFERENCES Maskapai(ID)
        ON DELETE NO ACTION
        ON UPDATE CASCADE
);

CREATE TABLE PerubahanStatusJadwalPenerbangan (
    ID                    INT IDENTITY(1,1) PRIMARY KEY,
    JadwalPenerbanganID   INT      NOT NULL,
    StatusPenerbanganID   INT      NOT NULL,
    WaktuPerubahanTerjadi DATETIME NOT NULL,
    PerkiraanWaktuDelay   INT      NULL,
    CONSTRAINT FK_Perubahan_Jadwal FOREIGN KEY (JadwalPenerbanganID)
        REFERENCES JadwalPenerbangan(ID)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT FK_Perubahan_Status FOREIGN KEY (StatusPenerbanganID)
        REFERENCES StatusPenerbangan(ID)
        ON DELETE NO ACTION
        ON UPDATE CASCADE
);

CREATE TABLE TransaksiHeader (
    ID                  INT IDENTITY(1,1) PRIMARY KEY,
    AkunID              INT           NOT NULL,
    TanggalTransaksi    DATETIME      NOT NULL,
    JadwalPenerbanganID INT           NOT NULL,
    JumlahPenumpang     INT           NOT NULL,
    TotalHarga          DECIMAL(18,2) NOT NULL,
    KodePromoID         INT           NULL,
    CONSTRAINT FK_Transaksi_Akun FOREIGN KEY (AkunID)
        REFERENCES Akun(ID)
        ON DELETE NO ACTION
        ON UPDATE CASCADE,
    CONSTRAINT FK_Transaksi_Jadwal FOREIGN KEY (JadwalPenerbanganID)
        REFERENCES JadwalPenerbangan(ID)
        ON DELETE NO ACTION
        ON UPDATE CASCADE,
    CONSTRAINT FK_Transaksi_Promo FOREIGN KEY (KodePromoID)
        REFERENCES KodePromo(ID)
        ON DELETE NO ACTION
        ON UPDATE CASCADE
);

CREATE TABLE TransaksiDetail (
    ID                   INT IDENTITY(1,1) PRIMARY KEY,
    TransaksiHeaderID    INT          NOT NULL,
    TitelPenumpang       VARCHAR(10)  NOT NULL,
    NamaLengkapPenumpang VARCHAR(150) NOT NULL,
    CONSTRAINT FK_Detail_Header FOREIGN KEY (TransaksiHeaderID)
        REFERENCES TransaksiHeader(ID)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GO
