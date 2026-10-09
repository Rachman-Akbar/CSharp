-- =============================================
-- SEED realistis: BromoAirlines (MySQL / MariaDB)
-- Cara pakai:  mysql -u root < Database.Seed.sql
--              atau: dotnet run --project . -- --seed
-- Idempotent: aman dijalankan berulang (IGNORE / NOT EXISTS / ON DUPLICATE).
-- Password seed = plaintext demo (didukung fallback login);
-- akun daftar-baru otomatis memakai hash BCrypt.
-- =============================================
USE `bromo-airlines`;

-- ---------- Negara ----------
INSERT INTO Negara (Nama, IbukotaNegara)
SELECT * FROM (SELECT 'Indonesia' AS Nama, 'Jakarta' AS IbukotaNegara) t
WHERE NOT EXISTS (SELECT 1 FROM Negara WHERE Nama = 'Indonesia');
INSERT INTO Negara (Nama, IbukotaNegara)
SELECT * FROM (SELECT 'Malaysia' AS Nama, 'Kuala Lumpur' AS IbukotaNegara) t
WHERE NOT EXISTS (SELECT 1 FROM Negara WHERE Nama = 'Malaysia');
INSERT INTO Negara (Nama, IbukotaNegara)
SELECT * FROM (SELECT 'Singapura' AS Nama, 'Singapura' AS IbukotaNegara) t
WHERE NOT EXISTS (SELECT 1 FROM Negara WHERE Nama = 'Singapura');
INSERT INTO Negara (Nama, IbukotaNegara)
SELECT * FROM (SELECT 'Thailand' AS Nama, 'Bangkok' AS IbukotaNegara) t
WHERE NOT EXISTS (SELECT 1 FROM Negara WHERE Nama = 'Thailand');
INSERT INTO Negara (Nama, IbukotaNegara)
SELECT * FROM (SELECT 'Australia' AS Nama, 'Canberra' AS IbukotaNegara) t
WHERE NOT EXISTS (SELECT 1 FROM Negara WHERE Nama = 'Australia');
INSERT INTO Negara (Nama, IbukotaNegara)
SELECT * FROM (SELECT 'Jepang' AS Nama, 'Tokyo' AS IbukotaNegara) t
WHERE NOT EXISTS (SELECT 1 FROM Negara WHERE Nama = 'Jepang');

-- ---------- Bandara (real, Indonesia + Changi) ----------
INSERT INTO Bandara (Nama, KodeIATA, Kota, NegaraID, JumlahTerminal, Alamat) VALUES
('Bandara Juanda', 'SUB', 'Surabaya', (SELECT ID FROM Negara WHERE Nama='Indonesia' LIMIT 1), 2, 'Jl. Raya Bandara Juanda, Sidoarjo, Jawa Timur'),
('Bandara Soekarno Hatta', 'CGK', 'Jakarta', (SELECT ID FROM Negara WHERE Nama='Indonesia' LIMIT 1), 3, 'Tangerang, Banten'),
('Bandara Ngurah Rai', 'DPS', 'Denpasar', (SELECT ID FROM Negara WHERE Nama='Indonesia' LIMIT 1), 2, 'Jl. Raya Gusti Ngurah Rai, Badung, Bali'),
('Bandara Sultan Hasanuddin', 'UPG', 'Makassar', (SELECT ID FROM Negara WHERE Nama='Indonesia' LIMIT 1), 2, 'Mandai, Maros, Sulawesi Selatan'),
('Bandara Kualanamu', 'KNO', 'Medan', (SELECT ID FROM Negara WHERE Nama='Indonesia' LIMIT 1), 1, 'Beringin, Deli Serdang, Sumatera Utara'),
('Bandara Yogyakarta', 'YIA', 'Yogyakarta', (SELECT ID FROM Negara WHERE Nama='Indonesia' LIMIT 1), 1, 'Temon, Kulon Progo, DIY'),
('Bandara Ahmad Yani', 'SRG', 'Semarang', (SELECT ID FROM Negara WHERE Nama='Indonesia' LIMIT 1), 1, 'Jl. Puad Ahmad Yani, Semarang'),
('Bandara Adi Soemarmo', 'SOC', 'Solo', (SELECT ID FROM Negara WHERE Nama='Indonesia' LIMIT 1), 1, 'Ngaru-aru, Boyolali, Jawa Tengah'),
('Bandara Sepinggan', 'BPN', 'Balikpapan', (SELECT ID FROM Negara WHERE Nama='Indonesia' LIMIT 1), 1, 'Jl. Marsma R. Iswahyudi, Balikpapan'),
('Bandara Sam Ratulangi', 'MDC', 'Manado', (SELECT ID FROM Negara WHERE Nama='Indonesia' LIMIT 1), 1, 'Jl. A.A. Maramis, Manado'),
('Bandara Minangkabau', 'PDG', 'Padang', (SELECT ID FROM Negara WHERE Nama='Indonesia' LIMIT 1), 1, 'Ketaping, Padang Pariaman, Sumatera Barat'),
('Bandara Changi', 'SIN', 'Singapura', (SELECT ID FROM Negara WHERE Nama='Singapura' LIMIT 1), 4, 'Airport Blvd, Singapura')
ON DUPLICATE KEY UPDATE Kota=VALUES(Kota), JumlahTerminal=VALUES(JumlahTerminal), Alamat=VALUES(Alamat);

