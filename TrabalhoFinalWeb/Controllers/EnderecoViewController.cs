using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoFinalWeb.Models;

namespace TrabalhoFinalWeb.Controllers
{
    public class EnderecoViewController : Controller
    {
        // GET: EnderecoView
        public ActionResult Index()
        {
            List<Endereco> Enderecos = new List<Endereco>();

            AppDbContext contexto = new AppDbContext();

            foreach (Endereco e in contexto.Enderecos.ToList<Endereco>())
            {
                Enderecos.Add(e);
            }

            return View(Enderecos);
        }

        public ActionResult AdicionaEndereco()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdicionaEndereco(Endereco endereco)
        {

            using (var ctx = new AppDbContext())
            {

                if(endereco.Logradouro == null || endereco.Numero <= 0 || endereco.Cep == null ||
                    endereco.Cidade == null || endereco.Estado == null)
                {
                    ModelState.AddModelError("", "Preencha todos os campos!");
                    return View();
                }

                ctx.Enderecos.Add(new Endereco()
                {
                    Logradouro = endereco.Logradouro,
                    Numero = endereco.Numero,
                    Cep = endereco.Cep,
                    Cidade = endereco.Cidade,
                    Estado = endereco.Estado
                });

                ctx.SaveChanges();
            }

            return View();
        }

    }
}