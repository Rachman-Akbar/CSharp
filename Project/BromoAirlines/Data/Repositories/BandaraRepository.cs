using BromoAirlines.Data;
using BromoAirlines.Models;
using MySqlConnector;

namespace BromoAirlines.Data.Repositories;

/// <summary>Repository Bandara: list A-Z, cek duplikat case-insensitive kecuali ID sendiri (aturan J).</summary>
public sealed class BandaraRepository
{
    public async Task<List<Bandara>> GetAllAsync(string? cari = null)
    {
        var list = new List<Bandara>();
        await using var conn = Database.CreateConnection();
        await conn.OpenAsync();
        var sql = @"SELECT b.ID,b.Nama,b.KodeIATA,b.Kota,b.NegaraID,n.Nama,b.JumlahTerminal,b.Alamat
                    FROM Bandara b LEFT JOIN Negara n ON n.ID=b.NegaraID
                    WHERE (@c IS NULL OR b.Nama LIKE CONCAT('%',@c,'%') OR b.KodeIATA LIKE CONCAT('%',@c,'%') OR b.Kota LIKE CONCAT('%',@c,'%'))
                    ORDER BY b.Nama ASC";
        await using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@c", string.IsNullOrWhiteSpace(cari) ? (object)DBNull.Value : cari!);
        await using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync())
            list.Add(new Bandara
            {
                ID = r.GetInt32(0), Nama = r.GetString(1), KodeIATA = r.GetString(2),
                Kota = r.GetString(3), NegaraID = r.GetInt32(4),
                NamaNegara = r.IsDBNull(5) ? null : r.GetString(5),
                JumlahTerminal = r.GetInt32(6), Alamat = r.GetString(7)
            });
        return list;
    }

    public async Task<bool> ExistsNamaAsync(string nama, int kecualiId = 0)
    {
        await using var conn = Database.CreateConnection();
        await conn.OpenAsync();
        await using var cmd = new MySqlCommand(
            "SELECT COUNT(1) FROM Bandara WHERE LOWER(Nama)=LOWER(@n) AND ID<>@id", conn);
        cmd.Parameters.AddWithValue("@n", nama);
        cmd.Parameters.AddWithValue("@id", kecualiId);
        return Convert.ToInt32(await cmd.ExecuteScalarAsync()) > 0;
    }

    public async Task<bool> ExistsIataAsync(string iata, int kecualiId = 0)
    {
        await using var conn = Database.CreateConnection();
        await conn.OpenAsync();
        await using var cmd = new MySqlCommand(
            "SELECT COUNT(1) FROM Bandara WHERE UPPER(KodeIATA)=UPPER(@k) AND ID<>@id", conn);
        cmd.Parameters.AddWithValue("@k", iata);
        cmd.Parameters.AddWithValue("@id", kecualiId);
        return Convert.ToInt32(await cmd.ExecuteScalarAsync()) > 0;
    }

    public async Task<int> InsertAsync(Bandara b)
    {
        await using var conn = Database.CreateConnection();
        await conn.OpenAsync();
        await using var cmd = new MySqlCommand(
            "INSERT INTO Bandara(Nama,KodeIATA,Kota,NegaraID,JumlahTerminal,Alamat) VALUES(@n,@i,@k,@g,@j,@a); SELECT LAST_INSERT_ID();", conn);
        cmd.Parameters.AddWithValue("@n", b.Nama);
        cmd.Parameters.AddWithValue("@i", b.KodeIATA.ToUpperInvariant());
        cmd.Parameters.AddWithValue("@k", b.Kota);
        cmd.Parameters.AddWithValue("@g", b.NegaraID);
        cmd.Parameters.AddWithValue("@j", b.JumlahTerminal);
        cmd.Parameters.AddWithValue("@a", b.Alamat);
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }

    public async Task UpdateAsync(Bandara b)
    {
        await using var conn = Database.CreateConnection();
        await conn.OpenAsync();
        await using var cmd = new MySqlCommand(
            "UPDATE Bandara SET Nama=@n,KodeIATA=@i,Kota=@k,NegaraID=@g,JumlahTerminal=@j,Alamat=@a WHERE ID=@id", conn);
        cmd.Parameters.AddWithValue("@n", b.Nama);
        cmd.Parameters.AddWithValue("@i", b.KodeIATA.ToUpperInvariant());
        cmd.Parameters.AddWithValue("@k", b.Kota);
        cmd.Parameters.AddWithValue("@g", b.NegaraID);
        cmd.Parameters.AddWithValue("@j", b.JumlahTerminal);
        cmd.Parameters.AddWithValue("@a", b.Alamat);
        cmd.Parameters.AddWithValue("@id", b.ID);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await using var conn = Database.CreateConnection();
        await conn.OpenAsync();
        await using var cmd = new MySqlCommand("DELETE FROM Bandara WHERE ID=@id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        await cmd.ExecuteNonQueryAsync(); // error 1451 (FK) ditangani di UI -> pesan ramah
    }
}
