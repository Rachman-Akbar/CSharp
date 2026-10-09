// Percabangan

Console.WriteLine("=== Belajar C#: Percabangan If-Else dan Switch ===");
Console.WriteLine();

// --- 0a. Operator Perbandingan : hasilnya selalu bool (true / false) ---
int a = 10; // Nilai pembanding pertama
int b = 3;  // Nilai pembanding kedua
Console.WriteLine($"a = {a}, b = {b}");       // Tampilkan nilai awal
Console.WriteLine($"a == b : {a == b}");      // == sama dengan? 10 == 3 -> False
Console.WriteLine($"a != b : {a != b}");      // != tidak sama dengan? 10 != 3 -> True
Console.WriteLine($"a > b  : {a > b}");       // > lebih besar? 10 > 3 -> True
Console.WriteLine($"a < b  : {a < b}");       // < lebih kecil? 10 < 3 -> False
Console.WriteLine($"a >= 10: {a >= 10}");     // >= lebih besar atau sama dengan? -> True
Console.WriteLine($"b <= 3 : {b <= 3}");      // <= lebih kecil atau sama dengan? -> True

// --- 0b. Operator Logika : menggabungkan beberapa kondisi ---
bool sudahMakan = true;   // Kondisi pertama
bool adaUang = false;     // Kondisi kedua
Console.WriteLine($"sudahMakan && adaUang : {sudahMakan && adaUang}"); // AND: dua-duanya harus true
Console.WriteLine($"sudahMakan || adaUang : {sudahMakan || adaUang}"); // OR : salah satu true saja cukup
Console.WriteLine($"!sudahMakan           : {!sudahMakan}");           // NOT: membalik true<->false

Console.WriteLine();

// ============================================================================
// BAGIAN 1 : IF TUNGGAL (satu pilihan, tanpa alternatif)
// Pola: if (kondisi) { ... } -> blok jalan HANYA jika kondisi true.
// ============================================================================
int nilai = 85; // Nilai ujian siswa
Console.WriteLine($"[IF] nilai = {nilai}"); // Tampilkan nilai yang diuji

// --- 1a. IF sederhana : cek apakah lulus (nilai >= 75) ---
if (nilai >= 75) // Kondisi: apakah nilai 85 >= 75? Ya (true), maka blok di bawah dijalankan
{
    Console.WriteLine("[IF] Selamat, kamu LULUS!"); // Baris ini HANYA tampil jika kondisi true
}

// --- 1b. IF yang kondisinya false : blok dilewati (tidak error, hanya loncat) ---
int umur = 15; // Umur anak
if (umur >= 17) // Kondisi: 15 >= 17? Tidak (false), maka blok dilewati
{
    Console.WriteLine("[IF] Kamu boleh buat KTP."); // Baris ini TIDAK tampil
}
Console.WriteLine("[IF] Program lanjut terus walau IF di atas dilewati."); // Bukti program tidak berhenti

Console.WriteLine();

// ============================================================================
// BAGIAN 2 : IF-ELSE (dua pilihan : jika ya lakukan A, jika tidak lakukan B)
// Pola: if (kondisi) {...} else {...} -> salah satu PASTI jalan.
// ============================================================================
int angka = 7; // Angka yang mau dicek ganjil / genap
Console.WriteLine($"[IF-ELSE] angka = {angka}"); // Tampilkan angka

// --- 2a. IF-ELSE ganjil genap : % 2 == 0 artinya habis dibagi 2 (genap) ---
if (angka % 2 == 0) // Kondisi: sisa bagi 7:2 = 1, jadi 1 == 0? False
{
    Console.WriteLine("[IF-ELSE] Angka GENAP."); // Jalur jika kondisi true (tidak dijalankan)
}
else // Jalur cadangan jika kondisi di atas false
{
    Console.WriteLine("[IF-ELSE] Angka GANJIL."); // Jalur ini yang dijalankan
}

// --- 2b. IF-ELSE dengan operator logika (&&) : dua syarat sekaligus ---
int uang = 50000;         // Uang yang dimiliki
bool tokoBuka = true;     // Status toko
if (uang >= 20000 && tokoBuka) // Syarat 1: uang cukup DAN syarat 2: toko buka. Keduanya true -> masuk IF
{
    Console.WriteLine("[IF-ELSE] Kamu bisa jajan!"); // Dijalankan karena true && true = true
}
else // Dijalankan jika salah satu / keduanya false
{
    Console.WriteLine("[IF-ELSE] Jajan batal."); // Tidak dijalankan
}

