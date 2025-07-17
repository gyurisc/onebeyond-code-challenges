using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FineController : ControllerBase
    {
        private readonly ILogger<FineController> _logger;
        private readonly IFineRepository _fineRepository;

        public FineController(ILogger<FineController> logger, IFineRepository fineRepository)
        {
            _logger = logger;
            _fineRepository = fineRepository;
        }

        [HttpGet]
        [Route("GetFines/{borrowerId}")]
        public IList<Fine> GetFines(Guid borrowerId)
        {
            return _fineRepository.GetFinesByBorrower(borrowerId);
        }

        [HttpPost]
        [Route("PayFine/{fineId}")]
        public bool PayFine(Guid fineId)
        { 
            return _fineRepository.PayFine(fineId);
        }
    }
}
