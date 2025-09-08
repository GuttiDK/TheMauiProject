using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using MauiHello.Services;

namespace MauiHello.Models
{
    public class Tip : INotifyPropertyChanged
    {
        private string _billAmount = "0,00";
        private string _tipAmount = "0,00";
        private string _totalAmount = "0,00";
        private double _tipPct;
        private CultureInfo _currentCulture = CultureInfo.CreateSpecificCulture("da-DK");

        public Tip()
        {
            // Load the default tip percentage from settings
            _tipPct = SettingsService.GetDefaultTipPercent();
        }

        public string BillAmount
        {
            get => _billAmount;
            set
            {
                string validatedValue = ValidateInput(value);
                if (_billAmount != validatedValue)
                {
                    _billAmount = validatedValue;
                    OnPropertyChanged();
                    CalculateTip();
                }
            }
        }

        public string TipAmount
        {
            get => _tipAmount;
            set
            {
                if (_tipAmount != value)
                {
                    _tipAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TotalAmount
        {
            get => _totalAmount;
            set
            {
                if (_totalAmount != value)
                {
                    _totalAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        public double TipPct
        {
            get => _tipPct;
            set
            {
                // Sørg for at tip procent er mellem 0 og 100
                double validatedValue = Math.Max(0, Math.Min(100, value));
                if (_tipPct != validatedValue)
                {
                    _tipPct = validatedValue;
                    OnPropertyChanged();
                    CalculateTip();
                }
            }
        }

        public CultureInfo CurrentCulture
        {
            get => _currentCulture;
            set
            {
                if (_currentCulture != value)
                {
                    _currentCulture = value;
                    OnPropertyChanged();
                    CalculateTip();
                }
            }
        }

        public string TipPctDisplay => $"{(int)TipPct}%";

        // Tilføjet property for decimal værdi af regning
        public decimal BillAmountDecimal
        {
            get
            {
                if (decimal.TryParse(BillAmount, out decimal result))
                {
                    return Math.Max(0, result); // Sørg for at det er positivt
                }
                return 0;
            }
        }

        // Tilføjet property for decimal værdi af tip
        public decimal TipAmountDecimal
        {
            get
            {
                return BillAmountDecimal * (decimal)TipPct / 100;
            }
        }

        // Tilføjet property for decimal værdi af total
        public decimal TotalAmountDecimal
        {
            get
            {
                return BillAmountDecimal + TipAmountDecimal;
            }
        }

        private string ValidateInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                input = "";

            // Fjern alle negative tegn først
            input = input.Replace("-", "");

            // Only allow digits and one decimal separator
            string decimalSeparator = CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string pattern = $"[^0-9{Regex.Escape(decimalSeparator)}]";
            input = Regex.Replace(input, pattern, "");

            // Prevent multiple decimal separators
            int firstSeparator = input.IndexOf(decimalSeparator);
            if (firstSeparator >= 0)
            {
                int lastSeparator = input.LastIndexOf(decimalSeparator);
                if (lastSeparator != firstSeparator)
                {
                    input = input.Remove(lastSeparator, 1);
                }
            }

            // Begræns til maksimalt 2 decimaler
            if (firstSeparator >= 0 && input.Length > firstSeparator + 3)
            {
                input = input.Substring(0, firstSeparator + 3);
            }

            // Forhindre meget høje beløb (over 1 million)
            if (decimal.TryParse(input, out decimal testValue) && testValue > 1000000)
            {
                input = "1000000" + decimalSeparator + "00";
            }

            // If empty, set to 0.00
            if (string.IsNullOrWhiteSpace(input))
                input = "0" + decimalSeparator + "00";

            return input;
        }

        public void CalculateTip()
        {
            decimal bill = BillAmountDecimal;
            decimal tip = TipAmountDecimal;
            decimal total = TotalAmountDecimal;
            
            TipAmount = tip.ToString("C", CurrentCulture);
            TotalAmount = total.ToString("C", CurrentCulture);
            
            // For debugging purposes
            Console.WriteLine($"Bill: {bill}, Tip%: {TipPct}, Tip: {tip}, Total: {total}");
            
            OnPropertyChanged(nameof(TipPctDisplay));
            OnPropertyChanged(nameof(BillAmountDecimal));
            OnPropertyChanged(nameof(TipAmountDecimal));
            OnPropertyChanged(nameof(TotalAmountDecimal));
        }

        public void RoundDown()
        {
            decimal total = TotalAmountDecimal;
            total = Math.Floor(total);
            TotalAmount = total.ToString("C", CurrentCulture);
        }

        public void RoundUp()
        {
            decimal total = TotalAmountDecimal;
            total = Math.Ceiling(total);
            TotalAmount = total.ToString("C", CurrentCulture);
        }

        // Ny metode til at sætte en specifik tip værdi
        public void SetTipPercentage(double percentage)
        {
            TipPct = Math.Max(0, Math.Min(100, percentage));
        }

        // Ny metode til at få en formateret tip beskrivelse
        public string GetTipDescription()
        {
            return TipPct switch
            {
                < 10 => "Lav tip",
                >= 10 and < 15 => "Standard tip",
                >= 15 and < 20 => "God tip",
                >= 20 and < 25 => "Generøs tip",
                >= 25 => "Meget generøs tip",
                _ => "Ingen tip"
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}