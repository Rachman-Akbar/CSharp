using Avalonia.Interactivity;
using BromoAirlines.Services;

namespace BromoAirlines.Forms;

public partial class RegisterWindow : Avalonia.Controls.Window
{
    private readonly AuthService _auth = new();

    public RegisterWindow()
    {
        InitializeComponent();
        DpTgl.SelectedDate = DateTimeOffset.Now.AddYears(-20);
        BtnDaftar.Click += OnDaftar;
        BtnToLogin.Click += (_, _) => { new LoginWindow().Show(); Close(); };
    }

    private void ShowErr(string m) { LblErr.Text = m; LblErr.IsVisible = true; }

    private async void OnDaftar(object? s, RoutedEventArgs e)
    {
        LblErr.IsVisible = false;
        BtnDaftar.IsEnabled = false;
        try
        {
            var tgl = (DpTgl.SelectedDate ?? DateTimeOffset.Now).DateTime;
            var (akun, err) = await _auth.RegisterCustomerAsync(
                TxtUser.Text?.Trim() ?? "", TxtPass.Text ?? "",
                TxtNama.Text?.Trim() ?? "", tgl, TxtTelp.Text?.Trim() ?? "");
            if (akun is null) { ShowErr(err); return; }
            new CustomerMainWindow().Show(); Close(); // Customer only, tanpa hak Admin (aturan H)
        }
        catch (MySqlConnector.MySqlException ex) when (ex.Number is 1062)
        { ShowErr("Username sudah dipakai."); }
        catch (Exception ex) { ShowErr($"Gagal mendaftar: {ex.Message}"); }
        finally { BtnDaftar.IsEnabled = true; }
    }
}
