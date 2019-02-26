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

        public int? ParentCategoryId { get; set; }

        public virtual ICollection<Category> Subcategories { get; set; }
        public virtual Category ParentCategory { get; set; }
    }
}