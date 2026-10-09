using Avalonia.Controls;
using Avalonia.Interactivity;
using BromoAirlines.Data.Repositories;
using BromoAirlines.Helpers;
using BromoAirlines.Models;
using static BromoAirlines.Data.Repositories.StatusRepository;

namespace BromoAirlines.Forms.Pages;

public partial class UbahStatusView : UserControl
{
    private readonly StatusRepository _repo = new();
    private List<StatusPenerbangan> _status = new();

    public UbahStatusView()
    {
        InitializeComponent();
        InStatus.SelectionChanged += (_, _) =>
            InDelay.IsEnabled = (InStatus.SelectedItem as StatusPenerbangan)?.Nama == "Delay";
        BtnRefresh.Click += async (_, _) => await Load();
        BtnSimpan.Click += OnSimpan;
        _ = Init();
    }

    private async Task Init()
    {
        _status = await _repo.GetStatusListAsync();
        InStatus.ItemsSource = _status;
        await Load();
    }

    private async Task Load()
    {
        var list = await _repo.GetJadwalWithLastStatusAsync();
        Grid.ItemsSource = list.Select(x => new
        {
            x.Jadwal,
            StatusTerakhir = x.StatusTerakhir == "Delay" && x.DelayMenit.HasValue
                ? DurationHelper.ToDelayText(x.DelayMenit.Value) : x.StatusTerakhir,
            WaktuUbah = x.WaktuUbah.HasValue ? x.WaktuUbah.Value.ToString("dd-MM-yyyy HH:mm:ss") : "-"
        }).ToList();
        _raw = list;
    }
    private List<JadwalStatus> _raw = new();
    private void Err(string m) { LblErr.Text = m; LblErr.IsVisible = true; LblOk.IsVisible = false; }

    private async void OnSimpan(object? s, RoutedEventArgs e)
    {
        LblErr.IsVisible = false; LblOk.IsVisible = false;
        if (Grid.SelectedItem is null) { Err("Pilih jadwal dulu."); return; }
        if (InStatus.SelectedItem is not StatusPenerbangan st) { Err("Pilih status baru."); return; }
        var idx = Grid.SelectedIndex;
        if (idx < 0 || idx >= _raw.Count) { Err("Pilih jadwal dulu."); return; }
        int? delay = null;
        if (st.Nama == "Delay")
        {
            if (!DurationHelper.TryParse(InDelay.Text?.Trim() ?? "", out var mnt)) { Err("Durasi delay format 'XX jam YY menit', > 0."); return; }
            delay = mnt;
        }
        try
        {
            await _repo.InsertAsync(_raw[idx].Jadwal.ID, st.ID, delay);
            LblOk.Text = "Status tersimpan (riwayat baru ditambahkan).";
            LblOk.IsVisible = true;
            await Load();
        }
        catch (Exception ex) { Err($"Gagal menyimpan: {ex.Message}"); }
    }
}
