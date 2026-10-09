# Materi C# : Array dan List Dasar

> File kode: `Program.cs` (full code + komentar tiap baris, bisa `dotnet run`)
> File ini: penjelasan konsep + rangkuman sintaks. Baca file ini dulu, lalu bedah `Program.cs`.

## 0. Tujuan

Setelah materi ini kamu bisa:

1. Membuat dan membaca `array` lewat `index` (mulai dari `0`).
2. Mengulang isi array dengan `for` ber-index dan `foreach`.
3. Menghitung jumlah, rata-rata, dan nilai terbesar dari array.
4. Memakai operasi siap pakai: `Array.Sort`, `Array.Reverse`, `Array.IndexOf`.
5. Membuat `List<T>` yang fleksibel: `Add`, `Remove`, `RemoveAt`, `Contains`.
6. Memilih yang tepat: `array` vs `List`.

## 1. Array Dasar

Variabel biasa = 1 kotak untuk 1 nilai. Array = 1 rak dengan banyak kotak bernomor (`index`).

```csharp
string[] daftarNama = { "Andi", "Budi", "Citra" };
// index: 0=Andi, 1=Budi, 2=Citra. Jumlah isi = daftarNama.Length = 3

daftarNama[0]; // baca kotak pertama → "Andi"
daftarNama[1] = "Budi Santoso"; // ubah kotak kedua

int[] nilai = new int[4]; // buat 4 kotak dulu (terisi 0), isi belakangan
nilai[0] = 80;
```

Aturan:

- Index mulai dari `0`. Isi ke-3 = index `2`.
- `daftarNama[3]` pada array isi 3 = **error** (hanya 0–2).
- Isi **bisa diubah**, jumlah **tetap** (tidak bisa tambah kotak baru).

## 2. Perulangan Array

Jangan tulis satu-satu, pakai loop. Rumus index: `for (i = 0; i < array.Length; i++)`.

```csharp
// Butuh nomor urut / mau ubah isi → FOR ber-index
for (int i = 0; i < daftarNama.Length; i++)
{
    Console.WriteLine($"No.{i + 1}: {daftarNama[i]}");
}

// Hanya baca isi → FOREACH (lebih simpel)
foreach (string nama in daftarNama)
{
    Console.WriteLine($"Halo, {nama}!");
}
```

> Pakai `< Length`, BUKAN `<= Length`. Kelebihan 1 = error index.

### Pola hitung: jumlah, rata-rata, terbesar

```csharp
int jumlah = 0;
foreach (int n in nilai) // nilai = {80, 90, 75, 95}
    jumlah += n;         // → 340

double rataRata = (double)jumlah / nilai.Length; // 340 / 4 = 85

int terbesar = nilai[0]; // tebak awal = isi pertama
for (int i = 1; i < nilai.Length; i++)
    if (nilai[i] > terbesar)
        terbesar = nilai[i]; // → 95
```

## 3. Operasi Array Siap Pakai

```csharp
int[] angkaAcak = { 5, 2, 8, 1, 3 };

Array.Sort(angkaAcak);    // urutkan: 1, 2, 3, 5, 8
Array.Reverse(angkaAcak); // balik urutan: 8, 5, 3, 2, 1 (bukan mengurutkan!)

Array.IndexOf(nilai, 75);  // cari 75 → 2 (posisi index)
Array.IndexOf(nilai, 100); // tidak ketemu → -1
```

Tips cetak rapi:

```csharp
string.Join(", ", angkaAcak); // gabung isi jadi teks "5, 2, 8, 1, 3"
```

## 4. List Dasar

`List` = tas karet: jumlah **fleksibel**, bisa tambah/hapus kapan saja.
(Array = rak paten: jumlah tetap.)

```csharp
List<string> belanja = new List<string> { "Buku", "Pensil" };

belanja.Add("Penghapus"); // tambah di belakang
belanja[1] = "Pensil Warna"; // ubah (sama seperti array)
belanja.Remove("Penghapus");  // hapus berdasar NAMA isi
belanja.RemoveAt(0);          // hapus berdasar INDEX

belanja.Contains("Buku"); // cek ada/tidak → true/false
belanja.Count;            // jumlah isi (array pakai .Length!)
```

| Array | List |
|---|---|
| Jumlah **tetap** | Jumlah **fleksibel** |
| `.Length` | `.Count` |
| Data pasti: hari (7), bulan (12) | Data berubah: keranjang, antrian |

```csharp
string[] hariTetap = { "Senin", ..., "Minggu" }; // pasti 7 → array pas
List<string> antrian = new List<string> { "Andi" };
antrian.Add("Budi"); // orang baru datang, tinggal tambah
```

Loop `List` sama seperti array, tapi batasnya `.Count`:

```csharp
for (int i = 0; i < belanja.Count; i++)
    Console.WriteLine($"{i + 1}. {belanja[i]}");
```

## 5. Latihan Mini (BAGIAN 6 di `Program.cs`)

Nilai 4 siswa (`skor = {80, 95, 70, 85}`, rata-rata `82.5`):

1. `for` ber-index → jumlahkan `totalSkor`, hitung `rataSkor`.
2. `List<string>` kosong → tampung yang `skor > rata-rata` (jumlah belum tahu!).
3. Hasil: `Budi (95), Dedi (85)` — 2 orang di atas rata-rata.

Coba tambah siswa/skor, jalankan ulang, lihat `rata-rata` dan List ikut berubah.

## 6. Kesalahan Umum

1. **Index mulai 1** — salah. Index pertama = `0`.
2. **`<= Length`** — salah. Pakai `< Length` / `< Count`.
3. **`Length` vs `Count`** — array pakai `Length`, `List` pakai `Count`. Tertukar = error compile.
4. **Tambah isi ke array** — tidak bisa (`array.Add` tidak ada). Pakai `List`, atau buat array baru.
5. **`Remove` yang tidak ada** — tidak error, hanya tidak terjadi apa-apa (cek `Contains` dulu bila ragu).
6. **Lupa `(double)` saat rata-rata** — `340 / 4` dalam `int` = `85`, tapi `335 / 4 = 83` (koma hilang). Cast dulu.

## 7. Cara Menjalankan

```bash
cd /home/akbar/Projects/Kelas/Array
dotnet run
```

Output yang benar menampilkan: `[ARRAY]`, `[FOR]`, `[FOREACH]`, `[HITUNG] Jumlah = 340, Rata-rata = 85`, `[MAX] = 95`, `[SORT]`, `[REVERSE]`, `[INDEXOF]`, `[LIST]`, `[VS]`, lalu latihan `Total = 330, Rata-rata = 82.5`.
