﻿using HomeBudget2.DAL.Interfaces;
using HomeBudget2.Models;
using HomeBudget2.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HomeBudget2.Service
{
    public class FinancialOperationService : IFinancialOperationService
    {

        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IFinancialOperationRepository _financialOperationRepository;

        public FinancialOperationService(IBankAccountRepository bankAccountRepository, ISubCategoryRepository subCategoryRepository, IFinancialOperationRepository financialOperationRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _subCategoryRepository = subCategoryRepository;
            _financialOperationRepository = financialOperationRepository;
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



        public FinancialOperationViewModel FulfillHistoryViewModelWithFinancialOperationAndListOfFinancialOperations(FinancialOperationViewModel financialOperationVm)
        {
            financialOperationVm.FinancialOperation.BankAccount = _bankAccountRepository
                .GetWhere(ba => ba.Id == financialOperationVm.FinancialOperation.BankAccountId).FirstOrDefault();

            financialOperationVm.ListOfFinancialOperations = new List<FinancialOperation>();

            List<FinancialOperation> expensesList = _financialOperationRepository.GetWhere(fo =>
                fo.BankAccountId == financialOperationVm.FinancialOperation.BankAccountId);
            expensesList.ForEach(expense => expense.AmountOfMoney *= (-1));

            List<FinancialOperation> incomesList = _financialOperationRepository.GetWhere(fo =>
                fo.TargetBankAccountId == financialOperationVm.FinancialOperation.BankAccountId);




            financialOperationVm.ListOfFinancialOperations.AddRange(expensesList);
            financialOperationVm.ListOfFinancialOperations.AddRange(incomesList);
            financialOperationVm.ListOfFinancialOperations = financialOperationVm.ListOfFinancialOperations.OrderByDescending(fo => fo.DateTime.Date).ToList();

            return financialOperationVm;
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