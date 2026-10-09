// ============================================================================
// MATERI C# LANJUTAN : Array dan List Dasar
// File     : Program.cs
// Cara run : dotnet run (dari folder Array/)
// Syarat   : Sudah paham Variabel, Percabangan, dan Perulangan
// ============================================================================

// ----------------------------------------------------------------------------
// BAGIAN 0 : Pengenalan
// Variabel biasa = 1 kotak untuk 1 nilai. Array/List = 1 rak dengan BANYAK kotak.
// ----------------------------------------------------------------------------
Console.WriteLine("=== Belajar C#: Array dan List Dasar ===");
Console.WriteLine(); // Baris kosong agar output rapi

// ============================================================================
// BAGIAN 1 : ARRAY DASAR (ukuran TETAP, index mulai dari 0)
// Pola: tipe[] nama = { isi1, isi2, isi3 };
// Index: kotak pertama = 0, kedua = 1, dst. Jumlah isi = .Length
// ============================================================================

// --- 1a. Membuat array string (3 nama) ---
string[] daftarNama = { "Andi", "Budi", "Citra" }; // Array 3 isi: index 0=Andi, 1=Budi, 2=Citra
Console.WriteLine($"[ARRAY] Jumlah isi (Length) = {daftarNama.Length}"); // Length = 3

// --- 1b. Membaca isi lewat index [posisi] ---
Console.WriteLine($"[ARRAY] index 0 = {daftarNama[0]}"); // Kotak pertama = Andi
Console.WriteLine($"[ARRAY] index 1 = {daftarNama[1]}"); // Kotak kedua = Budi
Console.WriteLine($"[ARRAY] index 2 = {daftarNama[2]}"); // Kotak ketiga = Citra
// Console.WriteLine(daftarNama[3]); // <-- ERROR jika dibuka! Index 3 tidak ada (hanya 0-2)

// --- 1c. Mengubah isi lewat index (array BISA diubah isinya, tapi JUMLAH tetap) ---
daftarNama[1] = "Budi Santoso"; // Ganti index 1 dari "Budi" menjadi "Budi Santoso"
Console.WriteLine($"[ARRAY] index 1 setelah diubah = {daftarNama[1]}"); // Tampilkan hasil ganti

// --- 1d. Membuat array int dengan 'new' (tentukan ukuran dulu, isi belakangan) ---
int[] nilai = new int[4]; // Buat array 4 kotak, semua terisi 0 dulu (default int)
nilai[0] = 80; // Isi kotak 0 dengan 80
nilai[1] = 90; // Isi kotak 1 dengan 90
nilai[2] = 75; // Isi kotak 2 dengan 75
nilai[3] = 95; // Isi kotak 3 dengan 95
Console.WriteLine($"[ARRAY] nilai = [{nilai[0]}, {nilai[1]}, {nilai[2]}, {nilai[3]}]"); // Tampilkan semua

Console.WriteLine(); // Pemisah bagian

// ============================================================================
// BAGIAN 2 : PERULANGAN ARRAY (cara membaca semua isi tanpa tulis satu-satu)
// Aturan index: for (i = 0; i < array.Length; i++) -> pakai '<', BUKAN '<='!
// ============================================================================

// --- 2a. FOR ber-index : butuh nomor urut ATAU mau ubah isi ---
Console.WriteLine("[FOR] Cetak semua nama + nomor:"); // Judul output
for (int i = 0; i < daftarNama.Length; i++) // i = 0,1,2 (berhenti sebelum 3 = Length)
{
    Console.WriteLine($"[FOR] No.{i + 1}: {daftarNama[i]}"); // i+1 agar nomor mulai dari 1 (manusiawi)
}

// --- 2b. FOREACH : hanya baca isi, tanpa index (lebih simpel) ---
Console.WriteLine("[FOREACH] Cetak semua nama (tanpa nomor):"); // Judul output
foreach (string nama in daftarNama) // 'nama' berganti tiap putaran: Andi, Budi Santoso, Citra
{
    Console.WriteLine($"[FOREACH] Halo, {nama}!"); // Tampilkan tiap nama
}

// --- 2c. Menghitung jumlah & rata-rata dengan loop ---
int jumlah = 0; // Penampung jumlah, awalnya 0
foreach (int n in nilai) // Ambil tiap nilai satu per satu: 80, 90, 75, 95
{
    jumlah += n; // Tambahkan: 0+80=80, 80+90=170, 170+75=245, 245+95=340
}
double rataRata = (double)jumlah / nilai.Length; // Rata-rata = 340 / 4 = 85. Cast agar koma dipertahankan
Console.WriteLine($"[HITUNG] Jumlah = {jumlah}, Rata-rata = {rataRata}"); // Hasil akhir

