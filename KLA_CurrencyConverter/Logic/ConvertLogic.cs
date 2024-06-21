using System.Globalization;

namespace KLA_NumberConverter_ServerSide.Logic
{
    public class ConvertLogic
    {
        // Define all numbers from 0 to 19
        private static readonly Dictionary<int, string> Numbers = new Dictionary<int, string>
            {
                { 0, "zero" }, { 1, "one" }, { 2, "two" }, { 3, "three" }, { 4, "four" },
                { 5, "five" }, { 6, "six" }, { 7, "seven" }, { 8, "eight" }, { 9, "nine" },
                { 10, "ten" }, { 11, "eleven" }, { 12, "twelve" }, { 13, "thirteen" }, { 14, "fourteen" },
                { 15, "fifteen" }, { 16, "sixteen" }, { 17, "seventeen" }, { 18, "eighteen" }, { 19, "nineteen" }
            };

        // Define all tens from 20 to 90
        private static readonly Dictionary<int, string> Tens = new Dictionary<int, string>
            {
                { 2, "twenty" }, { 3, "thirty" }, { 4, "forty" }, { 5, "fifty" }, { 6, "sixty" },
                { 7, "seventy" }, { 8, "eighty" }, { 9, "ninety" }
            };

        public string AmountToWords(string amount)
        {
            try
            {
                // Check if number contains white spaces and remove them if any
                amount = amount.Replace(" ", "");

                // Split the amount into dollars and cents
                var parts = amount.Split(',');

                if (parts.Length > 2)
                {
                    throw new Exception("Invalid amount format.");
                }

                // Find dollars value
                var dollars = int.Parse(parts[0], NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
                // ensures that cents are properly formatted with two digits (for example 10,5 = 10,50) 
                var cents = parts.Length > 1 ? int.Parse(parts[1].PadRight(2, '0')) : 0;

                // Ensure dollars and cents are within valid ranges
                if (dollars > 999999999)
                {
                    throw new Exception("Dollars part exceeds the maximum allowed value of 999 999 999");
                }

                if (cents > 99)
                {
                    throw new Exception("Cents part exceeds the maximum allowed value of 99");
                }

                var dollarWords = NumberToWords(dollars) + (dollars == 1 ? " dollar" : " dollars");
                var centWords = NumberToWords(cents) + (cents == 1 ? " cent" : " cents");

                var result = cents > 0
                            ? $"{dollarWords} and {centWords}"
                            : dollarWords;
                return result;
            }
            catch (Exception)
            {
                throw new Exception("Invalid amount format.");
            }
        }

        /// <summary>
        /// Base function to Convert Numbers to Words
        /// </summary>
        /// <param name="number"></param>
        /// <returns>String representation of provided number by calling ConvertNumberToWords()</returns>
        private string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            return ConvertNumberToWords(number).Trim();
        }

        /// <summary>
        /// Recursive function to Convert number to Words. Covered "million", "thousand" and "hundred" as per requirements
        /// </summary>
        /// <param name="number"></param>
        /// <returns>String representation of provided number</returns>
        private string ConvertNumberToWords(int number)
        {
            if (number < 20)
                return Numbers[number];

            if (number < 100)
                return Tens[number / 10] + (number % 10 > 0 ? "-" + Numbers[number % 10] : "");

            if (number < 1000)
                return Numbers[number / 100] + " hundred" + (number % 100 > 0 ? " " + ConvertNumberToWords(number % 100) : "");

            if (number < 1000000)
                return ConvertNumberToWords(number / 1000) + " thousand" + (number % 1000 > 0 ? " " + ConvertNumberToWords(number % 1000) : "");

            return ConvertNumberToWords(number / 1000000) + " million" + (number % 1000000 > 0 ? " " + ConvertNumberToWords(number % 1000000) : "");
        }


    }
}
