using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Get(int id)
        {
            try
            {
                var invoice = _service.GetInvoiceById(id);
                if (invoice == null)
                {
                    return NotFound($"Producto con el ID {id} no encontrado");
                }

                return Ok(invoice);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno al obtener la factura.");
            }
        }

        // POST api/<InvoiceController>
        [HttpPost]
        public IActionResult Post(Invoice oInvoice)
        {
            try
            {
                if (oInvoice == null)
                {
                    return BadRequest("Datos de la factura inválidos");
                }

                bool result = _service.SaveInvoice(oInvoice);

                if (result)
                {
                    return Ok("Factura guardada exitosamente");
                }
                else
                {
                    return StatusCode(500, "Error interno al guardar la factura");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno al procesar solicitud");
            }
        }

        // PUT api/<InvoiceController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Invoice oInvoice)
        {
            try
            {
                if (oInvoice == null)
                {
                    return BadRequest("Datos de producto inválidos");
                }

                oInvoice.NroFactura = id;
                //llamamos al servicio
                bool result = _service.SaveInvoice(oInvoice);


                if (result)
                {
                    return Ok("Factura actualizada con exito");
                }
                else
                {
                    return NotFound("No se pudo actualizar la factura con el Nro solicitado");
                }

            }
            catch (Exception)
            {

                return StatusCode(500, "Error al actualizar la factura");
            }
        }

        // DELETE api/<InvoiceController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = _service.DeleteInvoice(id);
                if (result)
                {
                    return Ok("Factura eliminada con exito");
                }
                else
                {
                    return NotFound("No se pudo eliminar la factura o no existe");
                }
            }
            catch (Exception)
            {

                return StatusCode(500, "Error interno al procesar solicitud");
            }
        }
    }
}
