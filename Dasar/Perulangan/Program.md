// ============================================================================
// MATERI C# LANJUTAN : Perulangan (For, While, dan Foreach)
// File     : Program.cs
// Cara run : dotnet run (dari folder Perulangan/)
// Syarat   : Sudah paham Variabel & Percabangan (materi sebelumnya)
// ============================================================================

// ----------------------------------------------------------------------------
// BAGIAN 0 : Pengenalan Perulangan (Loop)
// Perulangan = menjalankan blok kode BERULANG-KALI tanpa menulis ulang.
// ----------------------------------------------------------------------------
Console.WriteLine("=== Belajar C#: Perulangan For, While, dan Foreach ===");
Console.WriteLine(); // Baris kosong agar output rapi

// ============================================================================
// BAGIAN 1 : FOR (jumlah pengulangan SUDAH JELAS / pasti)
// Pola: for (nilaiAwal; kondisi; perubahan) { ... }
// 1) nilaiAwal  : dijalankan SEKALI di awal (contoh: int i = 1)
// 2) kondisi    : dicek SEBELUM tiap putaran, true = lanjut, false = berhenti
// 3) perubahan  : dijalankan SETELAH tiap putaran (contoh: i++)
// ============================================================================

// --- 1a. FOR dasar : cetak angka 1 sampai 5 ---
Console.WriteLine("[FOR] Cetak 1 sampai 5:"); // Judul output
for (int i = 1; i <= 5; i++) // i mulai 1; selama i<=5 lanjut; tiap putaran i+1. Putaran: 1,2,3,4,5
{
    Console.WriteLine($"[FOR] i = {i}"); // Tampilkan nilai i saat ini
}

// --- 1b. FOR mundur : cetak 5 sampai 1 dengan i-- ---
Console.WriteLine("[FOR] Mundur 5 sampai 1:"); // Judul output
for (int i = 5; i >= 1; i--) // i mulai 5; selama i>=1 lanjut; tiap putaran i-1. Putaran: 5,4,3,2,1
{
    Console.WriteLine($"[FOR] i = {i}"); // Tampilkan nilai i saat ini
}

// --- 1c. FOR loncat 2 : cetak bilangan genap 2-10 dengan i += 2 ---
Console.WriteLine("[FOR] Bilangan genap 2-10:"); // Judul output
for (int i = 2; i <= 10; i += 2) // i mulai 2; tiap putaran tambah 2. Putaran: 2,4,6,8,10
{
    Console.WriteLine($"[FOR] i = {i}"); // Tampilkan nilai i (selalu genap)
}

// --- 1d. FOR menghitung total : jumlahkan 1+2+...+10 ---
int total = 0; // Penampung hasil, awalnya 0
for (int i = 1; i <= 10; i++) // Ulangi i dari 1 sampai 10
{
    total += i; // Sama dengan total = total + i. Putaran 1: 0+1=1, putaran 2: 1+2=3, dst.
}
Console.WriteLine($"[FOR] Jumlah 1+2+...+10 = {total}"); // Hasil akhir = 55

Console.WriteLine(); // Pemisah bagian

// ============================================================================
// BAGIAN 2 : WHILE (ulang SELAMA kondisi true, jumlah belum pasti)
// Pola: while (kondisi) { ... } -> cek kondisi DULU, baru jalan. Bisa 0x jalan!
// WAJIB ada perubahan di dalam blok agar tidak infinite loop (jalan selamanya).
// ============================================================================

// --- 2a. WHILE dasar : sama seperti FOR 1-5 tapi gaya while ---
Console.WriteLine("[WHILE] Cetak 1 sampai 5:"); // Judul output
int j = 1; // Langkah 1 - Nilai awal di LUAR loop (ciri khas while)
while (j <= 5) // Langkah 2 - Kondisi: selama j<=5 lanjut. Dicek sebelum tiap putaran
{
    Console.WriteLine($"[WHILE] j = {j}"); // Tampilkan nilai j saat ini
    j++; // Langkah 3 - Perubahan WAJIB! Tanpa ini j=1 selamanya -> infinite loop
}

