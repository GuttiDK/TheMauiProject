using Microsoft.Maui.Controls;
using MauiHello.ViewModels;

namespace MauiHello.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void AboutToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage(), true);
        }

        private async void FeedbackToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new FeedbackPage(), true);
        }

        private async void SettingsToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage(), true);
        }
    }
}
