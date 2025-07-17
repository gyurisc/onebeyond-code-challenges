using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class ReservationRepository : IReservationRepository
    {
        public bool CancelReservation(Guid reservationId)
        {
            using (var context = new LibraryContext()) 
            {
                var reservation = context.Reservations.Find(reservationId);
                if (reservation == null) return false;

                reservation.IsActive = false;

                var remainingReservations = context.Reservations
                    .Where(r => r.BookId == reservation.BookId &&
                        r.IsActive &&
                        r.WaitListPosition > reservation.WaitListPosition)
                    .ToList();

                foreach(var r in remainingReservations)
                {
                    r.WaitListPosition--;
                }

                context.SaveChanges();
                return true;
            }
        }

        public Guid CreateReservation(Reservation reservation)
        {
            using (var context = new LibraryContext())
            {
                var isAvailable = context.Catalogue
                    .Any(bs => bs.Book.Id == reservation.BookId && bs.OnLoanTo == null);

                if (isAvailable) 
                {
                    throw new InvalidOperationException("Book is currently available.");
                }

                var existingReservation = context.Reservations
                    .FirstOrDefault(r => r.BorrowerId == reservation.BorrowerId &&
                        r.BookId == reservation.BookId &&
                        r.IsActive);

                if (existingReservation != null)
                {
                    throw new InvalidOperationException("Borrower already has reservation for this book!");
                }

                reservation.WaitListPosition = context.Reservations
                    .Count(r => r.BookId == reservation.BookId && r.IsActive) + 1;

                context.Reservations.Add(reservation); 
                context.SaveChanges();

                return reservation.Id; 


            }
        }

        public Reservation GetNextReservation(Guid bookId)
        {
            using (var context = new LibraryContext())
            { 
                var reservation = context.Reservations
                    .Where(r => r.BookId == bookId && r.IsActive)
                    .OrderBy(r => r.ReservationDate)
                    .FirstOrDefault();

                return reservation;
            }
        }

        public IList<Reservation> GetReservationsByBook(Guid bookId)
        {
            using (var context = new LibraryContext()) 
            {
                var reservationsForBook = context.Reservations
                    .Where(r => r.BookId == bookId && r.IsActive)
                    .OrderBy(r => r.ReservationDate)
                    .ToList(); 

                return reservationsForBook;
            }
        }

        public List<ReservationDto> GetReservationsByBorrower(Guid borrowerId)
        {
            using (var context = new LibraryContext())
            {
                var reservations = context.Reservations
                    .Where(r => r.BorrowerId == borrowerId & r.IsActive)
                    .Select(r => new ReservationDto 
                    { 
                        ReservationId = r.Id,
                        BookTitle = context.Books.First(b => b.Id == r.BookId).Name,
                        AuthorName = context.Books.First(b => b.Id == r.BookId).Author.Name,
                        WaitListPosition = r.WaitListPosition, 
                        EstimatedAvailability = CalculateAvailability(r.BookId, r.WaitListPosition)

                    })
                    .ToList();


                return reservations;
            }
        }

        private DateTime? CalculateAvailability(Guid bookId, int waitListPosition)
        {
            using (var context = new LibraryContext())
            {
                var loanEndDates = context.Catalogue
                    .Where(bs => bs.Book.Id == bookId && bs.LoanEndDate.HasValue)
                    .Select(bs => bs.LoanEndDate.Value)
                    .OrderBy(d => d)
                    .ToList();

                if (loanEndDates.Count >= waitListPosition)
                {
                    return loanEndDates[waitListPosition - 1];
                }

                // Estimate if we do not have enough loan end dates.
                var averageLoanDays = 14;
                var baseDate = loanEndDates.LastOrDefault(); 
                return baseDate.AddDays(averageLoanDays * (waitListPosition - loanEndDates.Count));

            }
        }
    }
}
