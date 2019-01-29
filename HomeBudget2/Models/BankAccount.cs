using System.Collections.Generic;

namespace HomeBudget2.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public double InitialBalance { get; set; }
        public double Balance { get; set; }
        public string AccountName { get; set; }

        public string UserId { get; set; }


        public virtual ICollection<FinancialOperation> FinancialOperations { get; set; }
    }
}