-- ---------- Maskapai (real Indonesia) ----------
INSERT INTO Maskapai (Nama, Perusahaan, JumlahKru, Deskripsi)
SELECT * FROM (SELECT 'Garuda Indonesia' AS Nama, 'PT Garuda Indonesia (Persero) Tbk' AS Perusahaan, 18 AS JumlahKru, 'Flag carrier full-service Indonesia' AS Deskripsi) t
WHERE NOT EXISTS (SELECT 1 FROM Maskapai WHERE Nama='Garuda Indonesia');
INSERT INTO Maskapai (Nama, Perusahaan, JumlahKru, Deskripsi)
SELECT * FROM (SELECT 'Citilink' AS Nama, 'PT Citilink Indonesia' AS Perusahaan, 10 AS JumlahKru, 'LCC anak usaha Garuda Indonesia' AS Deskripsi) t
WHERE NOT EXISTS (SELECT 1 FROM Maskapai WHERE Nama='Citilink');
INSERT INTO Maskapai (Nama, Perusahaan, JumlahKru, Deskripsi)
SELECT * FROM (SELECT 'Lion Air' AS Nama, 'PT Lion Mentari Airlines' AS Perusahaan, 12 AS JumlahKru, 'LCC terbesar Indonesia, Lion Group' AS Deskripsi) t
WHERE NOT EXISTS (SELECT 1 FROM Maskapai WHERE Nama='Lion Air');
INSERT INTO Maskapai (Nama, Perusahaan, JumlahKru, Deskripsi)
SELECT * FROM (SELECT 'Batik Air' AS Nama, 'PT Batik Air Indonesia' AS Perusahaan, 10 AS JumlahKru, 'Full-service Lion Group' AS Deskripsi) t
WHERE NOT EXISTS (SELECT 1 FROM Maskapai WHERE Nama='Batik Air');
INSERT INTO Maskapai (Nama, Perusahaan, JumlahKru, Deskripsi)
SELECT * FROM (SELECT 'AirAsia Indonesia' AS Nama, 'PT Indonesia AirAsia' AS Perusahaan, 8 AS JumlahKru, 'LCC grup AirAsia' AS Deskripsi) t
WHERE NOT EXISTS (SELECT 1 FROM Maskapai WHERE Nama='AirAsia Indonesia');

