# BromoAirlines — Desktop (Avalonia, MariaDB)

Aplikasi penjualan tiket pesawat: Admin (master + status) & Customer (cari, pesan, riwayat).
Adaptasi Linux dari spesifikasi WinForms + SQL Server (lihat § Portabilitas).

## 1. Cara menjalankan

```bash
# 1) Skema (sekali saja / reset total)
mysql -u root < Database.MySql.sql

# 2) Data realistis siap testing (idempotent, boleh diulang)
mysql -u root < Database.Seed.sql
# atau tanpa membuka UI:
dotnet run --project /home/akbar/Projects/CSharp/Project/BromoAirlines -- --seed
```

Isi seed (`Database.Seed.sql`, dijalankan via `Data/Seeder.cs`):
6 Negara, 12 Bandara real (SUB, CGK, DPS, UPG, KNO, YIA, SRG, SOC, BPN, MDC, PDG, SIN),
5 Maskapai (Garuda, Citilink, Lion, Batik, AirAsia Indonesia), 5 Status,
5 Promo (`HEMAT20`, `BROMO10`, `TERBANG15`, `MERDEKA25`, `PELAJAR5`, berlaku s/d 2027),
6 Akun, 14 Jadwal (tanggal relatif `CURDATE()+1..+5`, siap dicari di form Cari),
1 contoh Delay (GA-1001 45 mnt) + 1 Berangkat, 1 transaksi contoh milik `andi` + HEMAT20.

Koneksi: `App.config` → `bromo-airlines` (`Server=localhost;Uid=root`, tanpa password).
File SQL Server asli tetap ada: `Database.sql`.

## 2. Akun uji (dibuat aman, password BCrypt/plain-kompatibel)

| Username | Password   | Role     |
|----------|------------|----------|
| admin    | admin1234  | Admin    |
| andi     | andi12345  | Customer |

Daftar akun baru → selalu Customer (tanpa hak Admin).

## 3. Fitur (semua fungsional, parameterized query)

- Login (show/hide password, busy, block lintas-role) → Admin/CustomerMain
- Daftar Akun (unik, min 8, telp 10–15 digit, tgl ≤ hari ini)
- Admin sidebar: Master Bandara (sort A–Z, IATA 3 huruf unik CI), Maskapai (kru ≥1),
  Jadwal (regex `AA-1234`, asal≠tujuan, `HH:mm`, durasi `XX jam YY menit` menit 0–59,
  harga ≥1, tolak lewat tengah malam, sort DESC), Kode Promo (KAPITAL unik, ≥1),
  Ubah Status (terakhir per `WaktuPerubahanTerjadi`, kosong=`Sesuai Jadwal`,
  `Delay (selama ±XX jam YY menit)`, `dd-MM-yyyy HH:mm:ss`, insert riwayat)
- Customer: Cari (asal≠tujuan, pesan `tidak ada jadwal`), Pesan
  (`Diskon=MIN(Total*Persen/100,Max)`, harga dari DB, `MySqlTransaction` + rollback),
  Riwayat (milik sendiri saja + detail penumpang)
- Hapus terhalang FK (error 1451) → pesan ramah. Format tanggal `dd-MM-yyyy`.

## 4. Struktur

```
Models/Entities.cs  Data/Database.cs  Data/Repositories/  Services/AuthService.cs,
Services/PromoService.cs  Helpers/Validator.cs,DurationHelper.cs,Session.cs
Forms/LoginWindow,RegisterWindow,AdminMainWindow,CustomerMainWindow
Forms/Pages/MasterBandaraView,MasterMaskapaiView,MasterJadwalView,MasterPromoView,
  UbahStatusView,CariView,PesanWindow,RiwayatView
Resources/splash-logo.png (aset logo dari PDF)  App.config  Database.sql/.MySql.sql
```

## 5. Hasil pengujian

- `dotnet build`: **succeeded, 0 Error**.
- Helper (12): IATA, kode penerbangan, telp, password, durasi, tengah malam, diskon — PASS.
- Integrasi DB (29, `/tmp/itest`): login admin/customer/gagal, duplikat, pendek,
  telp invalid, CRUD bandara/maskapai/jadwal/promo, IATA, asal-sama, tengah malam,
  FK 1451, `Sesuai Jadwal`, Delay + format, diskon capped, transaksi rollback atomik,
  isolasi customer, logout, koneksi gagal — **29 pass 0 fail**.

## 6. Portabilitas & batasan

- WinForms → Avalonia `Window/UserControl` (WinForms tak jalan di Linux); pola
  sidebar + DataGrid + Simpan/Batal/Ubah/Hapus dipertahankan.
- SQL Server `Sql*` → MariaDB `MySqlConnector` (`MySql*`), pola ADO.NET sama.
- `FK_Jadwal_Berangkat/Tujuan`: `ON UPDATE RESTRICT` (MariaDB error 1901 bila kolom
  FK ber-CASCADE dipakai di `CHECK`); validasi asal≠tujuan tetap via CHECK + aplikasi.
- Tanpa `DataDictionary.xlsx` / `Style.pdf` / `Resources` asli: tipe disimpulkan dari
  ERD + PDF; logo dari ekstraksi `pdfimages` PDF C1.
- TODO: ganti `logo-blue.png` bila dapat aset resmi; tambah pagination bila data besar.
