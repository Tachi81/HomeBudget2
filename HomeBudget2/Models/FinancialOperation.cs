using HomeBudget2.Validation_Attributes;
using System;

namespace HomeBudget2.Models
{
    public class FinancialOperation
    {
        public int Id { get; set; }
        [NotEqualToZero]
        public double AmountOfMoney { get; set; }

        public DateTime DateTime { get; set; }
        public string Note { get; set; }

        public int? BankAccountId { get; set; }
        public int? SubCategoryId { get; set; }

        public int? TargetBankAccountId { get; set; }

        public string SourceOfMoney { get; set; }
        public string DestinationOfMoney { get; set; }


        public virtual BankAccount BankAccount { get; set; }
        public virtual BankAccount TargetBankAccount { get; set; }

        public virtual Category SubCategory { get; set; }

        public string UserId { get; set; }



        public bool IsTransfer { get; set; }
        public bool IsExpense { get; set; }
        public bool IsIncome { get; set; }


    }
}


