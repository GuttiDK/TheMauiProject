using System.Globalization;
using System.Text.RegularExpressions;

namespace MauiHello.Converters
{
    /// <summary>
    /// Konverterer et tal til en procentværdi med % tegn
    /// </summary>
    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                // Parameter kan bruges til at specificere antal decimaler
                int decimals = 0;
                if (parameter is string paramString && int.TryParse(paramString, out int paramDecimals))
                {
                    decimals = paramDecimals;
                }

                return $"{doubleValue.ToString($"F{decimals}", culture)}%";
            }
            
            if (value is int intValue)
            {
                return $"{intValue}%";
            }

            return "0%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                string cleanValue = stringValue.Replace("%", "").Trim();
                if (double.TryParse(cleanValue, out double result))
                {
                    return result;
                }
            }
            return 0.0;
        }
    }

    /// <summary>
    /// Sikrer at beløb ikke kan være negative og formaterer dem korrekt
    /// </summary>
    public class PositiveAmountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                // Fjern negative tegn og formatér korrekt
                string cleanValue = stringValue.Replace("-", "").Trim();
                
                if (decimal.TryParse(cleanValue, out decimal amount))
                {
                    // Sørg for at beløbet er positivt
                    amount = Math.Max(0, amount);
                    return amount.ToString("N2", culture);
                }
            }
            return "0,00";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                string cleanValue = stringValue.Replace("-", "").Trim();
                if (decimal.TryParse(cleanValue, out decimal amount))
                {
                    return Math.Max(0, amount).ToString("F2", culture);
                }
            }
            return "0,00";
        }
    }

    /// <summary>
    /// Formaterer valuta med farver baseret på beløb
    /// </summary>
    public class CurrencyColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string currencyString && !string.IsNullOrEmpty(currencyString))
            {
                // Forsøg at parse valuta til decimal
                string cleanValue = currencyString.Replace("kr", "").Replace("€", "").Replace("$", "").Trim();
                if (decimal.TryParse(cleanValue, out decimal amount))
                {
                    if (amount == 0)
                        return Colors.Gray;
                    else if (amount > 0 && amount < 50)
                        return Colors.Green;
                    else if (amount >= 50 && amount < 200)
                        return Colors.Blue;
                    else
                        return Colors.Red; // Høje beløb
                }
            }
            return Colors.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Konverterer slider værdi til en mere læsbar format
    /// </summary>
    public class SliderValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                // Rund af til nærmeste heltal for bedre brugeroplevelse
                int roundedValue = (int)Math.Round(doubleValue);
                return $"Tip: {roundedValue}%";
            }
            return "Tip: 0%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Validerer og formaterer beløb input
    /// </summary>
    public class BillAmountValidator : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() ?? "0,00";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string input)
            {
                if (string.IsNullOrEmpty(input))
                    return "0,00";

                // Fjern negative tegn
                input = input.Replace("-", "");

                // Kun tillade cifre og decimal separator
                string decimalSeparator = culture.NumberFormat.NumberDecimalSeparator;
                string pattern = $"[^0-9{Regex.Escape(decimalSeparator)}]";
                input = Regex.Replace(input, pattern, "");

                // Forhindre flere decimal separatorer
                int firstSeparator = input.IndexOf(decimalSeparator);
                if (firstSeparator >= 0)
                {
                    int lastSeparator = input.LastIndexOf(decimalSeparator);
                    if (lastSeparator != firstSeparator)
                    {
                        input = input.Remove(lastSeparator, 1);
                    }
                }

                // Hvis tomt, sæt til 0,00
                if (string.IsNullOrWhiteSpace(input))
                    input = "0,00";

                return input;
            }
            return "0,00";
        }
    }

    /// <summary>
    /// Generisk converter der kan konvertere boolean værdier til enhver objekttype
    /// Bruges til at vælge mellem to forskellige objekter baseret på en boolean værdi
    /// </summary>
    /// <typeparam name="T">Typen af objekter der skal vælges mellem</typeparam>
    public class BoolToObjectConverter<T> : IValueConverter
    {
        /// <summary>
        /// Objektet der returneres når værdien er true
        /// </summary>
        public T TrueObject { get; set; }

        /// <summary>
        /// Objektet der returneres når værdien er false
        /// </summary>
        public T FalseObject { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? TrueObject : FalseObject;
            }
            return FalseObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("BoolToObjectConverter does not support ConvertBack");
        }
    }
}