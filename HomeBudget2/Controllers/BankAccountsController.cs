using HomeBudget2.DAL.Interfaces;
using HomeBudget2.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HomeBudget2.Controllers
{
    [Authorize]
    public class BankAccountsController : Controller
    {       
        private readonly IUnitOfWork _unitOfWork;

        public BankAccountsController(IUnitOfWork unitOfWork)
        {         
            _unitOfWork = unitOfWork;
        }

        // GET: BankAccounts
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var bankaccountVm = new BankAccountViewModel();

            bankaccountVm.BankAccountsList = _unitOfWork.BankAccountRepo.GetWhere(bankAccount => bankAccount.Id > 0 && bankAccount.UserId == userId);

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
            bankaccountVm.BankAccount = _unitOfWork.BankAccountRepo.GetWhere(bankAccount => bankAccount.Id == id).FirstOrDefault();
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
            // bankaccountVm.BankAccountsList = _unitOfWork.BankAccountRepo.GetWhere(bankAccount => bankAccount.Id > 0);

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
                var userId = User.Identity.GetUserId();
                bankAccountVm.BankAccount.Balance = bankAccountVm.BankAccount.InitialBalance;
                bankAccountVm.BankAccount.UserId = userId;
                _unitOfWork.BankAccountRepo.Create(bankAccountVm.BankAccount);
                _unitOfWork.Complete();
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
            bankaccountVm.BankAccount = _unitOfWork.BankAccountRepo.GetWhere(bankAccount => bankAccount.Id == id).FirstOrDefault();
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
                var userId = User.Identity.GetUserId();
                bankAccountVm.BankAccount.UserId = userId;
                _unitOfWork.BankAccountLogic.CalculateBalanceOfSelectedAccount(bankAccountVm.BankAccount);
                _unitOfWork.BankAccountRepo.Update(bankAccountVm.BankAccount);
                _unitOfWork.Complete();
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

            bankaccountVm.BankAccount = _unitOfWork.BankAccountRepo.GetWhere(bankAccount => bankAccount.Id == id).FirstOrDefault();
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

            bankaccountVm.BankAccount = _unitOfWork.BankAccountRepo.GetWhere(bankAccount => bankAccount.Id == id).FirstOrDefault();

            _unitOfWork.BankAccountRepo.Delete(bankaccountVm.BankAccount);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }


    }
}
