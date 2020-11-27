using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TrabalhoFinalWeb.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("OrcamentoContext")
        {

        }

        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<DetalheOrcamento> Detalhes { get; set; }

    }
}