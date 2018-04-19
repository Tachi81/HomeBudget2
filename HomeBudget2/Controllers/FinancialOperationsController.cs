using HomeBudget2.DAL.Interfaces;
using HomeBudget2.Models;
using HomeBudget2.ViewModels;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HomeBudget2.Controllers
{
    public class FinancialOperationsController : Controller
    {
        private readonly IFinancialOperationRepository _financialOperationRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;

        public FinancialOperationsController(IFinancialOperationRepository financialOperationRepository,
            IBankAccountRepository bankAccountRepository, ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository)
        {
            _financialOperationRepository = financialOperationRepository;
            _bankAccountRepository = bankAccountRepository;
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
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
            AddSelectListsToViewModel(financialOperationVm);

            financialOperationVm.FinancialOperation = new FinancialOperation() { IsExpense = true };

            return View("Create", financialOperationVm);
        }

        // GET: FinancialOperations/Create Income
        public ActionResult CreateIncome()
        {
            FinancialOperationViewModel financialOperationVm = new FinancialOperationViewModel();
            AddSelectListsToViewModel(financialOperationVm);
            financialOperationVm.FinancialOperation = new FinancialOperation() { IsIncome = true };
            return View("Create", financialOperationVm);
        }
        // GET: FinancialOperations/Create Transfer
        public ActionResult CreateTransfer()
        {
            FinancialOperationViewModel financialOperationVm = new FinancialOperationViewModel();
            var bankaccounts = _bankAccountRepository.GetWhere(ba => ba.Id > 0);
            AddSelectListsToViewModel(financialOperationVm);
            financialOperationVm.FinancialOperation = new FinancialOperation() { IsTransfer = true };

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
                SetSourceOfMoneyAndDestinationOfMoney(financialOperationVm);

                _financialOperationRepository.Create(financialOperationVm.FinancialOperation);

                return ChooseIndexToGo(financialOperationVm);
            }
            AddSelectListsToViewModel(financialOperationVm);

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
            AddSelectListsToViewModel(financialOperationVm);
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
                _financialOperationRepository.Update(financialOperationVm.FinancialOperation);
                return ChooseIndexToGo(financialOperationVm);
            }
            AddSelectListsToViewModel(financialOperationVm);
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
            return ChooseIndexToGo(financialOperationVm);
        }




        private void AddSelectListsToViewModel(FinancialOperationViewModel financialOperationVm)
        {
            var bankaccounts = _bankAccountRepository.GetWhere(ba => ba.Id > 0);
            var subcategories = _subCategoryRepository.GetWhere(sc => sc.Id > 0 && sc.IsIncome);
            financialOperationVm.SelectListOfBankAccounts = new SelectList(bankaccounts, "Id", "AccountName");
            financialOperationVm.SelectListOfSubCategories = new SelectList(subcategories, "Id", "SubCategoryName");

        }

        private ActionResult ChooseIndexToGo(FinancialOperationViewModel financialOperationVm)
        {
            if (financialOperationVm.FinancialOperation.IsExpense)
            {
                return RedirectToAction("ExpensesIndex");
            }
            if (financialOperationVm.FinancialOperation.IsIncome)
            {
                return RedirectToAction("IncomesIndex");
            }
            return RedirectToAction("TransfersIndex");
        }

        private static void SetSourceOfMoneyAndDestinationOfMoney(FinancialOperationViewModel financialOperationVm)
        {
            if (financialOperationVm.FinancialOperation.IsExpense)
            {
                financialOperationVm.FinancialOperation.SourceOfMoney =
                    financialOperationVm.FinancialOperation.BankAccount.AccountName;

                financialOperationVm.FinancialOperation.DestinationOfMoney =
                    financialOperationVm.FinancialOperation.SubCategory.SubCategoryName;
            }
            if (financialOperationVm.FinancialOperation.IsIncome)
            {
                financialOperationVm.FinancialOperation.SourceOfMoney =
                    financialOperationVm.FinancialOperation.SubCategory.SubCategoryName;

                financialOperationVm.FinancialOperation.DestinationOfMoney =
                    financialOperationVm.FinancialOperation.BankAccount.AccountName;
            }
            if (financialOperationVm.FinancialOperation.IsTransfer)
            {
                financialOperationVm.FinancialOperation.SourceOfMoney =
                    financialOperationVm.FinancialOperation.BankAccount.AccountName;

                financialOperationVm.FinancialOperation.DestinationOfMoney =
                    financialOperationVm.FinancialOperation.TargetBankAccount.AccountName;
            }
        }
    }
}
