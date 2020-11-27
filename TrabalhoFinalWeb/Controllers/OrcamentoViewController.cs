using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoFinalWeb.Models;

namespace TrabalhoFinalWeb.Controllers
{
    public class OrcamentoViewController : Controller
    {
        // GET: OrcamentoView
        public ActionResult Index()
        {
            List<Orcamento> Orcamentos = new List<Orcamento>();

            AppDbContext contexto = new AppDbContext();

            foreach (Orcamento o in contexto.Orcamentos.ToList<Orcamento>())
            {
                Orcamentos.Add(o);
            }

            return View(Orcamentos);
        }

        public ActionResult AdicionaOrcamento()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdicionaOrcamento(Orcamento orcamento)
        {

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
                    ModelState.AddModelError("", "Insira um ID de cliente válido!");
                    return View();
                }

                ctx.SaveChanges();
            }

            return View();
        }
    }
}