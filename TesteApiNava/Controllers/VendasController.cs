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
            return _context.venda.ToList();
        }
        [HttpGet("{id}", Name ="ObterVenda")]
        public ActionResult<Venda> GetResult(int id)
        {
            var venda = _context.venda.FirstOrDefault(v=> v.Id == id);
            if (venda == null)
                return NotFound();
            return venda;
        }
        [HttpPost]
        public ActionResult Post([FromBody] Venda venda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            venda.DataVenda = DateTime.Now;
            venda.Status = "Aguardando pagamento";
            _context.Add(venda);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterVenda",
                new { Identificador = venda.Id },venda);
        }
        [HttpPut("{id}")]
        public  ActionResult Put(int id, [FromBody] string status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var venda = _context.venda.FirstOrDefault(v=> v.Id == id);

            if (venda == null)
            {
                return NotFound();
            }

            if (venda.Status == "Aguardando pagamento")
            {
                if (status== "Pagamento Aprovado" || status == "Cancelada")
                {
                    venda.Status = status;
                    _context.Update(venda);
                }
            }
            if (venda.Status == "Pagamento Aprovado")
            {
                if (status == "Enviado para Transportador" || status == "Cancelada")
                {
                    venda.Status = status;
                    _context.Update(venda);
                }
            }
            if (venda.Status == "Enviado para Transportador")
            {
                if (status == "Entregue")
                {
                    venda.Status = status;
                    _context.Update(venda);
                }
            }
            _context.SaveChanges();
            return Ok(venda);

        }

    }
}
