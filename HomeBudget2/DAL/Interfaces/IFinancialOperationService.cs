using HomeBudget2.ViewModels;

namespace HomeBudget2.DAL.Interfaces
{
    public interface IFinancialOperationService
    {
        FinancialOperationViewModel CreateViewModelWithAll(bool isExpense, bool isIncome);

        void AddSelectListsToViewModel(FinancialOperationViewModel financialOperationVm, bool isExpense);


        string ChooseActionToGo(FinancialOperationViewModel financialOperationVm);


        void SetSourceOfMoneyAndDestinationOfMoney(FinancialOperationViewModel financialOperationVm);


        FinancialOperationViewModel FulfillHistoryViewModelWithFinancialOperationAndListOfFinancialOperations(FinancialOperationViewModel financialOperationVm);
    }
}