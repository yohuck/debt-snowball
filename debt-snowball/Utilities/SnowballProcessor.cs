namespace debt_snowball.Utilities
{
    public class SnowballProcessor
{
    
        public static void ProcessSnowball(double extraPayment, List<Debt> debts)
        {
            Console.WriteLine(extraPayment);
            foreach (Debt debt in debts)
            {
                Console.WriteLine(debt.Name);
                Console.WriteLine(debt.Balance);
                Console.WriteLine(debt.MinimumPayment);
                Console.WriteLine(debt.Rate);
                Console.WriteLine(debt.DueDay);

            }
        }
    }
}
