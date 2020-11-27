using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrabalhoFinalWeb.Models
{
    public class ClienteDTO
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public int IdEndereco { get; set; }
    }
}