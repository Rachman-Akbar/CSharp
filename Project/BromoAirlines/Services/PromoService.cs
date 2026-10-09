namespace BromoAirlines.Services;

/// <summary>Rumus diskon resmi (aturan Q): Diskon = MIN(Total*Persen/100, Max).</summary>
public static class PromoService
{
    public static (decimal diskon, decimal totalBayar) Hitung(decimal totalHarga, decimal persen, decimal maks)
    {
        var d = totalHarga * persen / 100m;
        if (d > maks) d = maks;
        if (d < 0) d = 0;
        return (d, totalHarga - d);
    }
}
