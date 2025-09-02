using Microsoft.Maui.Controls;

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
            // Here you could save feedback to a database or send to a server
            confirmationLabel.Text = "Thank you for your feedback!";
            await Task.Delay(1500);
            confirmationLabel.Text = "";
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true); // animated back
        }
    }
}