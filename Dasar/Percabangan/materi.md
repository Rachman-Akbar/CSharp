# Materi C# : Percabangan (If, Else, If-Else, dan Switch)

> File kode: `Program.cs` (full code, bisa `dotnet run`)
> File ini: penjelasan konsep + rangkuman sintaks. Baca file ini dulu, lalu bedah `Program.cs`.

## 0. Tujuan

Setelah materi ini kamu bisa:

1. Menulis kondisi dengan operator perbandingan dan logika.
2. Memilih struktur yang tepat: `if` / `if-else` / `if-else if-else` / `switch`.
3. Menulis `switch` klasik dan `switch expression` modern.
4. Memakai `ternary (? :)` untuk IF-ELSE satu baris.
5. Membuat program kasir sederhana dengan diskon + member.

## 1. Fondasi: Operator Perbandingan dan Logika

Semua kondisi `if` / `while` hasilnya `bool`: `true` atau `false`.

### 1a. Operator perbandingan

| Operator | Arti | Contoh (`a=10, b=3`) | Hasil |
|---|---|---|---|
| `==` | sama dengan | `a == b` | `False` |
| `!=` | tidak sama dengan | `a != b` | `True` |
| `>` | lebih besar | `a > b` | `True` |
| `<` | lebih kecil | `a < b` | `False` |
| `>=` | lebih besar / sama dengan | `a >= 10` | `True` |
| `<=` | lebih kecil / sama dengan | `b <= 3` | `True` |

### 1b. Operator logika

| Operator | Arti | Contoh | Hasil (`sudahMakan=true, adaUang=false`) |
|---|---|---|---|
| `&&` | AND: keduanya harus `true` | `sudahMakan && adaUang` | `False` |
| `\|\|` | OR: salah satu `true` cukup | `sudahMakan \|\| adaUang` | `True` |
| `!` | NOT: membalik | `!sudahMakan` | `False` |

## 2. IF Tunggal

Blok jalan **hanya jika** kondisi `true`. Kalau `false`, blok dilewati (tidak error).

```csharp
int nilai = 85;

if (nilai >= 75) // 85 >= 75? true → jalan
{
    Console.WriteLine("Selamat, kamu LULUS!");
}

int umur = 15;
if (umur >= 17) // 15 >= 17? false → dilewati
{
    Console.WriteLine("Kamu boleh buat KTP.");
}
Console.WriteLine("Program lanjut terus.");
```

## 3. IF-ELSE

Dua pilihan, **salah satu pasti jalan**.

```csharp
int angka = 7;

if (angka % 2 == 0) // 7 % 2 = 1, 1 == 0? false
{
    Console.WriteLine("Angka GENAP.");
}
else // cadangan jika kondisi false
{
    Console.WriteLine("Angka GANJIL."); // ← yang tampil
}
```

Dengan `&&` (dua syarat sekaligus):

```csharp
int uang = 50000;
bool tokoBuka = true;

if (uang >= 20000 && tokoBuka) // true && true = true
{
    Console.WriteLine("Kamu bisa jajan!");
}
else
{
    Console.WriteLine("Jajan batal.");
}
```

## 4. IF-ELSE IF-ELSE (Bertingkat)

Dicek dari **atas ke bawah**. Yang pertama `true` langsung menang, sisanya dilewati.
Cocok untuk: grade, kategori umur, rentang angka.

```csharp
int skor = 82;

if (skor >= 90)      // 82 >= 90? false → lanjut
{
    Console.WriteLine("Grade A");
}
else if (skor >= 80) // 82 >= 80? true → MENANG
{
    Console.WriteLine("Grade B"); // ← tampil, sisanya dilewati
}
else if (skor >= 70)
{
    Console.WriteLine("Grade C");
}
else if (skor >= 60)
{
    Console.WriteLine("Grade D");
}
else // semua di atas false (skor < 60)
{
    Console.WriteLine("Grade E (Tidak Lulus)");
}
```

> Urutan PENTING: dari yang paling ketat / terbesar dulu.
> Kalau `skor >= 60` ditaruh paling atas, semua nilai 60+ menang di sana (salah).

Contoh dengan `||`:

```csharp
int umurOrang = 10;

if (umurOrang < 5 || umurOrang > 100)
    Console.WriteLine("Umur tidak wajar.");
else if (umurOrang < 13) // 10 < 13 = true → menang
    Console.WriteLine("Kategori: Anak-anak.");
else if (umurOrang < 20)
    Console.WriteLine("Kategori: Remaja.");
else
    Console.WriteLine("Kategori: Dewasa.");
```

