using Avalonia.Controls;
using Avalonia.Interactivity;
using BromoAirlines.Data.Repositories;
using BromoAirlines.Helpers;
using BromoAirlines.Models;

namespace BromoAirlines.Forms.Pages;

public partial class MasterBandaraView : UserControl
{
    private readonly BandaraRepository _repo = new();
    private readonly NegaraRepository _neg = new();
    private int _editId;
    private List<Negara> _negara = new();

    public MasterBandaraView()
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
        _negara = await _neg.GetAllAsync();
        InNegara.ItemsSource = _negara;
        if (_negara.Count > 0) InNegara.SelectedIndex = 0;
        await Load();
    }

    private async Task Load()
    {
        Grid.ItemsSource = await _repo.GetAllAsync(TxtCari.Text);
    }

    private void Reset()
    {
        _editId = 0;
        InNama.Text = ""; InIata.Text = ""; InKota.Text = ""; InAlamat.Text = "";
        InTerminal.Value = 1; if (_negara.Count > 0) InNegara.SelectedIndex = 0;
        LblErr.IsVisible = false; Grid.SelectedItem = null;
    }

    private void Err(string m) { LblErr.Text = m; LblErr.IsVisible = true; }

    private void OnUbah(object? s, RoutedEventArgs e)
    {
        if (Grid.SelectedItem is not Bandara b) { Err("Pilih data dulu."); return; }
        _editId = b.ID;
        InNama.Text = b.Nama; InIata.Text = b.KodeIATA; InKota.Text = b.Kota;
        InAlamat.Text = b.Alamat; InTerminal.Value = b.JumlahTerminal;
        var n = _negara.FindIndex(x => x.ID == b.NegaraID);
        if (n >= 0) InNegara.SelectedIndex = n;
        LblErr.IsVisible = false;
    }

    private async void OnSimpan(object? s, RoutedEventArgs e)
    {
        LblErr.IsVisible = false;
        var nama = InNama.Text?.Trim() ?? "";
        var iata = InIata.Text?.Trim().ToUpperInvariant() ?? "";
        var kota = InKota.Text?.Trim() ?? "";
        var alamat = InAlamat.Text?.Trim() ?? "";
        var term = (int)(InTerminal.Value ?? 0);
        if (string.IsNullOrWhiteSpace(nama)) { Err("Nama wajib diisi."); return; }
        if (!Validator.IsIataValid(iata, out var e1)) { Err(e1); return; }
        if (string.IsNullOrWhiteSpace(kota)) { Err("Kota wajib diisi."); return; }
        if (!Validator.IsMinimal1(term, "Jumlah terminal", out var e2)) { Err(e2); return; }
        if (InNegara.SelectedItem is not Negara ng) { Err("Negara wajib dipilih."); return; }
        if (await _repo.ExistsNamaAsync(nama, _editId)) { Err("Nama bandara sudah dipakai."); return; }
        if (await _repo.ExistsIataAsync(iata, _editId)) { Err("Kode IATA sudah dipakai."); return; }
        var b = new Bandara { ID = _editId, Nama = nama, KodeIATA = iata, Kota = kota, NegaraID = ng.ID, JumlahTerminal = term, Alamat = alamat };
        try
        {
            if (_editId == 0) await _repo.InsertAsync(b); else await _repo.UpdateAsync(b);
            Reset(); await Load();
        }
        catch (Exception ex) { Err($"Gagal menyimpan: {ex.Message}"); }
    }

    private async void OnHapus(object? s, RoutedEventArgs e)
    {
        if (Grid.SelectedItem is not Bandara b) { Err("Pilih data dulu."); return; }
        LblErr.IsVisible = false;
        try { await _repo.DeleteAsync(b.ID); await Load(); }
        catch (MySqlConnector.MySqlException ex) when (ex.Number is 1451)
        { Err("Bandara masih digunakan oleh data penerbangan."); }
        catch (Exception ex) { Err($"Gagal menghapus: {ex.Message}"); }
    }
}
