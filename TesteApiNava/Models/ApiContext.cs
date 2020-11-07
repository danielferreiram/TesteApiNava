using Microsoft.EntityFrameworkCore;
using System;

namespace TesteApiNava.Models
{
    public class ApiContext : DbContext
    {
        public DbSet<Venda> venda { get; set; }
        public DbSet<Vendedor> vendedor { get; set; }
        public ApiContext(DbContextOptions options) : base(options)
        {}
       
    }
}
