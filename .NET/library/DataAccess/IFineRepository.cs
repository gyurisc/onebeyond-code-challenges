using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IFineRepository
    {
        public Guid CreateFine(Fine fine);
        public bool PayFine(Fine fine);
    }
}
