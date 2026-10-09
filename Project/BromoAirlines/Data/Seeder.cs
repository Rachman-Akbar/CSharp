using BromoAirlines.Data;
using MySqlConnector;

namespace BromoAirlines.Data;

/// <summary>Seeder: menjalankan Database.Seed.sql (idempotent) via ADO.NET.</summary>
public static class Seeder
{
    public static string FindSeedFile()
    {
        var candidates = new[]
        {
            Path.Combine(AppContext.BaseDirectory, "Database.Seed.sql"),
            Path.Combine(Directory.GetCurrentDirectory(), "Database.Seed.sql"),
            "/home/akbar/Projects/CSharp/Project/BromoAirlines/Database.Seed.sql",
        };
        foreach (var p in candidates)
            if (File.Exists(p)) return p;
        throw new FileNotFoundException("Database.Seed.sql tidak ditemukan.");
    }

    public static async Task<(bool ok, string msg)> SeedAsync()
    {
        string file;
        try { file = FindSeedFile(); }
        catch (Exception ex) { return (false, ex.Message); }
        var statements = File.ReadAllText(file)
            .Split(';')
            .Select(chunk => string.Join("\n",
                chunk.Split('\n')
                     .Select(l => l.Trim())
                     .Where(l => l.Length > 0 && !l.StartsWith("--"))))
            .Select(s => s.Trim())
            .Where(s => s.Length > 0 && !s.StartsWith("USE "))
            .ToList();
        int n = 0;
        try
        {
            await using var conn = Database.CreateConnection();
            await conn.OpenAsync();
            foreach (var sql in statements)
            {
                if (sql.StartsWith("--")) continue;
                await using var cmd = new MySqlCommand(sql, conn);
                await cmd.ExecuteNonQueryAsync();
                n++;
            }
            return (true, $"Seed OK: {n} statement dari {Path.GetFileName(file)}.");
        }
        catch (Exception ex)
        {
            return (false, $"Seed gagal pada statement ke-{n + 1}: {ex.Message}");
        }
    }
}
