using HomeBudget2.DAL.Interfaces;
using HomeBudget2.ViewModels;
using System.Linq;

namespace HomeBudget2.BusinessLogic
{
    public class BankAccountLogic : IBankAccountLogic
    {
        private readonly IUnitOfWork _unitOfWork;

        public BankAccountLogic(IUnitOfWork unitOfWork)
        {
                        _unitOfWork = unitOfWork;
        }

        public void CalculateBalanceOfAllAccountsAndUpdateThem()
        {
            var bankAccountList = _unitOfWork.BankAccountRepo.GetWhere(x => x.Id > 0).ToList();
            foreach (var bankAccount in bankAccountList)
            {
                var sumOfExpenses = _unitOfWork.FinancialOperatiosRepo.GetWhere(financialOperation => financialOperation.BankAccountId == bankAccount.Id).Sum(e => e.AmountOfMoney);
                var sumOfIncomes = _unitOfWork.FinancialOperatiosRepo.GetWhere(financialOperation => financialOperation.TargetBankAccountId == bankAccount.Id).Sum(e => e.AmountOfMoney);

                bankAccount.Balance = bankAccount.InitialBalance - sumOfExpenses + sumOfIncomes;
                _unitOfWork.BankAccountRepo.Update(bankAccount);               
            }
        }

        public BankAccountViewModel CalculateBalanceOfSelectedAccount(BankAccountViewModel bankAccountVm)
        {
            var sumOfExpenses = _unitOfWork.FinancialOperatiosRepo
                .GetWhere(financialOperation => financialOperation.BankAccountId == bankAccountVm
                                                    .BankAccount.Id).Sum(e => e.AmountOfMoney);
            var sumOfIncomes = _unitOfWork.FinancialOperatiosRepo
                .GetWhere(financialOperation => financialOperation.TargetBankAccountId == bankAccountVm.BankAccount.Id)
                .Sum(e => e.AmountOfMoney);

            bankAccountVm.BankAccount.Balance = bankAccountVm.BankAccount.InitialBalance - sumOfExpenses + sumOfIncomes;

            return bankAccountVm;
        }
    }
}