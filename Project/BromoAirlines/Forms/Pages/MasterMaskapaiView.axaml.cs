using Avalonia.Controls;
using Avalonia.Interactivity;
using BromoAirlines.Data.Repositories;
using BromoAirlines.Helpers;
using BromoAirlines.Models;

namespace BromoAirlines.Forms.Pages;

public partial class MasterMaskapaiView : UserControl
{
    private readonly MaskapaiRepository _repo = new();
    private int _editId;

    public MasterMaskapaiView()
    {
        InitializeComponent();
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
        _editId = 0; InNama.Text = ""; InPerusahaan.Text = ""; InDeskripsi.Text = "";
        InKru.Value = 1; LblErr.IsVisible = false; Grid.SelectedItem = null;
    }
    private void Err(string m) { LblErr.Text = m; LblErr.IsVisible = true; }

    private void OnUbah(object? s, RoutedEventArgs e)
    {
        if (Grid.SelectedItem is not Maskapai m) { Err("Pilih data dulu."); return; }
        _editId = m.ID;
        InNama.Text = m.Nama; InPerusahaan.Text = m.Perusahaan;
        InKru.Value = m.JumlahKru; InDeskripsi.Text = m.Deskripsi;
        LblErr.IsVisible = false;
    }

    private async void OnSimpan(object? s, RoutedEventArgs e)
    {
        LblErr.IsVisible = false;
        var nama = InNama.Text?.Trim() ?? "";
        var prs = InPerusahaan.Text?.Trim() ?? "";
        var kru = (int)(InKru.Value ?? 0);
        if (string.IsNullOrWhiteSpace(nama)) { Err("Nama wajib diisi."); return; }
        if (string.IsNullOrWhiteSpace(prs)) { Err("Perusahaan wajib diisi."); return; }
        if (!Validator.IsMinimal1(kru, "Jumlah kru", out var e1)) { Err(e1); return; }
        var m = new Maskapai { ID = _editId, Nama = nama, Perusahaan = prs, JumlahKru = kru, Deskripsi = InDeskripsi.Text?.Trim() ?? "" };
        try
        {
            if (_editId == 0) await _repo.InsertAsync(m); else await _repo.UpdateAsync(m);
            Reset(); await Load();
        }
        catch (Exception ex) { Err($"Gagal menyimpan: {ex.Message}"); }
    }

    private async void OnHapus(object? s, RoutedEventArgs e)
    {
        if (Grid.SelectedItem is not Maskapai m) { Err("Pilih data dulu."); return; }
        LblErr.IsVisible = false;
        try { await _repo.DeleteAsync(m.ID); await Load(); }
        catch (MySqlConnector.MySqlException ex) when (ex.Number is 1451)
        { Err("Maskapai masih dipakai oleh jadwal penerbangan."); }
        catch (Exception ex) { Err($"Gagal menghapus: {ex.Message}"); }
    }
}
