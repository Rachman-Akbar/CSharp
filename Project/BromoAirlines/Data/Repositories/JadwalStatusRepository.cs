using BromoAirlines.Data;
using BromoAirlines.Models;
using MySqlConnector;

namespace BromoAirlines.Data.Repositories;

public sealed class JadwalRepository
{
    private static JadwalPenerbangan Map(MySqlDataReader r) => new()
    {
        ID = r.GetInt32(0), KodePenerbangan = r.GetString(1),
        BandaraKeberangkatanID = r.GetInt32(2), BandaraTujuanID = r.GetInt32(3),
        MaskapaiID = r.GetInt32(4), TanggalWaktuKeberangkatan = r.GetDateTime(5),
        DurasiPenerbangan = r.GetInt32(6), HargaPerTiket = r.GetDecimal(7),
        NamaBerangkat = r.IsDBNull(8) ? null : r.GetString(8),
        NamaTujuan = r.IsDBNull(9) ? null : r.GetString(9),
        NamaMaskapai = r.IsDBNull(10) ? null : r.GetString(10)
    };

    private const string Base = @"SELECT j.ID,j.KodePenerbangan,j.BandaraKeberangkatanID,j.BandaraTujuanID,j.MaskapaiID,
        j.TanggalWaktuKeberangkatan,j.DurasiPenerbangan,j.HargaPerTiket,b1.Nama,b2.Nama,m.Nama
        FROM JadwalPenerbangan j
        LEFT JOIN Bandara b1 ON b1.ID=j.BandaraKeberangkatanID
        LEFT JOIN Bandara b2 ON b2.ID=j.BandaraTujuanID
        LEFT JOIN Maskapai m ON m.ID=j.MaskapaiID";

    public async Task<List<JadwalPenerbangan>> GetAllAsync(string? cari = null)
    {
        var r = new List<JadwalPenerbangan>();
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand(Base +
            " WHERE (@c IS NULL OR j.KodePenerbangan LIKE CONCAT('%',@c,'%'))" +
            " ORDER BY j.TanggalWaktuKeberangkatan DESC", c);
        cmd.Parameters.AddWithValue("@c", string.IsNullOrWhiteSpace(cari) ? (object)DBNull.Value : cari!);
        await using var rd = await cmd.ExecuteReaderAsync();
        while (await rd.ReadAsync()) r.Add(Map(rd));
        return r;
    }

    public async Task<JadwalPenerbangan?> GetByIdAsync(int id)
    {
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand(Base + " WHERE j.ID=@id LIMIT 1", c);
        cmd.Parameters.AddWithValue("@id", id);
        await using var rd = await cmd.ExecuteReaderAsync();
        return await rd.ReadAsync() ? Map(rd) : null;
    }

    public async Task<bool> ExistsKodeAsync(string kode, int kecualiId = 0)
    {
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand("SELECT COUNT(1) FROM JadwalPenerbangan WHERE KodePenerbangan=@k AND ID<>@id", c);
        cmd.Parameters.AddWithValue("@k", kode);
        cmd.Parameters.AddWithValue("@id", kecualiId);
        return Convert.ToInt32(await cmd.ExecuteScalarAsync()) > 0;
    }

    public async Task<int> InsertAsync(JadwalPenerbangan j)
    {
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand(
            "INSERT INTO JadwalPenerbangan(KodePenerbangan,BandaraKeberangkatanID,BandaraTujuanID,MaskapaiID,TanggalWaktuKeberangkatan,DurasiPenerbangan,HargaPerTiket) VALUES(@k,@a,@t,@m,@tw,@d,@h); SELECT LAST_INSERT_ID();", c);
        cmd.Parameters.AddWithValue("@k", j.KodePenerbangan.ToUpperInvariant());
        cmd.Parameters.AddWithValue("@a", j.BandaraKeberangkatanID);
        cmd.Parameters.AddWithValue("@t", j.BandaraTujuanID);
        cmd.Parameters.AddWithValue("@m", j.MaskapaiID);
        cmd.Parameters.AddWithValue("@tw", j.TanggalWaktuKeberangkatan);
        cmd.Parameters.AddWithValue("@d", j.DurasiPenerbangan);
        cmd.Parameters.AddWithValue("@h", j.HargaPerTiket);
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }

