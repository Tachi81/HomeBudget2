using HomeBudget2.DAL.Interfaces;
using HomeBudget2.Models;
using HomeBudget2.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace HomeBudget2.Service
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;

        public SubcategoryService(ISubCategoryRepository subCategoryRepository, ICategoryRepository categoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
            _categoryRepository = categoryRepository;
        }


        public SubCategoryViewModel CreateSubCategoryViewModelWithSpecificId(int? id)
        {
            SubCategoryViewModel subCategoryVm = new SubCategoryViewModel();
            subCategoryVm.SubCategory = _subCategoryRepository
                .GetWhereWithIncludes(subCategory => subCategory.Id == id, subCategory => subCategory.Category)
                .FirstOrDefault();
            return subCategoryVm;
        }

        public SubCategoryViewModel CreateSubCatVmWithListOfSubCat(bool isSubCategoryAnExpenseSubCat)
        {
            SubCategoryViewModel subCategoryVm = new SubCategoryViewModel();
            subCategoryVm.ListOfSubCategories =
                _subCategoryRepository
                    .GetWhereWithIncludes(
                        subCat => subCat.Id > 0 && isSubCategoryAnExpenseSubCat ? subCat.IsExpense : subCat.IsIncome,
                        subcat => subcat.Category).OrderBy(sc => sc.Category.CategoryName).ToList();
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

        public SubCategoryViewModel CreateSubCatVmWithSubCatAndWithSelectList(bool isSubCategoryAnExpenseSubCat)
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

        public void AddSelectListOfCategoriesToSubCategoryVm(SubCategoryViewModel subCategoryVm)
        {
            var categories = _categoryRepository.GetWhere(category => category.Id > 0 && subCategoryVm.SubCategory.IsExpense ? category.IsExpense : category.IsIncome);
            subCategoryVm.SelectListOfCategories = new SelectList(categories, "Id", "CategoryName");
        }
    }
}