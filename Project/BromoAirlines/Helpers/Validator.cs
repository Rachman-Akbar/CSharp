using System.Text.RegularExpressions;

namespace BromoAirlines.Helpers;

/// <summary>Kumpulan validasi sesuai PDF + prompt G–M.</summary>
public static class Validator
{
    public static bool IsUsernameValid(string u, out string err)
    {
        err = "";
        if (string.IsNullOrWhiteSpace(u)) { err = "Username wajib diisi."; return false; }
        return true;
    }

    public static bool IsPasswordValid(string p, out string err)
    {
        err = "";
        if (string.IsNullOrEmpty(p) || p.Length < 8) { err = "Password minimal 8 karakter."; return false; }
        return true;
    }

    public static bool IsTeleponValid(string t, out string err)
    {
        err = "";
        if (string.IsNullOrWhiteSpace(t) || !Regex.IsMatch(t, @"^\d{10,15}$"))
        { err = "Nomor telepon harus angka 10-15 digit."; return false; }
        return true;
    }

    public static bool IsIataValid(string kode, out string err)
    {
        err = "";
        if (string.IsNullOrWhiteSpace(kode) || !Regex.IsMatch(kode.Trim(), @"^[A-Za-z]{3}$"))
        { err = "Kode IATA harus tepat 3 huruf."; return false; }
        return true;
    }

    public static bool IsKodePenerbanganValid(string kode, out string err)
    {
        err = "";
        if (string.IsNullOrWhiteSpace(kode) || !Regex.IsMatch(kode.Trim(), @"^[A-Za-z]{2}-\d{4}$"))
        { err = "Kode penerbangan format AA-1234 (2 huruf-strip-4 angka)."; return false; }
        return true;
    }

    public static bool IsKodePromoValid(string kode, out string err)
    {
        err = "";
        if (string.IsNullOrWhiteSpace(kode)) { err = "Kode promo wajib diisi."; return false; }
        if (kode != kode.ToUpperInvariant()) { err = "Kode promo harus huruf kapital semua."; return false; }
        return true;
    }

    public static bool IsMinimal1(decimal v, string nama, out string err)
    {
        err = "";
        if (v < 1) { err = $"{nama} minimal 1."; return false; }
        return true;
    }

    public static bool IsMinimal1(int v, string nama, out string err)
    {
        err = "";
        if (v < 1) { err = $"{nama} minimal 1."; return false; }
        return true;
    }

    public static bool IsTanggalLahirValid(DateTime tgl, out string err)
    {
        err = "";
        if (tgl.Date > DateTime.Today) { err = "Tanggal lahir tidak boleh di masa depan."; return false; }
        return true;
    }

    /// <summary>Aturan bisnis: berangkat + durasi tidak boleh lewat tengah malam (hari yang sama).</summary>
    public static bool IsTidakLewatTengahMalam(DateTime berangkat, int durasiMenit, out string err)
    {
        err = "";
        if (berangkat.AddMinutes(durasiMenit).Date != berangkat.Date)
        { err = "Keberangkatan + durasi tidak boleh melewati tengah malam."; return false; }
        return true;
    }
}
