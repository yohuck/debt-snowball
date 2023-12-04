
using System.Net.Http;
using System.Text;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using debt_snowball.Data;
using System.Collections.Specialized;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Identity.Client;

namespace debt_snowball.Utilities
{
    public class SnowballProcessor
    {


        public async static Task<BaseSnowballNumbers> ProcessSnowball(double extraPayment, List<Debt> debts)
        {
            double totalPayment = 0;
            double totalDebt = 0;
            double totalInterestPaid = 0;
            double totalPaymentsMade = 0;
            double totalMonths = 0;

  

            List<CalculateResponse> responses = new List<CalculateResponse>();

            foreach (Debt debt in debts)
            {
          
                totalPayment += debt.MinimumPayment;
                totalDebt += debt.Balance;

                CalculateResponse debtCalc = await CallCalculate(debt.Balance * 100, debt.MinimumPayment * 100, debt.DueDay, debt.Rate / 100);

                responses.Add(debtCalc);

            }

            foreach (CalculateResponse response in responses)
            {
                totalInterestPaid += response.Amortization.APR.FinanceCharge;
                totalPaymentsMade += response.Amortization.APR.TotalOfPayments;

                int paymentCount = response.Amortization.AmortizationTable.Count(item => item.AmortizationLineType == "Payment");

                totalMonths = Math.Max(totalMonths, paymentCount);
            }

            Console.WriteLine(totalPayment);
            Console.WriteLine(totalDebt);

            return new BaseSnowballNumbers
            {
                TotalPayment = totalPayment,
                TotalDebt = totalDebt,
                TotalInterestPaid = totalInterestPaid,
                TotalPaymentsMade = totalPaymentsMade,
                TotalMonths = totalMonths
            };
        }

        private static async Task<CalculateResponse> CallCalculate(double balance, double payment, int day, double rate)
        {
            var CustomerId = "YOUR_API_KEY";
            string today = DateTime.Now.ToString("yyyy-MM-dd");

            int dayOfMonth = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            if (dayOfMonth > day)
            {
                if(month == 12)
                {
                    month = 1;
                    year++;
                }
                else
                {
                    month++;
                }

            }

            string Document = "{\"CFM\" : [{\"Amount\" : " + balance + ",\"Event\" : \"Loan\",\"Number\" : 1,\"StartDate\" : \"" + today + "\"},{\"Amount\" : " + payment + ",\"Event\" : \"Payment\",\"Number\" : \"Unknown\",\"Period\" : \"Monthly\",\"StartDate\" : \"" + year + "-" + month + "-" + day + "\"}],\"Label\" : \"\",\"Period\" : \"ExactDays\",\"Rate\" : " + rate + ",\"Schema\" : 6,\"YearLength\" : 365}";

            var data = new
            {
                CustomerId, // Ensure this variable is defined and set before using
                CreateAmSchedule = true,
                RoundingType = "Balloon",
                SpecificLine = 2,
                LineAdjust = "AllAmounts",
                Document // Ensure this variable is defined and set before using
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.PostAsync("https://tvaluerestws.com/webapi/calculate", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string responseContent = await response.Content.ReadAsStringAsync();
                            return JsonSerializer.Deserialize<CalculateResponse>(responseContent);
                        }
                        else
                        {
                            // Log error message or handle it as per your application's error handling policy
                            throw new HttpRequestException($"Request failed with status code: {response.StatusCode} and message: {response.ReasonPhrase}");
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request related errors here
                Console.WriteLine($"An error occurred while sending the request: {ex.Message}");
                throw;
            }
            catch (JsonException ex)
            {
                // Handle JSON deserialization errors here
                Console.WriteLine($"An error occurred while deserializing the response: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Handle other unforeseen errors
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }

        }
    }
}