// --- 2d. Mencari nilai terbesar (max) dengan loop + IF ---
int terbesar = nilai[0]; // Tebak awal: yang terbesar = isi pertama (80)
for (int i = 1; i < nilai.Length; i++) // Mulai dari index 1 (0 sudah jadi tebakan awal)
{
    if (nilai[i] > terbesar) // Jika isi ini lebih besar dari tebakan...
    {
        terbesar = nilai[i]; // ...ganti tebakan dengan isi ini. 90>80 ganti, 75 tidak, 95>90 ganti
    }
}
Console.WriteLine($"[MAX] Nilai terbesar = {terbesar}"); // Hasil: 95

Console.WriteLine(); // Pemisah bagian

// ============================================================================
// BAGIAN 3 : OPERASI ARRAY SIAP PAKAI (Array.Sort, Reverse, IndexOf)
// ============================================================================

// --- 3a. Array.Sort : mengurutkan (kecil ke besar / A ke Z) ---
int[] angkaAcak = { 5, 2, 8, 1, 3 }; // Array belum urut
Console.WriteLine($"[SORT] Sebelum: [{string.Join(", ", angkaAcak)}]"); // string.Join = gabung isi jadi teks "5, 2, 8, 1, 3"
Array.Sort(angkaAcak); // Urutkan langsung di tempat: jadi 1, 2, 3, 5, 8
Console.WriteLine($"[SORT] Sesudah: [{string.Join(", ", angkaAcak)}]"); // Tampilkan hasil urut

// --- 3b. Array.Reverse : membalik urutan (bukan mengurutkan!) ---
Array.Reverse(angkaAcak); // Balik: 1,2,3,5,8 menjadi 8,5,3,2,1
Console.WriteLine($"[REVERSE] Dibalik: [{string.Join(", ", angkaAcak)}]"); // Tampilkan hasil balik

// --- 3c. Array.IndexOf : mencari posisi index suatu nilai (-1 = tidak ketemu) ---
int posisi = Array.IndexOf(nilai, 75); // Cari angka 75 di array 'nilai' (80,90,75,95) -> ketemu di index 2
Console.WriteLine($"[INDEXOF] Angka 75 ada di index = {posisi}"); // Hasil: 2
int tidakAda = Array.IndexOf(nilai, 100); // Cari 100 -> tidak ada -> -1
Console.WriteLine($"[INDEXOF] Angka 100 ada di index = {tidakAda} (artinya tidak ketemu)"); // Hasil: -1

Console.WriteLine(); // Pemisah bagian

// ============================================================================
// BAGIAN 4 : LIST DASAR (ukuran FLEKSIBEL, bisa tambah/hapus kapan saja)
// Pola: List<tipe> nama = new List<tipe> { isi awal (opsional) };
// Butuh 'using System.Collections.Generic;' TAPI sudah otomatis (ImplicitUsings).
// Array = rak PATEN (jumlah tetap). List = tas KARET (melar sesuai isi).
// ============================================================================

// --- 4a. Membuat List + Add (tambah di belakang) ---
List<string> belanja = new List<string> { "Buku", "Pensil" }; // List awal 2 isi
Console.WriteLine($"[LIST] Awal ({belanja.Count} isi): [{string.Join(", ", belanja)}]"); // Count = jumlah isi List (mirip Length)
belanja.Add("Penghapus"); // Tambah 1 isi di belakang -> Buku, Pensil, Penghapus
belanja.Add("Penggaris"); // Tambah lagi -> 4 isi
Console.WriteLine($"[LIST] Setelah Add ({belanja.Count} isi): [{string.Join(", ", belanja)}]"); // Tampilkan 4 isi

// --- 4b. Membaca & mengubah List (sama seperti array, pakai index + Count) ---
Console.WriteLine($"[LIST] index 0 = {belanja[0]}"); // Baca kotak pertama = Buku
belanja[1] = "Pensil Warna"; // Ubah index 1 dari "Pensil" menjadi "Pensil Warna"
Console.WriteLine($"[LIST] index 1 setelah diubah = {belanja[1]}"); // Tampilkan hasil ubah

// --- 4c. Remove (hapus isi tertentu) & RemoveAt (hapus index tertentu) ---
belanja.Remove("Penghapus"); // Hapus isi bernama "Penghapus" (jika ada 2 yang sama, yang pertama dihapus)
Console.WriteLine($"[LIST] Setelah Remove Penghapus: [{string.Join(", ", belanja)}]"); // Sisa 3 isi
belanja.RemoveAt(0); // Hapus index 0 (Buku) -> isi geser maju
Console.WriteLine($"[LIST] Setelah RemoveAt(0): [{string.Join(", ", belanja)}]"); // Sisa: Pensil Warna, Penggaris

