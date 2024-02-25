using Microsoft.AspNetCore.Mvc;
using L01_2020SS601_Julio_Serrano.Models;
using static L01_2020SS601_Julio_Serrano.Models.Restaurante;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L01_2020SS601_Julio_Serrano.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : Controller
    {
        
        private readonly RestauranteDBContext _RestauranteDBContext;

        public ClientesController(RestauranteDBContext context)
        {
            _RestauranteDBContext = context;
        }
      
        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _RestauranteDBContext.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _RestauranteDBContext.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _RestauranteDBContext.Clientes.Add(cliente);
            await _RestauranteDBContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.ClienteId }, cliente);
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return BadRequest();
            }

            _RestauranteDBContext.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _RestauranteDBContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _RestauranteDBContext.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _RestauranteDBContext.Clientes.Remove(cliente);
            await _RestauranteDBContext.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _RestauranteDBContext.Clientes.Any(e => e.ClienteId == id);
        }
    }
}
