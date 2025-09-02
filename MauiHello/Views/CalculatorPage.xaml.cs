using System;
using Microsoft.Maui.Controls;

namespace MauiHello.Views
{
    public partial class CalculatorPage : ContentPage
    {
        double _currentValue = 0;
        double _lastValue = 0;
        string _operator = "";
        bool _isNewEntry = true;

        public CalculatorPage()
        {
            InitializeComponent();
            displayLabel.Text = "0";
        }

        private void OnDigitClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            string digit = button.Text;

            if (_isNewEntry || displayLabel.Text == "0")
            {
                displayLabel.Text = digit;
                _isNewEntry = false;
            }
            else
            {
                displayLabel.Text += digit;
            }
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            displayLabel.Text = "0";
            _currentValue = 0;
            _lastValue = 0;
            _operator = "";
            _isNewEntry = true;
        }

        private void OnSignClicked(object sender, EventArgs e)
        {
            if (double.TryParse(displayLabel.Text, out double value))
            {
                value = -value;
                displayLabel.Text = value.ToString();
            }
        }

        private void OnPercentClicked(object sender, EventArgs e)
        {
            if (double.TryParse(displayLabel.Text, out double value))
            {
                value = value / 100;
                displayLabel.Text = value.ToString();
                _isNewEntry = true;
            }
        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            SetOperator("+");
        }

        private void OnSubtractClicked(object sender, EventArgs e)
        {
            SetOperator("-");
        }

        private void OnMultiplyClicked(object sender, EventArgs e)
        {
            SetOperator("*");
        }

        private void OnDivideClicked(object sender, EventArgs e)
        {
            SetOperator("/");
        }

        private void SetOperator(string op)
        {
            if (double.TryParse(displayLabel.Text, out double value))
            {
                _lastValue = value;
                _operator = op;
                _isNewEntry = true;
            }
        }

        private void OnEqualsClicked(object sender, EventArgs e)
        {
            if (double.TryParse(displayLabel.Text, out double value))
            {
                _currentValue = value;
                double result = _lastValue;

                switch (_operator)
                {
                    case "+": result += _currentValue; break;
                    case "-": result -= _currentValue; break;
                    case "*": result *= _currentValue; break;
                    case "/": result = _currentValue != 0 ? result / _currentValue : 0; break;
                }

                displayLabel.Text = result.ToString();
                _lastValue = result;
                _isNewEntry = true;
                _operator = "";
            }
        }

        private void OnDecimalClicked(object sender, EventArgs e)
        {
            if (_isNewEntry)
            {
                displayLabel.Text = "0.";
                _isNewEntry = false;
            }
            else if (!displayLabel.Text.Contains("."))
            {
                displayLabel.Text += ".";
            }
        }
    }
}