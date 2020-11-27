using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoFinalWeb.Models;

namespace TrabalhoFinalWeb.Controllers
{
    public class ClienteViewController : Controller
    {
        // GET: ClienteView
        public ActionResult Index()
        {
            List<Cliente> Clientes = new List<Cliente>();

            AppDbContext contexto = new AppDbContext();

            foreach (Cliente c in contexto.Clientes.ToList<Cliente>())
            {
                Clientes.Add(c);
            }

            return View(Clientes);
        }

        public ActionResult AdicionaCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdicionaCliente(Cliente cliente)
        {

            using (var ctx = new AppDbContext())
            {

                if (cliente.Nome == null || cliente.Telefone == null || cliente.Email == null)
                {
                    ModelState.AddModelError("", "Preencha todos os campos!");
                    return View();
                }

                ctx.Clientes.Add(new Cliente()
                {
                    Nome = cliente.Nome,
                    Telefone = cliente.Telefone,
                    Email = cliente.Email,
                    IdEndereco = cliente.IdEndereco
                });

                bool checarEndereco = false;
                foreach (Endereco e in ctx.Enderecos)
                {
                    if (e.IdEndereco == cliente.IdEndereco)
                    {
                        checarEndereco = true;
                    }
                }
                if (checarEndereco == false)
                {
                    ModelState.AddModelError("", "Insira um ID de endereço válido!");
                    return View();
                }

                ctx.SaveChanges();
            }

            return View();
        }
    }
}