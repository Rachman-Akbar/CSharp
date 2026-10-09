// ============================================================================
// MATERI C# LANJUTAN : Method & Fungsi Dasar
// File     : Program.cs
// Cara run : dotnet run (dari folder Method/)
// Syarat   : Sudah paham Variabel, Percabangan, Perulangan, dan Array/List
// Catatan  : File ini pakai Top-Level Statement (tanpa class Main).
//            Method didefinisikan di BAWAH, dipanggil di ATAS.
// ============================================================================

// ----------------------------------------------------------------------------
// BAGIAN 0 : Pengenalan
// Method = blok kode bernama yang bisa dipanggil berulang-kali.
// void   = method yang BEKERJA tapi tidak mengembalikan nilai.
// return = method yang MENGHASILKAN nilai untuk dipakai lagi.
// ----------------------------------------------------------------------------
Console.WriteLine("=== Belajar C#: Method & Fungsi Dasar ===");
Console.WriteLine(); // Baris kosong agar output rapi

// ============================================================================
// BAGIAN 1 : METHOD void TANPA PARAMETER (paling sederhana)
// Pola: void Nama() { ... } -> dipanggil dengan: Nama();
// ============================================================================
Sapa(); // Memanggil method Sapa (lihat definisi di BAGIAN 1 bawah). Output: halo 2 baris
Sapa(); // Memanggil lagi -> kode yang sama jalan ulang tanpa ditulis ulang

// ============================================================================
// BAGIAN 2 : METHOD DENGAN PARAMETER (masukan agar fleksibel)
// Parameter = variabel titipan saat memanggil. Argumen = nilai nyata yang dikirim.
// ============================================================================
SapaNama("Andi"); // Mengirim "Andi" sebagai argumen -> parameter nama = "Andi"
SapaNama("Citra"); // Mengirim "Citra" -> parameter nama = "Citra"
TampilkanBiodata("Budi", 20); // Dua parameter: nama="Budi", umur=20 (urutan HARUS sama!)

// ============================================================================
// BAGIAN 3 : FUNGSI DENGAN return (menghasilkan nilai)
// Pola: tipe Nama() { ... return nilai; } -> hasilnya ditampung: var x = Nama();
// ============================================================================
int hasilTambah = Tambah(10, 3); // Panggil Tambah(10,3) -> return 13 -> disimpan ke hasilTambah
Console.WriteLine($"[RETURN] Tambah(10, 3) = {hasilTambah}"); // Tampilkan 13
Console.WriteLine($"[RETURN] Tambah(5, 7) = {Tambah(5, 7)}"); // Bisa langsung dipakai tanpa ditampung dulu

double luas = LuasPersegiPanjang(12.5, 8.0); // Panggil fungsi luas -> return 100 -> simpan ke luas
Console.WriteLine($"[RETURN] Luas 12.5 x 8 = {luas}"); // Tampilkan 100

string gradeB = TentukanGrade(82); // Panggil fungsi grade -> return "B"
Console.WriteLine($"[RETURN] Skor 82 = Grade {gradeB}"); // Tampilkan B

Console.WriteLine(); // Pemisah bagian

// ============================================================================
// BAGIAN 4 : PARAMETER DEFAULT, NAMED ARGUMENT, DAN OVERLOADING
// ============================================================================

// --- 4a. Parameter default : punya nilai cadangan jika argumen tidak dikirim ---
SapaDefault(); // Tanpa argumen -> pakai default nama = "Tamu"
SapaDefault("Dedi"); // Dengan argumen -> default diganti "Dedi"

// --- 4b. Named argument : menyebut nama parameter agar urutan bebas & jelas ---
TampilkanBiodata(umur: 25, nama: "Eka"); // Urutan dibalik tapi disebut namanya -> tetap benar
double volume = HitungVolume(panjang: 10, lebar: 5, tinggi: 2); // Jelas mana panjang/lebar/tinggi
Console.WriteLine($"[NAMED] Volume 10x5x2 = {volume}"); // Tampilkan 100

// --- 4c. Overloading : NAMA sama, PARAMETER beda (jumlah/tipe). C# memilih otomatis ---
// Catatan: local function di Top-Level TIDAK bisa overload, jadi di sini pakai nama
// berbeda (TambahDesimal, TambahTiga). Konsep overload penuh berlaku di dalam class.
Console.WriteLine($"[OVERLOAD] Tambah(4, 6) = {Tambah(4, 6)}"); // Versi int -> 10
Console.WriteLine($"[OVERLOAD] TambahDesimal(4.5, 6.2) = {TambahDesimal(4.5, 6.2)}"); // Versi double -> 10.7
Console.WriteLine($"[OVERLOAD] TambahTiga(1, 2, 3) = {TambahTiga(1, 2, 3)}"); // Versi 3 angka -> 6

Console.WriteLine(); // Pemisah bagian

// ============================================================================
// BAGIAN 5 : FUNGSI LAINNYA (params, expression-bodied, ref/out, rekursi)
// ============================================================================

