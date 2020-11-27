using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrabalhoFinalWeb.Models
{
    public class OrcamentoDTO
    {
        public int IdOrcamento { get; set; }
        public DateTime Data { get; set; }
        public decimal Total { get; set; }
        public int Status { get; set; }
        public int IdCliente { get; set; }

    }
}