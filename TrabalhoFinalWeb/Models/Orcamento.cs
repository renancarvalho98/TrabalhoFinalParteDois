using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrabalhoFinalWeb.Models
{
    [Table("Orcamentos")]
    public class Orcamento
    {
        [Key]
        public int IdOrcamento { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public int IdCliente { get; set; }
        
        public virtual Cliente Cliente { get; set; }


    }
}