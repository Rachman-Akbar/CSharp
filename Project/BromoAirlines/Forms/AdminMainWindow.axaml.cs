using Avalonia.Controls;
using BromoAirlines.Forms.Pages;
using BromoAirlines.Helpers;

namespace BromoAirlines.Forms;

public partial class AdminMainWindow : Window
{
    public AdminMainWindow()
    {
        InitializeComponent();
        if (!Session.IsAdmin) { new LoginWindow().Show(); Close(); return; }
        LblUser.Text = $"— {Session.Current?.Nama} (@{Session.Current?.Username})";
        MBandaraBtn.Click += (_, _) => Nav("Master Bandara", new MasterBandaraView());
        MMaskapaiBtn.Click += (_, _) => Nav("Master Maskapai", new MasterMaskapaiView());
        MJadwalBtn.Click += (_, _) => Nav("Master Jadwal Penerbangan", new MasterJadwalView());
        MPromoBtn.Click += (_, _) => Nav("Master Kode Promo", new MasterPromoView());
        MStatusBtn.Click += (_, _) => Nav("Ubah Status Penerbangan", new UbahStatusView());
        BtnLogout.Click += async (_, _) =>
        {
            Session.Clear(); new LoginWindow().Show(); Close();
            await Task.CompletedTask;
        };
        Closed += (_, _) => { if (!Session.IsLoggedIn) return; };
        Nav("Master Bandara", new MasterBandaraView());
    }

    private void Nav(string title, UserControl v) { LblTitle.Text = title; Host.Content = v; }
}
