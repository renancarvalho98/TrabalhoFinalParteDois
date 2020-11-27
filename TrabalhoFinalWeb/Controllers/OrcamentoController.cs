using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrabalhoFinalWeb.Models;

namespace TrabalhoFinalWeb.Controllers
{
    public class OrcamentoController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetOrcamentos()
        {
            AppDbContext _context;
            _context = new AppDbContext();
            var data = _context.Orcamentos.ToList();
            IList<Orcamento> orcamentos = data;

            return Ok(orcamentos);
        }

        AppDbContext contexto = new AppDbContext();
        public IHttpActionResult PostOrcamento(OrcamentoDTO orcamento)
        {
            if (!ModelState.IsValid || orcamento == null)
                return BadRequest("Dados do orçamento inválidos.");

            using (var ctx = new AppDbContext())
            {
                ctx.Orcamentos.Add(new Orcamento()
                {
                    Total = 0,
                    Data = DateTime.Now,
                    Status = orcamento.Status,
                    IdCliente = orcamento.IdCliente
                });

                bool checarCliente = false;
                foreach (Cliente c in ctx.Clientes)
                {
                    if (c.IdCliente == orcamento.IdCliente)
                    {
                        checarCliente = true;
                    }
                }
                if (checarCliente == false)
                {
                    return BadRequest("Insira um ID de cliente válido!");
                }

                ctx.SaveChanges();
            }
            return Ok(orcamento);
        }
    }
}
