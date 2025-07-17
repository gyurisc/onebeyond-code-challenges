using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IFineRepository
    {
        public Guid CreateFine(Fine fine);
        IList<Fine> GetFinesByBorrower(Guid borrowerId);
        public bool PayFine(Guid fineId);
    }
}
