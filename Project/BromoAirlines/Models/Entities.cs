namespace BromoAirlines.Models;

public sealed class Akun
{
    public int ID { get; set; }
    public string Username { get; set; } = "";
    public string Password { get; set; } = ""; // hash BCrypt, jangan taruh di Session
    public string Nama { get; set; } = "";
    public DateTime TanggalLahir { get; set; }
    public string NomorTelepon { get; set; } = "";
    public bool MerupakanAdmin { get; set; }
}

public sealed class Negara
{
    public int ID { get; set; }
    public string Nama { get; set; } = "";
    public string IbukotaNegara { get; set; } = "";
}

public sealed class Bandara
{
    public int ID { get; set; }
    public string Nama { get; set; } = "";
    public string KodeIATA { get; set; } = "";
    public string Kota { get; set; } = "";
    public int NegaraID { get; set; }
    public string? NamaNegara { get; set; } // join, bukan kolom
    public int JumlahTerminal { get; set; }
    public string Alamat { get; set; } = "";
}

public sealed class Maskapai
{
    public int ID { get; set; }
    public string Nama { get; set; } = "";
    public string Perusahaan { get; set; } = "";
    public int JumlahKru { get; set; }
    public string Deskripsi { get; set; } = "";
}

public sealed class StatusPenerbangan
{
    public int ID { get; set; }
    public string Nama { get; set; } = "";
}

public sealed class KodePromo
{
    public int ID { get; set; }
    public string Kode { get; set; } = "";
    public decimal PersentaseDiskon { get; set; }
    public decimal MaksimumDiskon { get; set; }
    public DateTime BerlakuSampai { get; set; }
    public string Deskripsi { get; set; } = "";
}

public sealed class JadwalPenerbangan
{
    public int ID { get; set; }
    public string KodePenerbangan { get; set; } = "";
    public int BandaraKeberangkatanID { get; set; }
    public int BandaraTujuanID { get; set; }
    public int MaskapaiID { get; set; }
    public DateTime TanggalWaktuKeberangkatan { get; set; }
    public int DurasiPenerbangan { get; set; } // menit, sesuai DB INT
    public decimal HargaPerTiket { get; set; }
    // Kolom join untuk tampil (bukan kolom DB):
    public string? NamaBerangkat { get; set; }
    public string? NamaTujuan { get; set; }
    public string? NamaMaskapai { get; set; }
}

public sealed class PerubahanStatus
{
    public int ID { get; set; }
    public int JadwalPenerbanganID { get; set; }
    public int StatusPenerbanganID { get; set; }
    public string? NamaStatus { get; set; }
    public DateTime WaktuPerubahanTerjadi { get; set; }
    public int? PerkiraanWaktuDelay { get; set; } // menit, NULL jika bukan Delay
}

public sealed class TransaksiHeader
{
    public int ID { get; set; }
    public int AkunID { get; set; }
    public DateTime TanggalTransaksi { get; set; }
    public int JadwalPenerbanganID { get; set; }
    public int JumlahPenumpang { get; set; }
    public decimal TotalHarga { get; set; } // total bayar setelah diskon
    public int? KodePromoID { get; set; }
}

public sealed class TransaksiDetail
{
    public int ID { get; set; }
    public int TransaksiHeaderID { get; set; }
    public string TitelPenumpang { get; set; } = "";
    public string NamaLengkapPenumpang { get; set; } = "";
}
