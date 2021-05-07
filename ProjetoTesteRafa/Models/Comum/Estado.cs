using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoTesteRafa.Models.Comum
{
    public class Estados
    {
        [NotMapped]
        public string data_sigla_estado { get; set; }

        [NotMapped]
        public string data_nome_estado { get; set; }


        public List<Estados> ListaEstados()
        {
            return new List<Estados>()
            {
                new Estados(){ data_sigla_estado="AC", data_nome_estado="Acre"},
                new Estados(){ data_sigla_estado="AL", data_nome_estado="Alagoas"},
                new Estados(){ data_sigla_estado="AP", data_nome_estado="Amapá"},
                new Estados(){ data_sigla_estado="AM", data_nome_estado="Amazonas"},
                new Estados(){ data_sigla_estado="BA", data_nome_estado="Bahia"},
                new Estados(){ data_sigla_estado="CE", data_nome_estado="Ceará"},
                new Estados(){ data_sigla_estado="DF", data_nome_estado="Distrito Federal"},
                new Estados(){ data_sigla_estado="ES", data_nome_estado="Espírito Santo"},
                new Estados(){ data_sigla_estado="GO", data_nome_estado="Goiás"},
                new Estados(){ data_sigla_estado="MA", data_nome_estado="Maranhão"},
                new Estados(){ data_sigla_estado="MT", data_nome_estado="Mato Grosso"},
                new Estados(){ data_sigla_estado="MS", data_nome_estado="Mato Grosso do Sul"},
                new Estados(){ data_sigla_estado="MG", data_nome_estado="Minas Gerais"},
                new Estados(){ data_sigla_estado="PA", data_nome_estado="Pará"},
                new Estados(){ data_sigla_estado="PB", data_nome_estado="Paraíba"},
                new Estados(){ data_sigla_estado="PR", data_nome_estado="Paraná"},
                new Estados(){ data_sigla_estado="PE", data_nome_estado="Pernambuco"},
                new Estados(){ data_sigla_estado="PI", data_nome_estado="Piauí"},
                new Estados(){ data_sigla_estado="RJ", data_nome_estado="Rio de Janeiro"},
                new Estados(){ data_sigla_estado="RN", data_nome_estado="Rio Grande do Norte"},
                new Estados(){ data_sigla_estado="RS", data_nome_estado="Rio Grande do Sul"},
                new Estados(){ data_sigla_estado="RO", data_nome_estado="Rondônia"},
                new Estados(){ data_sigla_estado="RR", data_nome_estado="Roraima"},
                new Estados(){ data_sigla_estado="SC", data_nome_estado="Santa Catarina"},
                new Estados(){ data_sigla_estado="SP", data_nome_estado="São Paulo"},
                new Estados(){ data_sigla_estado="SE", data_nome_estado="Sergipe"},
                new Estados(){ data_sigla_estado="TO", data_nome_estado="Tocantins"}
            };
        }

    }

}