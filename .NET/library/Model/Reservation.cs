namespace OneBeyondApi.Model
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Guid BorrowerId { get; set; }
        public Guid BookId { get; set; }
        public DateTime Created { get; set; }
        public int WaitListPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