Console.WriteLine();

// ============================================================================
// BAGIAN 3 : IF-ELSE IF-ELSE (pilihan bertingkat / berjenjang)
// Pola: cek dari atas ke bawah, yang pertama true langsung menang, sisanya dilewati.
// Cocok untuk: grade nilai, kategori umur, level, rentang angka.
// ============================================================================
int skor = 82; // Skor ujian 0-100 yang mau dikonversi ke grade
Console.WriteLine($"[IF-ELSE IF] skor = {skor}"); // Tampilkan skor

// --- 3a. Urutan PENTING: dari yang paling ketat / terbesar dulu ---
if (skor >= 90) // Cek 1: apakah 82 >= 90? False -> lanjut ke cek berikutnya
{
    Console.WriteLine("[IF-ELSE IF] Grade A"); // Tidak dijalankan
}
else if (skor >= 80) // Cek 2: apakah 82 >= 80? True -> MENANG, blok ini jalan, sisanya dilewati
{
    Console.WriteLine("[IF-ELSE IF] Grade B"); // <-- INI yang tampil
}
else if (skor >= 70) // Cek 3: dilewati karena sudah ada yang menang di atas
{
    Console.WriteLine("[IF-ELSE IF] Grade C");
}
else if (skor >= 60) // Cek 4: dilewati juga
{
    Console.WriteLine("[IF-ELSE IF] Grade D");
}
else // Cadangan terakhir: jika SEMUA cek di atas false (skor < 60)
{
    Console.WriteLine("[IF-ELSE IF] Grade E (Tidak Lulus)");
}

// --- 3b. Contoh kedua dengan OR (||) : kategori anak / dewasa ---
int umurOrang = 10; // Umur yang dicek
if (umurOrang < 5 || umurOrang > 100) // Kondisi: balita ekstrem ATAU terlalu tua (data tidak wajar)
{
    Console.WriteLine("[IF-ELSE IF] Umur tidak wajar."); // 10 tidak masuk sini
}
else if (umurOrang < 13) // Kondisi: di bawah 13 tahun -> anak-anak. 10 < 13 = True -> menang
{
    Console.WriteLine("[IF-ELSE IF] Kategori: Anak-anak."); // <-- INI yang tampil
}
else if (umurOrang < 20) // Remaja 13-19 (dilewati karena sudah menang)
{
    Console.WriteLine("[IF-ELSE IF] Kategori: Remaja.");
}
else // Dewasa 20+ (dilewati)
{
    Console.WriteLine("[IF-ELSE IF] Kategori: Dewasa.");
}

// --- 3c. IF bersarang (Nested IF) : IF di dalam IF untuk 2 tahap pengecekan ---
string username = "admin"; // Input username
string password = "1234";  // Input password
if (username == "admin") // Tahap 1: cek username dulu. "admin" == "admin" -> True, masuk ke dalam
{
    // Baris ini baru dicapai jika username benar
    if (password == "1234") // Tahap 2: cek password. "1234" == "1234" -> True
    {
        Console.WriteLine("[NESTED IF] Login berhasil!"); // Keduanya benar -> sukses
    }
    else // Username benar TAPI password salah
    {
        Console.WriteLine("[NESTED IF] Password salah!"); // Tidak dijalankan
    }
}
else // Username saja sudah salah, tidak perlu cek password
{
    Console.WriteLine("[NESTED IF] Username tidak dikenal!"); // Tidak dijalankan
}

Console.WriteLine();

// ============================================================================
// BAGIAN 4 : SWITCH (pilihan cepat berdasarkan SATU nilai pasti)
// Pola: switch (variabel) { case nilai1: ... break; case nilai2: ... break; default: ... }
// Cocok untuk: kode hari, menu angka, huruf grade, nama bulan. BUKAN untuk rentang (>=).
// Setiap 'case' WAJIB diakhiri 'break;' agar tidak nyelonong ke case bawahnya.
// ============================================================================
int kodeHari = 3; // Kode hari 1-7 (1=Senin, 2=Selasa, 3=Rabu, dst.)
Console.WriteLine($"[SWITCH] kodeHari = {kodeHari}"); // Tampilkan kode

