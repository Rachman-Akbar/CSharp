# Materi C# : Method & Fungsi Dasar

> File kode: `Program.cs` (full code + komentar tiap baris, bisa `dotnet run`)
> File ini: penjelasan konsep + rangkuman sintaks. Baca file ini dulu, lalu bedah `Program.cs`.

## 0. Tujuan

Setelah materi ini kamu bisa:

1. Membedakan `void` (bekerja) dan `return` (menghasilkan nilai).
2. Menulis method ber-parameter dan memanggilnya dengan argumen benar.
3. Memakai parameter default, named argument, dan overloading.
4. Memakai `params`, expression-bodied (`=>`), method ber-array, dan rekursi.
5. Memecah program kasir jadi method kecil (1 method = 1 tugas).

> Catatan file: `Program.cs` pakai Top-Level Statement. Semua **pemanggilan** di atas,
> semua **definisi** method di bawah. Samakan namanya untuk membaca.

## 1. Method void Tanpa Parameter

Blok kode bernama yang dipanggil saat dibutuhkan.

```csharp
Sapa(); // panggil
Sapa(); // panggil lagi → jalan ulang tanpa tulis ulang

void Sapa() // definisi: void = tidak return
{
    Console.WriteLine("Halo, selamat datang di kelas C#!");
}
```

## 2. Method Dengan Parameter

Parameter = titipan saat definisi. Argumen = nilai nyata saat memanggil.

```csharp
SapaNama("Andi");            // argumen "Andi" → parameter nama = "Andi"
TampilkanBiodata("Budi", 20);// urutan HARUS sama: nama dulu, umur kemudian

void SapaNama(string nama)
{
    Console.WriteLine($"Halo, {nama}!");
}

void TampilkanBiodata(string nama, int umur)
{
    Console.WriteLine($"Nama: {nama}, Umur: {umur} tahun.");
}
```

## 3. Fungsi Dengan `return`

`return` mengembalikan nilai ke pemanggil. Setelah `return`, method berhenti.

```csharp
int hasilTambah = Tambah(10, 3); // 13 disimpan
Console.WriteLine(Tambah(5, 7)); // 12 langsung dipakai

int Tambah(int a, int b) // int di depan = janji return bilangan bulat
{
    return a + b;
}
```

Contoh gabungan method + percabangan:

```csharp
string TentukanGrade(int skor)
{
    if (skor >= 90) return "A";      // return langsung keluar, tak perlu else
    else if (skor >= 80) return "B"; // 82 → "B"
    else if (skor >= 70) return "C";
    return "D"; // cadangan (pengganti else terakhir)
}
```

## 4. Parameter Default, Named Argument, Overloading

### 4a. Parameter default

Punya nilai cadangan bila argumen tidak dikirim.

```csharp
SapaDefault();        // → "Halo, Tamu!"
SapaDefault("Dedi");  // → "Halo, Dedi!"

void SapaDefault(string nama = "Tamu")
{
    Console.WriteLine($"Halo, {nama}!");
}
```

### 4b. Named argument

Sebut nama parameter → urutan bebas dan lebih jelas.

```csharp
TampilkanBiodata(umur: 25, nama: "Eka"); // dibalik tapi tetap benar
double volume = HitungVolume(panjang: 10, lebar: 5, tinggi: 2); // 100
```

### 4c. Overloading

Nama **sama**, parameter **beda** (jumlah/tipe). C# memilih otomatis.

> Catatan: `Program.cs` pakai Top-Level Statement yang local function-nya
> **tidak bisa** overload, jadi di kode memakai nama berbeda
> (`Tambah` / `TambahDesimal` / `TambahTiga`). Di dalam `class`
> ketiganya boleh sama-sama bernama `Tambah`.

```csharp
Tambah(4, 6);          // versi (int, int) → 10
TambahDesimal(4.5, 6.2); // versi (double, double) → 10.7
TambahTiga(1, 2, 3);      // versi (int, int, int) → 6
```

## 5. Fungsi Lainnya

### 5a. `params` — banyak argumen tanpa array manual

```csharp
JumlahkanSemua(10, 20, 30, 40); // → 100
JumlahkanSemua(5, 5);           // → 10

int JumlahkanSemua(params int[] angka) // semua argumen dikemas jadi array
{
    int total = 0;
    foreach (int n in angka) total += n;
    return total;
}
```

### 5b. Expression-bodied — method 1 baris dengan `=>`

```csharp
int Kuadrat(int x) => x * x; // = { return x * x; }
string SapaSingkat(string nama) => $"Halo, {nama}!";
```

### 5c. Method dengan array

Kirim array utuh, olah di dalam, kembalikan 1 nilai.

```csharp
int[] nilaiUjian = { 80, 90, 75, 95 };
HitungRataRata(nilaiUjian); // → 85
CariTerbesar(nilaiUjian);   // → 95
```

### 5d. Rekursi — fungsi memanggil dirinya sendiri

Wajib ada **titik berhenti**, kalau tidak = `StackOverflow`.

```csharp
Faktorial(5); // 5×4×3×2×1 = 120

int Faktorial(int n)
{
    if (n <= 1) return 1;      // titik berhenti
    return n * Faktorial(n - 1); // panggil diri dengan n-1
}
```

## 6. Latihan Mini (BAGIAN 6 di `Program.cs`)

Kasir dipecah 3 tugas (`barang` + `harga` sejajar index):

| Method | Tugas | Hasil |
|---|---|---|
| `TampilkanStruk(barang, harga)` | cetak `1. Buku - Rp15000` ... | tampil (void) |
| `HitungTotal(harga)` | jumlahkan semua | `30000` |
| `HitungDiskon(total)` | 10% jika `>= 25000` | `3000` |

Bayar akhir: `30000 − 3000 = 27000`. Coba ubah harga di bawah `25000`, lihat diskon jadi `0`.

## 7. Kesalahan Umum

1. **Lupa `return` di semua jalur** — fungsi `int` wajib `return` di tiap cabang `if-else`.
2. **Urutan argumen tertukar** — `TampilkanBiodata(20, "Budi")` = error / salah. Pakai named argument bila ragu.
3. **`void` dipakai sebagai nilai** — `int x = Sapa();` error, karena `void` tidak menghasilkan apa-apa.
4. **Overload ambigu** — dua definisi terlalu mirip (misal beda cuma `int` vs `long`) bikin compiler bingung.
5. **Rekursi tanpa berhenti** — `Faktorial` tanpa `if (n <= 1)` = crash.
6. **Method tak pernah dipanggil** — definisi saja tidak jalan. Harus ada `Nama();` di alur utama.

## 8. Cara Menjalankan

```bash
cd /home/akbar/Projects/Kelas/Method
dotnet run
```

Output yang benar menampilkan: `[VOID]`, `[PARAM]`, `[RETURN]` (13, 100, Grade B),
`[DEFAULT]`, `[NAMED]`, `[OVERLOAD]`, `[PARAMS]`, `[EXPR]`, `[ARRAY-FUNC]`, `[REKURSI]` (120),
lalu kasir `Total Rp30000 → bayar Rp27000`.
