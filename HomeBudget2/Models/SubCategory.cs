namespace HomeBudget2.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }

        public bool IsExpense { get; set; }
        public bool IsIncome { get; set; }

        public virtual Category Category { get; set; }
    }
}