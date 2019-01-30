using HomeBudget2.ViewModels;

namespace HomeBudget2.DAL.Interfaces
{
    public interface ISubcategoryService
    {

        SubCategoryViewModel CreateSubCategoryViewModelWithSpecificId(int? id);

        SubCategoryViewModel CreateSubCatVmWithListOfSubCat(bool isSubCategoryAnExpenseSubCat, string userId);

        SubCategoryViewModel CreateSubCatVmWithSubCatAndWithSelectList(bool isSubCategoryAnExpenseSubCat, string userId, int? id = null);

        void AddSelectListOfCategoriesToSubCategoryVm(SubCategoryViewModel subCategoryVm, string userId);

        bool CanBeDeleted(int? id);

    }
}