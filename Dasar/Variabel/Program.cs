Console.WriteLine("=== Belajar C#: Tipe Data, Variabel, dan Operasi Sederhana ===");
Console.WriteLine();

int umur = 20;
Console.WriteLine($"[int] umur = {umur}");

double tinggiBadan = 170.5;
Console.WriteLine($"[double] tinggiBadan = {tinggiBadan}");

float suhu = 36.6f;
Console.WriteLine($"[float] suhu = {suhu}");

decimal hargaBuku = 75000.50m;
Console.WriteLine($"[decimal] hargaBuku = Rp{hargaBuku}");

string nama = "Budi";
Console.WriteLine($"[string] nama = {nama}");

char grade = 'A';
Console.WriteLine($"[char] grade = {grade}");

bool sudahLulus = true;
bool hujan = false;
Console.WriteLine($"[bool] sudahLulus = {sudahLulus}, hujan = {hujan}");

Console.WriteLine();

int nilaiUjian;
nilaiUjian = 85;
Console.WriteLine($"[variabel] nilaiUjian = {nilaiUjian}");

string kota = "Jakarta";
Console.WriteLine($"[variabel] kota = {kota}");

nilaiUjian = 90;
Console.WriteLine($"[variabel] nilaiUjian setelah diubah = {nilaiUjian}");

var jurusan = "RPL";
var angkatan = 2024;
Console.WriteLine($"[var] jurusan = {jurusan} (tipe: {jurusan.GetType().Name})");
Console.WriteLine($"[var] angkatan = {angkatan} (tipe: {angkatan.GetType().Name})");

const double PI = 3.14159;

Console.WriteLine($"[const] PI = {PI}");

string namaLengkap = "Budi Santoso";
int jumlahSiswa = 30;
Console.WriteLine($"[penamaan] namaLengkap = {namaLengkap}, jumlahSiswa = {jumlahSiswa}");

Console.WriteLine();

int a = 10;
int b = 3;
Console.WriteLine($"a = {a}, b = {b}");
Console.WriteLine($"a + b = {a + b}");
Console.WriteLine($"a - b = {a - b}");
Console.WriteLine($"a * b = {a * b}");
Console.WriteLine($"a / b = {a / b}");
Console.WriteLine($"a % b = {a % b}");

double hasilBagiDesimal = (double)a / b;
Console.WriteLine($"(double)a / b = {hasilBagiDesimal}");

int skor = 100;
skor += 10;
Console.WriteLine($"skor setelah += 10 : {skor}");
skor -= 5;
Console.WriteLine($"skor setelah -= 5  : {skor}");
skor *= 2;
Console.WriteLine($"skor setelah *= 2  : {skor}");
skor /= 3;
Console.WriteLine($"skor setelah /= 3  : {skor}");

int hitung = 5;
hitung++;
Console.WriteLine($"hitung setelah ++ : {hitung}");
hitung--;
Console.WriteLine($"hitung setelah -- : {hitung}");

Console.WriteLine();

string depan = "Selamat";
string belakang = "Pagi";
string sapaan = depan + " " + belakang + "!";
Console.WriteLine($"[gabung +] sapaan = {sapaan}");

string namaSiswa = "Andi";
int nilai = 95;
string laporan = $"Siswa {namaSiswa} mendapat nilai {nilai}.";
Console.WriteLine($"[interpolasi $] {laporan}");

Console.WriteLine();

int kecil = 100;
double besar = kecil;
Console.WriteLine($"[implisit] int {kecil} -> double {besar}");

double nilaiDouble = 9.99;
int nilaiInt = (int)nilaiDouble;
Console.WriteLine($"[casting] double {nilaiDouble} -> int {nilaiInt}");

string teksAngka = "123";
int angkaHasil = Convert.ToInt32(teksAngka);
Console.WriteLine($"[Convert] string \"{teksAngka}\" -> int {angkaHasil}");

int tahun = 2026;
string teksTahun = tahun.ToString();
Console.WriteLine($"[ToString] int {tahun} -> string \"{teksTahun}\"");

Console.WriteLine();

double panjang = 12.5;
double lebar = 8.0;
double luas = panjang * lebar;
Console.WriteLine("--- Latihan: Luas Persegi Panjang ---");
Console.WriteLine($"panjang = {panjang} cm");
Console.WriteLine($"lebar   = {lebar} cm");
Console.WriteLine($"luas = panjang x lebar = {panjang} x {lebar} = {luas} cm2");

Console.WriteLine();
Console.WriteLine("=== Selesai. Coba ubah-ubah nilainya lalu jalankan ulang! ===");
