using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoTesteRafa.Models
{
    public class cliente_empresa
    {
        [Key]
        public int cle_codigo { get; set; }
        [DisplayName("Razão Social")]
        public string cle_razaosocial_principal { get; set; }
        [DisplayName("Nome Fantasia")]
        public string cle_nomefantasia_principal { get; set; }
        [DisplayName("CNPJ")]
        public string cle_cnpj_principal { get; set; }
        [DisplayName("Ins. Municipal")]
        public string cle_inscricao_municipal { get; set; }
        [DisplayName("Ins. Estadual")]
        public string cle_inscricao_estadual { get; set; }
        [DisplayName("Ramo de Atividade")]
        public string cle_ramo_atividade { get; set; }
        [DisplayName("Número de Funcionários")]
        public string cle_numero_funcionario { get; set; }
        [DisplayName("Validade do Contrato")]
        public string cle_prazo_validade { get; set; }
        [DisplayName("Endereço")]
        public string cle_endereco { get; set; }
        [DisplayName("Número")]
        public int cle_numero { get; set; }
        [DisplayName("Complemento")]
        public string cle_complemento { get; set; }
        [DisplayName("Bairro")]
        public string cle_bairro { get; set; }
        [DisplayName("Cidade")]
        public string cle_cidade { get; set; }
        [DisplayName("Estado")]
        public string cle_estado { get; set; }
        [DisplayName("CEP")]
        public string cle_cep { get; set; }
        [DisplayName("Telefone")]
        public string cle_telefone_principal { get; set; }
        [DisplayName("WebProjetoTesteRafae")]
        public string cle_webProjetoTesteRafae { get; set; }
        [DisplayName("CNPJ Para Cobrança")]
        public string cle_cnpj_cobranca { get; set; }
        [DisplayName("Razão Social Para Cobrança")]
        public string cle_razaosocial_cobranca { get; set; }
        [DisplayName("Contato Financeiro")]
        public string cle_contato_financeiro { get; set; }
        [DisplayName("Telefone Financeiro")]
        public string cle_telefone_financeiro { get; set; }
        [DisplayName("E-mail Financeiro")]
        public string cle_email_financeiro { get; set; }
        [DisplayName("Responsável")]
        public string cle_nome_responsavel { get; set; }
        [DisplayName("E-mail do Responsável")]
        public string cle_email_responsavel { get; set; }
        [DisplayName("Função do Responsável")]
        public string cle_funcao_responsavel { get; set; }
        [DisplayName("Telefone do Responsável")]
        public string cle_telefone_responsavel { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data Ingresso")]
        public string cle_data_ingresso { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data Cancelamento")]
        public string cle_data_cancelamento { get; set; }

        [DisplayName("Quantidade de empresas contratadas")]
        public int cle_quantidade_unidade { get; set; }
        [DisplayName("Quantidade de usuários contratados")]
        public int cle_quantidade_usuario { get; set; }

        public int cle_quantidade_unidade_ativa { get; set; }

        public int cle_quantidade_usuario_ativo { get; set; }


        [DisplayName("Grupo empresarial ativo:")]
        public bool cle_ativo { get; set; }


        public virtual IList<cliente_empresa_usuario> cle_empresa_usuario { get; set; }
        
    }
}