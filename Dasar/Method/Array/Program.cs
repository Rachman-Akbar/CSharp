Console.WriteLine("=== Belajar C#: Array dan List Dasar ===");
Console.WriteLine();

string[] daftarNama = { "Andi", "Budi", "Citra" };
Console.WriteLine($"[ARRAY] Jumlah isi (Length) = {daftarNama.Length}");

Console.WriteLine($"[ARRAY] index 0 = {daftarNama[0]}");
Console.WriteLine($"[ARRAY] index 1 = {daftarNama[1]}");
Console.WriteLine($"[ARRAY] index 2 = {daftarNama[2]}");

daftarNama[1] = "Budi Santoso";
Console.WriteLine($"[ARRAY] index 1 setelah diubah = {daftarNama[1]}");

int[] nilai = new int[4];
nilai[0] = 80;
nilai[1] = 90;
nilai[2] = 75;
nilai[3] = 95;
Console.WriteLine($"[ARRAY] nilai = [{nilai[0]}, {nilai[1]}, {nilai[2]}, {nilai[3]}]");

Console.WriteLine();

Console.WriteLine("[FOR] Cetak semua nama + nomor:");
for (int i = 0; i < daftarNama.Length; i++)
{
    Console.WriteLine($"[FOR] No.{i + 1}: {daftarNama[i]}");
}

Console.WriteLine("[FOREACH] Cetak semua nama (tanpa nomor):");
foreach (string nama in daftarNama)
{
    Console.WriteLine($"[FOREACH] Halo, {nama}!");
}

int jumlah = 0;
foreach (int n in nilai)
{
    jumlah += n;
}
double rataRata = (double)jumlah / nilai.Length;
Console.WriteLine($"[HITUNG] Jumlah = {jumlah}, Rata-rata = {rataRata}");

int terbesar = nilai[0];
for (int i = 1; i < nilai.Length; i++)
{
    if (nilai[i] > terbesar)
    {
        terbesar = nilai[i];
    }
}
Console.WriteLine($"[MAX] Nilai terbesar = {terbesar}");

Console.WriteLine();

int[] angkaAcak = { 5, 2, 8, 1, 3 };
Console.WriteLine($"[SORT] Sebelum: [{string.Join(", ", angkaAcak)}]");
Array.Sort(angkaAcak);
Console.WriteLine($"[SORT] Sesudah: [{string.Join(", ", angkaAcak)}]");

Array.Reverse(angkaAcak);
Console.WriteLine($"[REVERSE] Dibalik: [{string.Join(", ", angkaAcak)}]");

int posisi = Array.IndexOf(nilai, 75);
Console.WriteLine($"[INDEXOF] Angka 75 ada di index = {posisi}");
int tidakAda = Array.IndexOf(nilai, 100);
Console.WriteLine($"[INDEXOF] Angka 100 ada di index = {tidakAda} (artinya tidak ketemu)");

Console.WriteLine();

List<string> belanja = new List<string> { "Buku", "Pensil" };
Console.WriteLine($"[LIST] Awal ({belanja.Count} isi): [{string.Join(", ", belanja)}]");
belanja.Add("Penghapus");
belanja.Add("Penggaris");
Console.WriteLine($"[LIST] Setelah Add ({belanja.Count} isi): [{string.Join(", ", belanja)}]");

Console.WriteLine($"[LIST] index 0 = {belanja[0]}");
belanja[1] = "Pensil Warna";
Console.WriteLine($"[LIST] index 1 setelah diubah = {belanja[1]}");

belanja.Remove("Penghapus");
Console.WriteLine($"[LIST] Setelah Remove Penghapus: [{string.Join(", ", belanja)}]");
belanja.RemoveAt(0);
Console.WriteLine($"[LIST] Setelah RemoveAt(0): [{string.Join(", ", belanja)}]");

bool adaBuku = belanja.Contains("Buku");
Console.WriteLine($"[LIST] Apakah ada 'Buku'? {adaBuku}");
bool adaPenggaris = belanja.Contains("Penggaris");
Console.WriteLine($"[LIST] Apakah ada 'Penggaris'? {adaPenggaris}");

Console.WriteLine("[LIST] Cetak sisa belanja dengan FOR:");
for (int i = 0; i < belanja.Count; i++)
{
    Console.WriteLine($"[LIST] {i + 1}. {belanja[i]}");
}

List<int> harga = new List<int> { 15000, 5000, 7000 };
harga.Add(3000);
int totalHarga = 0;
foreach (int h in harga)
{
    totalHarga += h;
}
Console.WriteLine($"[LIST] Total harga {harga.Count} barang = Rp{totalHarga}");

Console.WriteLine();

string[] hariTetap = { "Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu", "Minggu" };
List<string> antrian = new List<string> { "Andi" };
antrian.Add("Budi");
Console.WriteLine($"[VS] Hari (array, Length={hariTetap.Length}): {hariTetap[0]}...{hariTetap[6]}");
Console.WriteLine($"[VS] Antrian (list, Count={antrian.Count}): [{string.Join(", ", antrian)}]");

Console.WriteLine();

string[] siswa = { "Andi", "Budi", "Citra", "Dedi" };
int[] skor = { 80, 95, 70, 85 };
Console.WriteLine("--- Latihan: Nilai Siswa ---");

int totalSkor = 0;
for (int i = 0; i < skor.Length; i++)
{
    totalSkor += skor[i];
}
double rataSkor = (double)totalSkor / skor.Length;
Console.WriteLine($"Total = {totalSkor}, Rata-rata = {rataSkor}");

List<string> diAtasRata = new List<string>();
for (int i = 0; i < siswa.Length; i++)
{
    if (skor[i] > rataSkor)
    {
        diAtasRata.Add($"{siswa[i]} ({skor[i]})");
    }
}
Console.WriteLine($"Di atas rata-rata ({diAtasRata.Count} orang): [{string.Join(", ", diAtasRata)}]");

Console.WriteLine();
Console.WriteLine("=== Selesai. Coba tambah siswa/skor lalu jalankan ulang! ===");