// --- 4a. SWITCH angka : menentukan nama hari ---
switch (kodeHari) // Nilai yang dicek: kodeHari (isinya 3)
{
    case 1: // Jika kodeHari == 1
        Console.WriteLine("[SWITCH] Hari Senin"); // Tidak dijalankan
        break; // Wajib! Keluar dari switch setelah case cocok
    case 2: // Jika kodeHari == 2
        Console.WriteLine("[SWITCH] Hari Selasa"); // Tidak dijalankan
        break; // Keluar dari switch
    case 3: // Jika kodeHari == 3 -> COCOK!
        Console.WriteLine("[SWITCH] Hari Rabu"); // <-- INI yang tampil
        break; // Keluar, case 4-7 dilewati
    case 4: // Jika kodeHari == 4
        Console.WriteLine("[SWITCH] Hari Kamis");
        break;
    case 5: // Jika kodeHari == 5
        Console.WriteLine("[SWITCH] Hari Jumat");
        break;
    default: // Cadangan: jika TIDAK ADA case yang cocok (misal kode 8, 0, -1)
        Console.WriteLine("[SWITCH] Kode hari tidak dikenal (Sabtu/Minggu/bukan 1-5)");
        break; // Break terakhir opsional tapi disarankan tetap ditulis
}

// --- 4b. SWITCH string : menentukan sapaan berdasarkan peran ---
string peran = "guru"; // Peran pengguna: bisa "murid", "guru", "admin"
Console.WriteLine($"[SWITCH] peran = {peran}"); // Tampilkan peran
switch (peran) // SWITCH juga bisa mengecek string, bukan cuma angka
{
    case "murid": // Jika peran == "murid" (huruf kecil-besar harus pas!)
        Console.WriteLine("[SWITCH] Halo Murid, selamat belajar!"); // Tidak dijalankan
        break; // Keluar
    case "guru": // Jika peran == "guru" -> COCOK!
        Console.WriteLine("[SWITCH] Halo Guru, selamat mengajar!"); // <-- INI yang tampil
        break; // Keluar
    case "admin": // Jika peran == "admin"
        Console.WriteLine("[SWITCH] Halo Admin, jaga sistem baik-baik!");
        break;
    default: // Jika peran tidak dikenal, misal "tamu"
        Console.WriteLine("[SWITCH] Peran tidak dikenal.");
        break;
}

// --- 4c. SWITCH dengan beberapa case digabung (OR manual) : nama bulan ---
int kodeBulan = 2; // Kode bulan 1-12
Console.WriteLine($"[SWITCH] kodeBulan = {kodeBulan}"); // Tampilkan kode bulan
switch (kodeBulan) // Cek musim berdasarkan bulan (contoh sederhana)
{
    case 12: // Jika bulan 12 ...
    case 1:  // ATAU bulan 1 ...
    case 2:  // ATAU bulan 2 -> COCOK (kodeBulan = 2), langsung loncat ke bawah
        Console.WriteLine("[SWITCH] Musim Hujan"); // <-- INI yang tampil (3 case berbagi 1 aksi)
        break; // Keluar setelah aksi gabungan selesai
    case 6: // Bulan 6 ...
    case 7: // ATAU bulan 7 ...
    case 8: // ATAU bulan 8 ...
        Console.WriteLine("[SWITCH] Musim Kemarau"); // Tidak dijalankan
        break;
    default: // Bulan lainnya (3,4,5,9,10,11) = pancaroba
        Console.WriteLine("[SWITCH] Musim Pancaroba");
        break;
}

Console.WriteLine();

// ============================================================================
// BAGIAN 5 : SWITCH EXPRESSION (cara modern, C# 8+, lebih ringkas)
// Pola: variabelHasil = nilai switch { pola1 => hasil1, pola2 => hasil2, _ => hasilDefault }
// Tanda '_' artinya default (jika tidak ada pola yang cocok). Tanpa break!
// ============================================================================
char grade = 'B'; // Grade yang mau diubah jadi keterangan
Console.WriteLine($"[SWITCH EXPR] grade = {grade}"); // Tampilkan grade

