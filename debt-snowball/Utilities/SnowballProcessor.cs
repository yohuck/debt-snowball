
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

        public async static Task<List<CalculateResponse>> CalculateSnowball(double extraPayment, List<Debt> debts)
        {
            double leftOver;
            List<double> paymentCount = new List<double>();
            List<double> leftoverPayment = new List<double>();
            List<CalculateResponse> responseList = new List<CalculateResponse>();

            double currentExtraPayment = extraPayment * 100;



            foreach (Debt debt in debts)
            {
                double addMonths = 0;
                double bal = debt.Balance * 100;
                List<double> clonePaymentCount = paymentCount.ToList();
                List<CashFlowLineItemUnknown> paymentList = new List<CashFlowLineItemUnknown>();
                int day = debt.DueDay;
                int tester = clonePaymentCount.Count + 1;

                CashFlowLineItemUnknown loan = new CashFlowLineItemUnknown
                {
                    Amount = bal.ToString(),
                    Event = "Loan",
                    Number = "1",
                    StartDate = DateTime.Now.ToString("yyyy-MM-dd")
                };

               
            

                paymentList.Add(loan);
                double paymentAmount = currentExtraPayment;
                int extraPaymentCount = 0;
                while (tester > 0)
                {
                    if (clonePaymentCount.Count > 0)
                    {
                        try
                        {
                            paymentAmount = debt.MinimumPayment * 100 + leftoverPayment.FirstOrDefault();
                            if(leftoverPayment.Count > 0)
                            {
                                leftoverPayment.RemoveAt(0);
                            }
                        } catch
                        {
                            paymentAmount = debt.MinimumPayment * 100;
                        }
                        
                 

                    } else
                    {
                        paymentAmount = debt.MinimumPayment * 100 + currentExtraPayment;
                        Console.WriteLine("Hereeee");
                        if (leftoverPayment.Count > 0)
                        {
                            leftoverPayment.RemoveAt(0);
                        }

                    }
                    extraPaymentCount++;
                    double countOrUnknown = clonePaymentCount.FirstOrDefault();



                    string today = DateTime.Now.ToString("yyyy-MM-dd");
                    int dayOfMonth = DateTime.Now.Day;
                    int month = DateTime.Now.Month;
                    int year = DateTime.Now.Year;

                    if (dayOfMonth > day)
                    {
                        if (month == 12)
                        {
                            month = 1;
                            year++;
                        }
                        else
                        {
                            month++;
                        }

                    }

                    string dayOfMonthString = day.ToString("00");
                    string monthString = month.ToString("00");
                    string preAddMonths = $"{year}-{monthString}-{dayOfMonthString}";
                    string postAddMonths = DateHelper.AddMonths(preAddMonths, (int)addMonths);
                    string countOrUnknownString = countOrUnknown == 0 ? "Unknown" : countOrUnknown.ToString();

                    CashFlowLineItemUnknown payment = new CashFlowLineItemUnknown
                    {
                        Amount = paymentAmount.ToString(),
                        Event = "Payment",
                        Number = countOrUnknownString,
                        Period = "Monthly",
                        StartDate = postAddMonths
                    };

                    paymentList.Add(payment);

                    if(clonePaymentCount.FirstOrDefault() > 0)
                    {
                        addMonths += clonePaymentCount.FirstOrDefault();

                        clonePaymentCount.RemoveAt(0);

                     
                  

                    } 

                    tester--;


                };

                leftoverPayment = new List<double>();
                paymentCount = new List<double>();

                CalculateDocument document = new CalculateDocument
                {
                    CFM = paymentList,
                    Label = "",
                    Period = "ExactDays",
                    Rate = debt.Rate / 100,
                    Schema = 6,
                    YearLength = 365
                };

                CalculateRequest request = new CalculateRequest
                {
                    CustomerId = "YOUR_API_KEY",
                    CreateAmSchedule = true,
                    RoundingType = "Balloon",
                    SpecificLine = "2",
                    LineAdjustment = "AllAmounts",
                    Document = document
                };

                var json = JsonSerializer.Serialize(request);
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
                                Console.WriteLine(responseContent);
                                CalculateResponse aaa = JsonSerializer.Deserialize<CalculateResponse>(responseContent);
                                responseList.Add(aaa);


                                List<CashFlowLineItem> numberOfPayments = aaa.CFM.CFM;
                                int length = numberOfPayments.Count - 1;
                                int index = 0;
                             
                                foreach (CashFlowLineItem item in numberOfPayments)
                                {
                                    if (item.Event == "Payment")
                                    {
                                        paymentCount.Add(item.Number);
                                        if (index == length && item.Amount < paymentAmount)
                                        {
                                            leftOver = paymentAmount - item.Amount;
                                            leftoverPayment.Add(leftOver);
                                        }
                                        else
                                        {
                                            leftoverPayment.Add(0);
                                        }


                                    }
                                    index++;


                                }

                                Console.WriteLine("wowwowowow");
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
          
                    Console.WriteLine($"An error occurred while sending the request: {ex.Message}");
                    throw;
                }
                catch (JsonException ex)
                {
              
                    Console.WriteLine($"An error occurred while deserializing the response: {ex.Message}");
                    throw;
                }
                catch (Exception ex)
                {
               
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    throw;
                }

                Console.WriteLine("Wow");
                currentExtraPayment += debt.MinimumPayment * 100;

            }

         

            return responseList;
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
                if (month == 12)
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
