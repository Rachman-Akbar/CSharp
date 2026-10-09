using System.Globalization;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Kalkulator.Avalonia;

public partial class MainWindow : Window
{
    private double? _nilaiTersimpan;          // operand pertama / hasil berjalan
    private string _operatorAktif = string.Empty;
    private double _operandTerakhir;          // operand kedua terakhir (untuk tekan = berulang)
    private bool _inputBaru = true;           // display siap diganti digit baru
    private bool _baruHitung;                 // baru saja tekan =
    private bool _adaError;                   // kondisi error (mis. bagi nol)

    public MainWindow()
    {
        InitializeComponent();
        KeyDown += OnKeyDown;
    }

    // ---- Angka ----
    private void BtnAngka_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Content is string digit)
            MasukkanDigit(digit);
    }

    private void MasukkanDigit(string digit)
    {
        if (_adaError)
            ResetSemua();

        // Setelah "=", digit baru berarti perhitungan baru (seperti kalkulator umum)
        if (_baruHitung)
        {
            LblHistory.Text = string.Empty;
            _nilaiTersimpan = null;
            _operatorAktif = string.Empty;
            _baruHitung = false;
        }

        var txt = TxtDisplay.Text ?? "0";
        if (_inputBaru || txt is "0" or "-0")
        {
            TxtDisplay.Text = txt.StartsWith("-") ? "-" + digit : digit;
            _inputBaru = false;
        }
        else if (txt.Replace("-", "").Replace(".", "").Length < 12)
        {
            TxtDisplay.Text = txt + digit;
        }
    }

    private void BtnTitik_Click(object? sender, RoutedEventArgs e) => MasukkanTitik();

    private void MasukkanTitik()
    {
        if (_adaError)
            ResetSemua();

        if (_baruHitung)
        {
            LblHistory.Text = string.Empty;
            _nilaiTersimpan = null;
            _operatorAktif = string.Empty;
            _baruHitung = false;
        }

        var txt = TxtDisplay.Text ?? "0";
        if (_inputBaru)
        {
            TxtDisplay.Text = "0.";
            _inputBaru = false;
        }
        else if (!txt.Contains('.'))
        {
            TxtDisplay.Text = txt + ".";
        }
    }

    // ---- Operator ----
    private void BtnOperator_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button btn)
            SetOperator(btn.Tag?.ToString() ?? "+");
    }

    private void SetOperator(string op)
    {
        if (_adaError)
            return;

        double angkaSekarang = ParseDisplay();

        // Ada operasi tertunda dan user sudah mengetik operand kedua -> hitung dulu (chain)
        if (!_inputBaru && !_baruHitung && _nilaiTersimpan.HasValue && !string.IsNullOrEmpty(_operatorAktif))
        {
            if (!HitungHasil(ulangiRiwayat: false))
                return;
            angkaSekarang = _nilaiTersimpan ?? angkaSekarang;
        }
        // Baru selesai hitung (=) -> lanjutkan dari hasil tersebut
        else if (_baruHitung)
        {
            angkaSekarang = _nilaiTersimpan ?? angkaSekarang;
        }

        _nilaiTersimpan = angkaSekarang;
        _operatorAktif = op;
        _inputBaru = true;
        _baruHitung = false;
        LblHistory.Text = $"{FormatAngka(_nilaiTersimpan.Value)} {SimbolOperator(op)}";
    }

    private void BtnSamaDengan_Click(object? sender, RoutedEventArgs e) => HitungHasil(true);

    /// <returns>false jika gagal (error), true jika berhasil.</returns>
    private bool HitungHasil(bool ulangiRiwayat)
    {
        if (_adaError)
            return false;
        if (string.IsNullOrEmpty(_operatorAktif) || !_nilaiTersimpan.HasValue)
            return false;

        double angkaPertama = _nilaiTersimpan.Value;
        // Tekan = berulang -> ulangi dengan operand terakhir (mis. 2+3= = => 8)
        double angkaKedua = _baruHitung ? _operandTerakhir : ParseDisplay();

        if (_operatorAktif == "/" && angkaKedua == 0)
        {
            TampilkanError("Tidak dapat membagi dengan nol");
            return false;
        }

        double hasil = _operatorAktif switch
        {
            "+" => angkaPertama + angkaKedua,
            "-" => angkaPertama - angkaKedua,
            "*" => angkaPertama * angkaKedua,
            "/" => angkaPertama / angkaKedua,
            _ => angkaKedua
        };

        if (double.IsInfinity(hasil) || double.IsNaN(hasil))
        {
            TampilkanError("Hasil tidak valid");
            return false;
        }

        _operandTerakhir = angkaKedua;
        if (ulangiRiwayat || _baruHitung)
            LblHistory.Text = $"{FormatAngka(angkaPertama)} {SimbolOperator(_operatorAktif)} {FormatAngka(angkaKedua)} =";
        else
            LblHistory.Text = $"{FormatAngka(hasil)} {SimbolOperator(_operatorAktif)}";

        TxtDisplay.Text = FormatAngka(hasil);
        _nilaiTersimpan = hasil;
        _inputBaru = true;
        _baruHitung = true;
        return true;
    }

    private void TampilkanError(string pesan)
    {
        TxtDisplay.Text = "Error";
        LblHistory.Text = pesan;
        _nilaiTersimpan = null;
        _operatorAktif = string.Empty;
        _inputBaru = true;
        _baruHitung = false;
        _adaError = true;
    }

    // ---- C, Backspace, %, ± ----
    private void BtnClear_Click(object? sender, RoutedEventArgs e) => ResetSemua();

    private void ResetSemua()
    {
        TxtDisplay.Text = "0";
        LblHistory.Text = string.Empty;
        _nilaiTersimpan = null;
        _operatorAktif = string.Empty;
        _operandTerakhir = 0;
        _inputBaru = true;
        _baruHitung = false;
        _adaError = false;
    }

    private void BtnBackspace_Click(object? sender, RoutedEventArgs e)
    {
        if (_adaError || _inputBaru || _baruHitung)
            return;
        var txt = TxtDisplay.Text ?? "0";
        if (txt.Length > 1)
        {
            txt = txt[..^1];
            if (txt is "-" or "" or "-0")
            {
                TxtDisplay.Text = "0";
                _inputBaru = true;
            }
            else
            {
                TxtDisplay.Text = txt.TrimEnd('.');
            }
        }
        else
        {
            TxtDisplay.Text = "0";
            _inputBaru = true;
        }
    }

    private void BtnPersen_Click(object? sender, RoutedEventArgs e)
    {
        if (_adaError)
            return;

        double sekarang = ParseDisplay();
        double hasilPersen;

        // Perilaku standar: 200 + 10% = 220 ; 200 × 10% = 20
        if (_nilaiTersimpan.HasValue && !string.IsNullOrEmpty(_operatorAktif) && !_baruHitung)
        {
            hasilPersen = (_operatorAktif is "+" or "-")
                ? _nilaiTersimpan.Value * sekarang / 100.0
                : sekarang / 100.0;
        }
        else
        {
            hasilPersen = sekarang / 100.0;
        }

        TxtDisplay.Text = FormatAngka(hasilPersen);
        _inputBaru = false;
    }

    private void BtnPlusMinus_Click(object? sender, RoutedEventArgs e)
    {
        if (_adaError)
            return;

        double nilai = -ParseDisplay();
        if (nilai == 0)
            nilai = 0; // hindari "-0"
        TxtDisplay.Text = FormatAngka(nilai);

        // Jika display sedang menunjukkan nilai tersimpan (baru tekan operator/=),
        // ikut perbarui agar operasi lanjutan memakai nilai yang benar.
        if (_inputBaru && (_baruHitung || !string.IsNullOrEmpty(_operatorAktif)))
            _nilaiTersimpan = nilai;

        _inputBaru = false;
    }

    // ---- Keyboard ----
    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        bool shift = e.KeyModifiers.HasFlag(KeyModifiers.Shift);
        string? digit = e.Key switch
        {
            >= Key.D0 and <= Key.D9 => ((int)e.Key - (int)Key.D0).ToString(),
            >= Key.NumPad0 and <= Key.NumPad9 => ((int)e.Key - (int)Key.NumPad0).ToString(),
            _ => null
        };

        if (digit != null)
        {
            // Shift+5 = %, Shift+8 = × pada keyboard US
            if (shift && e.Key == Key.D5) { BtnPersen_Click(null, new RoutedEventArgs()); e.Handled = true; return; }
            if (shift && e.Key == Key.D8) { SetOperator("*"); e.Handled = true; return; }
            if (shift && e.Key is Key.D9 or Key.D0) return; // kurung, abaikan
            MasukkanDigit(digit);
            e.Handled = true;
            return;
        }

        switch (e.Key)
        {
            case Key.Add:
            case Key.Subtract:
            case Key.Multiply:
            case Key.Divide:
                SetOperator(e.Key switch
                {
                    Key.Add => "+",
                    Key.Subtract => "-",
                    Key.Multiply => "*",
                    _ => "/"
                });
                break;
            case Key.OemPlus:
                if (shift) SetOperator("+");
                else HitungHasil(true);
                break;
            case Key.OemMinus: SetOperator("-"); break;
            case Key.Oem2: SetOperator("/"); break;      // tombol / ?
            case Key.OemComma:
            case Key.OemPeriod:
            case Key.Decimal:
            case Key.Separator:
                MasukkanTitik();
                break;
            case Key.Enter:
                HitungHasil(true);
                break;
            case Key.Back:
                BtnBackspace_Click(null, new RoutedEventArgs());
                break;
            case Key.Escape:
            case Key.Delete:
                ResetSemua();
                break;
            case Key.C:
                ResetSemua();
                break;
            case Key.P when shift: // Shift+P = %
                BtnPersen_Click(null, new RoutedEventArgs());
                break;
            default: return;
        }
        e.Handled = true;
    }

    private double ParseDisplay() =>
        double.TryParse(TxtDisplay.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double h) ? h : 0;

    private static string FormatAngka(double nilai)
    {
        if (nilai == 0)
            return "0";
        if (System.Math.Abs(nilai) >= 1e15 || System.Math.Abs(nilai) < 1e-12)
            return nilai.ToString("G6", CultureInfo.InvariantCulture);
        return nilai.ToString("G12", CultureInfo.InvariantCulture);
    }

    private static string SimbolOperator(string op) => op switch
    {
        "*" => "×",
        "/" => "÷",
        "-" => "−",
        _ => op
    };
}