// --- 5a. params : menerima BANYAK argumen tanpa array manual ---
int totalBanyak = JumlahkanSemua(10, 20, 30, 40); // Kirim 4 angka sekaligus -> 100
Console.WriteLine($"[PARAMS] JumlahkanSemua(10,20,30,40) = {totalBanyak}"); // Tampilkan 100
Console.WriteLine($"[PARAMS] JumlahkanSemua(5, 5) = {JumlahkanSemua(5, 5)}"); // Bisa 2 angka -> 10

// --- 5b. Expression-bodied : method 1 baris dengan => (tanpa kurung kurawal) ---
Console.WriteLine($"[EXPR] Kuadrat(7) = {Kuadrat(7)}"); // 7*7 = 49
Console.WriteLine($"[EXPR] SapaSingkat(\"Fajar\") = {SapaSingkat("Fajar")}"); // "Halo, Fajar!"

// --- 5c. Method dengan Array/List : olah koleksi di dalam fungsi ---
int[] nilaiUjian = { 80, 90, 75, 95 }; // Array 4 nilai untuk diolah fungsi
Console.WriteLine($"[ARRAY-FUNC] Rata-rata = {HitungRataRata(nilaiUjian)}"); // Kirim array -> return 85
Console.WriteLine($"[ARRAY-FUNC] Terbesar = {CariTerbesar(nilaiUjian)}"); // Kirim array -> return 95

// --- 5d. Rekursi : fungsi yang memanggil dirinya sendiri (wajib ada titik berhenti!) ---
Console.WriteLine($"[REKURSI] Faktorial(5) = {Faktorial(5)}"); // 5*4*3*2*1 = 120
Console.WriteLine($"[REKURSI] Faktorial(3) = {Faktorial(3)}"); // 3*2*1 = 6

Console.WriteLine(); // Pemisah bagian

// ============================================================================
// BAGIAN 6 : LATIHAN MINI (kasir dipecah jadi method kecil-kecil)
// Aturan bagus: 1 method = 1 tugas. Main hanya mengatur alur.
// ============================================================================
string[] barang = { "Buku", "Pensil", "Penghapus", "Penggaris" }; // Nama barang (tetap -> array)
int[] harga = { 15000, 5000, 3000, 7000 }; // Harga sejajar index dengan nama
Console.WriteLine("--- Latihan: Kasir dengan Method ---"); // Judul latihan
TampilkanStruk(barang, harga); // Tugas 1: cetak struk (void, langsung tampil)
int totalBelanja = HitungTotal(harga); // Tugas 2: hitung total (return int) -> 30000
int diskon = HitungDiskon(totalBelanja); // Tugas 3: hitung diskon (return int) -> 3000 (10%)
Console.WriteLine($"Total       : Rp{totalBelanja}"); // Tampilkan total
Console.WriteLine($"Diskon      : -Rp{diskon}"); // Tampilkan diskon
Console.WriteLine($"Harus bayar : Rp{totalBelanja - diskon}"); // Tampilkan bayar akhir 27000

Console.WriteLine();
Console.WriteLine("=== Selesai. Coba panggil method dengan angka/nama lain! ===");

// ============================================================================
// DEFINISI METHOD (semua method dikumpulkan di bawah garis ini)
// Cara baca: samakan NAMA pemanggil di atas dengan NAMA definisi di bawah.
// ============================================================================

// --- 1. Method void tanpa parameter : hanya kerja, tidak return ---
void Sapa() // Definisi: nama Sapa, tanpa () isi, void = tidak return
{
    Console.WriteLine("[VOID] Halo, selamat datang di kelas C#!"); // Baris kerja 1
    Console.WriteLine("[VOID] Method ini dipanggil, bukan dijalankan otomatis."); // Baris kerja 2
}

// --- 2a. Method dengan 1 parameter string ---
void SapaNama(string nama) // Parameter 'nama' = titipan teks dari pemanggil
{
    Console.WriteLine($"[PARAM] Halo, {nama}! Senang bertemu denganmu."); // Pakai parameter di dalam
}

// --- 2b. Method dengan 2 parameter (string + int) ---
void TampilkanBiodata(string nama, int umur) // Parameter 1 nama, parameter 2 umur (urutan penting!)
{
    Console.WriteLine($"[PARAM] Nama: {nama}, Umur: {umur} tahun."); // Tampilkan keduanya
}

// --- 3a. Fungsi return int : menjumlahkan 2 bilangan ---
int Tambah(int a, int b) // int di depan = janji return bilangan bulat. a,b = titipan angka
{
    return a + b; // Kembalikan hasil ke pemanggil. Setelah return, method berhenti
}

// --- 3b. Fungsi return double : luas persegi panjang ---
double LuasPersegiPanjang(double panjang, double lebar) // Return double karena bisa koma
{
    return panjang * lebar; // Kembalikan panjang x lebar (12.5 x 8 = 100)
}

