using static L01_2020SS601_Julio_Serrano.Models.Restaurante;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace L01_2020SS601_Julio_Serrano.Models
{
    public class RestauranteDBContext : DbContext
    {
        public RestauranteDBContext(DbContextOptions<RestauranteDBContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Plato> Platos { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
    }
}
