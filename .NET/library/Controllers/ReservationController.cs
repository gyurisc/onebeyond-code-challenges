using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(ILogger<ReservationController> logger, IReservationRepository reservationRepository)
        {
            _logger = logger;
            _reservationRepository = reservationRepository;
        }

        [HttpPost]
        [Route("ReserveBook")]
        public Guid ReserveBook(ReserveBookRequest request) 
        {
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                BorrowerId = request.BorrowerId,
                BookId = request.BookId,
                ReservationDate = DateTime.Now,
                IsActive = true
            };

            return _reservationRepository.CreateReservation(reservation);
        }

        [HttpGet]
        [Route("GetReservations/{borrowerId}")]
        public IList<ReservationDto> GetReservations(Guid borrowerId) 
        { 
            return _reservationRepository.GetReservationsByBorrower(borrowerId);
        }


        [HttpDelete]
        [Route("CancelReservation/{reservationId}")]
        public bool CancelReservation(Guid reservationId)
        {
            return _reservationRepository.CancelReservation(reservationId); 
        }

        [HttpGet]
        [Route("GetBookReservations/{bookId}")]
        public IList<Reservation> GetBookReservations(Guid bookId)
        {
            return _reservationRepository.GetReservationsByBook(bookId);
        }
    }
}
