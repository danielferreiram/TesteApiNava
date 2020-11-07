using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace TesteApiNava.Models
{
    public class Vendedor
    {
        public Vendedor()
        {
            Vendas = new Collection<Venda>();
        }
        public int vendedorId { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public ICollection<Venda> Vendas { get; set; }
    }
}
