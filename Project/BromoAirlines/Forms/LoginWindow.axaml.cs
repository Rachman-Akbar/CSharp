using Avalonia.Interactivity;
using BromoAirlines.Services;

namespace BromoAirlines.Forms;

public partial class LoginWindow : Avalonia.Controls.Window
{
    private readonly AuthService _auth = new();

    public LoginWindow()
    {
        InitializeComponent();
        ChkShow.IsCheckedChanged += (_, _) =>
            TxtPass.PasswordChar = ChkShow.IsChecked == true ? '\0' : '•';
        BtnLogin.Click += OnLogin;
        BtnToRegister.Click += (_, _) => { new RegisterWindow().Show(); Close(); };
    }

    private void ShowErr(string m) { LblErr.Text = m; LblErr.IsVisible = true; }

    private async void OnLogin(object? s, RoutedEventArgs e)
    {
        LblErr.IsVisible = false;
        var u = TxtUser.Text?.Trim() ?? "";
        var p = TxtPass.Text ?? "";
        if (string.IsNullOrWhiteSpace(u)) { ShowErr("Username wajib diisi."); return; }
        if (string.IsNullOrWhiteSpace(p)) { ShowErr("Password wajib diisi."); return; }
        BtnLogin.IsEnabled = false; Busy.IsVisible = true;
        try
        {
            var (akun, err) = await _auth.LoginAsync(u, p);
            if (akun is null) { ShowErr(err); return; }
            Avalonia.Controls.Window next = akun.MerupakanAdmin
                ? new AdminMainWindow()
                : new CustomerMainWindow();
            next.Show(); Close();
        }
        catch (Exception ex) { ShowErr($"Koneksi database gagal: {ex.Message}"); }
        finally { BtnLogin.IsEnabled = true; Busy.IsVisible = false; }
    }
}
