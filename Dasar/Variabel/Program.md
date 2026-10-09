// Variabel
 
Console.WriteLine("=== Belajar C#: Tipe Data, Variabel, dan Operasi Sederhana ===");
Console.WriteLine();

// ============================================================================
// BAGIAN 1 : TIPE DATA DASAR DI C#
// ============================================================================

// --- 1a. int : bilangan bulat (contoh: -10, 0, 25, 100) ---
// Ukuran: 4 byte, rentang: -2.147.483.648 sampai 2.147.483.647
int umur = 20; // Variabel 'umur' bertipe int, diisi nilai 20
Console.WriteLine($"[int] umur = {umur}");

// --- 1b. double : bilangan desimal presisi ganda (contoh: 3.14, 9.81) ---
// Tipe desimal default di C#. Cocok untuk perhitungan umum / ilmiah.
double tinggiBadan = 170.5; // Variabel 'tinggiBadan' bertipe double
Console.WriteLine($"[double] tinggiBadan = {tinggiBadan}");

// --- 1c. float : bilangan desimal presisi tunggal ---
// Harus diakhiri huruf 'f'. Lebih hemat memori dari double, tapi kurang presisi.
float suhu = 36.6f; // 'f' artinya nilai ini bertipe float, bukan double
Console.WriteLine($"[float] suhu = {suhu}");

// --- 1d. decimal : bilangan desimal presisi tinggi untuk uang ---
// Harus diakhiri huruf 'm'. Wajib dipakai untuk harga, gaji, keuangan.
decimal hargaBuku = 75000.50m; // 'm' artinya nilai ini bertipe decimal
Console.WriteLine($"[decimal] hargaBuku = Rp{hargaBuku}");

// --- 1e. string : teks / kumpulan karakter, ditulis dalam tanda kutip ganda ---
string nama = "Budi"; // Variabel 'nama' bertipe string, berisi teks "Budi"
Console.WriteLine($"[string] nama = {nama}");

// --- 1f. char : satu karakter tunggal, ditulis dalam tanda kutip satu ---
char grade = 'A'; // Variabel 'grade' bertipe char, hanya boleh 1 karakter
Console.WriteLine($"[char] grade = {grade}");

// --- 1g. bool : nilai logika, hanya true (benar) atau false (salah) ---
bool sudahLulus = true;  // Variabel 'sudahLulus' bernilai benar
bool hujan = false;      // Variabel 'hujan' bernilai salah
Console.WriteLine($"[bool] sudahLulus = {sudahLulus}, hujan = {hujan}");

Console.WriteLine();

// ============================================================================
// BAGIAN 2 : VARIABEL (Deklarasi, Inisialisasi, var, const)
// ============================================================================

// --- 2a. Deklarasi lalu inisialisasi (dua langkah terpisah) ---
int nilaiUjian;          // Langkah 1 - Deklarasi: membuat kotak bernama 'nilaiUjian' bertipe int
nilaiUjian = 85;         // Langkah 2 - Inisialisasi/Penugasan: mengisi kotak itu dengan 85
Console.WriteLine($"[variabel] nilaiUjian = {nilaiUjian}");

// --- 2b. Deklarasi + inisialisasi langsung (satu baris, paling umum) ---
string kota = "Jakarta"; // Langsung buat variabel 'kota' dan isi "Jakarta"
Console.WriteLine($"[variabel] kota = {kota}");

// --- 2c. Mengubah nilai variabel (nilai lama ditimpa nilai baru) ---
nilaiUjian = 90;         // Nilai 85 tadi diganti menjadi 90
Console.WriteLine($"[variabel] nilaiUjian setelah diubah = {nilaiUjian}");

// --- 2d. Kata kunci 'var' : tipe data ditebak otomatis oleh compiler ---
var jurusan = "RPL";     // Compiler tahu 'jurusan' pasti string karena diisi teks
var angkatan = 2024;     // Compiler tahu 'angkatan' pasti int karena diisi bilangan bulat
Console.WriteLine($"[var] jurusan = {jurusan} (tipe: {jurusan.GetType().Name})");
Console.WriteLine($"[var] angkatan = {angkatan} (tipe: {angkatan.GetType().Name})");

// --- 2e. Kata kunci 'const' : nilai tetap, TIDAK bisa diubah lagi ---
const double PI = 3.14159; // Konstanta PI, nilainya dikunci selamanya
// PI = 3.14; // <-- BARIS INI ERROR jika dibuka! Const tidak boleh diubah.
Console.WriteLine($"[const] PI = {PI}");

// --- 2f. Aturan penamaan variabel yang baik ---
string namaLengkap = "Budi Santoso"; // camelCase: huruf pertama kecil, kata berikutnya kapital
int jumlahSiswa = 30;                // Gunakan nama yang jelas, jangan 'x' atau 'data1'
Console.WriteLine($"[penamaan] namaLengkap = {namaLengkap}, jumlahSiswa = {jumlahSiswa}");

Console.WriteLine();

// ============================================================================
// BAGIAN 3 : OPERASI SEDERHANA
// ============================================================================

