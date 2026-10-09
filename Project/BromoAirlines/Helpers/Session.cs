using BromoAirlines.Models;

namespace BromoAirlines.Helpers;

/// <summary>Session login: tanpa password plaintext (aturan G).</summary>
public static class Session
{
    public static Akun? Current { get; private set; }
    public static bool IsLoggedIn => Current is not null;
    public static bool IsAdmin => Current?.MerupakanAdmin == true;

    public static void Set(Akun akun) => Current = new Akun
    {
        ID = akun.ID,
        Username = akun.Username,
        Nama = akun.Nama,
        TanggalLahir = akun.TanggalLahir,
        NomorTelepon = akun.NomorTelepon,
        MerupakanAdmin = akun.MerupakanAdmin
    };

    public static void Clear() => Current = null;
}
