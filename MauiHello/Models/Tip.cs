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
                if (_tipPct != value)
                {
                    _tipPct = value;
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

        private string ValidateInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                input = "";

            // Only allow digits and one decimal separator
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
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

            // Prevent negative numbers
            if (input.StartsWith("-"))
                input = input.TrimStart('-');

            // If empty, set to 0.00
            if (string.IsNullOrWhiteSpace(input))
                input = "0,00";

            return input;
        }

        public void CalculateTip()
        {
            if (decimal.TryParse(BillAmount, out decimal bill))
            {
                decimal tip = bill * (decimal)TipPct / 100;
                decimal total = bill + tip;
                
                TipAmount = tip.ToString("C", CurrentCulture);
                TotalAmount = total.ToString("C", CurrentCulture);
                
                // For debugging purposes
                Console.WriteLine($"Bill: {bill}, Tip%: {TipPct}, Tip: {tip}, Total: {total}");
            }
            else
            {
                TipAmount = "";
                TotalAmount = "";
            }
            
            OnPropertyChanged(nameof(TipPctDisplay));
        }

        public void RoundDown()
        {
            if (decimal.TryParse(BillAmount, out decimal bill))
            {
                decimal tip = bill * (decimal)TipPct / 100;
                decimal total = bill + tip;
                total = Math.Floor(total);
                TotalAmount = total.ToString("C", CurrentCulture);
            }
        }

        public void RoundUp()
        {
            if (decimal.TryParse(BillAmount, out decimal bill))
            {
                decimal tip = bill * (decimal)TipPct / 100;
                decimal total = bill + tip;
                total = Math.Ceiling(total);
                TotalAmount = total.ToString("C", CurrentCulture);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}