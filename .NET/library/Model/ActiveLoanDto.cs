namespace OneBeyondApi.Model
{
    public class ActiveLoanDto
    {
        public Guid BorrowerId { get; set; }
        public string BorrowerName { get; set; }
        public string BorrowerEmail { get; set; }
        public Guid BookStockId { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor {  get; set; }
        public string BookISBN { get; set; }
        public DateTime LoanEndDate {  get; set; }
        public bool IsOverDue { get; set; }
    }
}
