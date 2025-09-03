using Microsoft.Maui.Controls;
using MauiHello.Models;

namespace MauiHello.Views
{
    [QueryProperty(nameof(Name), "name")]
    [QueryProperty(nameof(Address), "address")]
    [QueryProperty(nameof(Age), "age")]
    public partial class DetailsPage : ContentPage
    {
        private string _name = string.Empty;
        private string _address = string.Empty;
        private string _age = string.Empty;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                UpdateDisplay();
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                UpdateDisplay();
            }
        }

        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                UpdateDisplay();
            }
        }

        public DetailsPage()
        {
            InitializeComponent();
        }

        private void UpdateDisplay()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                welcomeLabel.Text = $"Welcome, {Name}!";
                nameLabel.Text = $"Name: {Name}";
            }

            if (!string.IsNullOrEmpty(Address))
            {
                addressLabel.Text = $"Address: {Address}";
            }

            if (!string.IsNullOrEmpty(Age))
            {
                ageLabel.Text = $"Age: {Age}";
            }
        }

        private async void OnGoBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}