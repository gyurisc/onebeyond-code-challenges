using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class ReservationRepository : IReservationRepository
    {
        public Guid CreateReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public List<ReservationDto> GetReservationsByBorrower(Guid borrowerId)
        {
            throw new NotImplementedException();
        }
    }
}
