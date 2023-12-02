
using System.Net.Http;
using System.Text;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            double unknownAmount = await MakeRequestAsync(totalDebt, totalPayment, 12, totalInterest);
          
            Console.WriteLine("Here");
        }

        private static async Task<double> MakeRequestAsync(double balance, double payment, int day, double rate)
        {
            var CustomerId = "YOUR_CODE";

            // TODO: correct date
            string Document = "{\"CFM\" : [{\"Amount\" : " + "400000" + ",\"Event\" : \"Loan\",\"Number\" : 1,\"StartDate\" : \"2021-01-01\"},{\"Amount\" : " + "25000" + ",\"Event\" : \"Payment\",\"Number\" : \"Unknown\",\"Period\" : \"Monthly\",\"StartDate\" : \"2023-11-01\"}],\"Label\" : \"\",\"Period\" : \"Monthly\",\"Rate\" : " + "0.023" + ",\"Schema\" : 6,\"YearLength\" : 365}";


            var data = new
            {
                CustomerId = CustomerId,
                CreateAmSchedule = false,
                RoundingType = "Ignore",
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

            using JsonDocument doc = JsonDocument.Parse(responseContent);

            JsonElement cfmElement = doc.RootElement.GetProperty("CFM");
            JsonElement unknownsElement = cfmElement.GetProperty("Unknowns");
            unknownValue = unknownsElement.GetProperty("Value").GetDouble();

            Console.WriteLine(unknownValue);
            Console.WriteLine("Here");

            return unknownValue;






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

    }
}
