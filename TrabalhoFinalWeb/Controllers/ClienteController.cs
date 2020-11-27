using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrabalhoFinalWeb.Models;

namespace TrabalhoFinalWeb.Controllers
{
    public class ClienteController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetClientes()
        {
            AppDbContext _context;
            _context = new AppDbContext();
            var data = _context.Clientes.ToList();
            IList<Cliente> clientes = data;

            return Ok(clientes);
        }

        AppDbContext contexto = new AppDbContext();

        public IHttpActionResult PostCliente(ClienteDTO cliente)
        {
            if (!ModelState.IsValid || cliente == null)
                return BadRequest("Dados do cliente inválidos.");

            using (var ctx = new AppDbContext())
            {

                if (cliente.Nome == null || cliente.Telefone == null || cliente.Email == null)
                {
                    return BadRequest("Preencha todos os campos!");
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
                    return BadRequest("Insira um ID de endereço válido!");
                }

                ctx.SaveChanges();
            }
            return Ok(cliente);
        }
    }
}
