using HomeBudget2.ViewModels;

namespace HomeBudget2.DAL.Interfaces
{
    public interface IBankAccountLogic
    {
        void CalculateBalanceOfAllAccountsAndUpdateThem();
        BankAccountViewModel CalculateBalanceOfSelectedAccount(BankAccountViewModel bankAccountVm);
    }
}