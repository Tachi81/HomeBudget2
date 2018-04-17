using HomeBudget2.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HomeBudget2.ViewModels
{
    public class SubCategoryViewModel
    {
        public SubCategory SubCategory { get; set; }
        public List<SubCategory> ListOfSubCategories { get; set; }

        public IEnumerable<SelectListItem> SelectListOfCategories { get; set; }
    }
}