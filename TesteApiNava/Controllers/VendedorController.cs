using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TesteApiNava.Models;

namespace TesteApiNava.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedorController : ControllerBase
    {
        private readonly ApiContext _context;
        public VendedorController(ApiContext contexto)
        {
            _context = contexto;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Vendedor>> GetActionResult()
        {
            return _context.vendedor.ToList();
        }
        [HttpGet("{id}", Name = "ObterVendedor")]
        public ActionResult<Vendedor> GetResult(int id)
        {
            var vendedor = _context.vendedor.FirstOrDefault(v => v.vendedorId == id);
            if (vendedor == null)
                return NotFound();
            return vendedor;
        }
        [HttpPost]
        public ActionResult Post([FromBody] Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
           _context.Add(vendedor);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterVenda",
                new { id = vendedor.vendedorId }, vendedor);
        }
      
      
    }
}
