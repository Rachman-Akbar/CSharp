using Avalonia.Controls;
using BromoAirlines.Data.Repositories;
using BromoAirlines.Helpers;

namespace BromoAirlines.Forms.Pages;

public partial class RiwayatView : UserControl
{
    private readonly TransaksiRepository _repo = new();

    public RiwayatView()
    {
        InitializeComponent();
        BtnRefresh.Click += async (_, _) => await Load();
        Grid.SelectionChanged += OnSel;
        _ = Load();
    }

    private async Task Load()
    {
        if (Session.Current is null) return;
        var r = await _repo.GetRiwayatAsync(Session.Current.ID);
        Grid.ItemsSource = r;
        LblEmpty.IsVisible = r.Count == 0;
    }

    private async void OnSel(object? s, SelectionChangedEventArgs e)
    {
        if (Grid.SelectedItem is not TransaksiRepository.Riwayat r || Session.Current is null) return;
        GridDetail.ItemsSource = await _repo.GetDetailAsync(r.HeaderID, Session.Current.ID);
    }
}
