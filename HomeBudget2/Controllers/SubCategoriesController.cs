using HomeBudget2.DAL.Interfaces;
using HomeBudget2.Models;
using HomeBudget2.ViewModels;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HomeBudget2.Controllers
{
    [Authorize]
    public class SubCategoriesController : Controller
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;

        public SubCategoriesController(ISubCategoryRepository subCategoryRepository, ICategoryRepository categoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
            _categoryRepository = categoryRepository;
        }

        // GET: Expense SubCategories Index
        public ActionResult ExpenseSubCategoryIndex()
        {
            bool isSubCategoryAnExpenseSubCat = true;
            SubCategoryViewModel subCategoryVm = CreateSubCatVmWithListOfSubCat(isSubCategoryAnExpenseSubCat);
            return View("Index", subCategoryVm);

        }

        // GET: Income SubCategories Index
        public ActionResult IncomeSubCategoryIndex()
        {
            bool isSubCategoryAnExpenseSubCat = false;
            var subCategoryVm = CreateSubCatVmWithListOfSubCat(isSubCategoryAnExpenseSubCat);
            return View("Index", subCategoryVm);

        }

        // GET: SubCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SubCategoryViewModel subCategoryVm = CreateSubCategoryViewModelWithSpecificId(id);

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
            SubCategoryViewModel subCategoryVm = CreateSubCatVmWithSubCatAndWithSelectList(isExpense);
            return View("Create", subCategoryVm);
        }

        // GET: SubCategories/Create Income SubCategory
        public ActionResult CreateIncomeSubCategory()
        {
            bool isExpense = false;
            SubCategoryViewModel subCategoryVm = CreateSubCatVmWithSubCatAndWithSelectList(isExpense);
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

            var categories = _categoryRepository.GetWhere(category => category.Id > 0 && subCategoryVm.SubCategory.IsExpense ? category.IsExpense : category.IsIncome);
            subCategoryVm.SelectListOfCategories = new SelectList(categories, "Id", "CategoryName");
            return View(subCategoryVm);
        }

        // GET: SubCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoryViewModel subCategoryVm = CreateSubCategoryViewModelWithSpecificId(id);
            if (subCategoryVm.SubCategory == null)
            {
                return HttpNotFound();
            }
            var categories = _categoryRepository.GetWhere(category => category.Id > 0 && subCategoryVm.SubCategory.IsExpense ? category.IsExpense : category.IsIncome);
            subCategoryVm.SelectListOfCategories = new SelectList(categories, "Id", "CategoryName");

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

            var categories = _categoryRepository.GetWhere(category => category.Id > 0 && subCategoryVm.SubCategory.IsExpense ? category.IsExpense : category.IsIncome);
            subCategoryVm.SelectListOfCategories = new SelectList(categories, "Id", "CategoryName");

            return View(subCategoryVm);
        }

        // GET: SubCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoryViewModel subCategoryVm = CreateSubCategoryViewModelWithSpecificId(id);
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
            SubCategoryViewModel subCategoryVm = CreateSubCategoryViewModelWithSpecificId(id);
            _subCategoryRepository.Delete(subCategoryVm.SubCategory);
            if (subCategoryVm.SubCategory.IsExpense)
            {
                return RedirectToAction("ExpenseSubCategoryIndex", "SubCategories");
            }
            return RedirectToAction("IncomeSubCategoryIndex", "SubCategories");
        }




        private SubCategoryViewModel CreateSubCategoryViewModelWithSpecificId(int? id)
        {
            SubCategoryViewModel subCategoryVm = new SubCategoryViewModel();
            subCategoryVm.SubCategory = _subCategoryRepository
                .GetWhereWithIncludes(subCategory => subCategory.Id == id, subCategory => subCategory.Category)
                .FirstOrDefault();
            return subCategoryVm;
        }

        private SubCategoryViewModel CreateSubCatVmWithListOfSubCat(bool isSubCategoryAnExpenseSubCat)
        {
            SubCategoryViewModel subCategoryVm = new SubCategoryViewModel();
            subCategoryVm.ListOfSubCategories =
                _subCategoryRepository.GetWhereWithIncludes(subCat => subCat.Id > 0 && isSubCategoryAnExpenseSubCat ? subCat.IsExpense : subCat.IsIncome);
            if (subCategoryVm.ListOfSubCategories.Count == 0)
            {
                SubCategory subCategory = new SubCategory();
                if (isSubCategoryAnExpenseSubCat)
                {
                    subCategory.IsExpense = true;
                }
                else
                {
                    subCategory.IsIncome = true;
                }
                subCategoryVm.ListOfSubCategories.Add(subCategory);
            }

            return subCategoryVm;
        }

        private SubCategoryViewModel CreateSubCatVmWithSubCatAndWithSelectList(bool isSubCategoryAnExpenseSubCat)
        {
            SubCategoryViewModel subCategoryVm = new SubCategoryViewModel();
            subCategoryVm.SubCategory = new SubCategory();
            if (isSubCategoryAnExpenseSubCat)
            {
                subCategoryVm.SubCategory.IsExpense = true;
            }
            else
            {
                subCategoryVm.SubCategory.IsIncome = true;
            }

            var categories = _categoryRepository.GetWhere(category => category.Id > 0 && isSubCategoryAnExpenseSubCat ? category.IsExpense : category.IsIncome);
            subCategoryVm.SelectListOfCategories = new SelectList(categories, "Id", "CategoryName");
            return subCategoryVm;
        }
    }
}
