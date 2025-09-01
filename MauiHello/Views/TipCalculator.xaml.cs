using Microsoft.Maui;
using System.Globalization;

namespace MauiHello.Views;

public partial class TipCalculator : ContentPage
{
    public TipCalculator()
    {
        InitializeComponent();
        CalculateTip(); // Initial beregning
    }

    private void OnSliderChanged(object sender, ValueChangedEventArgs e)
    {
        percentageLabel.Text = $"{(int)tipSlider.Value}%";
        CalculateTip();
    }

    private void OnBillOrTipChanged(object sender, TextChangedEventArgs e)
    {
        CalculateTip();
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
        if (!double.TryParse(billEntry.Text, out double billAmount)) return;

        double tipPercent = tipSlider.Value / 100;
        double tip = billAmount * tipPercent;
        double total = billAmount + tip;

        double roundedTotal = Math.Floor(total / 10) * 10;
        double newTip = roundedTotal - billAmount;

        tipLabel.Text = $"Tip: {newTip.ToString("C", CultureInfo.CreateSpecificCulture("da-DK"))}";
        totalLabel.Text = $"Total: {roundedTotal.ToString("C", CultureInfo.CreateSpecificCulture("da-DK"))}";
    }

    private void OnRoundUpClicked(object sender, EventArgs e)
    {
        if (!double.TryParse(billEntry.Text, out double billAmount)) return;

        double tipPercent = tipSlider.Value / 100;
        double tip = billAmount * tipPercent;
        double total = billAmount + tip;

        double roundedTotal = Math.Ceiling(total / 10) * 10;
        double newTip = roundedTotal - billAmount;

        tipLabel.Text = $"Tip: {newTip.ToString("C", CultureInfo.CreateSpecificCulture("da-DK"))}";
        totalLabel.Text = $"Total: {roundedTotal.ToString("C", CultureInfo.CreateSpecificCulture("da-DK"))}";
    }

    private void CalculateTip()
    {
        if (!double.TryParse(billEntry.Text, out double billAmount))
        {
            tipLabel.Text = "Tip: ";
            totalLabel.Text = "Total: ";
            return;
        }

        double tipPercent = tipSlider.Value / 100;
        double tip = billAmount * tipPercent;
        double total = billAmount + tip;

        tipLabel.Text = $"Tip: {tip.ToString("C", CultureInfo.CreateSpecificCulture("da-DK"))}";
        totalLabel.Text = $"Total: {total.ToString("C", CultureInfo.CreateSpecificCulture("da-DK"))}";
    }
}
