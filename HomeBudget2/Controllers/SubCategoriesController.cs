using HomeBudget2.DAL.Interfaces;
using HomeBudget2.ViewModels;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Web.Mvc;

namespace HomeBudget2.Controllers
{
    [Authorize]
    public class SubCategoriesController : Controller
    {       
        private readonly IUnitOfWork _unitOfWork;

        public SubCategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Expense SubCategories Index
        public ActionResult ExpenseSubCategoryIndex()
        {
            bool isSubCategoryAnExpenseSubCat = true;
            SubCategoryViewModel subCategoryVm = _unitOfWork.SubcategoryService.CreateSubCatVmWithListOfSubCat(isSubCategoryAnExpenseSubCat, User.Identity.GetUserId());
            return View("Index", subCategoryVm);

        }

        // GET: Income SubCategories Index
        public ActionResult IncomeSubCategoryIndex()
        {
            bool isSubCategoryAnExpenseSubCat = false;
            var subCategoryVm = _unitOfWork.SubcategoryService.CreateSubCatVmWithListOfSubCat(isSubCategoryAnExpenseSubCat, User.Identity.GetUserId());
            return View("Index", subCategoryVm);

        }

        // GET: SubCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SubCategoryViewModel subCategoryVm = _unitOfWork.SubcategoryService.CreateSubCategoryViewModelWithSpecificId(id);

            if (subCategoryVm.SubCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategoryVm);
        }



        // GET: SubCategories/Create Expense SubCategory
        public ActionResult CreateExpenseSubCategory()
        {
            bool isExpense = true;
            SubCategoryViewModel subCategoryVm = _unitOfWork.SubcategoryService.CreateSubCatVmWithSubCatAndWithSelectList(isExpense, User.Identity.GetUserId());
            return View("Create", subCategoryVm);
        }

        // GET: SubCategories/Create Income SubCategory
        public ActionResult CreateIncomeSubCategory()
        {
            bool isExpense = false;
            SubCategoryViewModel subCategoryVm = _unitOfWork.SubcategoryService.CreateSubCatVmWithSubCatAndWithSelectList(isExpense, User.Identity.GetUserId());
            return View("Create", subCategoryVm);
        }

        // GET: SubCategories/Create/5
        public ActionResult Create(int? id, bool isExpense)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoryViewModel subCategoryVm = _unitOfWork.SubcategoryService.CreateSubCatVmWithSubCatAndWithSelectList(isExpense, User.Identity.GetUserId(), id);
            if (subCategoryVm.SubCategory == null)
            {
                return HttpNotFound();
            }
            
            return View(subCategoryVm);
        }

        // POST: SubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubCategoryViewModel subCategoryVm)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                subCategoryVm.SubCategory.UserId = userId;
                _unitOfWork.CategoryRepo.Create(subCategoryVm.SubCategory);
                _unitOfWork.Complete();
                if (subCategoryVm.SubCategory.IsExpense)
                {
                    return RedirectToAction("ExpensesIndex", "FinancialOperations");
                }
                return RedirectToAction("IncomesIndex", "FinancialOperations");
            }

            _unitOfWork.SubcategoryService.AddSelectListOfCategoriesToSubCategoryVm(subCategoryVm, User.Identity.GetUserId());
            return View(subCategoryVm);
        }



        // GET: SubCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoryViewModel subCategoryVm = _unitOfWork.SubcategoryService.CreateSubCategoryViewModelWithSpecificId(id);
            if (subCategoryVm.SubCategory == null)
            {
                return HttpNotFound();
            }
            _unitOfWork.SubcategoryService.AddSelectListOfCategoriesToSubCategoryVm(subCategoryVm, User.Identity.GetUserId());

            return View(subCategoryVm);
        }

        // POST: SubCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubCategoryViewModel subCategoryVm)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                subCategoryVm.SubCategory.UserId = userId;
                _unitOfWork.CategoryRepo.Update(subCategoryVm.SubCategory);
                _unitOfWork.Complete();
                if (subCategoryVm.SubCategory.IsExpense)
                {
                    return RedirectToAction("ExpenseSubCategoryIndex", "SubCategories");
                }
                return RedirectToAction("IncomeSubCategoryIndex", "SubCategories");
            }

            _unitOfWork.SubcategoryService.AddSelectListOfCategoriesToSubCategoryVm(subCategoryVm, User.Identity.GetUserId());

            return View(subCategoryVm);
        }

        // GET: SubCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoryViewModel subCategoryVm = _unitOfWork.SubcategoryService.CreateSubCategoryViewModelWithSpecificId(id);
            if (subCategoryVm.SubCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategoryVm);
        }

        // POST: SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubCategoryViewModel subCategoryVm = _unitOfWork.SubcategoryService.CreateSubCategoryViewModelWithSpecificId(id);
            if (_unitOfWork.SubcategoryService.CanBeDeleted(id))
            {
                _unitOfWork.CategoryRepo.Delete(subCategoryVm.SubCategory);
                _unitOfWork.Complete();
            }
            else
            {
                //todo: alert user that he can't delete
            }

            if (subCategoryVm.SubCategory.IsExpense)
            {
                return RedirectToAction("ExpenseSubCategoryIndex", "SubCategories");
            }
            return RedirectToAction("IncomeSubCategoryIndex", "SubCategories");
        }
    }
}
