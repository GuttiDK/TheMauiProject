using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using MauiHello.Models;
using MauiHello.Services;

namespace MauiHello.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly MonkeyService _monkeyService;
        private Command _getMonkeysCommand;
        private Command<Monkey> _goToDetailsCommand;

        public ObservableCollection<Monkey> Monkeys { get; } = [];

        public ICommand GetMonkeysCommand => _getMonkeysCommand ??= new Command(async () => await GetMonkeysAsync());

        public ICommand GoToDetailsCommand => _goToDetailsCommand ??= new Command<Monkey>(async (monkey) =>
        {
            if (monkey == null)
                return;

            await Shell.Current.GoToAsync(nameof(Views.MonkeyDetailsPage), true, new Dictionary<string, object>
            {
                {"MyMonkey", monkey }
            });
        });

        public MainPageViewModel(MonkeyService monkeyService)
        {
            _monkeyService = monkeyService;
            Title = "Monkey Finder";
        }

        public async Task GetMonkeysAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var monkeys = await _monkeyService.GetMonkeys();

                if (Monkeys.Count != 0)
                    Monkeys.Clear();

                foreach (var monkey in monkeys)
                    Monkeys.Add(monkey);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false; // Stop the refresh indicator
            }
        }
    }
}