### Nested IF (IF di dalam IF)

Untuk pengecekan 2 tahap (misal login):

```csharp
string username = "admin";
string password = "1234";

if (username == "admin") // tahap 1 benar → masuk ke dalam
{
    if (password == "1234") // tahap 2 benar
        Console.WriteLine("Login berhasil!");
    else
        Console.WriteLine("Password salah!");
}
else
{
    Console.WriteLine("Username tidak dikenal!");
}
```

## 5. SWITCH

Pilihan cepat berdasarkan **satu nilai pasti**. Setiap `case` wajib `break`.

```csharp
int kodeHari = 3;

switch (kodeHari)
{
    case 1:
        Console.WriteLine("Hari Senin");
        break;
    case 2:
        Console.WriteLine("Hari Selasa");
        break;
    case 3: // cocok!
        Console.WriteLine("Hari Rabu"); // ← tampil
        break;
    case 4:
        Console.WriteLine("Hari Kamis");
        break;
    case 5:
        Console.WriteLine("Hari Jumat");
        break;
    default: // tidak ada case cocok
        Console.WriteLine("Kode hari tidak dikenal");
        break;
}
```

Aturan:

- Cocok untuk: kode hari, menu angka, huruf, nama. **BUKAN** untuk rentang (`>=`).
- `switch` bisa cek `string` juga (misal `peran = "guru"`).
- Beberapa `case` bisa digabung untuk 1 aksi:

```csharp
switch (kodeBulan)
{
    case 12:
    case 1:
    case 2: // 12, 1, atau 2 → sama-sama hujan
        Console.WriteLine("Musim Hujan");
        break;
    case 6:
    case 7:
    case 8:
        Console.WriteLine("Musim Kemarau");
        break;
    default:
        Console.WriteLine("Musim Pancaroba");
        break;
}
```

## 6. SWITCH EXPRESSION (Modern, C# 8+)

Lebih ringkas, tanpa `break`. Tanda `_` = default.

```csharp
char grade = 'B';

string pujian = grade switch
{
    'A' => "Sempurna!",
    'B' => "Bagus Sekali!", // ← cocok
    'C' => "Cukup, tingkatkan!",
    'D' => "Kurang, belajar lagi!",
    _ => "Tidak valid!"
};
Console.WriteLine($"{grade} = {pujian}");
```

### Bonus: Ternary (`? :`) — IF-ELSE satu baris

```csharp
// hasil = (kondisi) ? nilaiJikaTrue : nilaiJikaFalse
int suhu = 38;
string kondisiBadan = (suhu >= 37) ? "Demam" : "Sehat"; // → "Demam"
```

## 7. Latihan Mini (BAGIAN 6 di `Program.cs`)

Kasir + diskon member (`totalBelanja = 150000`, `tipeMember = "gold"`):

1. `IF-ELSE IF` → persen diskon belanja: `>= 200rb → 20%`, `>= 100rb → 10%` (150rb menang di sini), sisanya `0%`.
2. `SWITCH` → bonus member: `gold → 5%`, `silver → 3%`, lainnya `0%`.
3. Total: `totalDiskon = 10 + 5 = 15%`, `bayar = 150000 − 22500 = 127500`.

Coba ubah `totalBelanja` dan `tipeMember`, lalu jalankan ulang.

## 8. Kesalahan Umum

1. **`=` vs `==`** — `=` mengisi nilai, `==` membandingkan. `if (a = 3)` itu salah maksud.
2. **Urutan `else if` terbalik** — taruh syarat terbesar / paling ketat dulu.
3. **Lupa `break` di `switch`** — program nyelonong ke `case` bawahnya.
4. **`switch` untuk rentang** — `case >= 90:` tidak bisa di switch klasik, pakai `if-else if`.
5. **String case-sensitive** — `"Guru"` ≠ `"guru"`. Samakan huruf besar-kecil.
6. **Nested IF terlalu dalam** — kalau > 2 level, pertimbangkan `&&` / `switch`.

## 9. Cara Menjalankan

```bash
cd /home/akbar/Projects/Kelas/Percabangan
dotnet run
```

Output yang benar menampilkan: operator perbandingan/logika, `[IF]`, `[IF-ELSE]`, `[IF-ELSE IF]`, `[NESTED IF]`, `[SWITCH]`, `[SWITCH EXPR]`, `[TERNARY]`, lalu kasir `Rp150000 → bayar Rp127500`.
