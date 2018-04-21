using HomeBudget2.DAL.Interfaces;
using System.Linq;

namespace HomeBudget2.BusinessLogic
{
    public class BankAccountLogic : IBankAccountLogic
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IFinancialOperationRepository _financialOperationRepository;

        public BankAccountLogic(IBankAccountRepository bankAccountRepository, IFinancialOperationRepository financialOperationRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _financialOperationRepository = financialOperationRepository;
        }

        public void CalculateBalanceOfAllAccounts()
        {
            var bankAccountList = _bankAccountRepository.GetWhereWithIncludes(x => x.Id > 0).ToList();
            foreach (var bankAccount in bankAccountList)
            {
                var sumOfExpenses = _financialOperationRepository.GetWhere(financialOperation => financialOperation.BankAccountId == bankAccount.Id).Sum(e => e.AmountOfMoney);
                var sumOfIncomes = _financialOperationRepository.GetWhere(financialOperation => financialOperation.TargetBankAccountId == bankAccount.Id).Sum(e => e.AmountOfMoney);

                bankAccount.Balance = bankAccount.InitialBalance - sumOfExpenses + sumOfIncomes;
                _bankAccountRepository.Update(bankAccount);
            }

        }

    }
}