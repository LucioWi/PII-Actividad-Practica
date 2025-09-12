using Microsoft.AspNetCore.Mvc;
using Practica01.Domain;


namespace Practica02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailInvoiceController : ControllerBase
    {
        // GET: api/<DetailInvoiceController>
        [HttpGet]
        public IActionResult Get()
        {
            List<DetailInvoice> lst = null;
            try
            {
                
                return Ok(lst);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        // GET api/<DetailInvoiceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DetailInvoiceController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DetailInvoiceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DetailInvoiceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
