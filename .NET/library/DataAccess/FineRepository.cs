using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class FineRepository : IFineRepository
    {
        public Guid CreateFine(Fine fine)
        {
            using (var context = new LibraryContext()) 
            { 
                context.Fines.Add(fine);
                context.SaveChanges();
                return fine.Id;
            }
        }

        public IList<Fine> GetFinesByBorrower(Guid borrowerId)
        {
            using (var context = new LibraryContext())
            {
                var finesByBorrower = context.Fines
                    .Where(f => f.BorrowerId == borrowerId)
                    .OrderByDescending(f => f.CreatedDate)
                    .ToList();

                return finesByBorrower;
            }
        }

        public bool PayFine(Guid fineId)
        {
            using (var context = new LibraryContext())
            {
                var fine = context.Fines.Find(fineId);

                if (fine == null) return false; 

                fine.IsPaid = true;
                context.SaveChanges();
                return true;
            }
        }
    }
}
