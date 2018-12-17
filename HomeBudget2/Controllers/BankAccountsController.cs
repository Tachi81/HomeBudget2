﻿using HomeBudget2.DAL.Interfaces;
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
            var userId = User.Identity.GetUserId();
            var bankaccountVm = new BankAccountViewModel();

            bankaccountVm.BankAccountsList = _bankAccountRepository.GetWhere(bankAccount => bankAccount.Id > 0 && bankAccount.UserId == userId);

            return View(bankaccountVm);
        }

        // GET: BankAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            var bankaccountVm = new BankAccountViewModel();
            bankaccountVm.BankAccount = _bankAccountRepository.GetWhere(bankAccount => bankAccount.Id == id && bankAccount.UserId == userId).FirstOrDefault();
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
                var userId = User.Identity.GetUserId();
                bankAccountVm.BankAccount.Balance = bankAccountVm.BankAccount.InitialBalance;
                bankAccountVm.BankAccount.UserId = userId;
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
            var userId = User.Identity.GetUserId();
            var bankaccountVm = new BankAccountViewModel();
            bankaccountVm.BankAccount = _bankAccountRepository.GetWhere(bankAccount => bankAccount.Id == id && bankAccount.UserId == userId).FirstOrDefault();
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
                _bankAccountLogic.CalculateBalanceOfSelectedAccount(bankAccountVm.BankAccount);
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
            var userId = User.Identity.GetUserId();
            var bankaccountVm = new BankAccountViewModel();

            bankaccountVm.BankAccount = _bankAccountRepository.GetWhere(bankAccount => bankAccount.Id == id && bankAccount.UserId == userId).FirstOrDefault();
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
            var userId = User.Identity.GetUserId();
            bankaccountVm.BankAccount = _bankAccountRepository.GetWhere(bankAccount => bankAccount.Id == id && bankAccount.UserId == userId).FirstOrDefault();

            _bankAccountRepository.Delete(bankaccountVm.BankAccount);
            return RedirectToAction("Index");
        }


    }
}
