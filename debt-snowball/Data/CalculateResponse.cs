namespace debt_snowball.Data
{
    using System;
    using System.Collections.Generic;

    public class Unknowns
    {
        public string Type { get; set; }
        public double Value { get; set; }
    }

    public class CashFlowLineItem
    {
        public double Amount { get; set; }
        public string Event { get; set; }
        public int Number { get; set; }
        public DateTime StartDate { get; set; }
    }

    public class CashFlowMatrix
    {
        public List<CashFlowLineItem> CFM { get; set; }
        public string Label { get; set; }
        public string Period { get; set; }
        public double Rate { get; set; }
        public int Schema { get; set; }
        public DateTime TValueEngineBuild { get; set; }
        public Unknowns Unknowns { get; set; }
        public int YearLength { get; set; }
    }

    public class APR
    {
        public double AmountFinanced { get; set; }
        public double FinanceCharge { get; set; }
        public double Rate { get; set; }
        public double TotalOfPayments { get; set; }
    }

    public class AmortizationLineItem
    {
        public string AmortizationLineType { get; set; }
        public AmortizationData Data { get; set; }
        public DateTime Date { get; set; }
        public int EventId { get; set; }
        public int Line { get; set; }
        public int Sequence { get; set; }

        public class AmortizationData
        {
            public double Balance { get; set; }
            public double InterestAccrued { get; set; }
            public double InterestPaid { get; set; }
            public double LoanAmount { get; set; }
            public List<double> Loans { get; set; }
            public double PaymentAmount { get; set; }
            public List<double> Payments { get; set; }
            public double PrincipalPaid { get; set; }
            public double UnpaidInterestBalance { get; set; }
        }
    }

    public class Amortization
    {
        public List<AmortizationLineItem> AmortizationTable { get; set; } // Use object or a specific class if the structure is known
        public APR APR { get; set; }
        public double Rounding { get; set; }
        public Unknowns Unknowns { get; set; }
    }

    public class CalculateResponse
    {
        public Amortization Amortization { get; set; }
        public CashFlowMatrix CFM { get; set; }
    }

}
