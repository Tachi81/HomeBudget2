using HomeBudget2.Models;

namespace HomeBudget2.DAL.Interfaces
{
    public interface IBankAccountLogic
    {
        void CalculateBalanceOfAllAccountsAndUpdateThem(string userId, FinancialOperation finOperation = null);
        BankAccount CalculateBalanceOfSelectedAccount(BankAccount bankAccount);
    }
}