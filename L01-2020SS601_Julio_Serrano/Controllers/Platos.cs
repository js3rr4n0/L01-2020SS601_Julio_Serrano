using L01_2020SS601_Julio_Serrano.Models;
using Microsoft.AspNetCore.Mvc;
using static L01_2020SS601_Julio_Serrano.Models.Restaurante;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace L01_2020SS601_Julio_Serrano.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosController : ControllerBase
    {
        private readonly RestauranteDBContext _RestauranteDBContext;

        public PlatosController(RestauranteDBContext context)
        {
            _RestauranteDBContext = context;
        }

        // GET: api/Platos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plato>>> GetPlatos()
        {
            return await _RestauranteDBContext.Platos.ToListAsync();
        }

        // GET: api/Platos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plato>> GetPlato(int id)
        {
            var plato = await _RestauranteDBContext.Platos.FindAsync(id);

            if (plato == null)
            {
                return NotFound();
            }

            return plato;
        }

        // POST: api/Platos
        [HttpPost]
        public async Task<ActionResult<Plato>> PostPlato(Plato plato)
        {
            _RestauranteDBContext.Platos.Add(plato);
            await _RestauranteDBContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlato), new { id = plato.PlatoId }, plato);
        }

        // PUT: api/Platos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlato(int id, Plato plato)
        {
            if (id != plato.PlatoId)
            {
                return BadRequest();
            }

            _RestauranteDBContext.Entry(plato).State = EntityState.Modified;

            try
            {
                await _RestauranteDBContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatoExists(id))
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

        // DELETE: api/Platos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlato(int id)
        {
            var plato = await _RestauranteDBContext.Platos.FindAsync(id);
            if (plato == null)
            {
                return NotFound();
            }

            _RestauranteDBContext.Platos.Remove(plato);
            await _RestauranteDBContext.SaveChangesAsync();

            return NoContent();
        }

        private bool PlatoExists(int id)
        {
            return _RestauranteDBContext.Platos.Any(e => e.PlatoId == id);
        }

        // GET: api/Platos/PrecioMenorA/{precio}
        [HttpGet("PrecioMenorA/{precio}")]
        public async Task<ActionResult<IEnumerable<Plato>>> GetPlatosByPrecioMenorA(decimal precio)
        {
            var platos = await _RestauranteDBContext.Platos.Where(p => p.Precio < precio).ToListAsync();
            if (platos == null || platos.Count == 0)
            {
                return NotFound();
            }
            return platos;
        }
    }
}
