using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TrabalhoFinalWeb.Models;

namespace TrabalhoFinalWeb.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            IEnumerable<ProdutoModel> produtos = null;
            using (var client = new HttpClient())
            {

                // sincronizar com api
                client.BaseAddress = new Uri("https://trabalho-springboot.herokuapp.com/produtos");

                //HTTP GET
                var responseTask = client.GetAsync("produtos");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProdutoModel>>();
                    readTask.Wait();
                    produtos = readTask.Result;

                    using (var ctx = new AppDbContext())
                    {
                        if (ctx.Produtos.Count() < produtos.Count())
                        {
                            foreach (ProdutoModel p in produtos)
                            {

                                ctx.Produtos.Add(new ProdutoModel()
                                {
                                    IdProduto = p.IdProduto,
                                    Nome = p.Nome,
                                    Preco = p.Preco
                                });

                                ctx.SaveChanges();


                            }
                        }
                    }

                }
                else
                {
                    produtos = Enumerable.Empty<ProdutoModel>();
                    ModelState.AddModelError(string.Empty, "Tente novamente mais tarde.");
                }

                return View(produtos);
            }
        }

    }
}