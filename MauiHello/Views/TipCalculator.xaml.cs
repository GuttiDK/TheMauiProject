using System;
using Microsoft.Maui.Controls;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MauiHello.Views
{
    public partial class TipCalculator : ContentPage
    {
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

        private void OnFifteenPercentClicked(object sender, EventArgs e)
        {
            tipSlider.Value = 15;
        }

        private void OnTwentyPercentClicked(object sender, EventArgs e)
        {
            tipSlider.Value = 20;
        }

        private void OnRoundDownClicked(object sender, EventArgs e)
        {
            if (decimal.TryParse(billEntry.Text, out decimal bill))
            {
                decimal tip = bill * (decimal)tipSlider.Value / 100;
                decimal total = bill + tip;
                total = Math.Floor(total);
                totalLabel.Text = $"{total.ToString("C", CultureInfo.CreateSpecificCulture("da-DK"))}";
            }
        }

        private void OnRoundUpClicked(object sender, EventArgs e)
        {
            if (decimal.TryParse(billEntry.Text, out decimal bill))
            {
                decimal tip = bill * (decimal)tipSlider.Value / 100;
                decimal total = bill + tip;
                total = Math.Ceiling(total);
                totalLabel.Text = $"{total.ToString("C", CultureInfo.CreateSpecificCulture("da-DK"))}";
            }
        }

        private void UpdateTipAndTotal()
        {
            if (decimal.TryParse(billEntry.Text, out decimal bill))
            {
                decimal tip = bill * (decimal)tipSlider.Value / 100;
                decimal total = bill + tip;
                tipLabel.Text = $"{tip.ToString("C", CultureInfo.CreateSpecificCulture("da-DK"))}";
                totalLabel.Text = $"{total.ToString("C", CultureInfo.CreateSpecificCulture("da-DK"))}";
            }
            else
            {
                tipLabel.Text = "";
                totalLabel.Text = "";
            }
        }
    }
}
