using MauiHello.Views;

namespace MauiHello
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.NotePage), typeof(Views.NotePage));
            Routing.RegisterRoute("DetailsPage", typeof(DetailsPage));
            Routing.RegisterRoute(nameof(Views.MonkeyDetailsPage), typeof(Views.MonkeyDetailsPage));
        }
    }
}
