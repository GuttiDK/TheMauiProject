using Microsoft.Maui.Controls;

namespace MauiHello.Views
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void AboutToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage(), true); // animated navigation
        }

        private async void FeedbackToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new FeedbackPage(), true); // animated modal navigation
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
