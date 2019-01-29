using System.Collections.Generic;

namespace HomeBudget2.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public bool IsExpense { get; set; }
        public bool IsIncome { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<SubCategory> Subcategories { get; set; }
    }
}