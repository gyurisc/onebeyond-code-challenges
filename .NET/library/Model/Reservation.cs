﻿namespace OneBeyondApi.Model
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Guid BorrowerId { get; set; }
        public Guid BookId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int WaitListPosition { get; set; }
        public bool IsActive { get; set; }
        public DateTime? NotificationSent { get; set; } = null;
    }
}
