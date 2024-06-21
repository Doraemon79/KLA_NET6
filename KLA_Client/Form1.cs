using System.Text.Json;

namespace NumberConverterDesktop
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _httpClient;
        public Form1()
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5257/") };
        }

        private async void convertButton_Click(object sender, EventArgs e)
        {
            errorLabel.Text = ""; // Clear previous errors
            resultTextBox.Text = ""; // Clear previous results
            string amount = inputTextBox.Text;

            try
            {
                string response = await ConvertAmount(amount);
                resultTextBox.Text = response;
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message;
            }
        }

        private async Task<string> ConvertAmount(string amount)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/currency/convert?amount={amount}");

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent);
            return data["words"];
        }
    }
}
