using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ProjetoTesteRafa.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {}


        // Pega do Model passado ao "DbSet" e cria a tabela com o nome especificado a frente

        public DbSet<usuario_gerente> usuario_gerente { get; set; }

        public DbSet<perfis_usuario> perfis_usuario { get; set; }

        public DbSet<cliente_empresa> cliente_empresa { get; set; }

        public DbSet<cliente_empresa_usuario> cliente_empresa_usuario { get; set; }

        public DbSet<perfil_usuario_acesso> perfil_usuario_acesso { get; set; }





        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            // retira a convenção que pluraliza as tabelas no DB
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }

}