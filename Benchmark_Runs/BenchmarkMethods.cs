using BenchmarkDotNet.Attributes;
using System.Collections.Frozen;
using System.Globalization;

namespace Benchmark_Runs
{
    public class BenchmarkMethods
    {

        private int[] _newnumbers;
        private string[] _numbers;
        [GlobalSetup]
        public void Setup()
        {

            var random = new Random();
            _newnumbers = new int[10000];
            _numbers = new string[10000];
            for (int i = 0; i < 2000; i++)
            {
                _numbers[i] = random.Next(1, 10000).ToString();
                _newnumbers[i] = random.Next(1, 10000);
            }
            for (int i = 2000; i < 4000; i++)
            {
                _numbers[i] = random.Next(10000, 10000000).ToString();
                _newnumbers[i] = random.Next(10000, 10000000);
            }
            for (int i = 4000; i < _numbers.Length; i++)
            {
                _numbers[i] = random.Next(100000000, 999999999).ToString();
                _newnumbers[i] = random.Next(100000000, 999999999);
            }
        }


        /// <summary>
        /// Benchmark of converter with recursion
        /// </summary>
        //private static readonly string[] Numbers = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

        //// Define all tens from 0 to 90
        //private static readonly string[] Tens = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        private static readonly Dictionary<int, string> Numbers = new Dictionary<int, string>
    {
               { 0, "Zero" }, { 1, "One" }, { 2, "Two" }, { 3, "Three" }, { 4, "Four" },
        { 5, "Five" }, { 6, "Six" }, { 7, "Seven" }, { 8, "Eight" }, { 9, "Nine" },
            { 10, "Ten" }, { 11, "Eleven" }, { 12, "Twelve" }, { 13, "Thirteen" }, { 14, "Fourteen" },
        { 15, "Fifteen" }, { 16, "Sixteen" }, { 17, "Seventeen" }, { 18, "Eighteen" }, { 19, "Nineteen" }
    };

        private static readonly Dictionary<int, string> Tens = new Dictionary<int, string>
    {
         { 0, "Zero" },{1,"Ten" },{ 2, "Twenty" }, { 3, "Thirty" }, { 4, "Forty" }, { 5, "Fifty" }, { 6, "Sixty" },
        { 7, "Seventy" }, { 8, "Eighty" }, { 9, "Ninety" }
    };


        [Benchmark]
        public void RecursionToBenchmark()
        {
            foreach (var number in _numbers)
            {
                ConvertToWords(number);
            }

        }

        [Benchmark]
        public void ConverternNoRecursionToBenchmark()
        {

            foreach (var number in _numbers)
            {
                AmountToWordsNoRec(number);
            }
        }

        [Benchmark]
        public void ConverterChatGPTToBenchmark()
        {

            foreach (var number in _newnumbers)
            {
                ChatGPTConvert(number);
            }
        }

        public string ConvertToWords(string amount)
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

            var result = dollars > 0 && cents > 0
                ? $"{dollarWords} and {centWords}"
                : dollars > 0
                    ? dollarWords
                    : centWords;

            return result;
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
        /// Recursive function to Convert number to Words. Covered  "million", "thousand" and "hundred" as per requirements
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






        ////////////NoRecursion method with Frozen

        private static readonly FrozenDictionary<int, string> NumbersNoRec = new Dictionary<int, string>
            {
                { 0, "zero" }, { 1, "one" }, { 2, "two" }, { 3, "three" }, { 4, "four" },
                { 5, "five" }, { 6, "six" }, { 7, "seven" }, { 8, "eight" }, { 9, "nine" },
                { 10, "ten" }, { 11, "eleven" }, { 12, "twelve" }, { 13, "thirteen" }, { 14, "fourteen" },
                { 15, "fifteen" }, { 16, "sixteen" }, { 17, "seventeen" }, { 18, "eighteen" }, { 19, "nineteen" }
            }.ToFrozenDictionary();

