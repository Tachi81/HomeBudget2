﻿using HomeBudget2.DAL.Interfaces;
using HomeBudget2.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HomeBudget2.Controllers
{
    [Authorize]
    public class FinancialOperationsController : Controller
    {
        private readonly IFinancialOperationRepository _financialOperationRepository;

        private readonly IFinancialOperationService _financialOperationService;
        private readonly IBankAccountLogic _bankAccountLogic;


        public FinancialOperationsController(IFinancialOperationRepository financialOperationRepository,
            IFinancialOperationService financialOperationService, IBankAccountLogic bankAccountLogic)
        {
            _financialOperationRepository = financialOperationRepository;
            _financialOperationService = financialOperationService;
            _bankAccountLogic = bankAccountLogic;

        }



        // GET: FinancialOperations - HistoryChooseAccount
        public ActionResult HistoryChooseAccount()
        {
            FinancialOperationViewModel financialOperationVm = new FinancialOperationViewModel();
            var userId = User.Identity.GetUserId();
            financialOperationVm.UserId = userId;
            _financialOperationService.AddSelectListsToViewModel(financialOperationVm, true);

            return View("HistoryChooseAccount", financialOperationVm);
        }

        // GET: FinancialOperations - History
        public ActionResult History(FinancialOperationViewModel financialOperationVm)
        {
            if (financialOperationVm.FinancialOperation.BankAccountId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _financialOperationService.FulfillHistoryViewModelWithFinancialOperationAndListOfFinancialOperations(financialOperationVm);

            return View("History", financialOperationVm);
        }

        // GET: FinancialOperations - Expenses
        public ActionResult ExpensesIndex()
        {
            bool IsExpense = true;
            bool IsIncome = false;
            var userId = User.Identity.GetUserId();
            FinancialOperationViewModel financialOperationVm = _financialOperationService.CreateViewModelWithAll(IsExpense, IsIncome, userId);

            return View("Index", financialOperationVm);
        }


        // GET: FinancialOperations - Incomes
        public ActionResult IncomesIndex()
        {
            bool IsExpense = false;
            bool IsIncome = true;
            var userId = User.Identity.GetUserId();
            FinancialOperationViewModel financialOperationVm = _financialOperationService.CreateViewModelWithAll(IsExpense, IsIncome, userId);

            return View("Index", financialOperationVm);
        }

        // GET: FinancialOperations - Transfers
        public ActionResult TransfersIndex()
        {
            bool IsExpense = false;
            bool IsIncome = false;
            var userId = User.Identity.GetUserId();
            FinancialOperationViewModel financialOperationVm = _financialOperationService.CreateViewModelWithAll(IsExpense, IsIncome, userId);


            return View("Index", financialOperationVm);
        }

        // GET: FinancialOperations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialOperationViewModel financialOperationVm = new FinancialOperationViewModel();
            financialOperationVm.FinancialOperation =
                _financialOperationRepository.GetWhereWithIncludes(fo => fo.Id == id, fo => fo.SubCategory, fo => fo.SubCategory.Category).FirstOrDefault();

            if (financialOperationVm.FinancialOperation == null)
            {
                return HttpNotFound();
            }
            return View(financialOperationVm);
        }


        // POST: FinancialOperations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FinancialOperationViewModel financialOperationVm)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                financialOperationVm.FinancialOperation.UserId = userId;

                _financialOperationService.SetSourceOfMoneyAndDestinationOfMoney(financialOperationVm);



                _financialOperationRepository.Create(financialOperationVm.FinancialOperation);

                _bankAccountLogic.CalculateBalanceOfAllAccountsAndUpdateThem(userId);

                return RedirectToAction(_financialOperationService.ChooseActionToGo(financialOperationVm));
            }

            _financialOperationService.AddSelectListsToViewModel(financialOperationVm, financialOperationVm.FinancialOperation.IsExpense);

            return View(financialOperationVm);
        }



        // GET: FinancialOperations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialOperationViewModel financialOperationVm = new FinancialOperationViewModel();
            financialOperationVm.FinancialOperation =
                _financialOperationRepository.GetWhere(fo => fo.Id == id).FirstOrDefault();

            if (financialOperationVm.FinancialOperation == null)
            {
                return HttpNotFound();
            }

            var userId = User.Identity.GetUserId();
            financialOperationVm.UserId = userId;
            _financialOperationService.AddSelectListsToViewModel(financialOperationVm, financialOperationVm.FinancialOperation.IsExpense);
            return View(financialOperationVm);
        }

        // POST: FinancialOperations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FinancialOperationViewModel financialOperationVm)
        {
            if (ModelState.IsValid)
            {
                _financialOperationService.SetSourceOfMoneyAndDestinationOfMoney(financialOperationVm);

                var userId = User.Identity.GetUserId();
                financialOperationVm.FinancialOperation.UserId = userId;

                _financialOperationRepository.Update(financialOperationVm.FinancialOperation);

                _bankAccountLogic.CalculateBalanceOfAllAccountsAndUpdateThem(userId);
                return RedirectToAction(_financialOperationService.ChooseActionToGo(financialOperationVm));
            }
            _financialOperationService.AddSelectListsToViewModel(financialOperationVm, financialOperationVm.FinancialOperation.IsExpense);
            return View(financialOperationVm);
        }




        // GET: FinancialOperations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialOperationViewModel financialOperationVm = new FinancialOperationViewModel();
            financialOperationVm.FinancialOperation =
                _financialOperationRepository.GetWhere(fo => fo.Id == id).FirstOrDefault();

            if (financialOperationVm.FinancialOperation == null)
            {
                return HttpNotFound();
            }
            return View(financialOperationVm);
        }

        // POST: FinancialOperations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FinancialOperationViewModel financialOperationVm = new FinancialOperationViewModel();
            financialOperationVm.FinancialOperation =
                _financialOperationRepository.GetWhere(fo => fo.Id == id).FirstOrDefault();
            _financialOperationRepository.Delete(financialOperationVm.FinancialOperation);

            var userId = User.Identity.GetUserId();
            _bankAccountLogic.CalculateBalanceOfAllAccountsAndUpdateThem(userId);
            return RedirectToAction(_financialOperationService.ChooseActionToGo(financialOperationVm));
        }




    }
}