    public async Task UpdateAsync(JadwalPenerbangan j)
    {
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand(
            "UPDATE JadwalPenerbangan SET KodePenerbangan=@k,BandaraKeberangkatanID=@a,BandaraTujuanID=@t,MaskapaiID=@m,TanggalWaktuKeberangkatan=@tw,DurasiPenerbangan=@d,HargaPerTiket=@h WHERE ID=@id", c);
        cmd.Parameters.AddWithValue("@k", j.KodePenerbangan.ToUpperInvariant());
        cmd.Parameters.AddWithValue("@a", j.BandaraKeberangkatanID);
        cmd.Parameters.AddWithValue("@t", j.BandaraTujuanID);
        cmd.Parameters.AddWithValue("@m", j.MaskapaiID);
        cmd.Parameters.AddWithValue("@tw", j.TanggalWaktuKeberangkatan);
        cmd.Parameters.AddWithValue("@d", j.DurasiPenerbangan);
        cmd.Parameters.AddWithValue("@h", j.HargaPerTiket);
        cmd.Parameters.AddWithValue("@id", j.ID);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand("DELETE FROM JadwalPenerbangan WHERE ID=@id", c);
        cmd.Parameters.AddWithValue("@id", id);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<JadwalPenerbangan>> SearchAsync(int asalId, int tujuanId, DateTime tanggal)
    {
        var r = new List<JadwalPenerbangan>();
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand(Base +
            " WHERE j.BandaraKeberangkatanID=@a AND j.BandaraTujuanID=@t AND DATE(j.TanggalWaktuKeberangkatan)=DATE(@d)" +
            " ORDER BY j.TanggalWaktuKeberangkatan ASC", c);
        cmd.Parameters.AddWithValue("@a", asalId);
        cmd.Parameters.AddWithValue("@t", tujuanId);
        cmd.Parameters.AddWithValue("@d", tanggal.Date);
        await using var rd = await cmd.ExecuteReaderAsync();
        while (await rd.ReadAsync()) r.Add(Map(rd));
        return r;
    }
}

public sealed class StatusRepository
{
    public record JadwalStatus(JadwalPenerbangan Jadwal, string StatusTerakhir, int? DelayMenit, DateTime? WaktuUbah);

    public async Task<List<StatusPenerbangan>> GetStatusListAsync()
    {
        var r = new List<StatusPenerbangan>();
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand("SELECT ID,Nama FROM StatusPenerbangan ORDER BY Nama", c);
        await using var rd = await cmd.ExecuteReaderAsync();
        while (await rd.ReadAsync())
            r.Add(new StatusPenerbangan { ID = rd.GetInt32(0), Nama = rd.GetString(1) });
        return r;
    }

    /// <summary>Status terakhir per jadwal = WaktuPerubahanTerjadi terbesar; kosong = Sesuai Jadwal.</summary>
    public async Task<List<JadwalStatus>> GetJadwalWithLastStatusAsync()
    {
        var r = new List<JadwalStatus>();
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        const string sql = @"SELECT j.ID,j.KodePenerbangan,j.BandaraKeberangkatanID,j.BandaraTujuanID,j.MaskapaiID,
            j.TanggalWaktuKeberangkatan,j.DurasiPenerbangan,j.HargaPerTiket,b1.Nama,b2.Nama,m.Nama,
            s.Nama, p.PerkiraanWaktuDelay, p.WaktuPerubahanTerjadi
            FROM JadwalPenerbangan j
            LEFT JOIN Bandara b1 ON b1.ID=j.BandaraKeberangkatanID
            LEFT JOIN Bandara b2 ON b2.ID=j.BandaraTujuanID
            LEFT JOIN Maskapai m ON m.ID=j.MaskapaiID
            LEFT JOIN PerubahanStatusJadwalPenerbangan p ON p.ID=(
                SELECT p2.ID FROM PerubahanStatusJadwalPenerbangan p2
                WHERE p2.JadwalPenerbanganID=j.ID ORDER BY p2.WaktuPerubahanTerjadi DESC LIMIT 1)
            LEFT JOIN StatusPenerbangan s ON s.ID=p.StatusPenerbanganID
            ORDER BY j.TanggalWaktuKeberangkatan DESC";
        await using var cmd = new MySqlCommand(sql, c);
        await using var rd = await cmd.ExecuteReaderAsync();
        while (await rd.ReadAsync())
        {
            var j = new JadwalPenerbangan
            {
                ID = rd.GetInt32(0), KodePenerbangan = rd.GetString(1),
                BandaraKeberangkatanID = rd.GetInt32(2), BandaraTujuanID = rd.GetInt32(3),
                MaskapaiID = rd.GetInt32(4), TanggalWaktuKeberangkatan = rd.GetDateTime(5),
                DurasiPenerbangan = rd.GetInt32(6), HargaPerTiket = rd.GetDecimal(7),
                NamaBerangkat = rd.IsDBNull(8) ? null : rd.GetString(8),
                NamaTujuan = rd.IsDBNull(9) ? null : rd.GetString(9),
                NamaMaskapai = rd.IsDBNull(10) ? null : rd.GetString(10)
            };
            r.Add(new JadwalStatus(j,
                rd.IsDBNull(11) ? "Sesuai Jadwal" : rd.GetString(11),
                rd.IsDBNull(12) ? null : rd.GetInt32(12),
                rd.IsDBNull(13) ? null : rd.GetDateTime(13)));
        }
        return r;
    }

    public async Task InsertAsync(int jadwalId, int statusId, int? delayMenit)
    {
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand(
            "INSERT INTO PerubahanStatusJadwalPenerbangan(JadwalPenerbanganID,StatusPenerbanganID,WaktuPerubahanTerjadi,PerkiraanWaktuDelay) VALUES(@j,@s,NOW(),@d)", c);
        cmd.Parameters.AddWithValue("@j", jadwalId);
        cmd.Parameters.AddWithValue("@s", statusId);
        cmd.Parameters.AddWithValue("@d", delayMenit.HasValue ? delayMenit.Value : (object)DBNull.Value);
        await cmd.ExecuteNonQueryAsync();
    }
}