        // Define all tens from 20 to 90
        private static readonly FrozenDictionary<int, string> TensNoRec = new Dictionary<int, string>
            {
                { 2, "twenty" }, { 3, "thirty" }, { 4, "forty" }, { 5, "fifty" }, { 6, "sixty" },
                { 7, "seventy" }, { 8, "eighty" }, { 9, "ninety" }
            }.ToFrozenDictionary();

        public string AmountToWordsNoRec(string amount)
        {
            try
            {
                // Check if number contains white spaces and remove them if any
                amount = amount.Replace(" ", "");

                // Split the amount into dollars and cents
                var parts = amount.Split(',');

                if (parts.Length > 2 || amount == string.Empty)
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

                var dollarWords = ConvertNumberToWordsNoRec(dollars) + (dollars == 1 ? " dollar" : " dollars");
                var centWords = ConvertNumberToWordsNoRec(cents) + (cents == 1 ? " cent" : " cents");


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

        private string ConvertNumberToWordsNoRec(int number)
        {
            if (number == 0)
                return NumbersNoRec[0];

            var words = new List<string>();

            if (number >= 1000000)
            {
                words.Add(ConvertHundredToWordsNoRec(number / 1000000));
                words.Add("million");
                number %= 1000000;
            }

            if (number >= 1000)
            {
                words.Add(ConvertHundredToWordsNoRec(number / 1000));
                words.Add("thousand");
                number %= 1000;
            }

            if (number >= 100)
            {
                words.Add(ConvertHundredToWordsNoRec(number / 100));
                words.Add("hundred");
                number %= 100;
            }

            if (number > 0)
            {
                words.Add(ConvertTensAndUnitsToWordsNoRec(number));
            }

            return string.Join(" ", words);
        }

        private string ConvertHundredToWordsNoRec(int number)
        {
            var words = new List<string>();

            if (number >= 100)
            {
                words.Add(NumbersNoRec[number / 100]);
                words.Add("hundred");
                number %= 100;
            }

            if (number > 0)
            {
                words.Add(ConvertTensAndUnitsToWordsNoRec(number));
            }

            return string.Join(" ", words);
        }

        private string ConvertTensAndUnitsToWordsNoRec(int number)
        {
            var words = new List<string>();

            if (number >= 20)
            {
                words.Add(TensNoRec[number / 10]);
                number %= 10;
            }

            if (number > 0)
            {
                words.Add(NumbersNoRec[number]);
            }

            return string.Join("-", words);
        }

        /////ChatGPTMEthod/////////////////////////

        private static readonly string[] Units = {
        "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine",
        "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
    };

        private static readonly string[] Ten = {
        "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
    };

        private static readonly string[] ThousandsGroups = {
        "", "Thousand", "Million"
    };

        public static string ChatGPTConvert(int number)
        {
            if (number == 0)
                return "Zero";

            var words = "";

            int[] digitGroups = new int[3];
            int positiveNumber = Math.Abs(number);

            for (int i = 0; i < 3; i++)
            {
                digitGroups[i] = positiveNumber % 1000;
                positiveNumber /= 1000;
            }

            for (int i = 2; i >= 0; i--)
            {
                if (digitGroups[i] != 0)
                {
                    words += ConvertThreeDigitNumber(digitGroups[i]) + " " + ThousandsGroups[i] + " ";
                }
            }

            return words.Trim();
        }

        private static string ConvertThreeDigitNumber(int number)
        {
            string hundreds = "";
            string tensUnits = "";

            if (number >= 100)
            {
                hundreds = Units[number / 100] + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (number < 20)
                {
                    tensUnits = Units[number];
                }
                else
                {
                    tensUnits = Ten[number / 10];
                    if ((number % 10) > 0)
                    {
                        tensUnits += "-" + Units[number % 10];
                    }
                }
            }

            return (hundreds + tensUnits).Trim();
        }

    }
}
