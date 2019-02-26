using HomeBudget2.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HomeBudget2.ViewModels
{
    public class SubCategoryViewModel
    {
        public Category SubCategory { get; set; }
        public List<Category> ListOfSubCategories { get; set; }

        public IEnumerable<SelectListItem> SelectListOfCategories { get; set; }
    }
}