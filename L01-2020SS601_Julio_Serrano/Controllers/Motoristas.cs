using L01_2020SS601_Julio_Serrano.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static L01_2020SS601_Julio_Serrano.Models.Restaurante;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace L01_2020SS601_Julio_Serrano.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoristasController : ControllerBase
    {
        private readonly RestauranteDBContext _RestauranteDBContext;

        public MotoristasController(RestauranteDBContext context)
        {
            _RestauranteDBContext = context;
        }

        // GET: api/Motoristas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Motorista>>> GetMotoristas()
        {
            return await _RestauranteDBContext.Motoristas.ToListAsync();
        }

        // GET: api/Motoristas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Motorista>> GetMotorista(int id)
        {
            var motorista = await _RestauranteDBContext.Motoristas.FindAsync(id);

            if (motorista == null)
            {
                return NotFound();
            }

            return motorista;
        }

        // POST: api/Motoristas
        [HttpPost]
        public async Task<ActionResult<Motorista>> PostMotorista(Motorista motorista)
        {
            _RestauranteDBContext.Motoristas.Add(motorista);
            await _RestauranteDBContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMotorista), new { id = motorista.MotoristaId }, motorista);
        }

        // PUT: api/Motoristas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMotorista(int id, Motorista motorista)
        {
            if (id != motorista.MotoristaId)
            {
                return BadRequest();
            }

            _RestauranteDBContext.Entry(motorista).State = EntityState.Modified;

            try
            {
                await _RestauranteDBContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotoristaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Motoristas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotorista(int id)
        {
            var motorista = await _RestauranteDBContext.Motoristas.FindAsync(id);
            if (motorista == null)
            {
                return NotFound();
            }

            _RestauranteDBContext.Motoristas.Remove(motorista);
            await _RestauranteDBContext.SaveChangesAsync();

            return NoContent();
        }

        private bool MotoristaExists(int id)
        {
            return _RestauranteDBContext.Motoristas.Any(e => e.MotoristaId == id);
        }

        // GET: api/Motoristas/Nombre/{nombre}
        [HttpGet("Nombre/{nombre}")]
        public async Task<ActionResult<IEnumerable<Motorista>>> GetMotoristasByNombre(string nombre)
        {
            var motoristas = await _RestauranteDBContext.Motoristas.Where(m => m.NombreMotorista.Contains(nombre)).ToListAsync();
            if (motoristas == null || motoristas.Count == 0)
            {
                return NotFound();
            }
            return motoristas;
        }
    }
}
