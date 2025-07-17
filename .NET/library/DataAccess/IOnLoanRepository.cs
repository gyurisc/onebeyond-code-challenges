using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IOnLoanRepository
    {
        public IList<ActiveLoanDto> GetActiveLoans();
        public bool ReturnBook(Guid BookStockId);

    }
}
