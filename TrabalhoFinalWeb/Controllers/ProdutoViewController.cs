using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoFinalWeb.Models;

namespace TrabalhoFinalWeb.Controllers
{
    public class ProdutoViewController : Controller
    {
        // GET: ProdutoView
        public ActionResult Index()
        {
            List<ProdutoModel> Produtos = new List<ProdutoModel>();

            AppDbContext contexto = new AppDbContext();

            foreach (ProdutoModel p in contexto.Produtos.ToList<ProdutoModel>())
            {
                Produtos.Add(p);
            }

            return View(Produtos);
        }
    }
}