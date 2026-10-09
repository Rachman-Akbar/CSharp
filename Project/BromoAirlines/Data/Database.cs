using System.Configuration;
using MySqlConnector;

namespace BromoAirlines.Data;

/// <summary>Analog Database.cs sesuai prompt: kelola koneksi terpusat, buka sesingkat mungkin.</summary>
public static class Database
{
    private static string? _override;

    public static void SetConnectionString(string cs) => _override = cs;

    public static string ConnectionString
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(_override)) return _override!;
            try
            {
                var cs = ConfigurationManager.ConnectionStrings["BromoAirlinesDb"]?.ConnectionString;
                if (!string.IsNullOrWhiteSpace(cs)) return cs!;
            }
            catch { /* abaikan, pakai default */ }
            return "Server=localhost;Database=bromo-airlines;Uid=root;Pwd=;SslMode=None;";
        }
    }

    public static MySqlConnection CreateConnection() => new(ConnectionString);

    /// <summary>Cek koneksi cepat untuk penanganan error koneksi (aturan T).</summary>
    public static async Task<(bool ok, string error)> TryConnectAsync()
    {
        try
        {
            await using var conn = CreateConnection();
            await conn.OpenAsync();
            return (true, "");
        }
        catch (Exception ex)
        {
            return (false, $"Koneksi database gagal: {ex.Message}");
        }
    }
}
