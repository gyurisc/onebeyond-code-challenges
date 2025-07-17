using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IReservationRepository
    {
        public List<ReservationDto> GetReservationsByBorrower(Guid borrowerId);
        public Guid CreateReservation(Reservation reservation);
        bool CancelReservation(Guid reservationId);
        IList<Reservation> GetReservationsByBook(Guid bookId);
        Reservation GetNextReservation(Guid bookId);
    }
}
