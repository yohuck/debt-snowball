using debt_snowball.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

public class Debt
{
    public int DebtId { get; set; }
    public string Name { get; set; }
    public double Rate { get; set; }
    public double Balance { get; set; }
    public double MinimumPayment { get; set; }
    public int DueDay { get; set; } // Day of the month for the due date
    public ApplicationUser User { get; set; }

    public ICollection<Payment> Payments { get; set; }


}

public class Snowballa
{
    public int SnowballId { get; set; }
    public double ExtraPayment { get; set; }
    public ApplicationUser User { get; set; }
    public ICollection<Debt> Debts { get; set; }
}

public class Payment
{
    public int PaymentId { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public decimal NewBalance { get; set; }

}


public class Author
{
    public int AuthorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<Book> Books { get; set; }
}
public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
}