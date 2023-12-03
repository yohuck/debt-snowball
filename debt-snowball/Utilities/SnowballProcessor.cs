
using System.Net.Http;
using System.Text;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using debt_snowball.Data;

namespace debt_snowball.Utilities
{
    public class SnowballProcessor
    {


        public async static void ProcessSnowball(double extraPayment, List<Debt> debts)
        {
            double totalPayment = extraPayment;
            double totalDebt = 0;
            double totalInterest = 13.23;

            foreach (Debt debt in debts)
            {
                Console.WriteLine(debt.Name);
                Console.WriteLine(debt.Balance);
                Console.WriteLine(debt.MinimumPayment);
                Console.WriteLine(debt.Rate);
                Console.WriteLine(debt.DueDay);

                totalPayment += debt.MinimumPayment;
                totalDebt += debt.Balance;

            }

            Console.WriteLine(totalPayment);
            Console.WriteLine(totalDebt);
            CalculateResponse debtPayments = await CallCalculate(totalDebt, totalPayment, 12, 0.1456);

            Console.WriteLine("Here");
        }

        private static async Task<CalculateResponse> CallCalculate(double balance, double payment, int day, double rate)
        {
            var CustomerId = "YOUR_API_KEY";
            string today = DateTime.Now.ToString("yyyy-MM-dd");

            // TODO: correct date
            string Document = "{\"CFM\" : [{\"Amount\" : " + balance + ",\"Event\" : \"Loan\",\"Number\" : 1,\"StartDate\" : \"" + today + "\"},{\"Amount\" : " + payment + ",\"Event\" : \"Payment\",\"Number\" : \"Unknown\",\"Period\" : \"Monthly\",\"StartDate\" : \"2023-12-" + day + "\"}],\"Label\" : \"\",\"Period\" : \"Monthly\",\"Rate\" : " + rate + ",\"Schema\" : 6,\"YearLength\" : 365}";


            var data = new
            {
                CustomerId = CustomerId,
                CreateAmSchedule = true,
                RoundingType = "Balloon",
                SpecificLine = 2,
                LineAdjust = "AllAmounts",
                Document = Document
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            double unknownValue;
            string responseContent = "";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response;
                using (response = await client.PostAsync("https://tv6wsstaging.com/webapi/calculate", content))
                {
                    responseContent = await response.Content.ReadAsStringAsync();
                }
            }

            CalculateResponse calculateResponse = JsonSerializer.Deserialize<CalculateResponse>(responseContent);
            
          

            return calculateResponse;






        }

        public class RootObject
        {
            public CFM CFM { get; set; }
        }

        public class CFM
        {
            public Unknowns Unknowns { get; set; }
        }

        public class Unknowns
        {
            public string Value { get; set; }
        }

        public class CashFlowItem
        {
            public double Amount { get; set; }
            public string Event { get; set; }
            public int Number { get; set; }
            public string? Period { get; set; } // Explicitly marked as nullable
            public DateTime StartDate { get; set; }
        }

        public class CashFlowDeserializer
        {
            public static List<CashFlowItem> DeserializeCashFlowItems(string json)
            {
                return JsonSerializer.Deserialize<List<CashFlowItem>>(json);
            }
        }


    }
}
