using BromoAirlines.Data;
using BromoAirlines.Models;
using MySqlConnector;

namespace BromoAirlines.Data.Repositories;

/// <summary>Repository Akun: semua query parameterized (aturan S).</summary>
public sealed class AkunRepository
{
    public async Task<Akun?> GetByUsernameAsync(string username)
    {
        await using var conn = Database.CreateConnection();
        await conn.OpenAsync();
        await using var cmd = new MySqlCommand(
            "SELECT ID,Username,`Password`,Nama,TanggalLahir,NomorTelepon,MerupakanAdmin FROM Akun WHERE Username=@u LIMIT 1", conn);
        cmd.Parameters.AddWithValue("@u", username);
        await using var r = await cmd.ExecuteReaderAsync();
        if (!await r.ReadAsync()) return null;
        return new Akun
        {
            ID = r.GetInt32(0), Username = r.GetString(1), Password = r.GetString(2),
            Nama = r.GetString(3), TanggalLahir = r.GetDateTime(4),
            NomorTelepon = r.GetString(5), MerupakanAdmin = r.GetBoolean(6)
        };
    }

    public async Task<bool> UsernameExistsAsync(string username, MySqlConnection? ext = null, MySqlTransaction? tx = null)
    {
        var own = ext is null;
        var conn = ext ?? Database.CreateConnection();
        if (own) await conn.OpenAsync();
        await using var cmd = new MySqlCommand("SELECT COUNT(1) FROM Akun WHERE Username=@u", conn);
        if (tx is not null) cmd.Transaction = tx;
        cmd.Parameters.AddWithValue("@u", username);
        var n = Convert.ToInt32(await cmd.ExecuteScalarAsync());
        if (own) await conn.DisposeAsync();
        return n > 0;
    }

    public async Task<int> InsertCustomerAsync(Akun a)
    {
        await using var conn = Database.CreateConnection();
        await conn.OpenAsync();
        await using var cmd = new MySqlCommand(
            "INSERT INTO Akun(Username,`Password`,Nama,TanggalLahir,NomorTelepon,MerupakanAdmin) VALUES(@u,@p,@n,@t,@tel,0); SELECT LAST_INSERT_ID();", conn);
        cmd.Parameters.AddWithValue("@u", a.Username);
        cmd.Parameters.AddWithValue("@p", a.Password);
        cmd.Parameters.AddWithValue("@n", a.Nama);
        cmd.Parameters.AddWithValue("@t", a.TanggalLahir.Date);
        cmd.Parameters.AddWithValue("@tel", a.NomorTelepon);
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
}
