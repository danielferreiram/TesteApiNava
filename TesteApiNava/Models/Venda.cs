using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteApiNava.Models
{
    public class Venda
    {
        public DateTime DataVenda { get; set; }
        public int Id { get; set; }
        public string Itens { get; set; }
        public string Status { get; set; }
        public Vendedor Vendedor { get; set; }
        public int VendedorId { get; set; }
    }
}
