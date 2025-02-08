using Microsoft.AspNetCore.Mvc;
using ProjectTester.Domain.Models.Operations;
using ProjectTester.Services.Provider;

namespace ProjectTester.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ILogger<TaxController> _logger;
        private readonly ITransactionProvider _transactionProvider;

        public TaxController(ILogger<TaxController> logger, ITransactionProvider transactionProvider)
        {
            _logger = logger;
            _transactionProvider = transactionProvider;
        }

        [HttpGet("~/GetOperation")]
        public Operations GetOperation()
        {
            _logger.Log(LogLevel.Information, "Get operation.");
            return _transactionProvider.GetOperation();
        }

        [HttpPost("~/PostOperation")]
        public Operations PostOperation(Operations operations)
        {
            _logger.Log(LogLevel.Information, "Post operation.");
            return _transactionProvider.PostOperation(operations);
        }
    }
}