# Materi C# : Perulangan (For, While, dan Foreach)

> File kode: `Program.cs` (full code + komentar tiap baris, bisa `dotnet run`)
> File ini: penjelasan konsep + rangkuman sintaks. Baca file ini dulu, lalu bedah `Program.cs`.

## 0. Tujuan

Setelah materi ini kamu bisa:

1. Memilih loop yang tepat: `for` / `while` / `do-while` / `foreach`.
2. Menulis sintaks tanpa mencontek.
3. Menghindari infinite loop.
4. Memakai `break` dan `continue` dengan benar.
5. Membaca array dengan `for` ber-index dan `foreach`.

## 1. Konsep Dasar

Perulangan = menjalankan blok kode **berulang-kali** tanpa menulis ulang.

| Jenis | Kapan dipakai | Ciri |
|---|---|---|
| `for` | Jumlah pengulangan **sudah jelas** (misal 1–5, 10x) | Ada `nilaiAwal; kondisi; perubahan` dalam 1 baris |
| `while` | Ulang **selama** kondisi `true`, jumlah belum pasti | Cek kondisi **di awal**, bisa 0x jalan |
| `do-while` | Minimal jalan **1x**, kondisi dicek belakangan | Blok `do` dulu, baru `while(...)` |
| `foreach` | Baca **isi koleksi** satu per satu (array / List) | Tanpa index, tanpa ubah isi |

## 2. FOR

### Sintaks

```csharp
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine(i);
}
```

Artinya:

1. `int i = 1` → dijalankan **sekali** di awal.
2. `i <= 5` → dicek **sebelum tiap putaran**. `true` = lanjut, `false` = berhenti.
3. `i++` → dijalankan **setelah tiap putaran**.
4. Putaran: `1, 2, 3, 4, 5`.

### Variasi (lihat BAGIAN 1 di `Program.cs`)

| Contoh | Kode | Putaran |
|---|---|---|
| Maju | `for (int i = 1; i <= 5; i++)` | 1 2 3 4 5 |
| Mundur | `for (int i = 5; i >= 1; i--)` | 5 4 3 2 1 |
| Loncat 2 | `for (int i = 2; i <= 10; i += 2)` | 2 4 6 8 10 |
| Jumlahkan | `total += i;` di dalam loop 1–10 | hasil `55` |

## 3. WHILE dan DO-WHILE

### 3a. `while` — cek dulu, baru jalan

```csharp
int j = 1;          // 1. nilai awal di LUAR loop
while (j <= 5)      // 2. kondisi dicek tiap putaran
{
    Console.WriteLine(j);
    j++;            // 3. perubahan WAJIB, kalau lupa = infinite loop!
}
```

Contoh tak-pasti (BAGIAN 2b): menabung `20rb/bulan` sampai `tabungan >= 100rb`.
Tidak tahu butuh berapa bulan di awal → cocok pakai `while`.

### 3b. `do-while` — jalan dulu 1x, baru cek

```csharp
int k = 100;
do
{
    Console.WriteLine(k); // tetap tampil 1x walau k=100
    k++;
} while (k <= 5);         // dicek belakangan: 101<=5? false → berhenti
```

> Ingat: `while` bisa 0x jalan, `do-while` minimal 1x jalan.

## 4. FOREACH

Khusus membaca **isi** koleksi, tanpa mikir index.

```csharp
string[] daftarNama = { "Andi", "Budi", "Citra" };

foreach (string nama in daftarNama)
{
    Console.WriteLine($"Halo, {nama}!");
}
// Putaran 1: nama="Andi", 2: nama="Budi", 3: nama="Citra"
```

Aturan:

- `foreach (tipe namaSementara in koleksi)`.
- `array.Length` = jumlah isi (contoh: 4 nilai → rata-rata = `jumlah / Length`).
- Butuh **nomor urut / index**? Jangan `foreach`, pakai `for`:

```csharp
for (int i = 0; i < daftarNama.Length; i++)
{
    Console.WriteLine($"No.{i + 1}: {daftarNama[i]}");
}
```

> Index array mulai dari `0`. Isi ke-1 = index `0`.

## 5. BREAK dan CONTINUE

| Kata | Efek | Analogi |
|---|---|---|
| `break;` | Hentikan **seluruh** loop seketika | Keluar dari wahana |
| `continue;` | Lewati **putaran ini saja**, lanjut berikutnya | Skip 1 putaran komidi putar |

```csharp
// BREAK: ketemu 3 langsung STOP → tampil 1, 2 saja
for (int i = 1; i <= 5; i++)
{
    if (i == 3) break;
    Console.WriteLine(i);
}

// CONTINUE: lewati 3 → tampil 1, 2, 4, 5
for (int i = 1; i <= 5; i++)
{
    if (i == 3) continue;
    Console.WriteLine(i);
}
```

## 6. LOOP BERSARANG (Nested Loop)

Untuk tiap **1 putaran luar**, loop **dalam jalan penuh**.

```csharp
for (int luar = 1; luar <= 3; luar++)       // baris 1,2,3
{
    for (int dalam = 1; dalam <= 3; dalam++) // tiap baris: kolom 1,2,3
    {
        Console.Write($"{luar}x{dalam}={luar * dalam}\t");
    }
    Console.WriteLine(); // ganti baris
}
```

Hasil:

```text
1x1=1   1x2=2   1x3=3
2x1=2   2x2=4   2x3=6
3x1=3   3x2=6   3x3=9
```

## 7. Latihan Mini (BAGIAN 6 di `Program.cs`)

Struk belanja:

1. `for` ber-index → cetak `barang[i]` + `harga[i]` + hitung `totalBelanja`.
2. `if` di luar loop → jika `total >= 25000` diskon 10%.
3. `foreach` → sapa tiap nama barang tanpa index.

Coba ubah: tambah barang, ubah harga di bawah `25000`, lihat diskon hilang.

## 8. Kesalahan Umum

1. **Infinite loop** — lupa `j++` di `while`. Program tidak berhenti.
2. **`<=` vs `<` pada array** — pakai `i < array.Length`, bukan `<=` (kelebihan 1 = error index).
3. **Index mulai 0** — `barang[0]` = barang pertama, bukan `barang[1]`.
4. **`foreach` untuk ubah isi** — tidak bisa. Pakai `for` + index.
5. **Lupa `break` di `switch`** (materi lalu) vs **`break` di loop** — sama-sama keluar, tapi konteks beda.

## 9. Cara Menjalankan

```bash
cd /home/akbar/Projects/Kelas/Perulangan
dotnet run
```

Output yang benar menampilkan: `[FOR]`, `[WHILE]`, `[DO-WHILE]`, `[FOREACH]`, `[BREAK]`, `[CONTINUE]`, `[NESTED]`, lalu struk belanja `Rp30000 → bayar Rp27000`.
