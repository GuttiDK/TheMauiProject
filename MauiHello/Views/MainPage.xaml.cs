using Microsoft.Maui.Controls;
using MauiHello.ViewModels;

namespace MauiHello.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        private async void AboutToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage(), true); // animated navigation
        }

        private async void FeedbackToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new FeedbackPage(), true); // animated modal navigation
        }

        private async void SettingsToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage(), true); // animated navigation
        }

        private async void OnCounterClicked(object? sender, EventArgs e)
        {
            await _viewModel.GetMonkeysAsync();
        }
    }
}
