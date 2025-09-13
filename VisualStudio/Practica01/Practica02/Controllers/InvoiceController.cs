using Microsoft.AspNetCore.Mvc;
using Practica01.Domain;
using Practica01.Services;


namespace Practica02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private InvoiceService _service;
        public InvoiceController() 
        { 
            _service = new InvoiceService();
        }
        // GET: api/<InvoiceController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var invoices = _service.GetInvoice();
                return Ok(invoices);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        // GET api/<InvoiceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InvoiceController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<InvoiceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<InvoiceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