// --- 3a. Operasi Aritmatika Dasar (+ tambah, - kurang, * kali, / bagi, % sisa bagi) ---
int a = 10; // Operand pertama
int b = 3;  // Operand kedua
Console.WriteLine($"a = {a}, b = {b}");                   // Menampilkan nilai awal a dan b
Console.WriteLine($"a + b = {a + b}");                    // 10 + 3 = 13 (penjumlahan)
Console.WriteLine($"a - b = {a - b}");                    // 10 - 3 = 7  (pengurangan)
Console.WriteLine($"a * b = {a * b}");                    // 10 * 3 = 30 (perkalian)
Console.WriteLine($"a / b = {a / b}");                    // 10 / 3 = 3  (int dibagi int -> hasilnya int, koma dibuang!)
Console.WriteLine($"a % b = {a % b}");                    // 10 % 3 = 1  (sisa pembagian 10:3)

// --- 3b. Pembagian desimal yang benar (salah satu harus double/decimal) ---
double hasilBagiDesimal = (double)a / b; // Ubah 'a' jadi double dulu, maka hasil koma dipertahankan
Console.WriteLine($"(double)a / b = {hasilBagiDesimal}"); // 10.0 / 3 = 3.333...

// --- 3c. Operasi penugasan singkat (+=, -=, *=, /=) ---
int skor = 100;   // Nilai awal skor = 100
skor += 10;       // Sama dengan: skor = skor + 10  -> sekarang 110
Console.WriteLine($"skor setelah += 10 : {skor}");
skor -= 5;        // Sama dengan: skor = skor - 5   -> sekarang 105
Console.WriteLine($"skor setelah -= 5  : {skor}");
skor *= 2;        // Sama dengan: skor = skor * 2   -> sekarang 210
Console.WriteLine($"skor setelah *= 2  : {skor}");
skor /= 3;        // Sama dengan: skor = skor / 3   -> sekarang 70
Console.WriteLine($"skor setelah /= 3  : {skor}");

// --- 3d. Increment (++) dan Decrement (--) : tambah/kurang 1 ---
int hitung = 5;   // Nilai awal 5
hitung++;         // Sama dengan hitung = hitung + 1 -> jadi 6
Console.WriteLine($"hitung setelah ++ : {hitung}");
hitung--;         // Sama dengan hitung = hitung - 1 -> kembali 5
Console.WriteLine($"hitung setelah -- : {hitung}");

Console.WriteLine();

// ============================================================================
// BAGIAN 4 : OPERASI PADA STRING
// ============================================================================

// --- 4a. Penggabungan string dengan '+' (concatenation) ---
string depan = "Selamat";   // Kata pertama
string belakang = "Pagi";    // Kata kedua
string sapaan = depan + " " + belakang + "!"; // Gabung dengan spasi di tengah
Console.WriteLine($"[gabung +] sapaan = {sapaan}");

// --- 4b. Interpolasi string dengan '$' (cara modern & disarankan) ---
string namaSiswa = "Andi"; // Nama siswa
int nilai = 95;            // Nilai siswa
string laporan = $"Siswa {namaSiswa} mendapat nilai {nilai}."; // Sisipkan variabel dalam { }
Console.WriteLine($"[interpolasi $] {laporan}");

Console.WriteLine();

// ============================================================================
// BAGIAN 5 : KONVERSI TIPE DATA (Mengubah tipe satu ke tipe lain)
// ============================================================================

// --- 5a. Konversi implisit (otomatis, dari kecil ke besar, aman) ---
int kecil = 100;       // Tipe kecil (int)
double besar = kecil;  // Otomatis jadi double tanpa kode khusus. Aman, tidak ada data hilang.
Console.WriteLine($"[implisit] int {kecil} -> double {besar}");

// --- 5b. Konversi eksplisit / casting (manual, dari besar ke kecil, pakai kurung) ---
double nilaiDouble = 9.99;      // Nilai desimal
int nilaiInt = (int)nilaiDouble; // Paksa jadi int: koma dipotong -> 9 (data di belakang koma hilang!)
Console.WriteLine($"[casting] double {nilaiDouble} -> int {nilaiInt}");

// --- 5c. Konversi string -> angka dengan Convert / Parse ---
string teksAngka = "123";              // Teks yang isinya angka
int angkaHasil = Convert.ToInt32(teksAngka); // Ubah teks "123" menjadi bilangan 123
Console.WriteLine($"[Convert] string \"{teksAngka}\" -> int {angkaHasil}");

// --- 5d. Konversi angka -> string dengan .ToString() ---
int tahun = 2026;                // Bilangan tahun
string teksTahun = tahun.ToString(); // Ubah menjadi teks "2026"
Console.WriteLine($"[ToString] int {tahun} -> string \"{teksTahun}\"");

Console.WriteLine(); // Pemisah bagian

// ============================================================================
// BAGIAN 6 : LATIHAN MINI (Operasi sederhana digabung jadi satu)
// Menghitung luas persegi panjang: panjang x lebar
// ============================================================================
double panjang = 12.5;                 // Input: panjang dalam cm
double lebar = 8.0;                    // Input: lebar dalam cm
double luas = panjang * lebar;         // Proses: rumus luas = panjang * lebar
Console.WriteLine("--- Latihan: Luas Persegi Panjang ---"); // Judul latihan
Console.WriteLine($"panjang = {panjang} cm");               // Tampilkan panjang
Console.WriteLine($"lebar   = {lebar} cm");                 // Tampilkan lebar
Console.WriteLine($"luas = panjang x lebar = {panjang} x {lebar} = {luas} cm2"); // Tampilkan hasil

Console.WriteLine();
Console.WriteLine("=== Selesai. Coba ubah-ubah nilainya lalu jalankan ulang! ===");
