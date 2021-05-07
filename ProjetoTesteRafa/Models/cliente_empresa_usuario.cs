using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoTesteRafa.Models
{
    public class cliente_empresa_usuario
    {
        [Key]
        public int cus_codigo { get; set; }

        [Required(ErrorMessage = "Informe o usuário")]
        [Display(Name = "Usuário:")]
        public string cus_usuario { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha:")]
        public string cus_senha { get; set; }

        [Display(Name = "Lembrar-me?")]
        public bool cus_lembrar_me { get; set; }

        [Display(Name = "Nome:")]
        public string cus_nome { get; set; }

        [Display(Name = "Sorenome:")]
        public string cus_sobrenome { get; set; }

        [Display(Name = "Endereço:")]
        public string cus_endereco { get; set; }

        [Display(Name = "Número:")]
        public string cus_numero { get; set; }

        [Display(Name = "Complemento:")]
        public string cus_complemento { get; set; }

        [Display(Name = "Bairro:")]
        public string cus_bairro { get; set; }

        [Display(Name = "Cidade:")]
        public string cus_cidade { get; set; }

        [Display(Name = "Estado:")]
        public string cus_estado { get; set; }

        [Display(Name = "CEP:")]
        public string cus_cep { get; set; }

        [Display(Name = "Telefone:")]
        public string cus_telefone { get; set; }

        [Display(Name = "E-mail:")]
        public string cus_email { get; set; }

        [Display(Name = "Usuário ativo:")]
        public bool cus_ativo { get; set; }

        [NotMapped]
        public bool cus_ativo_anterior { get; set; }


        [Display(Name = "Empresa")]
        [ForeignKey("cliente_empresa")]
        public int cus_empresa { get; set; }
        public virtual cliente_empresa cliente_empresa { get; set; }



        // Vínculo com a tabela de perfil de acessos
        public virtual IList<perfil_usuario_acesso> cus_perfil_acesso { get; set; }


    }
}