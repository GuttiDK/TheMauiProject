using Microsoft.Maui.Controls;
using System.Text.Json;
using MauiHello.Models;

namespace MauiHello.Views
{
    [QueryProperty(nameof(UserName), "name")]
    [QueryProperty(nameof(PersonData), "person")]
    public partial class DetailsPage : ContentPage
    {
        private string _userName = string.Empty;
        private string _personData = string.Empty;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                UpdateWelcomeMessage();
            }
        }

        public string PersonData
        {
            get => _personData;
            set
            {
                _personData = value;
                UpdatePersonInformation();
            }
        }

        public bool HasPersonData { get; private set; }

        public DetailsPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void UpdateWelcomeMessage()
        {
            if (!string.IsNullOrEmpty(UserName))
            {
                welcomeLabel.Text = $"Welcome, {UserName}!";
            }
        }

        private void UpdatePersonInformation()
        {
            if (!string.IsNullOrEmpty(PersonData))
            {
                try
                {
                    var person = JsonSerializer.Deserialize<Person>(PersonData);
                    if (person != null)
                    {
                        nameLabel.Text = $"Name: {person.Name}";
                        addressLabel.Text = $"Address: {person.Address}";
                        ageLabel.Text = $"Age: {person.Age}";
                        HasPersonData = true;
                        OnPropertyChanged(nameof(HasPersonData));
                    }
                }
                catch (JsonException)
                {
                    // Handle JSON parsing error
                    HasPersonData = false;
                    OnPropertyChanged(nameof(HasPersonData));
                }
            }
            else
            {
                HasPersonData = false;
                OnPropertyChanged(nameof(HasPersonData));
            }
        }

        private async void OnGoBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}