// --- 2b. WHILE tebakan : ulang sampai syarat terpenuhi (jumlah tak pasti) ---
int tabungan = 0; // Saldo awal 0
int bulan = 0; // Hitung sudah berapa bulan menabung
while (tabungan < 100000) // Selama saldo BELUM sampai 100rb, terus menabung
{
    bulan++; // Tambah hitungan bulan dulu (bulan ke-1, ke-2, ...)
    tabungan += 20000; // Tiap bulan nabung 20rb. 0->20rb->40rb->60rb->80rb->100rb (5 bulan)
    Console.WriteLine($"[WHILE] Bulan {bulan}: tabungan = Rp{tabungan}"); // Lapor tiap bulan
}
Console.WriteLine($"[WHILE] Target tercapai dalam {bulan} bulan!"); // Setelah loop selesai (tabungan=100rb)

// --- 2c. DO-WHILE : cek kondisi BELAKANGAN, minimal jalan 1x walau kondisi false ---
Console.WriteLine("[DO-WHILE] Contoh jalan 1x walau kondisi langsung false:"); // Judul output
int k = 100; // Nilai awal 100
do // Blok DO selalu dijalankan DULU minimal 1x tanpa cek
{
    Console.WriteLine($"[DO-WHILE] k = {k} (tetap tampil 1x)"); // Tampil walau k=100 tidak <= 5
    k++; // Perubahan agar tidak infinite (k jadi 101)
} while (k <= 5); // Kondisi dicek BELAKANGAN: 101 <= 5? False -> berhenti setelah 1x

Console.WriteLine(); // Pemisah bagian

// ============================================================================
// BAGIAN 3 : FOREACH (khusus membaca ISI koleksi satu per satu, tanpa index)
// Pola: foreach (tipe namaSementara in koleksi) { ... }
// Cocok untuk: array, List, string. TIDAK bisa ubah isi / butuh nomor index (pakai FOR).
// ============================================================================

// --- 3a. FOREACH array string : sapa semua nama ---
string[] daftarNama = { "Andi", "Budi", "Citra" }; // Array 3 nama (index 0,1,2)
Console.WriteLine("[FOREACH] Daftar nama:"); // Judul output
foreach (string nama in daftarNama) // Ambil tiap isi array satu per satu ke variabel 'nama'
{
    Console.WriteLine($"[FOREACH] Halo, {nama}!"); // Putaran 1: Andi, 2: Budi, 3: Citra
}

// --- 3b. FOREACH array int : jumlahkan semua nilai ---
int[] nilaiUjian = { 80, 90, 75, 95 }; // Array 4 nilai
int jumlah = 0; // Penampung jumlah, awalnya 0
foreach (int nilai in nilaiUjian) // 'nilai' berganti tiap putaran: 80, lalu 90, lalu 75, lalu 95
{
    jumlah += nilai; // Tambahkan ke jumlah. 0+80=80, 80+90=170, 170+75=245, 245+95=340
    Console.WriteLine($"[FOREACH] nilai = {nilai}, jumlah sementara = {jumlah}"); // Lapor tiap putaran
}
double rataRata = (double)jumlah / nilaiUjian.Length; // Rata-rata = 340 / 4 = 85. Length = jumlah isi array
Console.WriteLine($"[FOREACH] Jumlah = {jumlah}, Rata-rata = {rataRata}"); // Hasil akhir

// --- 3c. FOREACH vs FOR : butuh index? pakai FOR biasa ---
Console.WriteLine("[FOR-index] Sama tapi tampil nomor urut:"); // Judul output
for (int i = 0; i < daftarNama.Length; i++) // FOR dengan index 0 sampai Length-1 (0,1,2)
{
    Console.WriteLine($"[FOR-index] No.{i + 1}: {daftarNama[i]}"); // daftarNama[i] = ambil isi index ke-i
}

Console.WriteLine(); // Pemisah bagian

// ============================================================================
// BAGIAN 4 : BREAK & CONTINUE (pengendali loop)
// break    = HENTIKAN seluruh loop seketika, loncat keluar.
// continue = LEWATI putaran ini saja, lanjut ke putaran berikutnya.
// ============================================================================

// --- 4a. BREAK : berhenti paksa saat ketemu angka 3 ---
Console.WriteLine("[BREAK] Berhenti saat i == 3:"); // Judul output
for (int i = 1; i <= 5; i++) // Seharusnya 1-5
{
    if (i == 3) // Cek: apakah i sudah 3?
    {
        Console.WriteLine("[BREAK] Ketemu 3, STOP!"); // Pesan sebelum berhenti
        break; // Hentikan loop SEKRANG. Putaran 4 dan 5 tidak pernah terjadi
    }
    Console.WriteLine($"[BREAK] i = {i}"); // Hanya tampil untuk i=1 dan 2
}