-- ---------- Status ----------
INSERT IGNORE INTO StatusPenerbangan (Nama) VALUES
('Sesuai Jadwal'), ('Delay'), ('Batal'), ('Berangkat'), ('Tiba');

-- ---------- Kode Promo ----------
INSERT INTO KodePromo (Kode, PersentaseDiskon, MaksimumDiskon, BerlakuSampai, Deskripsi) VALUES
('HEMAT20', 20, 100000, '2027-06-30', 'Diskon 20% maks Rp100rb'),
('BROMO10', 10, 50000, '2027-12-31', 'Diskon 10% maks Rp50rb'),
('TERBANG15', 15, 150000, '2027-03-31', 'Diskon 15% maks Rp150rb'),
('MERDEKA25', 25, 200000, '2027-08-31', 'Promo kemerdekaan 25% maks Rp200rb'),
('PELAJAR5', 5, 25000, '2027-12-31', 'Diskon pelajar 5% maks Rp25rb')
ON DUPLICATE KEY UPDATE PersentaseDiskon=VALUES(PersentaseDiskon), MaksimumDiskon=VALUES(MaksimumDiskon), BerlakuSampai=VALUES(BerlakuSampai);

-- ---------- Akun ----------
INSERT INTO Akun (Username, `Password`, Nama, TanggalLahir, NomorTelepon, MerupakanAdmin) VALUES
('admin', 'admin1234', 'Administrator', '1990-01-01', '081234567890', 1),
('andi', 'andi12345', 'Andi Taulany', '2003-01-02', '081234567891', 0),
('budi', 'budi12345', 'Budi Santoso', '1995-03-15', '081234567892', 0),
('siti', 'siti12345', 'Siti Rahayu', '1998-07-22', '081234567893', 0),
('dewi', 'dewi12345', 'Dewi Lestari', '2001-11-30', '081234567894', 0),
('agus', 'agus12345', 'Agus Wijaya', '1990-05-10', '081234567895', 0)
ON DUPLICATE KEY UPDATE Nama=VALUES(Nama);