// --- 5a. SWITCH EXPRESSION : grade huruf -> pujian ---
string pujian = grade switch // Cek nilai 'grade', hasilnya disimpan ke 'pujian'
{
    'A' => "Sempurna!",       // Jika grade == 'A', pujian = "Sempurna!"
    'B' => "Bagus Sekali!",   // Jika grade == 'B', pujian = "Bagus Sekali!" -> COCOK
    'C' => "Cukup, tingkatkan!", // Jika grade == 'C' ...
    'D' => "Kurang, belajar lagi!", // Jika grade == 'D' ...
    _ => "Tidak valid!"       // _ = default: jika grade selain A-D
};
Console.WriteLine($"[SWITCH EXPR] {grade} = {pujian}"); // Tampilkan hasil: B = Bagus Sekali!

// --- 5b. Ternary / Operator Kondisional (? :) : IF-ELSE satu baris ---
// Pola: hasil = (kondisi) ? nilaiJikaTrue : nilaiJikaFalse
int suhu = 38; // Suhu badan dalam Celcius
string kondisiBadan = (suhu >= 37) ? "Demam" : "Sehat"; // Jika suhu>=37 -> "Demam", selain itu -> "Sehat"
Console.WriteLine($"[TERNARY] suhu = {suhu}C -> {kondisiBadan}"); // 38 >= 37 = True -> "Demam"

Console.WriteLine();

// ============================================================================
// BAGIAN 6 : LATIHAN MINI (menggabungkan IF-ELSE IF + SWITCH)
// Studi kasus: program kasir sederhana -> hitung diskon + sapa member.
// ============================================================================
int totalBelanja = 150000;       // Input: total belanja pelanggan dalam rupiah
string tipeMember = "gold";      // Input: tipe member ("gold", "silver", atau lainnya)
Console.WriteLine("--- Latihan: Kasir + Diskon Member ---"); // Judul latihan
Console.WriteLine($"Total belanja : Rp{totalBelanja}");      // Tampilkan total awal
Console.WriteLine($"Tipe member   : {tipeMember}");           // Tampilkan tipe member

// --- 6a. Hitung persen diskon dengan IF-ELSE IF (berdasarkan total belanja) ---
int persenDiskon; // Variabel penampung, diisi di bawah tergantung kondisi
if (totalBelanja >= 200000) // Belanja >= 200rb -> diskon terbesar 20%
{
    persenDiskon = 20; // Tidak dijalankan (150rb < 200rb)
}
else if (totalBelanja >= 100000) // Belanja >= 100rb -> diskon 10%. 150rb memenuhi -> MENANG
{
    persenDiskon = 10; // <-- persenDiskon = 10
}
else // Belanja < 100rb -> tidak ada diskon
{
    persenDiskon = 0; // Tidak dijalankan
}
Console.WriteLine($"Diskon belanja: {persenDiskon}%"); // Tampilkan persen diskon belanja: 10%

// --- 6b. Tambahan sapaan & bonus dengan SWITCH (berdasarkan tipe member) ---
int bonusMember = 0; // Bonus tambahan khusus member, awalnya 0
switch (tipeMember) // Cek tipe member
{
    case "gold": // Jika member gold -> dapat bonus 5% + sapaan VIP
        Console.WriteLine("[KASIR] Terima kasih member GOLD!"); // Sapaan khusus gold
        bonusMember = 5; // Bonus 5%
        break; // Keluar
    case "silver": // Jika member silver -> dapat bonus 3%
        Console.WriteLine("[KASIR] Terima kasih member SILVER!");
        bonusMember = 3;
        break;
    default: // Bukan member -> tidak ada bonus
        Console.WriteLine("[KASIR] Terima kasih sudah berbelanja!");
        bonusMember = 0;
        break;
}
Console.WriteLine($"Bonus member  : {bonusMember}%"); // Tampilkan bonus: 5%

// --- 6c. Hitung total akhir : potong diskon belanja + bonus member sekaligus ---
int totalDiskon = persenDiskon + bonusMember; // Total potongan = 10 + 5 = 15%
int bayarAkhir = totalBelanja - (totalBelanja * totalDiskon / 100); // Rumus: total - (total * % / 100)
Console.WriteLine($"Total diskon  : {totalDiskon}%"); // Tampilkan total diskon gabungan: 15%
Console.WriteLine($"Harus bayar   : Rp{bayarAkhir}"); // Tampilkan yang harus dibayar: 150000 - 22500 = 127500

Console.WriteLine();
Console.WriteLine("=== Selesai. Coba ubah totalBelanja & tipeMember lalu jalankan ulang! ===");
