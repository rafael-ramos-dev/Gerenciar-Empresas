using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoTesteRafa.Models
{
    public class perfis_usuario
    {
        // Todas as configurações são referentes aos usuários gerente 

        [Key]
        public int pus_codigo { get; set; }

        //[Display(Name = "Perfil")]
        //public string pus_perfil { get; set; }

        [DisplayName("Criar usuário")]
        public bool pus_lerescrever_usuariogerente_create { get; set; }
        [DisplayName("Apagar usuário")]
        public bool pus_lerescrever_usuariogerente_delete { get; set; }
        [DisplayName("Acessar informações do usuário")]
        public bool pus_ler_usuariogerente_details { get; set; }
        [DisplayName("Editar Usuário")]
        public bool pus_lerescrever_usuariogerente_edit { get; set; }
        [DisplayName("Exibir configurações de usuário")]
        public bool pus_ler_usuariogerente_index { get; set; }
        [DisplayName("Mudar senha do usuário")]
        public bool pus_lerescrever_usuariogerente_mudarsenha { get; set; }

        [DisplayName("Criar cliente")]
        public bool pus_lerescrever_clienteempresa_create { get; set; }
        [DisplayName("Apagar cliente")]
        public bool pus_lerescrever_clienteempresa_delete { get; set; }
        [DisplayName("Exibir informações do cliente")]
        public bool pus_ler_clienteempresa_details { get; set; }
        [DisplayName("Editar cliente")]
        public bool pus_lerescrever_clienteempresa_edit { get; set; }
        [DisplayName("Acessar configurações do cliente")]
        public bool pus_ler_clienteempresa_index { get; set; }

        [DisplayName("Criar empresas ou filiais")]
        public bool pus_lerescrever_clienteempresaunidade_create { get; set; }
        [DisplayName("Apagar empresas ou filiais")]
        public bool pus_lerescrever_clienteempresaunidade_delete { get; set; }
        [DisplayName("Exibir informações das empresas ou filiais")]
        public bool pus_ler_clienteempresaunidade_details { get; set; }
        [DisplayName("Editar empresas ou filiais")]
        public bool pus_lerescrever_clienteempresaunidade_edit { get; set; }
        [DisplayName("Acessar configurações de empresas ou filiais")]
        public bool pus_ler_clienteempresaunidade_index { get; set; }

        [DisplayName("Criar usuário")]
        public bool pus_lerescrever_clienteempresausuario_create { get; set; }
        [DisplayName("Apagar usuário")]
        public bool pus_lerescrever_clienteempresausuario_delete { get; set; }
        [DisplayName("Exibir informações do usuário")]
        public bool pus_ler_clienteempresausuario_details { get; set; }
        [DisplayName("Editar usuário")]
        public bool pus_lerescrever_clienteempresausuario_edit { get; set; }
        [DisplayName("Acessar configurações de usuário")]
        public bool pus_ler_clienteempresausuario_index { get; set; }
        [DisplayName("Mudar senha do usuário")]
        public bool pus_lerescrever_clienteempresausuario_mudarsenha { get; set; }

        [DisplayName("Criar vínculo do usuário com uma empresa específica")]
        public bool pus_lerescrever_perfilusuarioempresa_create { get; set; }
        [DisplayName("Apagar vínculo do usuário com uma empresa específica")]
        public bool pus_lerescrever_perfilusuarioempresa_delete { get; set; }
        [DisplayName("Exibir informações do usuário em relação a uma empresa específica")]
        public bool pus_ler_perfilusuarioempresa_details { get; set; }
        [DisplayName("Editar as informações do usuário em relação a uma empresa específica")]
        public bool pus_lerescrever_perfilusuarioempresa_edit { get; set; }
        [DisplayName("Acessar as configurações de relacionamento entre usuário e empresa específica")]
        public bool pus_ler_perfilusuarioempresa_index { get; set; }


        [DisplayName("Criar nova política de acessos")]
        public bool pus_lerescrever_perfilusuarioacesso_create { get; set; }
        [DisplayName("Apagar política de acessos")]
        public bool pus_lerescrever_perfilusuarioacesso_delete { get; set; }
        [DisplayName("Exibir a política de acessos existente")]
        public bool pus_ler_perfilusuarioacesso_details { get; set; }
        [DisplayName("Editar política de acessos existente")]
        public bool pus_lerescrever_perfilusuarioacesso_edit { get; set; }
        [DisplayName("Acessar configurações de políticas de acesso")]
        public bool pus_ler_perfilusuarioacesso_index { get; set; }

        [DisplayName("Criar novos perfis Gerente")]
        public bool pus_lerescrever_perfilusuariogerente_create { get; set; }
        [DisplayName("Apagar perfis Gerente ")]
        public bool pus_lerescrever_perfilusuariogerente_delete { get; set; }
        [DisplayName("Exibir informações de perfis Gerente")]
        public bool pus_ler_perfilusuariogerente_details { get; set; }
        [DisplayName("Editar perfis Gerente")]
        public bool pus_lerescrever_perfilusuariogerente_edit { get; set; }
        [DisplayName("Configurações de perfis Gerente")]
        public bool pus_ler_perfilusuariogerente_index { get; set; }


        // Vínculo com a tabela de usuários de empresas
        [Display(Name = "Usuário")]
        [ForeignKey("usuario_gerente")]
        public int pus_usuariogerente { get; set; }

        public virtual usuario_gerente usuario_gerente { get; set; }
    }
}