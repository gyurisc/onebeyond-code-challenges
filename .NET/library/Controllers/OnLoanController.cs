using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;
using System.Collections;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OnLoanController : ControllerBase
    {
        private readonly ILogger<OnLoanController> _logger;
        private readonly IOnLoanRepository _onLoanRepository;

        public OnLoanController(ILogger<OnLoanController> logger, IOnLoanRepository onLoanRepository)
        {
            _logger = logger;
            _onLoanRepository = onLoanRepository;
        }

        [HttpGet]
        [Route("GetActiveLoans")]
        public IList<ActiveLoanDto> GetActiveLoans() 
        {
            return _onLoanRepository.GetActiveLoans();
        }

        [HttpPost]
        [Route("ReturnBook")]
        public bool ReturnBook(ReturnBookDto returnBook)
        {
            return _onLoanRepository.ReturnBook(returnBook.BookStockId);
        }
    }
}
