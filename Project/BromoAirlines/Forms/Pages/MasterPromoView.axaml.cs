using Avalonia.Controls;
using Avalonia.Interactivity;
using BromoAirlines.Data.Repositories;
using BromoAirlines.Helpers;
using BromoAirlines.Models;

namespace BromoAirlines.Forms.Pages;

public partial class MasterPromoView : UserControl
{
    private readonly KodePromoRepository _repo = new();
    private int _editId;

    public MasterPromoView()
    {
        InitializeComponent();
        InBerlaku.SelectedDate = DateTimeOffset.Now.AddMonths(6);
        BtnCari.Click += async (_, _) => await Load();
        BtnRefresh.Click += async (_, _) => { TxtCari.Text = ""; await Load(); };
        BtnSimpan.Click += OnSimpan;
        BtnBatal.Click += (_, _) => Reset();
        BtnUbah.Click += OnUbah;
        BtnHapus.Click += OnHapus;
        _ = Load();
    }

    private async Task Load() => Grid.ItemsSource = await _repo.GetAllAsync(TxtCari.Text);
    private void Reset()
    {
        _editId = 0; InKode.Text = ""; InDeskripsi.Text = "";
        InPersen.Value = 10; InMaks.Value = 50000;
        InBerlaku.SelectedDate = DateTimeOffset.Now.AddMonths(6);
        LblErr.IsVisible = false; Grid.SelectedItem = null;
    }
    private void Err(string m) { LblErr.Text = m; LblErr.IsVisible = true; }

    private void OnUbah(object? s, RoutedEventArgs e)
    {
        if (Grid.SelectedItem is not KodePromo k) { Err("Pilih data dulu."); return; }
        _editId = k.ID;
        InKode.Text = k.Kode; InPersen.Value = k.PersentaseDiskon;
        InMaks.Value = k.MaksimumDiskon;
        InBerlaku.SelectedDate = new DateTimeOffset(k.BerlakuSampai);
        InDeskripsi.Text = k.Deskripsi;
        LblErr.IsVisible = false;
    }

    private async void OnSimpan(object? s, RoutedEventArgs e)
    {
        LblErr.IsVisible = false;
        var kode = InKode.Text?.Trim() ?? "";
        if (!Validator.IsKodePromoValid(kode, out var e0)) { Err(e0); return; }
        var persen = (decimal)(InPersen.Value ?? 0);
        var maks = (decimal)(InMaks.Value ?? 0);
        if (!Validator.IsMinimal1(persen, "Persentase diskon", out var e1)) { Err(e1); return; }
        if (!Validator.IsMinimal1(maks, "Maksimum diskon", out var e2)) { Err(e2); return; }
        if (await _repo.ExistsKodeAsync(kode.ToUpperInvariant(), _editId)) { Err("Kode promo sudah dipakai."); return; }
        var k = new KodePromo
        {
            ID = _editId, Kode = kode.ToUpperInvariant(), PersentaseDiskon = persen,
            MaksimumDiskon = maks, BerlakuSampai = (InBerlaku.SelectedDate ?? DateTimeOffset.Now).DateTime,
            Deskripsi = InDeskripsi.Text?.Trim() ?? ""
        };
        try
        {
            if (_editId == 0) await _repo.InsertAsync(k); else await _repo.UpdateAsync(k);
            Reset(); await Load();
        }
        catch (Exception ex) { Err($"Gagal menyimpan: {ex.Message}"); }
    }

    private async void OnHapus(object? s, RoutedEventArgs e)
    {
        if (Grid.SelectedItem is not KodePromo k) { Err("Pilih data dulu."); return; }
        LblErr.IsVisible = false;
        try { await _repo.DeleteAsync(k.ID); await Load(); }
        catch (MySqlConnector.MySqlException ex) when (ex.Number is 1451)
        { Err("Kode promo sudah dipakai transaksi."); }
        catch (Exception ex) { Err($"Gagal menghapus: {ex.Message}"); }
    }
}
