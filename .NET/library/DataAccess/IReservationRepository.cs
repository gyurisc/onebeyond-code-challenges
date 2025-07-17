using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IReservationRepository
    {
        public List<ReservationDto> GetReservationsByBorrower(Guid borrowerId);
        public Guid CreateReservation(Reservation reservation);
    }
}
