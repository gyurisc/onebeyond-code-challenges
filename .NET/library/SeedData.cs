﻿using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi
{
    public class SeedData
    {
        public static void SetInitialData()
        {
            var ernestMonkjack = new Author
            {
                Name = "Ernest Monkjack"
            };
            var sarahKennedy = new Author
            {
                Name = "Sarah Kennedy"
            };
            var margaretJones = new Author
            {
                Name = "Margaret Jones"
            };

            var clayBook = new Book
            {
                Name = "The Importance of Clay",
                Format = BookFormat.Paperback,
                Author = ernestMonkjack,
                ISBN = "1305718181"
            };

            var agileBook = new Book
            {
                Name = "Agile Project Management - A Primer",
                Format = BookFormat.Hardback,
                Author = sarahKennedy,
                ISBN = "1293910102"
            };

            var rustBook = new Book
            {
                Id = Guid.NewGuid(),
                Name = "Rust Development Cookbook",
                Format = BookFormat.Paperback,
                Author = margaretJones,
                ISBN = "3134324111"
            };

            var daveSmith = new Borrower
            {
                Name = "Dave Smith",
                EmailAddress = "dave@smithy.com"
            };

            var lianaJames = new Borrower
            {
                Id = Guid.NewGuid(),
                Name = "Liana James",
                EmailAddress = "liana@gmail.com"
            };

            var bookOnLoanUntilToday = new BookStock {
                Book = clayBook,
                OnLoanTo = daveSmith,
                LoanEndDate = DateTime.Now.Date
            };

            var bookNotOnLoan = new BookStock
            {
                Book = clayBook,
                OnLoanTo = null,
                LoanEndDate = null
            };

            var bookOnLoanUntilNextWeek = new BookStock
            {
                Book = agileBook,
                OnLoanTo = lianaJames,
                LoanEndDate = DateTime.Now.Date.AddDays(7)
            };

            var rustBookStock = new BookStock
            {
                Book = rustBook,
                OnLoanTo = null,
                LoanEndDate = null
            };

            var bookOnLoanExpired = new BookStock
            {
                Id = Guid.NewGuid(),
                Book = clayBook,
                OnLoanTo = lianaJames,
                LoanEndDate = DateTime.Now.Date.AddDays(-4)
            };

            var reservationOfRustBook = new Reservation
            {
                Id = Guid.NewGuid(),
                BorrowerId = lianaJames.Id,
                BookId = rustBook.Id,
                IsActive = true,
                WaitListPosition = 1,
                ReservationDate = DateTime.Now.AddDays(-5),
            };

            var smallFine = new Fine
            {
                Id = Guid.NewGuid(),
                BorrowerId = lianaJames.Id,
                BookStockId = bookOnLoanExpired.Id,
                LoanEndDate = DateTime.Now.AddDays(-6),
                ReturnDate = DateTime.Now.AddDays(-3),
                OverDueInDays = 3, 
                AmountToPay = 2.5m,
                IsPaid = false,
                CreatedDate = DateTime.Now.AddDays(-3),
            };

            using (var context = new LibraryContext())
            {
                context.Authors.Add(ernestMonkjack);
                context.Authors.Add(sarahKennedy);
                context.Authors.Add(margaretJones);

                context.Books.Add(clayBook);
                context.Books.Add(agileBook);
                context.Books.Add(rustBook);

                context.Borrowers.Add(daveSmith);
                context.Borrowers.Add(lianaJames);

                context.Catalogue.Add(bookOnLoanUntilToday);
                context.Catalogue.Add(bookNotOnLoan);
                context.Catalogue.Add(bookOnLoanUntilNextWeek);
                context.Catalogue.Add(rustBookStock);
                context.Catalogue.Add(bookOnLoanExpired);
                context.Reservations.Add(reservationOfRustBook);
                context.Fines.Add(smallFine);

                context.SaveChanges();

            }
        }
    }
}
