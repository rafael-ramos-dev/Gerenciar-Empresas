using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoTesteRafa.Models
{
    public class usuario_gerente
    {
        [Key]
        public int uss_codigo { get; set; }

        [Required(ErrorMessage = "Informe o usuário")]
        [Display(Name = "Usuário")]
        public string uss_usuario { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string uss_senha { get; set; }


        [Display(Name = "Lembrar-me?")]
        public bool uss_lembrar_me { get; set; }

        [Display(Name = "Nome")]
        public string uss_nome { get; set; }

        [Display(Name = "Sorenome")]
        public string uss_sobrenome { get; set; }

        [Display(Name = "Endereço")]
        public string uss_endereco { get; set; }

        [Display(Name = "Número")]
        public string uss_numero { get; set; }

        [Display(Name = "Complemento")]
        public string uss_complemento { get; set; }

        [Display(Name = "Bairro")]
        public string uss_bairro { get; set; }

        [Display(Name = "Cidade")]
        public string uss_cidade { get; set; }

        [Display(Name = "Estado")]
        public string uss_estado { get; set; }

        [Display(Name = "CEP")]
        public string uss_cep { get; set; }

        [Display(Name = "Telefone")]
        public string uss_telefone { get; set; }

        [Display(Name = "E-mail")]
        public string uss_email { get; set; }

        [Display(Name = "Ativar Usuário")]
        public bool uss_ativo { get; set; }

        [NotMapped]
        public string uss_senha_antiga { get; set; }

        [NotMapped]
        public string uss_senha_nova { get; set; }


        //Para fazer a ligação com a entidade 'perfis_usuario'
        public virtual IList<perfis_usuario> perfis_usuario { get; set; }


    }


}