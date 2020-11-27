using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrabalhoFinalWeb.Models;

namespace TrabalhoFinalWeb.Controllers
{
    public class EnderecoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetEnderecos()
        {
            AppDbContext _context;
            _context = new AppDbContext();
            var data = _context.Enderecos.ToList();
            IList<Endereco> enderecos = data;

            return Ok(enderecos);
        }

        public IHttpActionResult PostEndereco(EnderecoDTO endereco)
        {
            if (!ModelState.IsValid || endereco == null)
                return BadRequest("Dados do endereço inválidos.");

            using (var ctx = new AppDbContext())
            {

                if (endereco.Logradouro == null || endereco.Numero <= 0 || endereco.Cep == null ||
                    endereco.Cidade == null || endereco.Estado == null)
                {
                    return BadRequest("Preencha todos os campos!");
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
            return Ok(endereco);
        }
    }
}
