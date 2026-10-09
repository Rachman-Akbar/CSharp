-- =============================================
-- Database: `bromo-airlines` (MySQL / MariaDB)
-- Konversi dari versi SQL Server, struktur sama
-- Cara pakai: mysql -u root < Database.MySql.sql
-- =============================================

CREATE DATABASE IF NOT EXISTS `bromo-airlines`
  CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

USE `bromo-airlines`;

SET FOREIGN_KEY_CHECKS = 0;

DROP TABLE IF EXISTS `TransaksiDetail`;
DROP TABLE IF EXISTS `TransaksiHeader`;
DROP TABLE IF EXISTS `PerubahanStatusJadwalPenerbangan`;
DROP TABLE IF EXISTS `JadwalPenerbangan`;
DROP TABLE IF EXISTS `Akun`;
DROP TABLE IF EXISTS `KodePromo`;
DROP TABLE IF EXISTS `StatusPenerbangan`;
DROP TABLE IF EXISTS `Maskapai`;
DROP TABLE IF EXISTS `Bandara`;
DROP TABLE IF EXISTS `Negara`;

SET FOREIGN_KEY_CHECKS = 1;

CREATE TABLE `Negara` (
    `ID` INT AUTO_INCREMENT PRIMARY KEY,
    `Nama` VARCHAR(100) NOT NULL,
    `IbukotaNegara` VARCHAR(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `Bandara` (
    `ID` INT AUTO_INCREMENT PRIMARY KEY,
    `Nama` VARCHAR(150) NOT NULL,
    `KodeIATA` CHAR(3) NOT NULL,
    `Kota` VARCHAR(100) NOT NULL,
    `NegaraID` INT NOT NULL,
    `JumlahTerminal` INT NOT NULL,
    `Alamat` VARCHAR(255) NOT NULL,
    CONSTRAINT `UQ_Bandara_Nama` UNIQUE (`Nama`),
    CONSTRAINT `UQ_Bandara_KodeIATA` UNIQUE (`KodeIATA`),
    CONSTRAINT `FK_Bandara_Negara` FOREIGN KEY (`NegaraID`)
        REFERENCES `Negara` (`ID`)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `Maskapai` (
    `ID` INT AUTO_INCREMENT PRIMARY KEY,
    `Nama` VARCHAR(100) NOT NULL,
    `Perusahaan` VARCHAR(150) NOT NULL,
    `JumlahKru` INT NOT NULL,
    `Deskripsi` VARCHAR(500) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `StatusPenerbangan` (
    `ID` INT AUTO_INCREMENT PRIMARY KEY,
    `Nama` VARCHAR(50) NOT NULL UNIQUE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `KodePromo` (
    `ID` INT AUTO_INCREMENT PRIMARY KEY,
    `Kode` VARCHAR(30) NOT NULL,
    `PersentaseDiskon` DECIMAL(5,2) NOT NULL,
    `MaksimumDiskon` DECIMAL(18,2) NOT NULL,
    `BerlakuSampai` DATE NOT NULL,
    `Deskripsi` VARCHAR(500) NOT NULL,
    CONSTRAINT `UQ_KodePromo_Kode` UNIQUE (`Kode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `Akun` (
    `ID` INT AUTO_INCREMENT PRIMARY KEY,
    `Username` VARCHAR(50) NOT NULL,
    `Password` VARCHAR(255) NOT NULL,
    `Nama` VARCHAR(100) NOT NULL,
    `TanggalLahir` DATE NOT NULL,
    `NomorTelepon` VARCHAR(20) NOT NULL,
    `MerupakanAdmin` TINYINT(1) NOT NULL,
    CONSTRAINT `UQ_Akun_Username` UNIQUE (`Username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `JadwalPenerbangan` (
    `ID` INT AUTO_INCREMENT PRIMARY KEY,
    `KodePenerbangan` VARCHAR(10) NOT NULL,
    `BandaraKeberangkatanID` INT NOT NULL,
    `BandaraTujuanID` INT NOT NULL,
    `MaskapaiID` INT NOT NULL,
    `TanggalWaktuKeberangkatan` DATETIME NOT NULL,
    `DurasiPenerbangan` INT NOT NULL,
    `HargaPerTiket` DECIMAL(18,2) NOT NULL,
    CONSTRAINT `UQ_Jadwal_Kode` UNIQUE (`KodePenerbangan`),
    CONSTRAINT `CK_Jadwal_BandaraBeda` CHECK (`BandaraKeberangkatanID` <> `BandaraTujuanID`),
    CONSTRAINT `FK_Jadwal_Berangkat` FOREIGN KEY (`BandaraKeberangkatanID`)
        REFERENCES `Bandara` (`ID`)
        ON DELETE RESTRICT
        ON UPDATE RESTRICT,
    CONSTRAINT `FK_Jadwal_Tujuan` FOREIGN KEY (`BandaraTujuanID`)
        REFERENCES `Bandara` (`ID`)
        ON DELETE RESTRICT
        ON UPDATE RESTRICT,
    CONSTRAINT `FK_Jadwal_Maskapai` FOREIGN KEY (`MaskapaiID`)
        REFERENCES `Maskapai` (`ID`)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `PerubahanStatusJadwalPenerbangan` (
    `ID` INT AUTO_INCREMENT PRIMARY KEY,
    `JadwalPenerbanganID` INT NOT NULL,
    `StatusPenerbanganID` INT NOT NULL,
    `WaktuPerubahanTerjadi` DATETIME NOT NULL,
    `PerkiraanWaktuDelay` INT NULL,
    CONSTRAINT `FK_Perubahan_Jadwal` FOREIGN KEY (`JadwalPenerbanganID`)
        REFERENCES `JadwalPenerbangan` (`ID`)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT `FK_Perubahan_Status` FOREIGN KEY (`StatusPenerbanganID`)
        REFERENCES `StatusPenerbangan` (`ID`)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `TransaksiHeader` (
    `ID` INT AUTO_INCREMENT PRIMARY KEY,
    `AkunID` INT NOT NULL,
    `TanggalTransaksi` DATETIME NOT NULL,
    `JadwalPenerbanganID` INT NOT NULL,
    `JumlahPenumpang` INT NOT NULL,
    `TotalHarga` DECIMAL(18,2) NOT NULL,
    `KodePromoID` INT NULL,
    CONSTRAINT `FK_Transaksi_Akun` FOREIGN KEY (`AkunID`)
        REFERENCES `Akun` (`ID`)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,
    CONSTRAINT `FK_Transaksi_Jadwal` FOREIGN KEY (`JadwalPenerbanganID`)
        REFERENCES `JadwalPenerbangan` (`ID`)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,
    CONSTRAINT `FK_Transaksi_Promo` FOREIGN KEY (`KodePromoID`)
        REFERENCES `KodePromo` (`ID`)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `TransaksiDetail` (
    `ID` INT AUTO_INCREMENT PRIMARY KEY,
    `TransaksiHeaderID` INT NOT NULL,
    `TitelPenumpang` VARCHAR(10) NOT NULL,
    `NamaLengkapPenumpang` VARCHAR(150) NOT NULL,
    CONSTRAINT `FK_Detail_Header` FOREIGN KEY (`TransaksiHeaderID`)
        REFERENCES `TransaksiHeader` (`ID`)
        ON DELETE CASCADE
        ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
