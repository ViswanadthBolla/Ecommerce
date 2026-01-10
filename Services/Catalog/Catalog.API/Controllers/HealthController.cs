using System.Threading.Tasks;
using Asp.Versioning;
using Catalog.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly ICatalogContext _context;
        public HealthController(ICatalogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                // Check MongoDB connectivity by querying a lightweight count
                await _context.Products.EstimatedDocumentCountAsync();
                return Ok(new { status = "Healthy" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(503, new { status = "Unhealthy", error = ex.Message });
            }
        }
    }
}