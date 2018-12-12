using HomeBudget2.DAL.Interfaces;
using HomeBudget2.ViewModels;
using System.Net;
using System.Web.Mvc;

namespace HomeBudget2.Controllers
{
    [Authorize]
    public class SubCategoriesController : Controller
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ISubcategoryService _subcategoryService;

        public SubCategoriesController(ISubCategoryRepository subCategoryRepository, ISubcategoryService subcategoryService)
        {
            _subCategoryRepository = subCategoryRepository;
            _subcategoryService = subcategoryService;
        }

        // GET: Expense SubCategories Index
        public ActionResult ExpenseSubCategoryIndex()
        {
            bool isSubCategoryAnExpenseSubCat = true;
            SubCategoryViewModel subCategoryVm = _subcategoryService.CreateSubCatVmWithListOfSubCat(isSubCategoryAnExpenseSubCat);
            return View("Index", subCategoryVm);

        }

        // GET: Income SubCategories Index
        public ActionResult IncomeSubCategoryIndex()
        {
            bool isSubCategoryAnExpenseSubCat = false;
            var subCategoryVm = _subcategoryService.CreateSubCatVmWithListOfSubCat(isSubCategoryAnExpenseSubCat);
            return View("Index", subCategoryVm);

        }

        // GET: SubCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SubCategoryViewModel subCategoryVm = _subcategoryService.CreateSubCategoryViewModelWithSpecificId(id);

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
            SubCategoryViewModel subCategoryVm = _subcategoryService.CreateSubCatVmWithSubCatAndWithSelectList(isExpense);
            return View("Create", subCategoryVm);
        }

        // GET: SubCategories/Create Income SubCategory
        public ActionResult CreateIncomeSubCategory()
        {
            bool isExpense = false;
            SubCategoryViewModel subCategoryVm = _subcategoryService.CreateSubCatVmWithSubCatAndWithSelectList(isExpense);
            return View("Create", subCategoryVm);
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
                _subCategoryRepository.Create(subCategoryVm.SubCategory);
                if (subCategoryVm.SubCategory.IsExpense)
                {
                    return RedirectToAction("ExpenseSubCategoryIndex", "SubCategories");
                }
                return RedirectToAction("IncomeSubCategoryIndex", "SubCategories");
            }

            _subcategoryService.AddSelectListOfCategoriesToSubCategoryVm(subCategoryVm);
            return View(subCategoryVm);
        }



        // GET: SubCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoryViewModel subCategoryVm = _subcategoryService.CreateSubCategoryViewModelWithSpecificId(id);
            if (subCategoryVm.SubCategory == null)
            {
                return HttpNotFound();
            }
            _subcategoryService.AddSelectListOfCategoriesToSubCategoryVm(subCategoryVm);

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
                _subCategoryRepository.Update(subCategoryVm.SubCategory);
                if (subCategoryVm.SubCategory.IsExpense)
                {
                    return RedirectToAction("ExpenseSubCategoryIndex", "SubCategories");
                }
                return RedirectToAction("IncomeSubCategoryIndex", "SubCategories");
            }

            _subcategoryService.AddSelectListOfCategoriesToSubCategoryVm(subCategoryVm);

            return View(subCategoryVm);
        }

        // GET: SubCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoryViewModel subCategoryVm = _subcategoryService.CreateSubCategoryViewModelWithSpecificId(id);
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
            SubCategoryViewModel subCategoryVm = _subcategoryService.CreateSubCategoryViewModelWithSpecificId(id);
            if (_subcategoryService.CanBeDeleted(id))
            {
                _subCategoryRepository.Delete(subCategoryVm.SubCategory);
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
