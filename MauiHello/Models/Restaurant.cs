using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiHello.Models
{
    public class Restaurant : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private string _imageUrl = string.Empty;
        private double _tipPct;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                if (_imageUrl != value)
                {
                    _imageUrl = value;
                    OnPropertyChanged();
                }
            }
        }

        public double TipPct
        {
            get => _tipPct;
            set
            {
                if (_tipPct != value)
                {
                    _tipPct = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TipPctDisplay));
                }
            }
        }

        public string TipPctDisplay => $"Tip Percent: {(int)TipPct}%";

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}