using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoTesteRafa.Models
{
    public class perfil_usuario_acesso
    {
        [Key]
        public int pua_codigo { get; set; }
        //public bool pua_ler_clienteempresa_create { get; set; }
        [DisplayName("Criar novo grupo empresarial")]
        public bool pua_lerescrever_clienteempresa_create { get; set; }
        [DisplayName("Apagar grupo empresarial")]
        public bool pua_lerescrever_clienteempresa_delete { get; set; }
        //public bool pua_escrever_clienteempresa_delete { get; set; }
        [DisplayName("Accessar informações do grupo empresarial")]
        public bool pua_ler_clienteempresa_details { get; set; }
        //public bool pua_ler_clienteempresa_edit { get; set; }
        [DisplayName("Editar grupo empresarial")]
        public bool pua_lerescrever_clienteempresa_edit { get; set; }
        [DisplayName("Acessar configurações do grupo empresarial")]
        public bool pua_ler_clienteempresa_index { get; set; }


        [DisplayName("Criar novas empresas ou filiais")]
        public bool pua_lerescrever_clienteempresaunidade_create { get; set; }
        //public bool pua_escrever_clienteempresaunidade_create { get; set; }
        [DisplayName("Apagar empresas ou filiais")]
        public bool pua_lerescrever_clienteempresaunidade_delete { get; set; }
        //public bool pua_escrever_clienteempresaunidade_delete { get; set; }
        [DisplayName("Acessar informações de empresas ou filiais")]
        public bool pua_ler_clienteempresaunidade_details { get; set; }
        [DisplayName("Editar empresas ou filiais")]
        public bool pua_lerescrever_clienteempresaunidade_edit { get; set; }
        //public bool pua_escrever_clienteempresaunidade_edit { get; set; }
        [DisplayName("Acessar configurações de empresas ou filiais")]
        public bool pua_ler_clienteempresaunidade_index { get; set; }


        [DisplayName("Criar usuário")]
        public bool pua_lerescrever_clienteempresausuario_create { get; set; }
        //public bool pua_escrever_clienteempresausuario_create { get; set; }
        [DisplayName("Apagar usuário")]
        public bool pua_lerescrever_clienteempresausuario_delete { get; set; }
        //public bool pua_escrever_clienteempresausuario_delete { get; set; }
        [DisplayName("Exibir informações do usuário")]
        public bool pua_ler_clienteempresausuario_details { get; set; }
        [DisplayName("Editar usuário")]
        public bool pua_lerescrever_clienteempresausuario_edit { get; set; }
        //public bool pua_escrever_clienteempresausuario_edit { get; set; }
        [DisplayName("Acessar configurações referente a usuários")]
        public bool pua_ler_clienteempresausuario_index { get; set; }
        [DisplayName("Trocar senha")]
        public bool pua_lerescrever_clienteempresausuario_mudarsenha { get; set; }
        //public bool pua_escrever_clienteempresausuario_mudarsenha { get; set; }


        [DisplayName("Criar novo perfil de vínculo entre usuário e empresa ou filial")]
        public bool pua_lerescrever_perfilusuarioempresa_create { get; set; }
        //public bool pua_escrever_perfilusuarioempresa_create { get; set; }
        [DisplayName("Apagar perfil de vínculo entre usuário e empresa ou filial")]
        public bool pua_lerescrever_perfilusuarioempresa_delete { get; set; }
        //public bool pua_escrever_perfilusuarioempresa_delete { get; set; }
        [DisplayName("Exibir informações do vínculo entre usuário e empresa ou filial")]
        public bool pua_ler_perfilusuarioempresa_details { get; set; }
        [DisplayName("Editar o vínculo entre usuário e empresa ou filial")]
        public bool pua_lerescrever_perfilusuarioempresa_edit { get; set; }
        //public bool pua_escrever_perfilusuarioempresa_edit { get; set; }
        [DisplayName("Acessar configurações de vínculo entre usuário e empresa ou filial")]
        public bool pua_ler_perfilusuarioempresa_index { get; set; }


        [DisplayName("Criar um novo perfil de acesso do usuário")]
        public bool pua_lerescrever_perfilusuarioacesso_create { get; set; }
        //public bool pua_escrever_perfilusuarioacesso_create { get; set; }
        [DisplayName("Apagar um perfil de acesso do usuário")]
        public bool pua_lerescrever_perfilusuarioacesso_delete { get; set; }
        //public bool pua_escrever_perfilusuarioacesso_delete { get; set; }
        [DisplayName("Exibir informações de um perfil de acesso do usuário")]
        public bool pua_ler_perfilusuarioacesso_details { get; set; }
        [DisplayName("Editar perfil de acesso do usuário")]
        public bool pua_lerescrever_perfilusuarioacesso_edit { get; set; }
        //public bool pua_escrever_perfilusuarioacesso_edit { get; set; }
        [DisplayName("Acessar configurações de perfil de acesso do usuário")]
        public bool pua_ler_perfilusuarioacesso_index { get; set; }


        // Vínculo com a tabela de usuários de empresas
        [Display(Name = "Usuário")]
        [ForeignKey("cliente_empresa_usuario")]
        public int pua_usuariocliente { get; set; }
        public virtual cliente_empresa_usuario cliente_empresa_usuario { get; set; }


        //[NotMapped]
        //public bool pua_acesso_variavel { get; set; }
    }
}