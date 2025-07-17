namespace OneBeyondApi.Model
{
    public class Fine
    {
        public Guid Id { get; set; }
        public Guid BorrowerId { get; set; }
        public Guid BookStockId { get; set; }
        public DateTime LoanEndDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int OverDueInDays { get; set; }
        public decimal AmountToPay { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
