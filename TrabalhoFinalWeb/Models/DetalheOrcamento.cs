using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabalhoFinalWeb.Models
{
    [Table("DetalhesOrcamento")]
    public class DetalheOrcamento
    {
        [Key]
        public int IdDetalhe { get; set; }
        [Required]
        public int IdProduto { get; set; }
        public virtual ProdutoModel Produto { get; set; }
        [Required]
        public int Qtd { get; set; }
        public decimal CustoTotal { get; set; }
        [Required]
        public int IdOrcamento { get; set; }

        public virtual Orcamento Orcamento { get; set; }

    }
}