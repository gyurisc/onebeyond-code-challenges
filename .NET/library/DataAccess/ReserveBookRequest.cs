namespace OneBeyondApi.DataAccess
{
    public class ReserveBookRequest
    {
        public Guid BookId { get; set; }
        public Guid BorrowerId { get; set; }
    }
}
