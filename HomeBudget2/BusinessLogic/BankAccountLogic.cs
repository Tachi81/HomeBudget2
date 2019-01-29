using HomeBudget2.DAL.Interfaces;
using HomeBudget2.Models;
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

        public void CalculateBalanceOfAllAccountsAndUpdateThem(string userId)
        {
            var bankAccountList = _unitOfWork.BankAccountRepo.GetWhereWithIncludes(x => x.Id > 0 && x.UserId == userId).ToList();
            foreach (var bankAccount in bankAccountList)
            {
                var sumOfExpenses = _unitOfWork.FinancialOperatiosRepo.
                    GetWhere(financialOperation => financialOperation.BankAccountId == bankAccount.Id && financialOperation.UserId == userId)
                    .Sum(e => e.AmountOfMoney);
                var sumOfIncomes = _unitOfWork.FinancialOperatiosRepo
                    .GetWhere(financialOperation => financialOperation.TargetBankAccountId == bankAccount.Id && financialOperation.UserId == userId)
                    .Sum(e => e.AmountOfMoney);

                bankAccount.Balance = bankAccount.InitialBalance - sumOfExpenses + sumOfIncomes;
                _unitOfWork.BankAccountRepo.Update(bankAccount);               
            }
        }

        public BankAccount CalculateBalanceOfSelectedAccount(BankAccount bankAccount)
        {
            var sumOfExpenses = _unitOfWork.FinancialOperatiosRepo
                .GetWhere(financialOperation => financialOperation.BankAccountId == bankAccount.Id)
                .Sum(e => e.AmountOfMoney);
            var sumOfIncomes = _unitOfWork.FinancialOperatiosRepo
                .GetWhere(financialOperation => financialOperation.TargetBankAccountId == bankAccount.Id)
                .Sum(e => e.AmountOfMoney);

            bankAccount.Balance = bankAccount.InitialBalance - sumOfExpenses + sumOfIncomes;

            return bankAccount;
        }
    }
}