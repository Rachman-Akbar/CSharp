using BromoAirlines.Data;
using BromoAirlines.Models;
using MySqlConnector;

namespace BromoAirlines.Data.Repositories;

public sealed class NegaraRepository
{
    public async Task<List<Negara>> GetAllAsync()
    {
        var r = new List<Negara>();
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand("SELECT ID,Nama,IbukotaNegara FROM Negara ORDER BY Nama", c);
        await using var rd = await cmd.ExecuteReaderAsync();
        while (await rd.ReadAsync())
            r.Add(new Negara { ID = rd.GetInt32(0), Nama = rd.GetString(1), IbukotaNegara = rd.GetString(2) });
        return r;
    }
}

public sealed class MaskapaiRepository
{
    public async Task<List<Maskapai>> GetAllAsync(string? cari = null)
    {
        var r = new List<Maskapai>();
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand(
            @"SELECT ID,Nama,Perusahaan,JumlahKru,Deskripsi FROM Maskapai
              WHERE (@c IS NULL OR Nama LIKE CONCAT('%',@c,'%') OR Perusahaan LIKE CONCAT('%',@c,'%'))
              ORDER BY Nama ASC", c);
        cmd.Parameters.AddWithValue("@c", string.IsNullOrWhiteSpace(cari) ? (object)DBNull.Value : cari!);
        await using var rd = await cmd.ExecuteReaderAsync();
        while (await rd.ReadAsync())
            r.Add(new Maskapai { ID = rd.GetInt32(0), Nama = rd.GetString(1), Perusahaan = rd.GetString(2), JumlahKru = rd.GetInt32(3), Deskripsi = rd.GetString(4) });
        return r;
    }

    public async Task<int> InsertAsync(Maskapai m)
    {
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand(
            "INSERT INTO Maskapai(Nama,Perusahaan,JumlahKru,Deskripsi) VALUES(@n,@p,@j,@d); SELECT LAST_INSERT_ID();", c);
        cmd.Parameters.AddWithValue("@n", m.Nama);
        cmd.Parameters.AddWithValue("@p", m.Perusahaan);
        cmd.Parameters.AddWithValue("@j", m.JumlahKru);
        cmd.Parameters.AddWithValue("@d", m.Deskripsi);
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }

    public async Task UpdateAsync(Maskapai m)
    {
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand(
            "UPDATE Maskapai SET Nama=@n,Perusahaan=@p,JumlahKru=@j,Deskripsi=@d WHERE ID=@id", c);
        cmd.Parameters.AddWithValue("@n", m.Nama);
        cmd.Parameters.AddWithValue("@p", m.Perusahaan);
        cmd.Parameters.AddWithValue("@j", m.JumlahKru);
        cmd.Parameters.AddWithValue("@d", m.Deskripsi);
        cmd.Parameters.AddWithValue("@id", m.ID);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand("DELETE FROM Maskapai WHERE ID=@id", c);
        cmd.Parameters.AddWithValue("@id", id);
        await cmd.ExecuteNonQueryAsync();
    }
}

public sealed class KodePromoRepository
{
    public async Task<List<KodePromo>> GetAllAsync(string? cari = null)
    {
        var r = new List<KodePromo>();
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand(
            @"SELECT ID,Kode,PersentaseDiskon,MaksimumDiskon,BerlakuSampai,Deskripsi FROM KodePromo
              WHERE (@c IS NULL OR Kode LIKE CONCAT('%',@c,'%')) ORDER BY BerlakuSampai DESC", c);
        cmd.Parameters.AddWithValue("@c", string.IsNullOrWhiteSpace(cari) ? (object)DBNull.Value : cari!);
        await using var rd = await cmd.ExecuteReaderAsync();
        while (await rd.ReadAsync())
            r.Add(new KodePromo { ID = rd.GetInt32(0), Kode = rd.GetString(1), PersentaseDiskon = rd.GetDecimal(2), MaksimumDiskon = rd.GetDecimal(3), BerlakuSampai = rd.GetDateTime(4), Deskripsi = rd.GetString(5) });
        return r;
    }

    public async Task<bool> ExistsKodeAsync(string kode, int kecualiId = 0)
    {
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand("SELECT COUNT(1) FROM KodePromo WHERE Kode=@k AND ID<>@id", c);
        cmd.Parameters.AddWithValue("@k", kode);
        cmd.Parameters.AddWithValue("@id", kecualiId);
        return Convert.ToInt32(await cmd.ExecuteScalarAsync()) > 0;
    }

    public async Task<KodePromo?> GetByKodeAsync(string kode)
    {
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand("SELECT ID,Kode,PersentaseDiskon,MaksimumDiskon,BerlakuSampai,Deskripsi FROM KodePromo WHERE Kode=@k LIMIT 1", c);
        cmd.Parameters.AddWithValue("@k", kode);
        await using var rd = await cmd.ExecuteReaderAsync();
        if (!await rd.ReadAsync()) return null;
        return new KodePromo { ID = rd.GetInt32(0), Kode = rd.GetString(1), PersentaseDiskon = rd.GetDecimal(2), MaksimumDiskon = rd.GetDecimal(3), BerlakuSampai = rd.GetDateTime(4), Deskripsi = rd.GetString(5) };
    }

    public async Task<int> InsertAsync(KodePromo k)
    {
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand(
            "INSERT INTO KodePromo(Kode,PersentaseDiskon,MaksimumDiskon,BerlakuSampai,Deskripsi) VALUES(@k,@p,@m,@b,@d); SELECT LAST_INSERT_ID();", c);
        cmd.Parameters.AddWithValue("@k", k.Kode.ToUpperInvariant());
        cmd.Parameters.AddWithValue("@p", k.PersentaseDiskon);
        cmd.Parameters.AddWithValue("@m", k.MaksimumDiskon);
        cmd.Parameters.AddWithValue("@b", k.BerlakuSampai.Date);
        cmd.Parameters.AddWithValue("@d", k.Deskripsi);
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }

    public async Task UpdateAsync(KodePromo k)
    {
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand(
            "UPDATE KodePromo SET Kode=@k,PersentaseDiskon=@p,MaksimumDiskon=@m,BerlakuSampai=@b,Deskripsi=@d WHERE ID=@id", c);
        cmd.Parameters.AddWithValue("@k", k.Kode.ToUpperInvariant());
        cmd.Parameters.AddWithValue("@p", k.PersentaseDiskon);
        cmd.Parameters.AddWithValue("@m", k.MaksimumDiskon);
        cmd.Parameters.AddWithValue("@b", k.BerlakuSampai.Date);
        cmd.Parameters.AddWithValue("@d", k.Deskripsi);
        cmd.Parameters.AddWithValue("@id", k.ID);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand("DELETE FROM KodePromo WHERE ID=@id", c);
        cmd.Parameters.AddWithValue("@id", id);
        await cmd.ExecuteNonQueryAsync();
    }
}
