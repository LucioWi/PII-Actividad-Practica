using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_EF2.Repositories;

namespace WebApi_EF2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IRepository _repository;

        public OrderController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public IActionResult GetByFilters([FromQuery]string client)
        {
            try
            {
                return Ok(_repository.GetByFilters(client));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
