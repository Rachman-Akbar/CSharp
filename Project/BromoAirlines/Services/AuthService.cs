using BromoAirlines.Data.Repositories;
using BromoAirlines.Helpers;
using BromoAirlines.Models;

namespace BromoAirlines.Services;

/// <summary>Auth: login + register customer, BCrypt, tanpa password di Session (aturan G/H/S).</summary>
public sealed class AuthService
{
    private readonly AkunRepository _repo = new();

    public async Task<(Akun? akun, string error)> LoginAsync(string username, string password)
    {
        if (!Validator.IsUsernameValid(username, out var e1)) return (null, e1);
        if (string.IsNullOrWhiteSpace(password)) return (null, "Password wajib diisi.");
        var a = await _repo.GetByUsernameAsync(username.Trim());
        if (a is null) return (null, "Username atau password salah.");
        bool ok;
        try { ok = BCrypt.Net.BCrypt.Verify(password, a.Password); }
        catch { ok = a.Password == password; } // kompat akun lama plaintext (jangan rusak kompatibilitas)
        if (!ok) return (null, "Username atau password salah.");
        Session.Set(a);
        return (Session.Current, "");
    }

    public async Task<(Akun? akun, string error)> RegisterCustomerAsync(string username, string password, string nama, DateTime tglLahir, string telp)
    {
        if (!Validator.IsUsernameValid(username, out var e1)) return (null, e1);
        if (!Validator.IsPasswordValid(password, out var e2)) return (null, e2);
        if (string.IsNullOrWhiteSpace(nama)) return (null, "Nama wajib diisi.");
        if (!Validator.IsTanggalLahirValid(tglLahir, out var e3)) return (null, e3);
        if (!Validator.IsTeleponValid(telp.Trim(), out var e4)) return (null, e4);
        username = username.Trim();
        if (await _repo.UsernameExistsAsync(username)) return (null, "Username sudah dipakai.");
        var hash = BCrypt.Net.BCrypt.HashPassword(password);
        var id = await _repo.InsertCustomerAsync(new Akun
        {
            Username = username, Password = hash, Nama = nama.Trim(),
            TanggalLahir = tglLahir.Date, NomorTelepon = telp.Trim()
        });
        var a = await _repo.GetByUsernameAsync(username);
        if (a is null) return (null, "Pendaftaran gagal, coba lagi.");
        Session.Set(a);
        return (Session.Current, "");
    }
}
