using MauiHello.Models;

namespace MauiHello.Views
{
    public partial class ShopPage : ContentPage
    {
        public AllRestaurants AllRestaurants { get; private set; }

        public ShopPage()
        {
            InitializeComponent();
            AllRestaurants = new AllRestaurants();
            BindingContext = AllRestaurants;
        }

        private async void OnAddRestaurantClicked(object sender, EventArgs e)
        {
            // Show a prompt to get restaurant details
            string name = await DisplayPromptAsync("Add Restaurant", 
                "Enter restaurant name:", 
                placeholder: "e.g. Noma, Copenhagen");
            
            if (string.IsNullOrWhiteSpace(name))
                return;

            string tipPctText = await DisplayPromptAsync("Add Restaurant", 
                "Enter tip percentage (0-30):", 
                initialValue: "15",
                keyboard: Keyboard.Numeric,
                placeholder: "15");
            
            if (!double.TryParse(tipPctText, out double tipPct) || tipPct < 0 || tipPct > 30)
            {
                await DisplayAlert("Error", "Please enter a valid tip percentage between 0 and 30", "OK");
                return;
            }

            // Create new restaurant with a random restaurant image
            var imageUrls = new[]
            {
                "https://images.unsplash.com/photo-1552566626-52f8b828add9?w=400&h=240&fit=crop&auto=format",
                "https://images.unsplash.com/photo-1517248135467-4c7edcad34c4?w=400&h=240&fit=crop&auto=format",
                "https://images.unsplash.com/photo-1414235077428-338989a2e8c0?w=400&h=240&fit=crop&auto=format",
                "https://images.unsplash.com/photo-1550966871-3ed3cdb5ed0c?w=400&h=240&fit=crop&auto=format"
            };

            var random = new Random();
            var randomImageUrl = imageUrls[random.Next(imageUrls.Length)];

            var newRestaurant = new Restaurant
            {
                Name = name,
                ImageUrl = randomImageUrl,
                TipPct = tipPct
            };

            AllRestaurants.AddRestaurant(newRestaurant);
            
            // Scroll to the new item
            await Task.Delay(100); // Give time for UI to update
            restaurantCollectionView.ScrollTo(newRestaurant, position: ScrollToPosition.End, animate: true);
        }

        private async void OnDeleteRestaurantClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Restaurant restaurant)
            {
                bool confirm = await DisplayAlert("Delete Restaurant", 
                    $"Are you sure you want to delete '{restaurant.Name}'?", 
                    "Delete", "Cancel");
                
                if (confirm)
                {
                    AllRestaurants.DeleteRestaurant(restaurant);
                }
            }
        }
    }
}