// --- 3c. Fungsi return string dengan IF di dalam (gabungan method + percabangan) ---
string TentukanGrade(int skor) // Terima skor, kembalikan huruf grade
{
    if (skor >= 90) // Cek dari terbesar dulu (aturan materi percabangan)
    {
        return "A"; // Return langsung keluar, tidak perlu else
    }
    else if (skor >= 80) // 82 memenuhi sini
    {
        return "B"; // Kembalikan "B", method selesai di sini
    }
    else if (skor >= 70)
    {
        return "C";
    }
    return "D"; // Cadangan jika semua di atas gagal (pengganti else terakhir)
}

// --- 4a. Parameter default : nama = "Tamu" jika tidak dikirim ---
void SapaDefault(string nama = "Tamu") // = "Tamu" artinya nilai cadangan
{
    Console.WriteLine($"[DEFAULT] Halo, {nama}!"); // Tanpa argumen -> Tamu, dengan argumen -> diganti
}

// --- 4b. Fungsi 3 parameter untuk named argument ---
double HitungVolume(double panjang, double lebar, double tinggi) // 3 ukuran kotak
{
    return panjang * lebar * tinggi; // Volume balok = p x l x t
}

// --- 4c. Overloading 1 : Tambah versi double ---
// Nama dibedakan (TambahDesimal) karena local function top-level tidak bisa overload.
// Di dalam class, ketiganya boleh sama-sama bernama 'Tambah'.
double TambahDesimal(double a, double b) // Parameter double -> dipanggil dengan angka koma
{
    return a + b; // 4.5 + 6.2 = 10.7
}

// --- 4c. Overloading 2 : Tambah versi 3 angka ---
int TambahTiga(int a, int b, int c) // Tiga parameter int -> dipanggil dengan 3 angka
{
    return a + b + c; // 1 + 2 + 3 = 6
}

// --- 5a. params : menampung banyak argumen jadi array otomatis ---
int JumlahkanSemua(params int[] angka) // 'params' = semua argumen dikemas jadi array 'angka'
{
    int total = 0; // Penampung, awalnya 0
    foreach (int n in angka) // Ulangi tiap titipan satu per satu
    {
        total += n; // Tambahkan ke total
    }
    return total; // Kembalikan jumlah semua
}

// --- 5b. Expression-bodied 1 : 1 baris tanpa { } ---
int Kuadrat(int x) => x * x; // Sama dengan { return x * x; } tapi ringkas. 7 -> 49

// --- 5b. Expression-bodied 2 : return string 1 baris ---
string SapaSingkat(string nama) => $"Halo, {nama}!"; // Langsung kembalikan teks sapaan

// --- 5c. Method terima array : hitung rata-rata ---
double HitungRataRata(int[] data) // Parameter array (kirim array utuh, bukan 1 angka)
{
    int jumlah = 0; // Penampung jumlah
    foreach (int n in data) // Jumlahkan semua isi array
    {
        jumlah += n;
    }
    return (double)jumlah / data.Length; // Bagi jumlah isi. Cast agar koma tidak hilang
}

// --- 5c. Method terima array : cari terbesar ---
int CariTerbesar(int[] data) // Terima array, kembalikan 1 angka terbesar
{
    int terbesar = data[0]; // Tebak awal = isi pertama
    for (int i = 1; i < data.Length; i++) // Bandingkan dari index 1 sampai akhir
    {
        if (data[i] > terbesar) // Jika ketemu lebih besar...
        {
            terbesar = data[i]; // ...ganti tebakan
        }
    }
    return terbesar; // Kembalikan pemenang akhir
}

// --- 5d. Rekursi : faktorial (n! = n x (n-1) x ... x 1) ---
int Faktorial(int n) // Contoh: Faktorial(5) = 5 x Faktorial(4) x ... sampai 1
{
    if (n <= 1) // Titik BERHENTI wajib! Jika tidak ada, memanggil selamanya -> stack overflow
    {
        return 1; // 1! = 1 dan 0! = 1, berhenti di sini
    }
    return n * Faktorial(n - 1); // Panggil diri sendiri dengan n-1. 5 * Faktorial(4) * ...
}

// --- 6a. Latihan: cetak struk dari 2 array sejajar ---
void TampilkanStruk(string[] namaBarang, int[] hargaBarang) // Terima 2 array sejajar index
{
    for (int i = 0; i < namaBarang.Length; i++) // Ulangi sesuai jumlah barang
    {
        Console.WriteLine($"{i + 1}. {namaBarang[i]} - Rp{hargaBarang[i]}"); // Nomor + nama + harga
    }
}

// --- 6b. Latihan: hitung total harga ---
int HitungTotal(int[] hargaBarang) // Terima array harga, kembalikan totalnya
{
    int total = 0; // Penampung
    foreach (int h in hargaBarang) // Jumlahkan semua
    {
        total += h;
    }
    return total; // 15000+5000+3000+7000 = 30000
}

// --- 6c. Latihan: hitung diskon 10% jika total >= 25000 ---
int HitungDiskon(int total) // Terima total, kembalikan nominal diskon (bukan persen)
{
    if (total >= 25000) // Syarat diskon (aturan materi percabangan)
    {
        return total * 10 / 100; // 10%: 30000*10/100 = 3000
    }
    return 0; // Tidak memenuhi syarat -> diskon 0
}
