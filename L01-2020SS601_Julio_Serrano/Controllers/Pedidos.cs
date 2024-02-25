using Microsoft.AspNetCore.Mvc;
using L01_2020SS601_Julio_Serrano.Models;
using static L01_2020SS601_Julio_Serrano.Models.Restaurante;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace L01_2020SS601_Julio_Serrano.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly RestauranteDBContext _RestauranteDBContext;

        public PedidosController(RestauranteDBContext context)
        {
            _RestauranteDBContext = context;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _RestauranteDBContext.Pedidos.ToListAsync();
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _RestauranteDBContext.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // POST: api/Pedidos
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            _RestauranteDBContext.Pedidos.Add(pedido);
            await _RestauranteDBContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.PedidoId }, pedido);
        }

        // PUT: api/Pedidos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.PedidoId)
            {
                return BadRequest();
            }

            _RestauranteDBContext.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _RestauranteDBContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
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

        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _RestauranteDBContext.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _RestauranteDBContext.Pedidos.Remove(pedido);
            await _RestauranteDBContext.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return _RestauranteDBContext.Pedidos.Any(e => e.PedidoId == id);
        }

        // GET: api/Pedidos/Cliente/{id}
        [HttpGet("Cliente/{id}")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidosByCliente(int id)
        {
            var pedidos = await _RestauranteDBContext.Pedidos.Where(p => p.ClienteId == id).ToListAsync();
            if (pedidos == null)
            {
                return NotFound();
            }
            return pedidos;
        }

        // GET: api/Pedidos/Motorista/{id}
        [HttpGet("Motorista/{id}")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidosByMotorista(int id)
        {
            var pedidos = await _RestauranteDBContext.Pedidos.Where(p => p.MotoristaId == id).ToListAsync();
            if (pedidos == null)
            {
                return NotFound();
            }
            return pedidos;
        }
    }
}
