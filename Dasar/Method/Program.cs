Console.WriteLine("=== Belajar C#: Method & Fungsi Dasar ===");
Console.WriteLine();

Sapa();
Sapa();

SapaNama("Andi");
SapaNama("Citra");
TampilkanBiodata("Budi", 20);

int hasilTambah = Tambah(10, 3);
Console.WriteLine($"[RETURN] Tambah(10, 3) = {hasilTambah}");
Console.WriteLine($"[RETURN] Tambah(5, 7) = {Tambah(5, 7)}");

double luas = LuasPersegiPanjang(12.5, 8.0);
Console.WriteLine($"[RETURN] Luas 12.5 x 8 = {luas}");

string gradeB = TentukanGrade(82);
Console.WriteLine($"[RETURN] Skor 82 = Grade {gradeB}");

Console.WriteLine();

SapaDefault();
SapaDefault("Dedi");

TampilkanBiodata(umur: 25, nama: "Eka");
double volume = HitungVolume(panjang: 10, lebar: 5, tinggi: 2);
Console.WriteLine($"[NAMED] Volume 10x5x2 = {volume}");

Console.WriteLine($"[OVERLOAD] Tambah(4, 6) = {Tambah(4, 6)}");
Console.WriteLine($"[OVERLOAD] TambahDesimal(4.5, 6.2) = {TambahDesimal(4.5, 6.2)}");
Console.WriteLine($"[OVERLOAD] TambahTiga(1, 2, 3) = {TambahTiga(1, 2, 3)}");

Console.WriteLine();

int totalBanyak = JumlahkanSemua(10, 20, 30, 40);
Console.WriteLine($"[PARAMS] JumlahkanSemua(10,20,30,40) = {totalBanyak}");
Console.WriteLine($"[PARAMS] JumlahkanSemua(5, 5) = {JumlahkanSemua(5, 5)}");

Console.WriteLine($"[EXPR] Kuadrat(7) = {Kuadrat(7)}");
Console.WriteLine($"[EXPR] SapaSingkat(\"Fajar\") = {SapaSingkat("Fajar")}");

int[] nilaiUjian = { 80, 90, 75, 95 };
Console.WriteLine($"[ARRAY-FUNC] Rata-rata = {HitungRataRata(nilaiUjian)}");
Console.WriteLine($"[ARRAY-FUNC] Terbesar = {CariTerbesar(nilaiUjian)}");

Console.WriteLine($"[REKURSI] Faktorial(5) = {Faktorial(5)}");
Console.WriteLine($"[REKURSI] Faktorial(3) = {Faktorial(3)}");

Console.WriteLine();

string[] barang = { "Buku", "Pensil", "Penghapus", "Penggaris" };
int[] harga = { 15000, 5000, 3000, 7000 };
Console.WriteLine("--- Latihan: Kasir dengan Method ---");
TampilkanStruk(barang, harga);
int totalBelanja = HitungTotal(harga);
int diskon = HitungDiskon(totalBelanja);
Console.WriteLine($"Total       : Rp{totalBelanja}");
Console.WriteLine($"Diskon      : -Rp{diskon}");
Console.WriteLine($"Harus bayar : Rp{totalBelanja - diskon}");

Console.WriteLine();
Console.WriteLine("=== Selesai. Coba panggil method dengan angka/nama lain! ===");

void Sapa()
{
    Console.WriteLine("[VOID] Halo, selamat datang di kelas C#!");
    Console.WriteLine("[VOID] Method ini dipanggil, bukan dijalankan otomatis.");
}

void SapaNama(string nama)
{
    Console.WriteLine($"[PARAM] Halo, {nama}! Senang bertemu denganmu.");
}

void TampilkanBiodata(string nama, int umur)
{
    Console.WriteLine($"[PARAM] Nama: {nama}, Umur: {umur} tahun.");
}

int Tambah(int a, int b)
{
    return a + b;
}

double LuasPersegiPanjang(double panjang, double lebar)
{
    return panjang * lebar;
}

string TentukanGrade(int skor)
{
    if (skor >= 90)
    {
        return "A";
    }
    else if (skor >= 80)
    {
        return "B";
    }
    else if (skor >= 70)
    {
        return "C";
    }
    return "D";
}

void SapaDefault(string nama = "Tamu")
{
    Console.WriteLine($"[DEFAULT] Halo, {nama}!");
}

double HitungVolume(double panjang, double lebar, double tinggi)
{
    return panjang * lebar * tinggi;
}

double TambahDesimal(double a, double b)
{
    return a + b;
}

int TambahTiga(int a, int b, int c)
{
    return a + b + c;
}

int JumlahkanSemua(params int[] angka)
{
    int total = 0;
    foreach (int n in angka)
    {
        total += n;
    }
    return total;
}

int Kuadrat(int x) => x * x;

string SapaSingkat(string nama) => $"Halo, {nama}!";

double HitungRataRata(int[] data)
{
    int jumlah = 0;
    foreach (int n in data)
    {
        jumlah += n;
    }
    return (double)jumlah / data.Length;
}

int CariTerbesar(int[] data)
{
    int terbesar = data[0];
    for (int i = 1; i < data.Length; i++)
    {
        if (data[i] > terbesar)
        {
            terbesar = data[i];
        }
    }
    return terbesar;
}

int Faktorial(int n)
{
    if (n <= 1)
    {
        return 1;
    }
    return n * Faktorial(n - 1);
}

void TampilkanStruk(string[] namaBarang, int[] hargaBarang)
{
    for (int i = 0; i < namaBarang.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {namaBarang[i]} - Rp{hargaBarang[i]}");
    }
}

int HitungTotal(int[] hargaBarang)
{
    int total = 0;
    foreach (int h in hargaBarang)
    {
        total += h;
    }
    return total;
}

int HitungDiskon(int total)
{
    if (total >= 25000)
    {
        return total * 10 / 100;
    }
    return 0;
}
