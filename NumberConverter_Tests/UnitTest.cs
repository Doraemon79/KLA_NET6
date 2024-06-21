using KLA_NumberConverter_ServerSide.Logic;

namespace NumberConverter_Tests
{
    public class ConvertLogicTests
    {
        private readonly ConvertLogic _convertLogic;

        public ConvertLogicTests()
        {
            _convertLogic = new ConvertLogic();
        }

        [Theory]
        [InlineData("123", "one hundred twenty-three dollars")]
        [InlineData("1001", "one thousand one dollars")]
        [InlineData("1000000", "one million dollars")]
        [InlineData("123123", "one hundred twenty-three thousand one hundred twenty-three dollars")]
        [InlineData("0", "zero dollars")]
        [InlineData("19", "nineteen dollars")]
        [InlineData("21", "twenty-one dollars")]
        [InlineData("105", "one hundred five dollars")]
        [InlineData("999999999", "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars")]
        public void AmountToWords_ValidAmount_ReturnsCorrectWords(string amount, string expectedWords)
        {
            // Act
            var result = _convertLogic.AmountToWords(amount);

            // Assert
            Assert.Equal(expectedWords, result);
        }

        [Theory]
        [InlineData("123,45", "one hundred twenty-three dollars and forty-five cents")]
        [InlineData("0,99", "zero dollars and ninety-nine cents")]
        [InlineData("1,01", "one dollar and one cent")]
        [InlineData("1000,50", "one thousand dollars and fifty cents")]
        public void AmountToWords_ValidAmountWithCents_ReturnsCorrectWords(string amount, string expectedWords)
        {
            // Act
            var result = _convertLogic.AmountToWords(amount);

            // Assert
            Assert.Equal(expectedWords, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("10000000000")] // Exceeds maximum allowed value
        [InlineData("1000,1000,1000")] // Invalid format
        [InlineData("abcd")] // Invalid number
        public void AmountToWords_InvalidAmount_ThrowsException(string amount)
        {
            // Act & Assert
            Assert.Throws<Exception>(() => _convertLogic.AmountToWords(amount));
        }
    }
}