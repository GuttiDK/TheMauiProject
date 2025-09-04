using System;
using Microsoft.Maui.Controls;
using System.Globalization;
using MauiHello.Models;

namespace MauiHello.Views
{
    public partial class TipCalculator : ContentPage
    {
        public Tip TipModel { get; private set; }

        public TipCalculator()
        {
            InitializeComponent();
            TipModel = new Tip();
            BindingContext = TipModel;
        }

        private async void OnFifteenPercentClicked(object sender, EventArgs e)
        {
            // Show alert for normal tip
            await DisplayAlert("Normal Tip", "Du har valgt normal tip på 15%", "OK");
            TipModel.TipPct = 15;
        }

        private async void OnTwentyPercentClicked(object sender, EventArgs e)
        {
            // Ask user if they want to give generous tip
            bool answer = await DisplayAlert("Generøs Tip", "Vil du give en generøs tip på 20%?", "Yes", "No");
            
            // Only set to 20% if user answered Yes
            if (answer)
            {
                TipModel.TipPct = 20;
            }
        }

        private void OnRoundDownClicked(object sender, EventArgs e)
        {
            TipModel.RoundDown();
        }

        private void OnRoundUpClicked(object sender, EventArgs e)
        {
            TipModel.RoundUp();
        }

        private async void OnCurrencyClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Vælg valuta", "Cancel", null, "Kroner (DK)", "Euro (EU)", "Dollars (US)");

            switch (action)
            {
                case "Kroner (DK)":
                    TipModel.CurrentCulture = CultureInfo.CreateSpecificCulture("da-DK");
                    break;
                case "Euro (EU)":
                    TipModel.CurrentCulture = CultureInfo.CreateSpecificCulture("de-DE");
                    break;
                case "Dollars (US)":
                    TipModel.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                    break;
                default:
                    return; // User cancelled
            }
        }
    }
}
