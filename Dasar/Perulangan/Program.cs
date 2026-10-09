Console.WriteLine("=== Belajar C#: Perulangan For, While, dan Foreach ===");
Console.WriteLine();

Console.WriteLine("[FOR] Cetak 1 sampai 5:");
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine($"[FOR] i = {i}");
}

Console.WriteLine("[FOR] Mundur 5 sampai 1:");
for (int i = 5; i >= 1; i--)
{
    Console.WriteLine($"[FOR] i = {i}");
}

Console.WriteLine("[FOR] Bilangan genap 2-10:");
for (int i = 2; i <= 10; i += 2)
{
    Console.WriteLine($"[FOR] i = {i}");
}

int total = 0;
for (int i = 1; i <= 10; i++)
{
    total += i;
}
Console.WriteLine($"[FOR] Jumlah 1+2+...+10 = {total}");

Console.WriteLine();

Console.WriteLine("[WHILE] Cetak 1 sampai 5:");
int j = 1;
while (j <= 5)
{
    Console.WriteLine($"[WHILE] j = {j}");
    j++;
}

int tabungan = 0;
int bulan = 0;
while (tabungan < 100000)
{
    bulan++;
    tabungan += 20000;
    Console.WriteLine($"[WHILE] Bulan {bulan}: tabungan = Rp{tabungan}");
}
Console.WriteLine($"[WHILE] Target tercapai dalam {bulan} bulan!");

Console.WriteLine("[DO-WHILE] Contoh jalan 1x walau kondisi langsung false:");
int k = 100;
do
{
    Console.WriteLine($"[DO-WHILE] k = {k} (tetap tampil 1x)");
    k++;
} while (k <= 5);

Console.WriteLine();

string[] daftarNama = { "Andi", "Budi", "Citra" };
Console.WriteLine("[FOREACH] Daftar nama:");
foreach (string nama in daftarNama)
{
    Console.WriteLine($"[FOREACH] Halo, {nama}!");
}

int[] nilaiUjian = { 80, 90, 75, 95 };
int jumlah = 0;
foreach (int nilai in nilaiUjian)
{
    jumlah += nilai;
    Console.WriteLine($"[FOREACH] nilai = {nilai}, jumlah sementara = {jumlah}");
}
double rataRata = (double)jumlah / nilaiUjian.Length;
Console.WriteLine($"[FOREACH] Jumlah = {jumlah}, Rata-rata = {rataRata}");

Console.WriteLine("[FOR-index] Sama tapi tampil nomor urut:");
for (int i = 0; i < daftarNama.Length; i++)
{
    Console.WriteLine($"[FOR-index] No.{i + 1}: {daftarNama[i]}");
}

Console.WriteLine();

Console.WriteLine("[BREAK] Berhenti saat i == 3:");
for (int i = 1; i <= 5; i++)
{
    if (i == 3)
    {
        Console.WriteLine("[BREAK] Ketemu 3, STOP!");
        break;
    }
    Console.WriteLine($"[BREAK] i = {i}");
}

Console.WriteLine("[CONTINUE] Lewati angka 3:");
for (int i = 1; i <= 5; i++)
{
    if (i == 3)
    {
        Console.WriteLine("[CONTINUE] Lewati 3...");
        continue;
    }
    Console.WriteLine($"[CONTINUE] i = {i}");
}

Console.WriteLine();

Console.WriteLine("[NESTED] Tabel perkalian 1-3:");
for (int luar = 1; luar <= 3; luar++)
{
    for (int dalam = 1; dalam <= 3; dalam++)
    {
        Console.Write($"{luar}x{dalam}={luar * dalam}\t");
    }
    Console.WriteLine();
}

Console.WriteLine();

string[] barang = { "Buku", "Pensil", "Penghapus", "Penggaris" };
int[] harga = { 15000, 5000, 3000, 7000 };
Console.WriteLine("--- Latihan: Struk Belanja ---");

int totalBelanja = 0;
for (int i = 0; i < barang.Length; i++)
{
    Console.WriteLine($"{i + 1}. {barang[i]} - Rp{harga[i]}");
    totalBelanja += harga[i];
}
Console.WriteLine($"Total belanja: Rp{totalBelanja}");

if (totalBelanja >= 25000)
{
    int diskon = totalBelanja * 10 / 100;
    int bayar = totalBelanja - diskon;
    Console.WriteLine($"Diskon 10%: -Rp{diskon}");
    Console.WriteLine($"Harus bayar: Rp{bayar}");
}
else
{
    Console.WriteLine($"Harus bayar: Rp{totalBelanja} (belum dapat diskon)");
}

Console.WriteLine("Terima kasih sudah membeli:");
foreach (string b in barang)
{
    Console.WriteLine($"- {b}");
}

Console.WriteLine();
Console.WriteLine("=== Selesai. Coba ubah harga/barang lalu jalankan ulang! ===");
