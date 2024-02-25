using System.ComponentModel.DataAnnotations;

namespace L01_2020SS601_Julio_Serrano.Models
{
    public class Restaurante
    {
        public class Cliente
        {
            [Key]
            public int ClienteId { get; set; }
            public string NombreCliente { get; set; }
            public string Direccion { get; set; }
        }

        public class Pedido
        {
            [Key]
            public int PedidoId { get; set; }
            public int MotoristaId { get; set; }
            public int ClienteId { get; set; }
            public int PlatoId { get; set; }
            public int Cantidad { get; set; }
            public decimal Precio { get; set; }
        }

        public class Plato
        {
            [Key]
            public int PlatoId { get; set; }
            public string NombrePlato { get; set; }
            public decimal Precio { get; set; }
        }

        public class Motorista
        {
            [Key]
            public int MotoristaId { get; set; }
            public string NombreMotorista { get; set; }
        }
    }
}
