using HomeBudget2.DAL.Interfaces;
using HomeBudget2.Models;
using HomeBudget2.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HomeBudget2.Service
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubcategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public SubCategoryViewModel CreateSubCategoryViewModelWithSpecificId(int? id)
        {
            SubCategoryViewModel subCategoryVm = new SubCategoryViewModel();
            subCategoryVm.SubCategory = _unitOfWork.CategoryRepo
                .GetWhereWithIncludes(subCategory => subCategory.Id == id, subCategory => subCategory.ParentCategory)
                .FirstOrDefault();
            return subCategoryVm;
        }
        
        public SubCategoryViewModel CreateSubCatVmWithListOfSubCat(bool isSubCategoryAnExpenseSubCat, string userId)
        {
            SubCategoryViewModel subCategoryVm = new SubCategoryViewModel();
            subCategoryVm.ListOfSubCategories =
                _unitOfWork.CategoryRepo
                    .GetWhereWithIncludes(
                        subCat => subCat.Id > 0 && subCat.UserId == userId && subCat.IsExpense == isSubCategoryAnExpenseSubCat,
                        subcat => subcat.ParentCategory).OrderBy(sc => sc.ParentCategory.CategoryName).ToList();
            if (subCategoryVm.ListOfSubCategories.Count == 0)
            {
                Category subCategory = new Category();
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

        public SubCategoryViewModel CreateSubCatVmWithSubCatAndWithSelectList(bool isSubCategoryAnExpenseSubCat, string userId, int? categoryId = null)
        {
            SubCategoryViewModel subCategoryVm = new SubCategoryViewModel();
            subCategoryVm.SubCategory = new Category();
            if (isSubCategoryAnExpenseSubCat)
            {
                subCategoryVm.SubCategory.IsExpense = true;
            }
            else
            {
                subCategoryVm.SubCategory.IsIncome = true;
            }

            var categories = new List<Category>();
            if (categoryId == null)
            {
                 categories = _unitOfWork.CategoryRepo.GetWhere(category => category.Id > 0 && category.UserId == userId && category.IsExpense == isSubCategoryAnExpenseSubCat);
            }
            else
            {
                categories = _unitOfWork.CategoryRepo.GetWhere(category => category.Id == categoryId && category.UserId == userId);

            }
            subCategoryVm.SelectListOfCategories = new SelectList(categories, "Id", "CategoryName");
            return subCategoryVm;
        }

        public void AddSelectListOfCategoriesToSubCategoryVm(SubCategoryViewModel subCategoryVm, string userId)
        {
            var categories = _unitOfWork.CategoryRepo.GetWhere(category => category.Id > 0
                                                                      && category.UserId == userId
                                                                      &&  category.IsExpense == subCategoryVm.SubCategory.IsExpense );
            subCategoryVm.SelectListOfCategories = new SelectList(categories, "Id", "CategoryName");
        }

        public bool CanBeDeleted(int? id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.FinancialOperations.Where(fo => fo.SubCategoryId == id).ToList().Count == 0;
            }
        }
    }
}