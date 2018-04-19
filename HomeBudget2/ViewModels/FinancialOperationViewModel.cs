using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeBudget2.Models;

namespace HomeBudget2.ViewModels
{
    public class FinancialOperationViewModel
    {
        public FinancialOperation FinancialOperation { get; set; }
        public List<FinancialOperation> ListOfFinancialOperations { get; set; }

        public IEnumerable<SelectListItem> SelectListOfBankAccounts{ get; set; }
        public IEnumerable<SelectListItem> SelectListOfCategories { get; set; }
        public IEnumerable<SelectListItem> SelectListOfSubCategories { get; set; }
    }
}