namespace OneBeyondApi.Model
{
    public class ReservationDto
    {
        public Guid ReservationId { get; set; }
        public string BookTitle { get; set; }
        public string AuthorName { get; set; }
        public int WaitListPosition { get; set; }
        public  DateTime? EstimatedAvailability { get; set; }
    }
}
