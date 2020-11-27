using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrabalhoFinalWeb.Models;

namespace TrabalhoFinalWeb.Controllers
{
    public class DetalheOrcamentoViewController : Controller
    {
        // GET: DetalheOrcamentoView
        public ActionResult Index()
        {
            List<DetalheOrcamento> Detalhes = new List<DetalheOrcamento>();

            AppDbContext contexto = new AppDbContext();

            foreach (DetalheOrcamento d in contexto.Detalhes.ToList<DetalheOrcamento>())
            {
                Detalhes.Add(d);
            }

            return View(Detalhes);
        }

        public ActionResult AdicionaDetalhe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdicionaDetalhe(DetalheOrcamento detalhe)
        {

            using (var ctx = new AppDbContext())
            {

                if (detalhe.Qtd <= 0)
                {
                    ModelState.AddModelError("", "Insira um valor válido para quantidade (maior que 0).");
                    return View();
                }

                bool checarProduto = false;
                foreach (ProdutoModel p in ctx.Produtos)
                {
                    if (p.IdProduto == detalhe.IdProduto)
                    {
                        detalhe.CustoTotal = detalhe.Qtd * p.Preco;
                        checarProduto = true;
                    }
                }
                if (checarProduto == false)
                {
                    ModelState.AddModelError("", "Insira um ID de produto válido!");
                    return View();
                }

                ctx.Detalhes.Add(new DetalheOrcamento()
                {
                    Qtd = detalhe.Qtd,
                    CustoTotal = detalhe.CustoTotal,
                    IdProduto = detalhe.IdProduto,
                    IdOrcamento = detalhe.IdOrcamento
                });

                bool checarOrcamento = false;
                foreach(Orcamento o in ctx.Orcamentos)
                {
                    if(o.IdOrcamento == detalhe.IdOrcamento)
                    {
                        o.Total = o.Total + detalhe.CustoTotal;
                        checarOrcamento = true;
                    }
                }
                if (checarOrcamento == false)
                {
                    ModelState.AddModelError("", "Insira um ID de orçamento válido!");
                    return View();
                }

                ctx.SaveChanges();
                

            }

            return View();
        }

        [HttpGet]
        public ActionResult DeletaDetalhe(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using(var ctx = new AppDbContext())
            {
                DetalheOrcamento detalhe = ctx.Detalhes.Find(id);
                if(detalhe == null )
                {
                    return View();
                }

                foreach(Orcamento o in ctx.Orcamentos)
                {
                    if(o.IdOrcamento == detalhe.IdOrcamento)
                    {
                        o.Total = o.Total - detalhe.CustoTotal;
                    }
                }

                ctx.Detalhes.Remove(detalhe);
                ctx.SaveChanges();
                return View();
            }

        }

    }
}