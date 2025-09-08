using MauiHello.ViewModels;

namespace MauiHello.Views
{
    public partial class MonkeyDetailsPage : ContentPage
    {
        public MonkeyDetailsPage(MonkeyDetailsPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}