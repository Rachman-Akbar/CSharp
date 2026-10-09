using Avalonia.Controls;
using BromoAirlines.Forms.Pages;
using BromoAirlines.Helpers;

namespace BromoAirlines.Forms;

public partial class CustomerMainWindow : Window
{
    public CustomerMainWindow()
    {
        InitializeComponent();
        if (!Session.IsLoggedIn || Session.IsAdmin) { new LoginWindow().Show(); Close(); return; }
        LblUser.Text = $"— {Session.Current?.Nama} (@{Session.Current?.Username})";
        MCariBtn.Click += (_, _) => Nav("Cari Penerbangan", new CariView());
        MRiwayatBtn.Click += (_, _) => Nav("Riwayat Transaksi", new RiwayatView());
        BtnLogout.Click += (_, _) => { Session.Clear(); new LoginWindow().Show(); Close(); };
        Nav("Cari Penerbangan", new CariView());
    }

    private void Nav(string title, UserControl v) { LblTitle.Text = title; Host.Content = v; }
}
