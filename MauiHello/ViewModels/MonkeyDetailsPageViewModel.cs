using MauiHello.Models;

namespace MauiHello.ViewModels
{
    [QueryProperty(nameof(Monkey), "MyMonkey")]
    public class MonkeyDetailsPageViewModel : BaseViewModel
    {
        private Monkey _monkey = new();

        public Monkey Monkey
        {
            get => _monkey;
            set => SetProperty(ref _monkey, value);
        }
    }
}