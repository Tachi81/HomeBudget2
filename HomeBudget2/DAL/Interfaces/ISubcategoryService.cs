using HomeBudget2.ViewModels;

namespace HomeBudget2.DAL.Interfaces
{
    public interface ISubcategoryService
    {

        SubCategoryViewModel CreateSubCategoryViewModelWithSpecificId(int? id);

        SubCategoryViewModel CreateSubCatVmWithListOfSubCat(bool isSubCategoryAnExpenseSubCat);

        SubCategoryViewModel CreateSubCatVmWithSubCatAndWithSelectList(bool isSubCategoryAnExpenseSubCat);
         
        void AddSelectListOfCategoriesToSubCategoryVm(SubCategoryViewModel subCategoryVm);

    }
}