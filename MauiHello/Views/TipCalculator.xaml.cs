using System;
using Microsoft.Maui.Controls;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MauiHello.Views
{
    public partial class TipCalculator : ContentPage
    {
        private CultureInfo currentCulture = CultureInfo.CreateSpecificCulture("da-DK");

        public TipCalculator()
        {
            InitializeComponent();
            billEntry.Text = "0,00"; // Set default value
            UpdateTipAndTotal();
        }

        private void OnBillOrTipChanged(object sender, EventArgs e)
        {
            // Remove non-numeric characters except decimal separator
            string input = billEntry.Text ?? "";

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

            // Update the Entry text if changed
            if (billEntry.Text != input)
                billEntry.Text = input;

            UpdateTipAndTotal();
        }

        private void OnSliderChanged(object sender, ValueChangedEventArgs e)
        {
            percentageLabel.Text = $"{(int)tipSlider.Value}%";
            UpdateTipAndTotal();
        }

        private async void OnFifteenPercentClicked(object sender, EventArgs e)
        {
            // Show alert for normal tip
            await DisplayAlert("Normal Tip", "Du har valgt normal tip på 15%", "OK");
            tipSlider.Value = 15;
        }

        private async void OnTwentyPercentClicked(object sender, EventArgs e)
        {
            // Ask user if they want to give generous tip
            bool answer = await DisplayAlert("Generøs Tip", "Vil du give en generøs tip på 20%?", "Yes", "No");
            
            // Only set to 20% if user answered Yes
            if (answer)
            {
                tipSlider.Value = 20;
            }
        }

        private void OnRoundDownClicked(object sender, EventArgs e)
        {
            if (decimal.TryParse(billEntry.Text, out decimal bill))
            {
                decimal tip = bill * (decimal)tipSlider.Value / 100;
                decimal total = bill + tip;
                total = Math.Floor(total);
                totalLabel.Text = $"{total.ToString("C", currentCulture)}";
            }
        }

        private void OnRoundUpClicked(object sender, EventArgs e)
        {
            if (decimal.TryParse(billEntry.Text, out decimal bill))
            {
                decimal tip = bill * (decimal)tipSlider.Value / 100;
                decimal total = bill + tip;
                total = Math.Ceiling(total);
                totalLabel.Text = $"{total.ToString("C", currentCulture)}";
            }
        }

        private async void OnCurrencyClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Vælg valuta", "Cancel", null, "Kroner (DK)", "Euro (EU)", "Dollars (US)");

            switch (action)
            {
                case "Kroner (DK)":
                    currentCulture = CultureInfo.CreateSpecificCulture("da-DK");
                    break;
                case "Euro (EU)":
                    currentCulture = CultureInfo.CreateSpecificCulture("de-DE");
                    break;
                case "Dollars (US)":
                    currentCulture = CultureInfo.CreateSpecificCulture("en-US");
                    break;
                default:
                    return; // User cancelled
            }

            // Update display with new currency
            UpdateTipAndTotal();
        }

        private void UpdateTipAndTotal()
        {
            if (decimal.TryParse(billEntry.Text, out decimal bill))
            {
                decimal tip = bill * (decimal)tipSlider.Value / 100;
                decimal total = bill + tip;
                tipLabel.Text = $"{tip.ToString("C", currentCulture)}";
                totalLabel.Text = $"{total.ToString("C", currentCulture)}";
            }
            else
            {
                tipLabel.Text = "";
                totalLabel.Text = "";
            }
        }
    }
}
