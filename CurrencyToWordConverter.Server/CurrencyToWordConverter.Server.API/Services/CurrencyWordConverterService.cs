using CurrencyToWordConverter.Server.API.Services.IService;

namespace CurrencyToWordConverter.Server.API.Services
{
    public class CurrencyWordConverterService : ICurrencyWordConverterService
    {
        private readonly Dictionary<decimal, string> NumberInWords = new Dictionary<decimal, string>
        {
            { 0, "zero" }, { 1, "one" }, { 2, "two" }, { 3, "three" }, { 4, "four" },
            { 5, "five" }, { 6, "six" }, { 7, "seven" }, { 8, "eight" }, { 9, "nine" },
            { 10, "ten" }, { 11, "eleven" }, { 12, "twelve" }, { 13, "thirteen" }, { 14, "fourteen" },
            { 15, "fifteen" }, { 16, "sixteen" }, { 17, "seventeen" }, { 18, "eighteen" }, { 19, "nineteen" },
            { 20, "twenty" }, { 30, "thirty" }, { 40, "forty" }, { 50, "fifty" },
            { 60, "sixty" }, { 70, "seventy" }, { 80, "eighty" }, { 90, "ninety" }
        };

        private readonly Dictionary<int, string> PlaceValuesOfNumber = new Dictionary<int, string>
        {
            { 1, "" }, { 1000, "thousand" }, { 1000000, "million" }
        };

        public async Task<string> CurrencyConversion(decimal currency)
        {
            int dollars = (int)Math.Floor(currency);
            int cents = (int)Math.Round((currency - dollars) * 100);

            string valueInWords = await ConvertCurrencyToWords(dollars) + " dollars";

            if (cents > 0)
                valueInWords += $" and {await ConvertCurrencyToWords(cents)} cents";

            if (valueInWords.Contains("one dollars"))
                valueInWords = valueInWords.Replace("one dollars", "one dollar");
            if (valueInWords.Contains("one cents"))
                valueInWords = valueInWords.Replace("one cents", "one cent");

            return valueInWords;
        }

        #region Internal Private Methods
        private async Task<string> ConvertCurrencyToWords(int currency)
        {
            if (currency == 0)
                return NumberInWords[0];

            string words = "";
            int placeValue = 1;

            while (currency > 0)
            {
                if (currency % 1000 != 0)
                    words = $"{await ConvertHundreds(currency % 1000)} {PlaceValuesOfNumber[placeValue]} {words}".Trim();

                currency /= 1000;
                placeValue *= 1000;
            }

            return words.Trim();
        }

        private async Task<string> ConvertHundreds(int currency)
        {
            if (currency == 0)
                return "";

            string words = "";

            if (currency >= 100)
            {
                words = $"{NumberInWords[currency / 100]} hundred ";
                currency %= 100;
            }

            if (currency > 0)
            {
                if (NumberInWords.ContainsKey(currency))
                {
                    words += NumberInWords[currency];
                }
                else
                {
                    words += $"{NumberInWords[currency - (currency % 10)]} {NumberInWords[currency % 10]}";
                }
            }

            return words.Trim();
        }
        #endregion
    }
}
