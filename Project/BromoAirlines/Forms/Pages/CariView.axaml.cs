using Avalonia.Controls;
using Avalonia.Interactivity;
using BromoAirlines.Data.Repositories;
using BromoAirlines.Models;

namespace BromoAirlines.Forms.Pages;

public partial class CariView : UserControl
{
    private readonly BandaraRepository _band = new();
    private readonly JadwalRepository _jad = new();

    public CariView()
    {
        InitializeComponent();
        BtnCari.Click += OnCari;
        BtnPilih.Click += OnPilih;
        _ = Init();
    }

    private async Task Init()
    {
        var b = await _band.GetAllAsync();
        InDari.ItemsSource = b; InKe.ItemsSource = new List<Bandara>(b);
        InTgl.SelectedDate = DateTimeOffset.Now;
    }

    private void Err(string m) { LblErr.Text = m; LblErr.IsVisible = true; }

    private async void OnCari(object? s, RoutedEventArgs e)
    {
        LblErr.IsVisible = false;
        if (InDari.SelectedItem is not Bandara a) { Err("Pilih bandara asal."); return; }
        if (InKe.SelectedItem is not Bandara t) { Err("Pilih bandara tujuan."); return; }
        if (a.ID == t.ID) { Err("Asal dan tujuan harus berbeda."); return; }
        var tgl = (InTgl.SelectedDate ?? DateTimeOffset.Now).DateTime;
        var hasil = await _jad.SearchAsync(a.ID, t.ID, tgl);
        Grid.ItemsSource = hasil;
        if (hasil.Count == 0) Err("Tidak ada jadwal untuk filter tersebut.");
    }

    private void OnPilih(object? s, RoutedEventArgs e)
    {
        if (Grid.SelectedItem is not JadwalPenerbangan j) { Err("Pilih jadwal dulu."); return; }
        new PesanWindow(j.ID).Show();
    }
}