// --- 4d. Contains (cek ada/tidak) & Clear (hapus semua) ---
bool adaBuku = belanja.Contains("Buku"); // Apakah "Buku" masih ada? Sudah dihapus -> False
Console.WriteLine($"[LIST] Apakah ada 'Buku'? {adaBuku}"); // Hasil: False
bool adaPenggaris = belanja.Contains("Penggaris"); // Apakah "Penggaris" ada? Ya -> True
Console.WriteLine($"[LIST] Apakah ada 'Penggaris'? {adaPenggaris}"); // Hasil: True

// --- 4e. Loop List (sama seperti array, tapi pakai .Count) ---
Console.WriteLine("[LIST] Cetak sisa belanja dengan FOR:"); // Judul output
for (int i = 0; i < belanja.Count; i++) // Ingat: List pakai Count, array pakai Length!
{
    Console.WriteLine($"[LIST] {i + 1}. {belanja[i]}"); // Nomor urut + nama barang
}

// --- 4f. List angka + hitung total (gabungan List + foreach + IF) ---
List<int> harga = new List<int> { 15000, 5000, 7000 }; // List 3 harga
harga.Add(3000); // Tambah 1 harga -> 4 isi: 15000, 5000, 7000, 3000
int totalHarga = 0; // Penampung total
foreach (int h in harga) // Jumlahkan semua seperti array
{
    totalHarga += h; // 0+15rb+5rb+7rb+3rb = 30rb
}
Console.WriteLine($"[LIST] Total harga {harga.Count} barang = Rp{totalHarga}"); // Hasil: Rp30000

Console.WriteLine(); // Pemisah bagian

// ============================================================================
// BAGIAN 5 : ARRAY vs LIST (kapan pakai yang mana?)
// | Array          | List                    |
// |----------------|-------------------------|
// | Jumlah TETAP   | Jumlah FLEKSIBEL        |
// | .Length        | .Count                  |
// | Cocok: data pasti (hari, bulan, nilai tetap) | Cocok: data berubah (keranjang, antrian) |
// ============================================================================
string[] hariTetap = { "Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu", "Minggu" }; // Jumlah pasti 7 -> array pas
List<string> antrian = new List<string> { "Andi" }; // Jumlah belum pasti -> list pas
antrian.Add("Budi"); // Orang baru datang, tinggal tambah (array harus buat ulang!)
Console.WriteLine($"[VS] Hari (array, Length={hariTetap.Length}): {hariTetap[0]}...{hariTetap[6]}"); // Senin...Minggu
Console.WriteLine($"[VS] Antrian (list, Count={antrian.Count}): [{string.Join(", ", antrian)}]"); // Andi, Budi

Console.WriteLine(); // Pemisah bagian

// ============================================================================
// BAGIAN 6 : LATIHAN MINI (menggabungkan Array + List + Loop + IF)
// Studi kasus: nilai siswa -> rata-rata + siapa di atas rata-rata.
// ============================================================================
string[] siswa = { "Andi", "Budi", "Citra", "Dedi" }; // Nama siswa (tetap 4 orang -> array)
int[] skor = { 80, 95, 70, 85 }; // Skor sejajar dengan nama (index sama!)
Console.WriteLine("--- Latihan: Nilai Siswa ---"); // Judul latihan

// --- 6a. Hitung rata-rata dengan FOR ber-index ---
int totalSkor = 0; // Penampung total
for (int i = 0; i < skor.Length; i++) // Ulangi 4x sesuai jumlah siswa
{
    totalSkor += skor[i]; // Jumlahkan tiap skor
}
double rataSkor = (double)totalSkor / skor.Length; // Rata-rata = 330 / 4 = 82.5
Console.WriteLine($"Total = {totalSkor}, Rata-rata = {rataSkor}"); // Tampilkan hasil

// --- 6b. Kumpulkan yang lulus (di atas rata-rata) ke List (jumlah belum tahu!) ---
List<string> diAtasRata = new List<string>(); // List kosong penampung nama lulus
for (int i = 0; i < siswa.Length; i++) // Cek tiap siswa satu per satu
{
    if (skor[i] > rataSkor) // Syarat: skor di atas 82.5? Budi 95 ya, Dedi 85 ya
    {
        diAtasRata.Add($"{siswa[i]} ({skor[i]})"); // Masukkan "Nama (skor)" ke List
    }
}
Console.WriteLine($"Di atas rata-rata ({diAtasRata.Count} orang): [{string.Join(", ", diAtasRata)}]"); // Budi (95), Dedi (85)

Console.WriteLine();
Console.WriteLine("=== Selesai. Coba tambah siswa/skor lalu jalankan ulang! ===");
