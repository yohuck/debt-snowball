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

    public class Amortization
    {
        public List<object> AmortizationTable { get; set; } // Use object or a specific class if the structure is known
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
