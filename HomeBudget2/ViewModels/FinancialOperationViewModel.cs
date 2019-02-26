using HomeBudget2.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HomeBudget2.ViewModels
{
    public class FinancialOperationViewModel
    {
        public FinancialOperation FinancialOperation { get; set; }
        public List<FinancialOperation> ListOfFinancialOperations { get; set; }

        public string UserId { get; set; }
        public string ErrorMessage { get; set; }

        public IEnumerable<SelectListItem> SelectListOfBankAccounts { get; set; }
        public IEnumerable<SelectListItem> SelectListOfCategories { get; set; }
        public IEnumerable<SelectListItem> SelectListOfSubCategories { get; set; }
    }
}