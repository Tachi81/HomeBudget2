namespace HomeBudget2.Models
{
    public class SourceOMoney
    {
        public int Id { get; set; }

        public int? SubCategoryId { get; set; }
        public int? BankAccountId { get; set; }

        public virtual SubCategory SubCategory { get; set; }
        public virtual BankAccount BankAccount { get; set; }

    }
}