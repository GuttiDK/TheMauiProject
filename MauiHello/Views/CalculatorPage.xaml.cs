using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MauiHello.Views
{
    public partial class CalculatorPage : ContentPage
    {
        double _currentValue = 0;
        double _lastValue = 0;
        string _operator = "";
        bool _isNewEntry = true;
        readonly CultureInfo _culture = new CultureInfo("da-DK");
        string _expression = "";

        public CalculatorPage()
        {
            InitializeComponent();
            displayLabel.Text = "0";
            expressionLabel.Text = "";
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

            UpdateExpression();
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            displayLabel.Text = "0";
            _currentValue = 0;
            _lastValue = 0;
            _operator = "";
            _isNewEntry = true;
            _expression = "";
            expressionLabel.Text = "";
        }

        private void OnSignClicked(object sender, EventArgs e)
        {
            if (double.TryParse(displayLabel.Text.Replace('.', ','), NumberStyles.Any, _culture, out double value))
            {
                value = -value;
                displayLabel.Text = value.ToString(_culture);
            }
            UpdateExpression();
        }

        private void OnPercentClicked(object sender, EventArgs e)
        {
            if (double.TryParse(displayLabel.Text.Replace('.', ','), NumberStyles.Any, _culture, out double value))
            {
                value = value / 100;
                displayLabel.Text = value.ToString(_culture);
                _isNewEntry = true;
            }
            UpdateExpression();
        }

        private void OnAddClicked(object sender, EventArgs e) => SetOperator("+", "+");
        private void OnSubtractClicked(object sender, EventArgs e) => SetOperator("-", "−");
        private void OnMultiplyClicked(object sender, EventArgs e) => SetOperator("*", "×");
        private void OnDivideClicked(object sender, EventArgs e) => SetOperator("/", "÷");

        private void SetOperator(string op, string opSymbol)
        {
            if (double.TryParse(displayLabel.Text.Replace('.', ','), NumberStyles.Any, _culture, out double value))
            {
                if (!string.IsNullOrEmpty(_operator))
                {
                    _lastValue = Calculate(_lastValue, value, _operator);
                    displayLabel.Text = _lastValue.ToString(_culture);
                }
                else
                {
                    _lastValue = value;
                }
                _operator = op;
                _isNewEntry = true;
                _expression = $"{_lastValue.ToString(_culture)} {opSymbol} ";
                expressionLabel.Text = _expression;
            }
        }

        private void OnEqualsClicked(object sender, EventArgs e)
        {
            if (double.TryParse(displayLabel.Text.Replace('.', ','), NumberStyles.Any, _culture, out double value))
            {
                _currentValue = value;
                double result = Calculate(_lastValue, _currentValue, _operator);

                displayLabel.Text = result.ToString(_culture);
                _expression = $"{_lastValue.ToString(_culture)} {GetOperatorSymbol(_operator)} {_currentValue.ToString(_culture)}";
                expressionLabel.Text = _expression;
                _lastValue = result;
                _isNewEntry = true;
                _operator = "";
            }
        }

        private void OnDecimalClicked(object sender, EventArgs e)
        {
            string decimalSeparator = _culture.NumberFormat.NumberDecimalSeparator;
            if (_isNewEntry)
            {
                displayLabel.Text = "0" + decimalSeparator;
                _isNewEntry = false;
            }
            else if (!displayLabel.Text.Contains(decimalSeparator))
            {
                displayLabel.Text += decimalSeparator;
            }
            UpdateExpression();
        }

        private double Calculate(double left, double right, string op)
        {
            return op switch
            {
                "+" => left + right,
                "-" => left - right,
                "*" => left * right,
                "/" => right != 0 ? left / right : 0,
                _ => right
            };
        }

        private string GetOperatorSymbol(string op) => op switch
        {
            "+" => "+",
            "-" => "−",
            "*" => "×",
            "/" => "÷",
            _ => ""
        };

        private void UpdateExpression()
        {
            if (!string.IsNullOrEmpty(_operator))
            {
                expressionLabel.Text = $"{_lastValue.ToString(_culture)} {GetOperatorSymbol(_operator)} {displayLabel.Text}";
            }
            else
            {
                expressionLabel.Text = "";
            }
        }
    }
}