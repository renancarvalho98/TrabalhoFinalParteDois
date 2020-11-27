using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrabalhoFinalWeb.Models;
using TrabalhoFinalWeb.Controllers;

namespace TrabalhoFinalWeb.Controllers
{
    public class DetalheOrcamentoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetDetalhes()
        {
            AppDbContext _context;
            _context = new AppDbContext();
            var data = _context.Detalhes.ToList();
            IList<DetalheOrcamento> detalhes = data;

            return Ok(detalhes);
        }

        AppDbContext contexto = new AppDbContext();

        public IHttpActionResult PostDetalhe(DetalheOrcamentoDTO detalhe)
        {
            if (!ModelState.IsValid || detalhe == null)
                return BadRequest("Dados do detalhe inválidos.");

            using (var ctx = new AppDbContext())
            {
                if (detalhe.Qtd <= 0 )
                {
                    return BadRequest("Insira um valor válido para quantidade (maior que 0).");
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
                    return BadRequest("Insira um ID de produto válido!");
                }

                ctx.Detalhes.Add(new DetalheOrcamento()
                {
                    Qtd = detalhe.Qtd,
                    CustoTotal = detalhe.CustoTotal,
                    IdProduto = detalhe.IdProduto,
                    IdOrcamento = detalhe.IdOrcamento
                }); ;

                bool checarOrcamento = false;
                foreach (Orcamento o in ctx.Orcamentos)
                {
                    if (o.IdOrcamento == detalhe.IdOrcamento)
                    {
                        o.Total = o.Total + detalhe.CustoTotal;
                        checarOrcamento = true;
                    }
                }
                if(checarOrcamento == false)
                {
                    return BadRequest("Insira um ID de orçamento válido!");
                }

                ctx.SaveChanges();
            }
            return Ok(detalhe);
        }

        public IHttpActionResult DeleteDetalhe(int? id)
        {
            if (id == null)
            {
                return BadRequest("Detalhe inexiste.");
            }

            using (var ctx = new AppDbContext())
            {
                DetalheOrcamento detalhe = ctx.Detalhes.Find(id);
                foreach (Orcamento o in ctx.Orcamentos)
                {
                    if (o.IdOrcamento == detalhe.IdOrcamento)
                    {
                        o.Total = o.Total - detalhe.CustoTotal;
                    }
                }
                ctx.Detalhes.Remove(detalhe);
                ctx.SaveChanges();
                return Ok(detalhe);
            }
            
        }

    }
}
