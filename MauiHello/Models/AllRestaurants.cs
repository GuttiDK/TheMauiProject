using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiHello.Models
{
    public class AllRestaurants : INotifyPropertyChanged
    {
        public ObservableCollection<Restaurant> Restaurants { get; set; }

        public bool HasNoRestaurants => Restaurants.Count == 0;

        public AllRestaurants()
        {
            Restaurants = new ObservableCollection<Restaurant>
            {
                new Restaurant
                {
                    Name = "BarneGarden, Copenhagen",
                    ImageUrl = "https://images.unsplash.com/photo-1517248135467-4c7edcad34c4?w=400&h=240&fit=crop&auto=format",
                    TipPct = 20
                },
                new Restaurant
                {
                    Name = "Geranium, Copenhagen", 
                    ImageUrl = "https://images.unsplash.com/photo-1414235077428-338989a2e8c0?w=400&h=240&fit=crop&auto=format",
                    TipPct = 15
                },
                new Restaurant
                {
                    Name = "Restaurant Ark",
                    ImageUrl = "https://images.unsplash.com/photo-1550966871-3ed3cdb5ed0c?w=400&h=240&fit=crop&auto=format",
                    TipPct = 10
                },
                new Restaurant
                {
                    Name = "Bistro Lupa, Copenhagen",
                    ImageUrl = "https://images.unsplash.com/photo-1559329007-40df8a9345d8?w=400&h=240&fit=crop&auto=format",
                    TipPct = 15
                },
                new Restaurant
                {
                    Name = "Stick'n'Sushi, all of Denmark",
                    ImageUrl = "https://images.unsplash.com/photo-1579584425555-c3ce17fd4351?w=400&h=240&fit=crop&auto=format",
                    TipPct = 5
                }
            };

            // Subscribe to collection changes to update empty state
            Restaurants.CollectionChanged += (s, e) => OnPropertyChanged(nameof(HasNoRestaurants));
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            Restaurants.Add(restaurant);
        }

        public void DeleteRestaurant(Restaurant restaurant)
        {
            Restaurants.Remove(restaurant);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}