-- ---------- Jadwal (tanggal relatif hari ini, siap testing Cari) ----------
INSERT IGNORE INTO JadwalPenerbangan (KodePenerbangan, BandaraKeberangkatanID, BandaraTujuanID, MaskapaiID, TanggalWaktuKeberangkatan, DurasiPenerbangan, HargaPerTiket) VALUES
('GA-1001', (SELECT ID FROM Bandara WHERE KodeIATA='SUB' LIMIT 1), (SELECT ID FROM Bandara WHERE KodeIATA='CGK' LIMIT 1), (SELECT ID FROM Maskapai WHERE Nama='Garuda Indonesia' LIMIT 1), TIMESTAMP(DATE_ADD(CURDATE(), INTERVAL 1 DAY), '06:00:00'), 90, 750000),
('GA-1002', (SELECT ID FROM Bandara WHERE KodeIATA='CGK' LIMIT 1), (SELECT ID FROM Bandara WHERE KodeIATA='DPS' LIMIT 1), (SELECT ID FROM Maskapai WHERE Nama='Garuda Indonesia' LIMIT 1), TIMESTAMP(DATE_ADD(CURDATE(), INTERVAL 1 DAY), '09:00:00'), 110, 950000),
('GA-1003', (SELECT ID FROM Bandara WHERE KodeIATA='SUB' LIMIT 1), (SELECT ID FROM Bandara WHERE KodeIATA='UPG' LIMIT 1), (SELECT ID FROM Maskapai WHERE Nama='Garuda Indonesia' LIMIT 1), TIMESTAMP(DATE_ADD(CURDATE(), INTERVAL 2 DAY), '12:00:00'), 120, 800000),
('QG-2001', (SELECT ID FROM Bandara WHERE KodeIATA='SUB' LIMIT 1), (SELECT ID FROM Bandara WHERE KodeIATA='DPS' LIMIT 1), (SELECT ID FROM Maskapai WHERE Nama='Citilink' LIMIT 1), TIMESTAMP(DATE_ADD(CURDATE(), INTERVAL 1 DAY), '07:30:00'), 60, 650000),
('QG-2002', (SELECT ID FROM Bandara WHERE KodeIATA='DPS' LIMIT 1), (SELECT ID FROM Bandara WHERE KodeIATA='CGK' LIMIT 1), (SELECT ID FROM Maskapai WHERE Nama='Citilink' LIMIT 1), TIMESTAMP(DATE_ADD(CURDATE(), INTERVAL 2 DAY), '14:00:00'), 110, 700000),
('QG-2003', (SELECT ID FROM Bandara WHERE KodeIATA='CGK' LIMIT 1), (SELECT ID FROM Bandara WHERE KodeIATA='YIA' LIMIT 1), (SELECT ID FROM Maskapai WHERE Nama='Citilink' LIMIT 1), TIMESTAMP(DATE_ADD(CURDATE(), INTERVAL 1 DAY), '17:30:00'), 65, 550000),
('JT-3001', (SELECT ID FROM Bandara WHERE KodeIATA='CGK' LIMIT 1), (SELECT ID FROM Bandara WHERE KodeIATA='UPG' LIMIT 1), (SELECT ID FROM Maskapai WHERE Nama='Lion Air' LIMIT 1), TIMESTAMP(DATE_ADD(CURDATE(), INTERVAL 2 DAY), '10:15:00'), 135, 850000),
('JT-3002', (SELECT ID FROM Bandara WHERE KodeIATA='UPG' LIMIT 1), (SELECT ID FROM Bandara WHERE KodeIATA='CGK' LIMIT 1), (SELECT ID FROM Maskapai WHERE Nama='Lion Air' LIMIT 1), TIMESTAMP(DATE_ADD(CURDATE(), INTERVAL 3 DAY), '16:45:00'), 135, 850000),
('JT-3003', (SELECT ID FROM Bandara WHERE KodeIATA='SUB' LIMIT 1), (SELECT ID FROM Bandara WHERE KodeIATA='BPN' LIMIT 1), (SELECT ID FROM Maskapai WHERE Nama='Lion Air' LIMIT 1), TIMESTAMP(DATE_ADD(CURDATE(), INTERVAL 3 DAY), '09:45:00'), 120, 950000),
('ID-4001', (SELECT ID FROM Bandara WHERE KodeIATA='CGK' LIMIT 1), (SELECT ID FROM Bandara WHERE KodeIATA='KNO' LIMIT 1), (SELECT ID FROM Maskapai WHERE Nama='Batik Air' LIMIT 1), TIMESTAMP(DATE_ADD(CURDATE(), INTERVAL 2 DAY), '08:30:00'), 150, 1100000),
('ID-4002', (SELECT ID FROM Bandara WHERE KodeIATA='KNO' LIMIT 1), (SELECT ID FROM Bandara WHERE KodeIATA='CGK' LIMIT 1), (SELECT ID FROM Maskapai WHERE Nama='Batik Air' LIMIT 1), TIMESTAMP(DATE_ADD(CURDATE(), INTERVAL 3 DAY), '13:00:00'), 150, 1100000),
('ID-4003', (SELECT ID FROM Bandara WHERE KodeIATA='DPS' LIMIT 1), (SELECT ID FROM Bandara WHERE KodeIATA='UPG' LIMIT 1), (SELECT ID FROM Maskapai WHERE Nama='Batik Air' LIMIT 1), TIMESTAMP(DATE_ADD(CURDATE(), INTERVAL 4 DAY), '07:00:00'), 130, 880000),
('QZ-5001', (SELECT ID FROM Bandara WHERE KodeIATA='CGK' LIMIT 1), (SELECT ID FROM Bandara WHERE KodeIATA='SIN' LIMIT 1), (SELECT ID FROM Maskapai WHERE Nama='AirAsia Indonesia' LIMIT 1), TIMESTAMP(DATE_ADD(CURDATE(), INTERVAL 4 DAY), '11:20:00'), 105, 900000),
('QZ-5002', (SELECT ID FROM Bandara WHERE KodeIATA='SIN' LIMIT 1), (SELECT ID FROM Bandara WHERE KodeIATA='CGK' LIMIT 1), (SELECT ID FROM Maskapai WHERE Nama='AirAsia Indonesia' LIMIT 1), TIMESTAMP(DATE_ADD(CURDATE(), INTERVAL 5 DAY), '15:40:00'), 105, 900000);

