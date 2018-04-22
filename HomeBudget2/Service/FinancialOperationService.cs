using HomeBudget2.DAL.Interfaces;
using HomeBudget2.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace HomeBudget2.Service
{
    public class FinancialOperationService : IFinancialOperationService
    {

        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;

        public FinancialOperationService(IBankAccountRepository bankAccountRepository, ISubCategoryRepository subCategoryRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _subCategoryRepository = subCategoryRepository;
        }


        public void AddSelectListsToViewModel(FinancialOperationViewModel financialOperationVm, bool isExpense)
        {
            var bankaccounts = _bankAccountRepository.GetWhere(ba => ba.Id > 0);
            var subcategories = _subCategoryRepository.GetWhere(sc => sc.Id > 0 && isExpense ? sc.IsExpense : sc.IsIncome);
            financialOperationVm.SelectListOfBankAccounts = new SelectList(bankaccounts, "Id", "AccountName");
            financialOperationVm.SelectListOfSubCategories = new SelectList(subcategories, "Id", "SubCategoryName");
        }



        public string ChooseActionToGo(FinancialOperationViewModel financialOperationVm)
        {
            if (financialOperationVm.FinancialOperation.IsExpense)
            {
                return "ExpensesIndex";
            }
            if (financialOperationVm.FinancialOperation.IsIncome)
            {
                return "IncomesIndex";
            }
            return "TransfersIndex";
        }

        public void SetSourceOfMoneyAndDestinationOfMoney(FinancialOperationViewModel financialOperationVm)
        {
            var bankAccount = _bankAccountRepository
                .GetWhere(ba => ba.Id == financialOperationVm.FinancialOperation.BankAccountId).FirstOrDefault();

            var subCategory = _subCategoryRepository
                .GetWhere(sc => sc.Id == financialOperationVm.FinancialOperation.SubCategoryId).FirstOrDefault();

            var targetAccount = _bankAccountRepository
                .GetWhere(ba => ba.Id == financialOperationVm.FinancialOperation.TargetBankAccountId).FirstOrDefault();

            if (financialOperationVm.FinancialOperation.IsExpense)
            {
                financialOperationVm.FinancialOperation.SourceOfMoney = bankAccount.AccountName;

                financialOperationVm.FinancialOperation.DestinationOfMoney =
                   subCategory.SubCategoryName;
            }
            if (financialOperationVm.FinancialOperation.IsIncome)
            {
                financialOperationVm.FinancialOperation.SourceOfMoney =
                   subCategory.SubCategoryName;

                financialOperationVm.FinancialOperation.DestinationOfMoney =
                    targetAccount.AccountName;
            }
            if (financialOperationVm.FinancialOperation.IsTransfer)
            {
                financialOperationVm.FinancialOperation.SourceOfMoney =
                   bankAccount.AccountName;

                financialOperationVm.FinancialOperation.DestinationOfMoney =
                    targetAccount.AccountName;
            }
        }
    }
}