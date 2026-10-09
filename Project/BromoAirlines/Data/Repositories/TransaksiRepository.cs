using BromoAirlines.Data;
using BromoAirlines.Models;
using BromoAirlines.Services;
using MySqlConnector;

namespace BromoAirlines.Data.Repositories;

public sealed class TransaksiRepository
{
    public record Riwayat(int HeaderID, DateTime Tgl, string Kode, string Rute, decimal Total, string? Promo, int Jml);

    public async Task<int> CreateAsync(int akunId, int jadwalId, List<(string titel, string nama)> penumpang, string? kodePromo)
    {
        if (penumpang.Count == 0) throw new InvalidOperationException("Jumlah penumpang minimal 1.");
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var tx = await c.BeginTransactionAsync();
        try
        {
            // Harga tepercaya dari DB (jangan percaya UI)
            int? promoId = null;
            decimal persen = 0, maks = 0;
            await using (var cmd = new MySqlCommand("SELECT HargaPerTiket FROM JadwalPenerbangan WHERE ID=@id", c, (MySqlTransaction)tx))
            {
                cmd.Parameters.AddWithValue("@id", jadwalId);
                var o = await cmd.ExecuteScalarAsync() ?? throw new InvalidOperationException("Jadwal tidak ditemukan.");
                var harga = Convert.ToDecimal(o);
                var totalKotor = harga * penumpang.Count;
                decimal diskon = 0;
                if (!string.IsNullOrWhiteSpace(kodePromo))
                {
                    await using var cp = new MySqlCommand("SELECT ID,PersentaseDiskon,MaksimumDiskon,BerlakuSampai FROM KodePromo WHERE Kode=@k LIMIT 1", c, (MySqlTransaction)tx);
                    cp.Parameters.AddWithValue("@k", kodePromo!.Trim().ToUpperInvariant());
                    await using var rd = await cp.ExecuteReaderAsync();
                    if (!await rd.ReadAsync()) throw new InvalidOperationException("Kode promo tidak valid.");
                    if (rd.GetDateTime(3).Date < DateTime.Today) throw new InvalidOperationException("Kode promo kedaluwarsa.");
                    promoId = rd.GetInt32(0); persen = rd.GetDecimal(1); maks = rd.GetDecimal(2);
                    await rd.CloseAsync();
                    (diskon, _) = PromoService.Hitung(totalKotor, persen, maks);
                }
                var totalBayar = totalKotor - diskon;
                await using var ch = new MySqlCommand(
                    "INSERT INTO TransaksiHeader(AkunID,TanggalTransaksi,JadwalPenerbanganID,JumlahPenumpang,TotalHarga,KodePromoID) VALUES(@a,NOW(),@j,@n,@t,@p); SELECT LAST_INSERT_ID();",
                    c, (MySqlTransaction)tx);
                ch.Parameters.AddWithValue("@a", akunId);
                ch.Parameters.AddWithValue("@j", jadwalId);
                ch.Parameters.AddWithValue("@n", penumpang.Count);
                ch.Parameters.AddWithValue("@t", totalBayar);
                ch.Parameters.AddWithValue("@p", promoId.HasValue ? promoId.Value : (object)DBNull.Value);
                var hid = Convert.ToInt32(await ch.ExecuteScalarAsync());
                foreach (var (titel, nama) in penumpang)
                {
                    if (string.IsNullOrWhiteSpace(nama)) throw new InvalidOperationException("Nama penumpang tidak boleh kosong.");
                    await using var cd = new MySqlCommand(
                        "INSERT INTO TransaksiDetail(TransaksiHeaderID,TitelPenumpang,NamaLengkapPenumpang) VALUES(@h,@t,@n)", c, (MySqlTransaction)tx);
                    cd.Parameters.AddWithValue("@h", hid);
                    cd.Parameters.AddWithValue("@t", titel);
                    cd.Parameters.AddWithValue("@n", nama.Trim());
                    await cd.ExecuteNonQueryAsync();
                }
                await tx.CommitAsync();
                return hid;
            }
        }
        catch { await tx.RollbackAsync(); throw; }
    }

    public async Task<List<Riwayat>> GetRiwayatAsync(int akunId)
    {
        var r = new List<Riwayat>();
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand(
            @"SELECT h.ID,h.TanggalTransaksi,j.KodePenerbangan,CONCAT(b1.Kota,' -> ',b2.Kota),h.TotalHarga,k.Kode,h.JumlahPenumpang
              FROM TransaksiHeader h JOIN JadwalPenerbangan j ON j.ID=h.JadwalPenerbanganID
              LEFT JOIN Bandara b1 ON b1.ID=j.BandaraKeberangkatanID
              LEFT JOIN Bandara b2 ON b2.ID=j.BandaraTujuanID
              LEFT JOIN KodePromo k ON k.ID=h.KodePromoID
              WHERE h.AkunID=@a ORDER BY h.TanggalTransaksi DESC", c);
        cmd.Parameters.AddWithValue("@a", akunId);
        await using var rd = await cmd.ExecuteReaderAsync();
        while (await rd.ReadAsync())
            r.Add(new Riwayat(rd.GetInt32(0), rd.GetDateTime(1), rd.GetString(2), rd.IsDBNull(3) ? "-" : rd.GetString(3), rd.GetDecimal(4), rd.IsDBNull(5) ? null : rd.GetString(5), rd.GetInt32(6)));
        return r;
    }

    public async Task<List<TransaksiDetail>> GetDetailAsync(int headerId, int akunId)
    {
        // Filter milik sendiri (aturan R): join header + cek AkunID
        var r = new List<TransaksiDetail>();
        await using var c = Database.CreateConnection();
        await c.OpenAsync();
        await using var cmd = new MySqlCommand(
            @"SELECT d.ID,d.TransaksiHeaderID,d.TitelPenumpang,d.NamaLengkapPenumpang FROM TransaksiDetail d
              JOIN TransaksiHeader h ON h.ID=d.TransaksiHeaderID
              WHERE d.TransaksiHeaderID=@h AND h.AkunID=@a", c);
        cmd.Parameters.AddWithValue("@h", headerId);
        cmd.Parameters.AddWithValue("@a", akunId);
        await using var rd = await cmd.ExecuteReaderAsync();
        while (await rd.ReadAsync())
            r.Add(new TransaksiDetail { ID = rd.GetInt32(0), TransaksiHeaderID = rd.GetInt32(1), TitelPenumpang = rd.GetString(2), NamaLengkapPenumpang = rd.GetString(3) });
        return r;
    }
}
