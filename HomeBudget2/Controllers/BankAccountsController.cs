using HomeBudget2.DAL.Interfaces;
using HomeBudget2.ViewModels;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HomeBudget2.Controllers
{
    [Authorize]
    public class BankAccountsController : Controller
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IBankAccountLogic _bankAccountLogic;

        public BankAccountsController(IBankAccountRepository bankAccountRepository, IBankAccountLogic bankAccountLogic)
        {
            _bankAccountRepository = bankAccountRepository;
            _bankAccountLogic = bankAccountLogic;
        }

        // GET: BankAccounts
        public ActionResult Index()
        {
            var bankaccountVm = new BankAccountViewModel();
            bankaccountVm.BankAccountsList = _bankAccountRepository.GetWhere(bankAccount => bankAccount.Id > 0);

            return View(bankaccountVm);
        }

        // GET: BankAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bankaccountVm = new BankAccountViewModel();
            bankaccountVm.BankAccount = _bankAccountRepository.GetWhere(bankAccount => bankAccount.Id == id).FirstOrDefault();
            if (bankaccountVm.BankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankaccountVm);
        }

        // GET: BankAccounts/Create
        public ActionResult Create()
        {
            var bankaccountVm = new BankAccountViewModel();
            // bankaccountVm.BankAccountsList = _bankAccountRepository.GetWhere(bankAccount => bankAccount.Id > 0);

            return View(bankaccountVm);
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BankAccountViewModel bankAccountVm)
        {
            if (ModelState.IsValid)
            {
                bankAccountVm.BankAccount.Balance = bankAccountVm.BankAccount.InitialBalance;
                _bankAccountRepository.Create(bankAccountVm.BankAccount);
                return RedirectToAction("Index");
            }

            return View(bankAccountVm);
        }

        // GET: BankAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bankaccountVm = new BankAccountViewModel();
            bankaccountVm.BankAccount = _bankAccountRepository.GetWhere(bankAccount => bankAccount.Id == id).FirstOrDefault();
            if (bankaccountVm.BankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankaccountVm);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BankAccountViewModel bankAccountVm)
        {
            if (ModelState.IsValid)
            {
                _bankAccountLogic.CalculateBalanceOfAllAccounts();
                _bankAccountRepository.Update(bankAccountVm.BankAccount);
                return RedirectToAction("Index");
            }
            return View(bankAccountVm);
        }

        // GET: BankAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bankaccountVm = new BankAccountViewModel();

            bankaccountVm.BankAccount = _bankAccountRepository.GetWhere(bankAccount => bankAccount.Id == id).FirstOrDefault();
            if (bankaccountVm.BankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankaccountVm);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var bankaccountVm = new BankAccountViewModel();

            bankaccountVm.BankAccount = _bankAccountRepository.GetWhere(bankAccount => bankAccount.Id == id).FirstOrDefault();

            _bankAccountRepository.Delete(bankaccountVm.BankAccount);
            return RedirectToAction("Index");
        }


    }
}