-- ---------- Contoh riwayat status (untuk testing Ubah Status) ----------
INSERT INTO PerubahanStatusJadwalPenerbangan (JadwalPenerbanganID, StatusPenerbanganID, WaktuPerubahanTerjadi, PerkiraanWaktuDelay)
SELECT j.ID, s.ID, NOW(), 45 FROM JadwalPenerbangan j, StatusPenerbangan s
WHERE j.KodePenerbangan='GA-1001' AND s.Nama='Delay'
AND NOT EXISTS (SELECT 1 FROM PerubahanStatusJadwalPenerbangan p WHERE p.JadwalPenerbanganID=j.ID);
INSERT INTO PerubahanStatusJadwalPenerbangan (JadwalPenerbanganID, StatusPenerbanganID, WaktuPerubahanTerjadi, PerkiraanWaktuDelay)
SELECT j.ID, s.ID, DATE_SUB(NOW(), INTERVAL 1 HOUR), NULL FROM JadwalPenerbangan j, StatusPenerbangan s
WHERE j.KodePenerbangan='JT-3001' AND s.Nama='Berangkat'
AND NOT EXISTS (SELECT 1 FROM PerubahanStatusJadwalPenerbangan p WHERE p.JadwalPenerbanganID=j.ID);

-- ---------- Contoh transaksi andi + HEMAT20 (2 pax GA-1001: kotor 1,5jt - 100rb = 1,4jt) ----------
INSERT INTO TransaksiHeader (AkunID, TanggalTransaksi, JadwalPenerbanganID, JumlahPenumpang, TotalHarga, KodePromoID)
SELECT a.ID, NOW(), j.ID, 2, 1400000,
  (SELECT ID FROM KodePromo WHERE Kode='HEMAT20' LIMIT 1)
FROM Akun a, JadwalPenerbangan j
WHERE a.Username='andi' AND j.KodePenerbangan='GA-1001'
AND NOT EXISTS (SELECT 1 FROM TransaksiHeader h WHERE h.AkunID=a.ID AND h.JadwalPenerbanganID=j.ID);
INSERT INTO TransaksiDetail (TransaksiHeaderID, TitelPenumpang, NamaLengkapPenumpang)
SELECT h.ID, 'Tn', 'Andi Taulany' FROM TransaksiHeader h
JOIN Akun a ON a.ID=h.AkunID JOIN JadwalPenerbangan j ON j.ID=h.JadwalPenerbanganID
WHERE a.Username='andi' AND j.KodePenerbangan='GA-1001'
AND NOT EXISTS (SELECT 1 FROM TransaksiDetail d WHERE d.TransaksiHeaderID=h.ID);
INSERT INTO TransaksiDetail (TransaksiHeaderID, TitelPenumpang, NamaLengkapPenumpang)
SELECT h.ID, 'Ny', 'Siti Taulany' FROM TransaksiHeader h
JOIN Akun a ON a.ID=h.AkunID JOIN JadwalPenerbangan j ON j.ID=h.JadwalPenerbanganID
WHERE a.Username='andi' AND j.KodePenerbangan='GA-1001'
AND (SELECT COUNT(*) FROM TransaksiDetail d WHERE d.TransaksiHeaderID=h.ID) = 1;
