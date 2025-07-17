using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class OnLoanRepository : IOnLoanRepository
    {
        private readonly IFineRepository _fineRepository;
        private readonly IReservationRepository _reservationRepository;

        public OnLoanRepository(IFineRepository fineRepository, IReservationRepository reservationRepository) 
        {
            _fineRepository = fineRepository;
            _reservationRepository = reservationRepository;
        }

        public IList<ActiveLoanDto> GetActiveLoans()
        {
            using (var context = new LibraryContext())
            {
                var activeLoans = context.Catalogue
                    .Where(bs => bs.OnLoanTo != null && bs.LoanEndDate.HasValue)
                    .Select(bs => new ActiveLoanDto 
                    { 
                        BorrowerId = bs.OnLoanTo.Id, 
                        BorrowerName = bs.OnLoanTo.Name,
                        BorrowerEmail = bs.OnLoanTo.EmailAddress,
                        BookStockId = bs.Id,
                        BookTitle = bs.Book.Name, 
                        BookAuthor = bs.Book.Author.Name,
                        LoanEndDate = bs.LoanEndDate.Value, 
                        IsOverDue = bs.LoanEndDate < DateTime.Now
                    })
                    .ToList();


                return activeLoans;
            }
        }

        public bool ReturnBook(Guid BookStockId)
        {
            using (var context = new LibraryContext())
            {
                // find the bookstock record 
                var bookStock = context.Catalogue.FirstOrDefault(bs => bs.Id == BookStockId); 

                // check if it is on loan
                if(bookStock?.OnLoanTo == null || !bookStock.LoanEndDate.HasValue)
                {
                    return false; 
                }

                var returnDate = DateTime.Now;
                var isOverDue = bookStock.LoanEndDate?.Date < returnDate.Date;

                // if it is overdue create a fine 
                if (isOverDue)
                { 
                    var loanEndDate = bookStock.LoanEndDate ?? returnDate;
                    var overdueDays = (int)(returnDate - loanEndDate).TotalDays;
                    var fine = new Fine
                    {
                        Id = Guid.NewGuid(),
                        BorrowerId = bookStock.OnLoanTo.Id,
                        BookStockId = bookStock.Id,
                        LoanEndDate = loanEndDate,
                        ReturnDate = returnDate,
                        CreatedDate = returnDate,
                        OverDueInDays = overdueDays,
                        AmountToPay = overdueDays * 0.6m,
                        IsPaid = false,
                    }; 

                    _fineRepository.CreateFine(fine);
                }

                // TODO: if we have reservation then process it 
                var nextReservation = _reservationRepository.GetNextReservation(bookStock.Book.Id);
                if (nextReservation != null)
                {
                    // Send notification that the book is available
                    using (var reservationContext = new LibraryContext())
                    {
                        var reservation = reservationContext.Reservations.Find(nextReservation);
                        if (reservation != null)
                        {
                            reservation.NotificationSent = DateTime.Now;
                            reservationContext.SaveChanges();
                        }
                    }
                }

                // update the book stock record so it is not loaned anymore 
                bookStock.OnLoanTo = null;
                bookStock.LoanEndDate = null;

                context.SaveChanges();
                return true;
            }
        }
    }
}
