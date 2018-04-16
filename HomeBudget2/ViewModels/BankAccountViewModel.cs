using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeBudget2.Models;

namespace HomeBudget2.ViewModels
{
    public class BankAccountViewModel
    {
        public BankAccount BankAccount { get; set; }
        public List<BankAccount> BankAccountsList { get; set; }
    }
}