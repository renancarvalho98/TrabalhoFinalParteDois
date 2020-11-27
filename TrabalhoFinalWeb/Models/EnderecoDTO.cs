using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrabalhoFinalWeb.Models
{
    public class EnderecoDTO
    {
        public int IdEndereco { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

    }

}