// --- 4b. CONTINUE : lewati angka 3, sisanya tetap jalan ---
Console.WriteLine("[CONTINUE] Lewati angka 3:"); // Judul output
for (int i = 1; i <= 5; i++) // Ulangi 1-5
{
    if (i == 3) // Cek: apakah i == 3?
    {
        Console.WriteLine("[CONTINUE] Lewati 3..."); // Pesan, lalu...
        continue; // ...loncat ke putaran berikutnya (baris bawah dilewati khusus putaran ini)
    }
    Console.WriteLine($"[CONTINUE] i = {i}"); // Tampil untuk 1,2,4,5 (3 dilewati)
}

Console.WriteLine(); // Pemisah bagian

// ============================================================================
// BAGIAN 5 : LOOP BERSARANG (Nested Loop) : loop di dalam loop
// Pola: untuk tiap 1 putaran loop LUAR, loop DALAM jalan penuh dari awal-akhir.
// Contoh klasik: tabel perkalian, pola bintang, jam:menit.
// ============================================================================
Console.WriteLine("[NESTED] Tabel perkalian 1-3:"); // Judul output
for (int luar = 1; luar <= 3; luar++) // Loop LUAR: baris 1, 2, 3
{
    for (int dalam = 1; dalam <= 3; dalam++) // Loop DALAM: untuk TIAP baris, kolom 1,2,3 penuh
    {
        Console.Write($"{luar}x{dalam}={luar * dalam}\t"); // \t = tab agar rapi. Contoh: 2x3=6
    }
    Console.WriteLine(); // Pindah baris setelah 1 baris penuh (3 kolom) selesai
}

Console.WriteLine(); // Pemisah bagian

// ============================================================================
// BAGIAN 6 : LATIHAN MINI (menggabungkan FOR + FOREACH + IF)
// Studi kasus: kasir sederhana -> struk belanja + diskon jika total besar.
// ============================================================================
string[] barang = { "Buku", "Pensil", "Penghapus", "Penggaris" }; // Nama barang dibeli
int[] harga = { 15000, 5000, 3000, 7000 }; // Harga sejajar dengan nama (index sama!)
Console.WriteLine("--- Latihan: Struk Belanja ---"); // Judul latihan

// --- 6a. Cetak struk dengan FOR ber-index (butuh nomor + harga sejajar) ---
int totalBelanja = 0; // Penampung total, awalnya 0
for (int i = 0; i < barang.Length; i++) // Ulangi sesuai jumlah barang (4x: index 0-3)
{
    Console.WriteLine($"{i + 1}. {barang[i]} - Rp{harga[i]}"); // No urut (i+1), nama, harga index sama
    totalBelanja += harga[i]; // Tambahkan harga ke total. 0+15rb+5rb+3rb+7rb = 30rb
}
Console.WriteLine($"Total belanja: Rp{totalBelanja}"); // Tampilkan total: Rp30000

// --- 6b. Cek diskon dengan IF (di luar loop, setelah total diketahui) ---
if (totalBelanja >= 25000) // Syarat diskon: belanja >= 25rb. 30rb memenuhi -> True
{
    int diskon = totalBelanja * 10 / 100; // Diskon 10%: 30000*10/100 = 3000
    int bayar = totalBelanja - diskon; // Bayar akhir: 30000-3000 = 27000
    Console.WriteLine($"Diskon 10%: -Rp{diskon}"); // Tampilkan potongan
    Console.WriteLine($"Harus bayar: Rp{bayar}"); // Tampilkan yang dibayar
}
else // Jika belanja < 25rb
{
    Console.WriteLine($"Harus bayar: Rp{totalBelanja} (belum dapat diskon)"); // Bayar penuh
}

// --- 6c. Sapa per barang dengan FOREACH (tidak butuh index, hanya baca nama) ---
Console.WriteLine("Terima kasih sudah membeli:"); // Penutup struk
foreach (string b in barang) // Ambil tiap nama barang satu per satu
{
    Console.WriteLine($"- {b}"); // Tampilkan dengan bullet
}

Console.WriteLine();
Console.WriteLine("=== Selesai. Coba ubah harga/barang lalu jalankan ulang! ===");
