using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBeyondApi.Tests
{
    [TestClass]
    public class FineRepositoryTests
    {
        [TestMethod]
        public void AddFine_ShouldCreateFineAndReturnId()
        {
            // Arrange 
            var repository = new FineRepository();
            var fine = new Fine
            {
                Id = Guid.NewGuid(),
                BorrowerId = Guid.NewGuid(),
                BookStockId = Guid.NewGuid(),
                LoanEndDate = DateTime.Now.AddDays(-5),
                ReturnDate = DateTime.Now.AddDays(-2),
                OverDueInDays = 3,
                AmountToPay = 1.50m,
                IsPaid = false,
                CreatedDate = DateTime.Now,
            };

            // Act 
            var result = repository.CreateFine(fine);

            // Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(result, fine.Id);

            // Check if the fine was saved
            var savedFines = repository.GetFinesByBorrower(fine.BorrowerId);
            Assert.IsNotNull(savedFines);
            Assert.AreEqual(1, savedFines.Count);
            Assert.AreEqual(fine.Id, savedFines.First().Id);
            Assert.AreEqual(1.50m, savedFines.First().AmountToPay);

        }
    }
}
