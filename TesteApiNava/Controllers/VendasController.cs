using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TesteApiNava.Models;

namespace TesteApiNava.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly ApiContext _context;
        public VendasController(ApiContext contexto)
        {
            _context = contexto;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Venda>> GetActionResult()
        {
            var venda = _context.venda.ToList();
            if (venda != null)
            {
                foreach (var item in venda)
                {
                    item.Vendedor = _context.vendedor.FirstOrDefault(v => v.Id == item.VendedorId);
                }
            }
            return venda;
        }
        [HttpGet("{id}", Name = "ObterVenda")]
        public ActionResult<Venda> GetResult(int id)
        {
            var venda = _context.venda.FirstOrDefault(v => v.IdentificadorVenda == id);
            if (venda == null)
                return NotFound();
            venda.Vendedor = _context.vendedor.FirstOrDefault(v => v.Id == venda.VendedorId);

            return venda;
        }
        [HttpPost]
        public ActionResult Post([FromBody] Venda venda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            venda.Vendedor = _context.vendedor.FirstOrDefault(v => v.Id == venda.VendedorId);
            if (venda.Vendedor == null)
            {
                return NotFound("Vendedor não encontrado.");
            }
            venda.DataVenda = DateTime.Now;
            venda.Status = "Aguardando pagamento";
            _context.Add(venda);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterVenda",
                new { Identificador = venda.IdentificadorVenda }, venda);
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var venda = _context.venda.FirstOrDefault(v => v.IdentificadorVenda == id);

            if (venda == null)
            {
                return NotFound();
            }

            if (venda.Status.ToUpper() == "AGUARDANDO PAGAMENTO")
            {
                if (status.ToUpper() == "PAGAMENTO APROVADO" || status.ToUpper() == "CANCELADA")
                {
                    venda.Status = status;
                    _context.Update(venda);
                }
            }
            else
            if (venda.Status.ToUpper() == "PAGAMENTO APROVADO")
            {
                if (status.ToUpper() == "ENVIADO PARA TRANSPORTADOR" || status.ToUpper() == "CANCELADA")
                {
                    venda.Status = status;
                    _context.Update(venda);
                }
            }
            else
            if (venda.Status.ToUpper() == "ENVIADO PARA TRANSPORTADOR")
            {
                if (status.ToUpper() == "ENTREGUE")
                {
                    venda.Status = status;
                    _context.Update(venda);
                }
            }

            else
                return NotFound();
            _context.SaveChanges();
            return Ok(venda);

        }

    }
}
