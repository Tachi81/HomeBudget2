using HomeBudget2.DAL.Interfaces;
using HomeBudget2.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HomeBudget2.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: ExpenseCategories Index
        public ActionResult ExpenseCategoryIndex()
        {
            var categories = _unitOfWork.CategoryRepo.GetWhere(cat => cat.Id > 0 && cat.IsExpense);
            if (categories.Count == 0)
            {
                Category category = new Category();
                category.IsExpense = true;
                categories.Add(category);
            }
            return View("Index", categories);
        }

        // GET: IncomeCategories Index
        public ActionResult IncomeCategoryIndex()
        {
            var categories = _unitOfWork.CategoryRepo.GetWhere(cat => cat.Id > 0 && cat.IsIncome);
            if (categories.Count == 0)
            {
                Category category = new Category();
                category.IsIncome = true;
                categories.Add(category);
            }
            return View("Index", categories);
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = _unitOfWork.CategoryRepo.GetWhere(cat => cat.Id == id).FirstOrDefault();
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create ExpenseCategory
        public ActionResult CreateExpenseCategory()
        {
            Category category = new Category();
            category.IsExpense = true;
            return View("Create", category);
        }

        // GET: Categories/Create IncomeCategory
        public ActionResult CreateIncomeCategory()
        {
            Category category = new Category();
            category.IsIncome = true;
            return View("Create", category);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepo.Create(category);
                if (category.IsExpense)
                {
                    return RedirectToAction("ExpenseCategoryIndex", "Categories");
                }
                return RedirectToAction("IncomeCategoryIndex", "Categories");
            }

            return View("Create", category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _unitOfWork.CategoryRepo.GetWhere(cat => cat.Id == id).FirstOrDefault();
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepo.Update(category);
                _unitOfWork.Complete();
                if (category.IsExpense)
                {
                    return RedirectToAction("ExpenseCategoryIndex", "Categories");
                }
                return RedirectToAction("IncomeCategoryIndex", "Categories");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _unitOfWork.CategoryRepo.GetWhere(cat => cat.Id == id).FirstOrDefault();
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = _unitOfWork.CategoryRepo.GetWhere(cat => cat.Id == id).FirstOrDefault();
            _unitOfWork.CategoryRepo.Delete(category);
            _unitOfWork.Complete();
            return RedirectToAction("ExpenseCategoryIndex");
        }


    }
}
