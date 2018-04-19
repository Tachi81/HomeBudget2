using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeBudget2.Models;

namespace HomeBudget2.Controllers
{
    public class FinancialOperationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FinancialOperations
        public ActionResult Index()
        {
            var financialOperations = db.FinancialOperations.Include(f => f.BankAccount).Include(f => f.SubCategory).Include(f => f.TargetBankAccount);
            return View(financialOperations.ToList());
        }

        // GET: FinancialOperations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialOperation financialOperation = db.FinancialOperations.Find(id);
            if (financialOperation == null)
            {
                return HttpNotFound();
            }
            return View(financialOperation);
        }

        // GET: FinancialOperations/Create
        public ActionResult Create()
        {
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "AccountName");
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "SubCategoryName");
            ViewBag.TargetBankAccountId = new SelectList(db.BankAccounts, "Id", "AccountName");
            return View();
        }

        // POST: FinancialOperations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AmountOfMoney,DateTime,Note,BankAccountId,SubCategoryId,TargetBankAccountId,SourceOMoney,DestinationOfMoney,IsTransfer,IsExpense,IsIncome")] FinancialOperation financialOperation)
        {
            if (ModelState.IsValid)
            {
                db.FinancialOperations.Add(financialOperation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "AccountName", financialOperation.BankAccountId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "SubCategoryName", financialOperation.SubCategoryId);
            ViewBag.TargetBankAccountId = new SelectList(db.BankAccounts, "Id", "AccountName", financialOperation.TargetBankAccountId);
            return View(financialOperation);
        }

        // GET: FinancialOperations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialOperation financialOperation = db.FinancialOperations.Find(id);
            if (financialOperation == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "AccountName", financialOperation.BankAccountId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "SubCategoryName", financialOperation.SubCategoryId);
            ViewBag.TargetBankAccountId = new SelectList(db.BankAccounts, "Id", "AccountName", financialOperation.TargetBankAccountId);
            return View(financialOperation);
        }

        // POST: FinancialOperations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AmountOfMoney,DateTime,Note,BankAccountId,SubCategoryId,TargetBankAccountId,SourceOMoney,DestinationOfMoney,IsTransfer,IsExpense,IsIncome")] FinancialOperation financialOperation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(financialOperation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "AccountName", financialOperation.BankAccountId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "SubCategoryName", financialOperation.SubCategoryId);
            ViewBag.TargetBankAccountId = new SelectList(db.BankAccounts, "Id", "AccountName", financialOperation.TargetBankAccountId);
            return View(financialOperation);
        }

        // GET: FinancialOperations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialOperation financialOperation = db.FinancialOperations.Find(id);
            if (financialOperation == null)
            {
                return HttpNotFound();
            }
            return View(financialOperation);
        }

        // POST: FinancialOperations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FinancialOperation financialOperation = db.FinancialOperations.Find(id);
            db.FinancialOperations.Remove(financialOperation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
