using Avalonia.Controls;
using Avalonia.Interactivity;
using BromoAirlines.Data.Repositories;
using BromoAirlines.Helpers;
using BromoAirlines.Models;
using BromoAirlines.Services;

namespace BromoAirlines.Forms.Pages;

public partial class PesanWindow : Window
{
    private readonly int _jadwalId;
    private readonly JadwalRepository _jad = new();
    private readonly KodePromoRepository _promo = new();
    private readonly TransaksiRepository _trx = new();
    private JadwalPenerbangan? _j;
    private KodePromo? _p;
    private readonly List<(ComboBox titel, TextBox nama)> _rows = new();

    public PesanWindow() // untuk XAML/designer
    {
        _jadwalId = 0;
        InitializeComponent();
    }

    public PesanWindow(int jadwalId)
    {
        _jadwalId = jadwalId;
        InitializeComponent();
        NumPax.ValueChanged += (_, _) => BuildPax();
        BtnCek.Click += OnCek;
        BtnBayar.Click += OnBayar;
        _ = Init();
    }

    private async Task Init()
    {
        _j = await _jad.GetByIdAsync(_jadwalId);
        if (_j is null) { Err("Jadwal tidak ditemukan."); BtnBayar.IsEnabled = false; return; }
        LblDetail.Text = $"{_j.KodePenerbangan} • {_j.NamaMaskapai} • {_j.NamaBerangkat} -> {_j.NamaTujuan} • {_j.TanggalWaktuKeberangkatan:dd-MM-yyyy HH:mm} • {Helpers.DurationHelper.ToText(_j.DurasiPenerbangan)} • Rp{_j.HargaPerTiket:N0}";
        BuildPax();
        Hitung();
    }

    private void BuildPax()
    {
        PaxPanel.Children.Clear(); _rows.Clear();
        var n = (int)(NumPax.Value ?? 1);
        for (var i = 0; i < n; i++)
        {
            var cb = new ComboBox { ItemsSource = new[] { "Tn", "Ny", "Nn" }, SelectedIndex = 0, Width = 80 };
            var tb = new TextBox { PlaceholderText = $"Penumpang {i + 1}", Width = 380 };
            var sp = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 8 };
            sp.Children.Add(cb); sp.Children.Add(tb);
            PaxPanel.Children.Add(sp);
            _rows.Add((cb, tb));
        }
    }

    private void Err(string m) { LblErr.Text = m; LblErr.IsVisible = true; LblOk.IsVisible = false; }

    private async void OnCek(object? s, RoutedEventArgs e)
    {
        LblErr.IsVisible = false;
        var kode = InPromo.Text?.Trim() ?? "";
        if (string.IsNullOrEmpty(kode)) { _p = null; LblPromo.Text = "Tanpa promo."; Hitung(); return; }
        _p = await _promo.GetByKodeAsync(kode.ToUpperInvariant());
        if (_p is null) { Err("Kode promo tidak valid."); return; }
        if (_p.BerlakuSampai.Date < DateTime.Today) { Err("Kode promo kedaluwarsa."); return; }
        LblPromo.Text = $"Promo {_p.Kode}: {_p.PersentaseDiskon}% maks Rp{_p.MaksimumDiskon:N0} s/d {_p.BerlakuSampai:dd-MM-yyyy}";
        Hitung();
    }

    private void Hitung()
    {
        if (_j is null) return;
        var n = _rows.Count == 0 ? 1 : _rows.Count;
        var kotor = _j.HargaPerTiket * n;
        if (_p is null) { LblTotal.Text = $"Total: Rp{kotor:N0}"; return; }
        var (diskon, bayar) = PromoService.Hitung(kotor, _p.PersentaseDiskon, _p.MaksimumDiskon);
        LblTotal.Text = $"Total: Rp{kotor:N0} - Diskon Rp{diskon:N0} = Rp{bayar:N0}";
    }

    private async void OnBayar(object? s, RoutedEventArgs e)
    {
        LblErr.IsVisible = false; LblOk.IsVisible = false;
        if (_j is null || Session.Current is null) { Err("Sesi / jadwal tidak valid."); return; }
        var list = new List<(string, string)>();
        foreach (var (cb, tb) in _rows)
        {
            var nm = tb.Text?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(nm)) { Err("Nama penumpang tidak boleh kosong."); return; }
            list.Add((cb.SelectedItem?.ToString() ?? "Tn", nm));
        }
        BtnBayar.IsEnabled = false;
        try
        {
            var hid = await _trx.CreateAsync(Session.Current.ID, _jadwalId, list,
                string.IsNullOrWhiteSpace(InPromo.Text) ? null : InPromo.Text);
            LblOk.Text = $"Pemesanan berhasil! ID Transaksi: {hid}. Lihat di Riwayat Transaksi.";
            LblOk.IsVisible = true;
        }
        catch (Exception ex) { Err(ex.Message); }
        finally { BtnBayar.IsEnabled = true; }
    }
}
