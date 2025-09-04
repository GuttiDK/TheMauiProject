using Microsoft.Maui.Controls;
using MauiHello.Models;

namespace MauiHello.Views
{
    public partial class FeedbackPage : ContentPage
    {
        public FeedbackPage()
        {
            InitializeComponent();
            ratingSlider.ValueChanged += OnRatingChanged;
        }

        private void OnRatingChanged(object sender, ValueChangedEventArgs e)
        {
            ratingLabel.Text = $"Rating: {e.NewValue:F0}";
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            // Validate person information
            if (ValidatePersonInput())
            {
                confirmationLabel.Text = "Thank you for your feedback!";
                await Task.Delay(1500);
                confirmationLabel.Text = "";
            }
        }

        private async void OnGoToDetailsSimpleClicked(object sender, EventArgs e)
        {
            // Use the name from the input field
            string userName = nameEntry.Text?.Trim();
            
            if (string.IsNullOrEmpty(userName))
            {
                await DisplayAlert("Error", "Please enter a name", "OK");
                return;
            }
            
            await Shell.Current.GoToAsync($"DetailsPage?name={userName}");
        }

        private async void OnGoToDetailsWithPersonClicked(object sender, EventArgs e)
        {
            // Validate input before creating person
            if (!ValidatePersonInput())
            {
                return;
            }

            // Create a Person object with user input
            var person = CreatePersonFromInput();
            
            if (person != null)
            {
                // Navigate with individual person properties - no URL encoding needed
                await Shell.Current.GoToAsync($"DetailsPage?name={person.Name}&address={person.Address}&age={person.Age}");
            }
        }

        private bool ValidatePersonInput()
        {
            string name = nameEntry.Text?.Trim();
            string address = addressEntry.Text?.Trim();
            string ageText = ageEntry.Text?.Trim();

            if (string.IsNullOrEmpty(name))
            {
                DisplayAlert("Error", "Please enter a name", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(address))
            {
                DisplayAlert("Error", "Please enter an address", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(ageText) || !int.TryParse(ageText, out int age) || age <= 0 || age > 150)
            {
                DisplayAlert("Error", "Please enter a valid age (1-150)", "OK");
                return false;
            }

            return true;
        }

        private Person? CreatePersonFromInput()
        {
            try
            {
                string name = nameEntry.Text?.Trim() ?? string.Empty;
                string address = addressEntry.Text?.Trim() ?? string.Empty;
                
                if (int.TryParse(ageEntry.Text?.Trim(), out int age))
                {
                    return new Person(name, address, age);
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"Failed to create person: {ex.Message}", "OK");
            }

            return null;
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true); // animated back
        }
    }
}