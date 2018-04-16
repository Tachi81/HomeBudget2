using System;

namespace HomeBudget2.Models
{
    public class FinancialOperation
    {
        public int Id { get; set; }
        public double AmountOfMoney { get; set; }
        public string DescriptionOfOperation { get; set; }
        public DateTime DateTime { get; set; }
        public string Note { get; set; }

        public int SourceOMoneyId { get; set; }
        public int DestinationOfMoneyId { get; set; }

        public bool IsTransfer { get; set; }
        public bool IsExpense { get; set; }
        public bool IsIncome { get; set; }

        public virtual SourceOMoney SourceOMoney { get; set; }
        public virtual DestinationOfMoney DestinationOfMoney { get; set; }
    }
}


