using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TesteApiNava.Models
{
    public class Venda
    {
        [Key]
        public int IdentificadorVenda { get; set; }
        public DateTime DataVenda { get; set; }
        [Required]
         public string Itens { get; set; }
        public string Status { get; set; }
        public Vendedor Vendedor { get; set; }
        public int VendedorId { get; set; }
    }
}
