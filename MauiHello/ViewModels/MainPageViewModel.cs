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

        public ObservableCollection<Monkey> Monkeys { get; } = new();

        public ICommand GetMonkeysCommand { get; }

        public MainPageViewModel(MonkeyService monkeyService)
        {
            _monkeyService = monkeyService;
            Title = "Monkey Finder";
            GetMonkeysCommand = new Command(async () => await GetMonkeysAsync());
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
            }
        }
    }
}