Console.WriteLine("=== Belajar C#: Percabangan If-Else dan Switch ===");
Console.WriteLine();

int a = 10;
int b = 3;
Console.WriteLine($"a = {a}, b = {b}");
Console.WriteLine($"a == b : {a == b}");
Console.WriteLine($"a != b : {a != b}");
Console.WriteLine($"a > b  : {a > b}");
Console.WriteLine($"a < b  : {a < b}");
Console.WriteLine($"a >= 10: {a >= 10}");
Console.WriteLine($"b <= 3 : {b <= 3}");

bool sudahMakan = true;
bool adaUang = false;
Console.WriteLine($"sudahMakan && adaUang : {sudahMakan && adaUang}");
Console.WriteLine($"sudahMakan || adaUang : {sudahMakan || adaUang}");
Console.WriteLine($"!sudahMakan           : {!sudahMakan}");

Console.WriteLine();

int nilai = 85;
Console.WriteLine($"[IF] nilai = {nilai}");

if (nilai >= 75)
{
    Console.WriteLine("[IF] Selamat, kamu LULUS!");
}

int umur = 15;
if (umur >= 17)
{
    Console.WriteLine("[IF] Kamu boleh buat KTP.");
}
Console.WriteLine("[IF] Program lanjut terus walau IF di atas dilewati.");

Console.WriteLine();

int angka = 7;
Console.WriteLine($"[IF-ELSE] angka = {angka}");

if (angka % 2 == 0)
{
    Console.WriteLine("[IF-ELSE] Angka GENAP.");
}
else
{
    Console.WriteLine("[IF-ELSE] Angka GANJIL.");
}

int uang = 50000;
bool tokoBuka = true;
if (uang >= 20000 && tokoBuka)
{
    Console.WriteLine("[IF-ELSE] Kamu bisa jajan!");
}
else
{
    Console.WriteLine("[IF-ELSE] Jajan batal.");
}

Console.WriteLine();

int skor = 82;
Console.WriteLine($"[IF-ELSE IF] skor = {skor}");

if (skor >= 90)
{
    Console.WriteLine("[IF-ELSE IF] Grade A");
}
else if (skor >= 80)
{
    Console.WriteLine("[IF-ELSE IF] Grade B");
}
else if (skor >= 70)
{
    Console.WriteLine("[IF-ELSE IF] Grade C");
}
else if (skor >= 60)
{
    Console.WriteLine("[IF-ELSE IF] Grade D");
}
else
{
    Console.WriteLine("[IF-ELSE IF] Grade E (Tidak Lulus)");
}

int umurOrang = 10;
if (umurOrang < 5 || umurOrang > 100)
{
    Console.WriteLine("[IF-ELSE IF] Umur tidak wajar.");
}
else if (umurOrang < 13)
{
    Console.WriteLine("[IF-ELSE IF] Kategori: Anak-anak.");
}
else if (umurOrang < 20)
{
    Console.WriteLine("[IF-ELSE IF] Kategori: Remaja.");
}
else
{
    Console.WriteLine("[IF-ELSE IF] Kategori: Dewasa.");
}

string username = "admin";
string password = "1234";
if (username == "admin")
{

    if (password == "1234")
    {
        Console.WriteLine("[NESTED IF] Login berhasil!");
    }
    else
    {
        Console.WriteLine("[NESTED IF] Password salah!");
    }
}
else
{
    Console.WriteLine("[NESTED IF] Username tidak dikenal!");
}

Console.WriteLine();

int kodeHari = 3;
Console.WriteLine($"[SWITCH] kodeHari = {kodeHari}");

switch (kodeHari)
{
    case 1:
        Console.WriteLine("[SWITCH] Hari Senin");
        break;
    case 2:
        Console.WriteLine("[SWITCH] Hari Selasa");
        break;
    case 3:
        Console.WriteLine("[SWITCH] Hari Rabu");
        break;
    case 4:
        Console.WriteLine("[SWITCH] Hari Kamis");
        break;
    case 5:
        Console.WriteLine("[SWITCH] Hari Jumat");
        break;
    default:
        Console.WriteLine("[SWITCH] Kode hari tidak dikenal (Sabtu/Minggu/bukan 1-5)");
        break;
}

string peran = "guru";
Console.WriteLine($"[SWITCH] peran = {peran}");
switch (peran)
{
    case "murid":
        Console.WriteLine("[SWITCH] Halo Murid, selamat belajar!");
        break;
    case "guru":
        Console.WriteLine("[SWITCH] Halo Guru, selamat mengajar!");
        break;
    case "admin":
        Console.WriteLine("[SWITCH] Halo Admin, jaga sistem baik-baik!");
        break;
    default:
        Console.WriteLine("[SWITCH] Peran tidak dikenal.");
        break;
}

int kodeBulan = 2;
Console.WriteLine($"[SWITCH] kodeBulan = {kodeBulan}");
switch (kodeBulan)
{
    case 12:
    case 1:
    case 2:
        Console.WriteLine("[SWITCH] Musim Hujan");
        break;
    case 6:
    case 7:
    case 8:
        Console.WriteLine("[SWITCH] Musim Kemarau");
        break;
    default:
        Console.WriteLine("[SWITCH] Musim Pancaroba");
        break;
}

Console.WriteLine();

char grade = 'B';
Console.WriteLine($"[SWITCH EXPR] grade = {grade}");

string pujian = grade switch
{
    'A' => "Sempurna!",
    'B' => "Bagus Sekali!",
    'C' => "Cukup, tingkatkan!",
    'D' => "Kurang, belajar lagi!",
    _ => "Tidak valid!"
};
Console.WriteLine($"[SWITCH EXPR] {grade} = {pujian}");

int suhu = 38;
string kondisiBadan = (suhu >= 37) ? "Demam" : "Sehat";
Console.WriteLine($"[TERNARY] suhu = {suhu}C -> {kondisiBadan}");

Console.WriteLine();

int totalBelanja = 150000;
string tipeMember = "gold";
Console.WriteLine("--- Latihan: Kasir + Diskon Member ---");
Console.WriteLine($"Total belanja : Rp{totalBelanja}");
Console.WriteLine($"Tipe member   : {tipeMember}");

int persenDiskon;
if (totalBelanja >= 200000)
{
    persenDiskon = 20;
}
else if (totalBelanja >= 100000)
{
    persenDiskon = 10;
}
else
{
    persenDiskon = 0;
}
Console.WriteLine($"Diskon belanja: {persenDiskon}%");

int bonusMember = 0;
switch (tipeMember)
{
    case "gold":
        Console.WriteLine("[KASIR] Terima kasih member GOLD!");
        bonusMember = 5;
        break;
    case "silver":
        Console.WriteLine("[KASIR] Terima kasih member SILVER!");
        bonusMember = 3;
        break;
    default:
        Console.WriteLine("[KASIR] Terima kasih sudah berbelanja!");
        bonusMember = 0;
        break;
}
Console.WriteLine($"Bonus member  : {bonusMember}%");

int totalDiskon = persenDiskon + bonusMember;
int bayarAkhir = totalBelanja - (totalBelanja * totalDiskon / 100);
Console.WriteLine($"Total diskon  : {totalDiskon}%");
Console.WriteLine($"Harus bayar   : Rp{bayarAkhir}");

Console.WriteLine();
Console.WriteLine("=== Selesai. Coba ubah totalBelanja & tipeMember lalu jalankan ulang! ===");
