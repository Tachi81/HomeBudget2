using HomeBudget2.DAL.Interfaces;
using HomeBudget2.Models;
using HomeBudget2.ViewModels;
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
            FinancialOperationViewModel financialOperationVm = new FinancialOperationViewModel();
            financialOperationVm.ListOfFinancialOperations =
                _financialOperationRepository.GetWhereWithIncludes(fo => fo.Id > 0 && fo.IsExpense,
                    fo => fo.SubCategory);
            financialOperationVm.FinancialOperation = new FinancialOperation() { IsExpense = true };
            return View("Index", financialOperationVm);
        }

        // GET: FinancialOperations - Incomes
        public ActionResult IncomesIndex()
        {
            FinancialOperationViewModel financialOperationVm = new FinancialOperationViewModel();
            financialOperationVm.ListOfFinancialOperations =
                _financialOperationRepository.GetWhereWithIncludes(fo => fo.Id > 0 && fo.IsIncome,
                    fo => fo.SubCategory);
            financialOperationVm.FinancialOperation = new FinancialOperation() { IsIncome = true };
            return View("Index", financialOperationVm);
        }

        // GET: FinancialOperations - Transfers
        public ActionResult TransfersIndex()
        {
            FinancialOperationViewModel financialOperationVm = new FinancialOperationViewModel();
            financialOperationVm.ListOfFinancialOperations =
                _financialOperationRepository.GetWhereWithIncludes(fo => fo.Id > 0 && fo.IsTransfer,
                    fo => fo.TargetBankAccount);
            financialOperationVm.FinancialOperation = new FinancialOperation() { IsTransfer = true };
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
                _financialOperationRepository.GetWhere(fo => fo.Id == id).FirstOrDefault();

            if (financialOperationVm.FinancialOperation == null)
            {
                return HttpNotFound();
            }
            return View(financialOperationVm);
        }

        // GET: FinancialOperations/Create Expense
        public ActionResult CreateExpense()
        {
            FinancialOperationViewModel financialOperationVm = new FinancialOperationViewModel();
            financialOperationVm.FinancialOperation = new FinancialOperation() { IsExpense = true };
            _financialOperationService.AddSelectListsToViewModel(financialOperationVm, financialOperationVm.FinancialOperation.IsExpense);

            return View("Create", financialOperationVm);
        }

        // GET: FinancialOperations/Create Income
        public ActionResult CreateIncome()
        {
            FinancialOperationViewModel financialOperationVm = new FinancialOperationViewModel();
            financialOperationVm.FinancialOperation = new FinancialOperation() { IsIncome = true };
            _financialOperationService.AddSelectListsToViewModel(financialOperationVm, financialOperationVm.FinancialOperation.IsExpense);
            return View("Create", financialOperationVm);
        }
        // GET: FinancialOperations/Create Transfer
        public ActionResult CreateTransfer()
        {
            FinancialOperationViewModel financialOperationVm = new FinancialOperationViewModel();
            financialOperationVm.FinancialOperation = new FinancialOperation() { IsTransfer = true };
            _financialOperationService.AddSelectListsToViewModel(financialOperationVm, financialOperationVm.FinancialOperation.IsExpense);

            return View("Create", financialOperationVm);
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
                _financialOperationService.SetSourceOfMoneyAndDestinationOfMoney(financialOperationVm);

                _financialOperationRepository.Create(financialOperationVm.FinancialOperation);

                _bankAccountLogic.CalculateBalanceOfAllAccountsAndUpdateThem();

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
                _financialOperationRepository.Update(financialOperationVm.FinancialOperation);

                _bankAccountLogic.CalculateBalanceOfAllAccountsAndUpdateThem();
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
            _bankAccountLogic.CalculateBalanceOfAllAccountsAndUpdateThem();
            return RedirectToAction(_financialOperationService.ChooseActionToGo(financialOperationVm));
        }




    }
}
