using System.ComponentModel;
using System.Runtime.CompilerServices;
using MauiHello.Services;

namespace MauiHello.Views
{
    public partial class SettingsPage : ContentPage, INotifyPropertyChanged
    {
        private double _defaultTipPercent;
        private double _currentDefaultTip;

        public double DefaultTipPercent
        {
            get => _defaultTipPercent;
            set
            {
                if (_defaultTipPercent != value)
                {
                    _defaultTipPercent = value;
                    OnPropertyChanged();
                }
            }
        }

        public double CurrentDefaultTip
        {
            get => _currentDefaultTip;
            set
            {
                if (_currentDefaultTip != value)
                {
                    _currentDefaultTip = value;
                    OnPropertyChanged();
                }
            }
        }

        public SettingsPage()
        {
            InitializeComponent();
            LoadCurrentSettings();
            BindingContext = this;
        }

        private void LoadCurrentSettings()
        {
            var savedTip = SettingsService.GetDefaultTipPercent();
            DefaultTipPercent = savedTip;
            CurrentDefaultTip = savedTip;
            
            // Reset the switch to off when loading
            resetSwitch.IsToggled = false;
        }

        private void OnTipSliderChanged(object sender, ValueChangedEventArgs e)
        {
            DefaultTipPercent = Math.Round(e.NewValue);
            tipSlider.Value = DefaultTipPercent; // Snap to whole numbers
        }

        private void OnPresetTipClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is string parameter)
            {
                if (double.TryParse(parameter, out double tipValue))
                {
                    DefaultTipPercent = tipValue;
                    tipSlider.Value = tipValue;
                }
            }
        }

        private async void OnResetSwitchToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value) // Switch is turned ON
            {
                bool confirm = await DisplayAlert(
                    "Nulstil til Standard", 
                    "Vil du nulstille tip procenten til 15%?", 
                    "Ja", 
                    "Nej");

                if (confirm)
                {
                    DefaultTipPercent = 15.0;
                    tipSlider.Value = 15.0;
                    SettingsService.ResetToDefaultTipPercent();
                    CurrentDefaultTip = 15.0;
                    
                    await DisplayAlert("Nulstillet", "Tip procent er nulstillet til 15%", "OK");
                }
                
                // Always turn the switch back off after action
                resetSwitch.IsToggled = false;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                SettingsService.SetDefaultTipPercent(DefaultTipPercent);
                CurrentDefaultTip = DefaultTipPercent;
                
                await DisplayAlert("Gemt", $"Standard tip er sat til {DefaultTipPercent:F0}%", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fejl", $"Kunne ikke gemme indstillingerne: {ex.Message}", "OK");
            }
        }

        public new event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}