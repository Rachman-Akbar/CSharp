using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Interactivity;
using BromoAirlines.Data.Repositories;
using BromoAirlines.Helpers;
using BromoAirlines.Models;

namespace BromoAirlines.Forms.Pages;

public partial class MasterJadwalView : UserControl
{
    private readonly JadwalRepository _repo = new();
    private readonly BandaraRepository _band = new();
    private readonly MaskapaiRepository _mask = new();
    private int _editId;
    private List<Bandara> _bandara = new();
    private List<Maskapai> _maskapai = new();

    public MasterJadwalView()
    {
        InitializeComponent();
        BtnCari.Click += async (_, _) => await Load();
        BtnRefresh.Click += async (_, _) => { TxtCari.Text = ""; await Load(); };
        BtnSimpan.Click += OnSimpan;
        BtnBatal.Click += (_, _) => Reset();
        BtnUbah.Click += OnUbah;
        BtnHapus.Click += OnHapus;
        _ = Init();
    }

    private async Task Init()
    {
        _bandara = await _band.GetAllAsync();
        _maskapai = await _mask.GetAllAsync();
        InDari.ItemsSource = _bandara; InKe.ItemsSource = _bandara;
        InMaskapai.ItemsSource = _maskapai;
        InTgl.SelectedDate = DateTimeOffset.Now;
        await Load();
    }

    private async Task Load() => Grid.ItemsSource = await _repo.GetAllAsync(TxtCari.Text);

    private void Reset()
    {
        _editId = 0; InKode.Text = ""; InWaktu.Text = ""; InDurasi.Text = "";
        InHarga.Value = 1; InTgl.SelectedDate = DateTimeOffset.Now;
        LblErr.IsVisible = false; Grid.SelectedItem = null;
    }
    private void Err(string m) { LblErr.Text = m; LblErr.IsVisible = true; }

    private void OnUbah(object? s, RoutedEventArgs e)
    {
        if (Grid.SelectedItem is not JadwalPenerbangan j) { Err("Pilih data dulu."); return; }
        _editId = j.ID;
        InKode.Text = j.KodePenerbangan;
        InTgl.SelectedDate = new DateTimeOffset(j.TanggalWaktuKeberangkatan.Date);
        InWaktu.Text = j.TanggalWaktuKeberangkatan.ToString("HH:mm");
        InDurasi.Text = DurationHelper.ToText(j.DurasiPenerbangan);
        InHarga.Value = j.HargaPerTiket;
        InDari.SelectedIndex = _bandara.FindIndex(x => x.ID == j.BandaraKeberangkatanID);
        InKe.SelectedIndex = _bandara.FindIndex(x => x.ID == j.BandaraTujuanID);
        InMaskapai.SelectedIndex = _maskapai.FindIndex(x => x.ID == j.MaskapaiID);
        LblErr.IsVisible = false;
    }

    private async void OnSimpan(object? s, RoutedEventArgs e)
    {
        LblErr.IsVisible = false;
        var kode = InKode.Text?.Trim().ToUpperInvariant() ?? "";
        if (!Validator.IsKodePenerbanganValid(kode, out var e0)) { Err(e0); return; }
        if (InDari.SelectedItem is not Bandara a) { Err("Bandara asal wajib dipilih."); return; }
        if (InKe.SelectedItem is not Bandara t) { Err("Bandara tujuan wajib dipilih."); return; }
        if (a.ID == t.ID) { Err("Bandara asal dan tujuan tidak boleh sama."); return; }
        if (InMaskapai.SelectedItem is not Maskapai m) { Err("Maskapai wajib dipilih."); return; }
        if (!Regex.IsMatch(InWaktu.Text?.Trim() ?? "", @"^([01]\d|2[0-3]):[0-5]\d$")) { Err("Waktu format 24 jam HH:mm."); return; }
        if (!DurationHelper.TryParse(InDurasi.Text?.Trim() ?? "", out var menit)) { Err("Durasi format 'XX jam YY menit', menit 0-59, total > 0."); return; }
        var harga = (decimal)(InHarga.Value ?? 0);
        if (!Validator.IsMinimal1(harga, "Harga tiket", out var e1)) { Err(e1); return; }
        var tgl = (InTgl.SelectedDate ?? DateTimeOffset.Now).Date;
        var wm = InWaktu.Text!.Trim().Split(':');
        var berangkat = tgl.AddHours(int.Parse(wm[0])).AddMinutes(int.Parse(wm[1]));
        if (!Validator.IsTidakLewatTengahMalam(berangkat, menit, out var e2)) { Err(e2 + " (aturan bisnis PDF)."); return; }
        if (await _repo.ExistsKodeAsync(kode, _editId)) { Err("Kode penerbangan sudah dipakai."); return; }
        var j = new JadwalPenerbangan
        {
            ID = _editId, KodePenerbangan = kode, BandaraKeberangkatanID = a.ID,
            BandaraTujuanID = t.ID, MaskapaiID = m.ID,
            TanggalWaktuKeberangkatan = berangkat, DurasiPenerbangan = menit, HargaPerTiket = harga
        };
        try
        {
            if (_editId == 0) await _repo.InsertAsync(j); else await _repo.UpdateAsync(j);
            Reset(); await Load();
        }
        catch (Exception ex) { Err($"Gagal menyimpan: {ex.Message}"); }
    }

    private async void OnHapus(object? s, RoutedEventArgs e)
    {
        if (Grid.SelectedItem is not JadwalPenerbangan j) { Err("Pilih data dulu."); return; }
        LblErr.IsVisible = false;
        try { await _repo.DeleteAsync(j.ID); await Load(); }
        catch (MySqlConnector.MySqlException ex) when (ex.Number is 1451)
        { Err("Jadwal masih dipakai transaksi / riwayat status."); }
        catch (Exception ex) { Err($"Gagal menghapus: {ex.Message}"); }
    }
}
