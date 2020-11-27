using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrabalhoFinalWeb.Models
{
    public class DetalheOrcamentoDTO
    {
        public int IdDetalhe { get; set; }
        public int Qtd { get; set; }
        public decimal CustoTotal { get; set; }

        public int IdProduto { get; set; }

        public int IdOrcamento { get; set; }
    }
}