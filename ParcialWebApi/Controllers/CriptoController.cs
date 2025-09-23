using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParcialWebApi.Models;
using ParcialWebApi.Repositories;

namespace ParcialWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriptoController : ControllerBase
    {
        private readonly ICryptoRepository _cryptoRepository;

        public CriptoController(ICryptoRepository cryptoRepository)
            {
                _cryptoRepository = cryptoRepository;
            }
        public class ValorUpdateDto
        {
            public double NuevoValor { get; set; }
        }

        // GET: api/crypto
        [HttpGet]
        public ActionResult<List<Criptomoneda>> GetAll()
        {
                var criptos = _cryptoRepository.GetAll();
                return Ok(criptos);
        }

            // GET: api/crypto/{id}
        [HttpGet("{id}")]
        public ActionResult<Criptomoneda> GetById(int id)
        {
            var cripto = _cryptoRepository.GetById(id);
            if (cripto == null)
                return NotFound($"No se encontró la criptomoneda con ID {id}");

            return Ok(cripto);
        }

        // GET: api/crypto/category/{nombre}
        [HttpGet("category/{nombre}")]
        public IActionResult GetByCategory(string nombre)
        {
            try
            {
                var criptos = _cryptoRepository.GetByCategory(nombre);

                if (criptos == null || criptos.Count == 0)
                    return NotFound($"No se encontraron criptomonedas en la categoría '{nombre}'");

                return Ok(criptos);
            }
            catch (Exception ex)
            {
                // Aquí podrías hacer logging del error si tienes un logger
                return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
            }
        }

        // PUT: api/crypto/{id}
        [HttpPut("{id}")]
            public IActionResult Update(int id, [FromBody] Criptomoneda cripto)
            {
                if (id != cripto.Id)
                    return BadRequest("El ID en la URL no coincide con el cuerpo de la solicitud.");

                var existente = _cryptoRepository.GetById(id);
                if (existente == null)
                    return NotFound($"No se encontró la criptomoneda con ID {id}");

                _cryptoRepository.Update(cripto);
                return NoContent();
            }

            // DELETE: api/crypto/{id}
            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                var existente = _cryptoRepository.GetById(id);
                if (existente == null)
                    return NotFound($"No se encontró la criptomoneda con ID {id}");

                _cryptoRepository.Delete(id);
                return NoContent();
            }

            [HttpPut("updatevalor/{simbolo}")]
            public IActionResult UpdateValorActual(string simbolo, [FromBody] ValorUpdateDto dto)
            {
                if (string.IsNullOrWhiteSpace(simbolo))
                    return BadRequest("El símbolo es requerido.");

                if (dto == null)
                    return BadRequest("El nuevo valor es requerido.");

                var updated = _cryptoRepository.UpdateValorBySimb(simbolo, dto.NuevoValor);

                if (!updated)
                    return NotFound($"No se encontró la criptomoneda con símbolo '{simbolo}'.");

                return NoContent();
            }
    }
    }
