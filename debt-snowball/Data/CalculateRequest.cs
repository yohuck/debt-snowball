namespace debt_snowball.Data
{
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
    using System;
    using System.Collections.Generic;

   

 

    public class CashFlowLineItemUnknown
    {
        public string Amount { get; set; }
        public string Event { get; set; }
        public string Number { get; set; }
        public string StartDate { get; set; }
        public string? Period { get; set; }
    }

  


    public class CalculateDocument
    {
        public List<CashFlowLineItemUnknown> CFM { get; set; }
        public string Label { get; set; }
        public string Period { get; set; }
        public double Rate { get; set; }
        public int Schema { get; set; }
        public int YearLength { get; set; }
    }

    

    public class CalculateRequest
    {
        public string CustomerId { get; set; }
        public bool CreateAmSchedule { get; set; }
        public string RoundingType { get; set; }
        public string SpecificLine { get; set; }
        public string LineAdjustment { get; set; }
        public CalculateDocument Document { get; set; }
    }


}
