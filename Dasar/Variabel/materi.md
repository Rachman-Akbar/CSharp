# Materi C# : Tipe Data, Variabel, dan Operasi Sederhana

> File kode: `Program.cs` (full code, bisa `dotnet run`)
> File ini: penjelasan konsep + rangkuman sintaks. Baca file ini dulu, lalu bedah `Program.cs`.

## 0. Tujuan

Setelah materi ini kamu bisa:

1. Menyebutkan tipe data dasar C#: `int`, `double`, `float`, `decimal`, `string`, `char`, `bool`.
2. Membuat variabel dengan deklarasi, inisialisasi, `var`, dan `const`.
3. Melakukan operasi aritmatika `+ - * / %` tanpa terjebak pembagian `int`.
4. Menggabungkan string dengan `+` dan interpolasi `$"..."`.
5. Mengonversi tipe data: implisit, casting, `Convert`, dan `.ToString()`.

## 1. Tipe Data Dasar

| Tipe | Isi | Contoh | Catatan |
|---|---|---|---|
| `int` | bilangan bulat | `20`, `-10` | 4 byte, −2,1 M s/d 2,1 M |
| `double` | desimal umum | `170.5` | default desimal di C#, presisi ganda |
| `float` | desimal hemat | `36.6f` | wajib akhiran `f` |
| `decimal` | desimal uang | `75000.50m` | wajib akhiran `m`, untuk harga/gaji |
| `string` | teks | `"Budi"` | kutip ganda `"..."` |
| `char` | 1 karakter | `'A'` | kutip satu `'...'` |
| `bool` | logika | `true` / `false` | hanya 2 nilai |

```csharp
int umur = 20;
double tinggiBadan = 170.5;
float suhu = 36.6f;            // tanpa f = dianggap double!
decimal hargaBuku = 75000.50m; // tanpa m = dianggap double!
string nama = "Budi";
char grade = 'A';
bool sudahLulus = true;
```

## 2. Variabel

Variabel = kotak bernama yang menyimpan nilai. Nilai bisa diubah, kecuali `const`.

### 2a. Deklarasi vs inisialisasi

```csharp
int nilaiUjian;   // 1. deklarasi: bikin kotak bernama nilaiUjian bertipe int
nilaiUjian = 85;  // 2. inisialisasi: isi kotak dengan 85

string kota = "Jakarta"; // deklarasi + isi sekaligus (paling umum)

nilaiUjian = 90;  // ubah nilai: 85 ditimpa 90
```

### 2b. `var` — tipe ditebak compiler

```csharp
var jurusan = "RPL"; // compiler tahu ini string
var angkatan = 2024;  // compiler tahu ini int

Console.WriteLine(jurusan.GetType().Name); // "String"
```

> `var` bukan tanpa-tipe. Tipenya dikunci saat pertama diisi, tidak bisa ganti.

### 2c. `const` — nilai dikunci

```csharp
const double PI = 3.14159;
// PI = 3.14; // ERROR! const tidak boleh diubah
```

### 2d. Aturan penamaan

- Pakai `camelCase`: `namaLengkap`, `jumlahSiswa` (huruf pertama kecil).
- Jelas dan bermakna: `jumlahSiswa`, bukan `x` atau `data1`.

## 3. Operasi Aritmatika

| Operator | Arti | Contoh (`a=10, b=3`) | Hasil |
|---|---|---|---|
| `+` | tambah | `a + b` | `13` |
| `-` | kurang | `a - b` | `7` |
| `*` | kali | `a * b` | `30` |
| `/` | bagi | `a / b` | `3` (bukan 3,33!) |
| `%` | sisa bagi | `a % b` | `1` |

> Jebakan: `int / int = int`. Koma dibuang! `10 / 3 = 3`.
> Mau koma? Salah satu harus desimal: `(double)a / b = 3.333...`.

### Penugasan singkat dan increment

```csharp
int skor = 100;
skor += 10; // = skor + 10 → 110
skor -= 5;  // = skor - 5  → 105
skor *= 2;  // = skor * 2  → 210
skor /= 3;  // = skor / 3  → 70

int hitung = 5;
hitung++; // +1 → 6
hitung--; // -1 → 5
```

## 4. Operasi String

```csharp
// 4a. Gabung dengan +
string sapaan = "Selamat" + " " + "Pagi" + "!"; // "Selamat Pagi!"

// 4b. Interpolasi dengan $ (modern, disarankan)
string namaSiswa = "Andi";
int nilai = 95;
string laporan = $"Siswa {namaSiswa} mendapat nilai {nilai}.";
```

## 5. Konversi Tipe Data

| Jenis | Arah | Cara | Contoh |
|---|---|---|---|
| Implisit (otomatis, aman) | kecil → besar | langsung | `double besar = kecil;` (`100 → 100`) |
| Casting (manual, koma hilang) | besar → kecil | `(tipe)nilai` | `(int)9.99 → 9` |
| String → angka | teks → bilangan | `Convert.ToInt32(...)` | `"123" → 123` |
| Angka → string | bilangan → teks | `.ToString()` | `2026 → "2026"` |

```csharp
int kecil = 100;
double besar = kecil; // otomatis, aman

double nilaiDouble = 9.99;
int nilaiInt = (int)nilaiDouble; // paksa: koma dipotong → 9

int angkaHasil = Convert.ToInt32("123"); // "123" → 123
string teksTahun = 2026.ToString();      // 2026 → "2026"
```

## 6. Latihan Mini (BAGIAN 6 di `Program.cs`)

Luas persegi panjang (`panjang × lebar`):

```csharp
double panjang = 12.5;
double lebar = 8.0;
double luas = panjang * lebar; // 12.5 × 8 = 100
Console.WriteLine($"luas = {panjang} x {lebar} = {luas} cm2");
```

Coba ubah `panjang` / `lebar`, jalankan ulang, lihat hasil berubah.

## 7. Kesalahan Umum

1. **`float` / `decimal` tanpa akhiran** — `36.6` dianggap `double`. Tulis `36.6f`, `75000.50m`.
2. **`int / int` berharap koma** — `10 / 3 = 3`. Pakai `(double)a / b` untuk `3.33`.
3. **`char` pakai kutip ganda** — `'A'` benar, `"A"` itu `string` (beda tipe).
4. **`const` diubah** — langsung error compile. Memang tujuannya dikunci.
5. **Nama variabel ambigu** — `x`, `data1` bikin bingung. Pakai nama jelas.

## 8. Cara Menjalankan

```bash
cd /home/akbar/Projects/Kelas/Variabel
dotnet run
```

Output yang benar menampilkan: `[int]`, `[double]`, `[float]`, `[decimal]`, `[string]`, `[char]`, `[bool]`, `[variabel]`, `[var]`, `[const]`, operasi `13 / 7 / 30 / 3 / 1`, gabung string, konversi, lalu luas `100 cm2`.
