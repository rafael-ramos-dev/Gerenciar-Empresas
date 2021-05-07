namespace Rafa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cliente_empresa",
                c => new
                    {
                        cle_codigo = c.Int(nullable: false, identity: true),
                        cle_razaosocial_principal = c.String(),
                        cle_nomefantasia_principal = c.String(),
                        cle_cnpj_principal = c.String(),
                        cle_inscricao_municipal = c.String(),
                        cle_inscricao_estadual = c.String(),
                        cle_ramo_atividade = c.String(),
                        cle_numero_funcionario = c.String(),
                        cle_prazo_validade = c.String(),
                        cle_endereco = c.String(),
                        cle_numero = c.Int(nullable: false),
                        cle_complemento = c.String(),
                        cle_bairro = c.String(),
                        cle_cidade = c.String(),
                        cle_estado = c.String(),
                        cle_cep = c.String(),
                        cle_telefone_principal = c.String(),
                        cle_webProjetoTesteRafae = c.String(),
                        cle_cnpj_cobranca = c.String(),
                        cle_razaosocial_cobranca = c.String(),
                        cle_contato_financeiro = c.String(),
                        cle_telefone_financeiro = c.String(),
                        cle_email_financeiro = c.String(),
                        cle_nome_responsavel = c.String(),
                        cle_email_responsavel = c.String(),
                        cle_funcao_responsavel = c.String(),
                        cle_telefone_responsavel = c.String(),
                        cle_data_ingresso = c.String(),
                        cle_data_cancelamento = c.String(),
                        cle_quantidade_unidade = c.Int(nullable: false),
                        cle_quantidade_usuario = c.Int(nullable: false),
                        cle_quantidade_unidade_ativa = c.Int(nullable: false),
                        cle_quantidade_usuario_ativo = c.Int(nullable: false),
                        cle_ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.cle_codigo);
            
            CreateTable(
                "dbo.cliente_empresa_usuario",
                c => new
                    {
                        cus_codigo = c.Int(nullable: false, identity: true),
                        cus_usuario = c.String(nullable: false),
                        cus_senha = c.String(nullable: false),
                        cus_lembrar_me = c.Boolean(nullable: false),
                        cus_nome = c.String(),
                        cus_sobrenome = c.String(),
                        cus_endereco = c.String(),
                        cus_numero = c.String(),
                        cus_complemento = c.String(),
                        cus_bairro = c.String(),
                        cus_cidade = c.String(),
                        cus_estado = c.String(),
                        cus_cep = c.String(),
                        cus_telefone = c.String(),
                        cus_email = c.String(),
                        cus_ativo = c.Boolean(nullable: false),
                        cus_empresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.cus_codigo)
                .ForeignKey("dbo.cliente_empresa", t => t.cus_empresa, cascadeDelete: true)
                .Index(t => t.cus_empresa);
            
            CreateTable(
                "dbo.perfil_usuario_acesso",
                c => new
                    {
                        pua_codigo = c.Int(nullable: false, identity: true),
                        pua_lerescrever_clienteempresa_create = c.Boolean(nullable: false),
                        pua_lerescrever_clienteempresa_delete = c.Boolean(nullable: false),
                        pua_ler_clienteempresa_details = c.Boolean(nullable: false),
                        pua_lerescrever_clienteempresa_edit = c.Boolean(nullable: false),
                        pua_ler_clienteempresa_index = c.Boolean(nullable: false),
                        pua_lerescrever_clienteempresaunidade_create = c.Boolean(nullable: false),
                        pua_lerescrever_clienteempresaunidade_delete = c.Boolean(nullable: false),
                        pua_ler_clienteempresaunidade_details = c.Boolean(nullable: false),
                        pua_lerescrever_clienteempresaunidade_edit = c.Boolean(nullable: false),
                        pua_ler_clienteempresaunidade_index = c.Boolean(nullable: false),
                        pua_lerescrever_clienteempresausuario_create = c.Boolean(nullable: false),
                        pua_lerescrever_clienteempresausuario_delete = c.Boolean(nullable: false),
                        pua_ler_clienteempresausuario_details = c.Boolean(nullable: false),
                        pua_lerescrever_clienteempresausuario_edit = c.Boolean(nullable: false),
                        pua_ler_clienteempresausuario_index = c.Boolean(nullable: false),
                        pua_lerescrever_clienteempresausuario_mudarsenha = c.Boolean(nullable: false),
                        pua_lerescrever_perfilusuarioempresa_create = c.Boolean(nullable: false),
                        pua_lerescrever_perfilusuarioempresa_delete = c.Boolean(nullable: false),
                        pua_ler_perfilusuarioempresa_details = c.Boolean(nullable: false),
                        pua_lerescrever_perfilusuarioempresa_edit = c.Boolean(nullable: false),
                        pua_ler_perfilusuarioempresa_index = c.Boolean(nullable: false),
                        pua_lerescrever_perfilusuarioacesso_create = c.Boolean(nullable: false),
                        pua_lerescrever_perfilusuarioacesso_delete = c.Boolean(nullable: false),
                        pua_ler_perfilusuarioacesso_details = c.Boolean(nullable: false),
                        pua_lerescrever_perfilusuarioacesso_edit = c.Boolean(nullable: false),
                        pua_ler_perfilusuarioacesso_index = c.Boolean(nullable: false),
                        pua_usuariocliente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.pua_codigo)
                .ForeignKey("dbo.cliente_empresa_usuario", t => t.pua_usuariocliente, cascadeDelete: true)
                .Index(t => t.pua_usuariocliente);
            
            CreateTable(
                "dbo.perfis_usuario",
                c => new
                    {
                        pus_codigo = c.Int(nullable: false, identity: true),
                        pus_lerescrever_usuariogerente_create = c.Boolean(nullable: false),
                        pus_lerescrever_usuariogerente_delete = c.Boolean(nullable: false),
                        pus_ler_usuariogerente_details = c.Boolean(nullable: false),
                        pus_lerescrever_usuariogerente_edit = c.Boolean(nullable: false),
                        pus_ler_usuariogerente_index = c.Boolean(nullable: false),
                        pus_lerescrever_usuariogerente_mudarsenha = c.Boolean(nullable: false),
                        pus_lerescrever_clienteempresa_create = c.Boolean(nullable: false),
                        pus_lerescrever_clienteempresa_delete = c.Boolean(nullable: false),
                        pus_ler_clienteempresa_details = c.Boolean(nullable: false),
                        pus_lerescrever_clienteempresa_edit = c.Boolean(nullable: false),
                        pus_ler_clienteempresa_index = c.Boolean(nullable: false),
                        pus_lerescrever_clienteempresaunidade_create = c.Boolean(nullable: false),
                        pus_lerescrever_clienteempresaunidade_delete = c.Boolean(nullable: false),
                        pus_ler_clienteempresaunidade_details = c.Boolean(nullable: false),
                        pus_lerescrever_clienteempresaunidade_edit = c.Boolean(nullable: false),
                        pus_ler_clienteempresaunidade_index = c.Boolean(nullable: false),
                        pus_lerescrever_clienteempresausuario_create = c.Boolean(nullable: false),
                        pus_lerescrever_clienteempresausuario_delete = c.Boolean(nullable: false),
                        pus_ler_clienteempresausuario_details = c.Boolean(nullable: false),
                        pus_lerescrever_clienteempresausuario_edit = c.Boolean(nullable: false),
                        pus_ler_clienteempresausuario_index = c.Boolean(nullable: false),
                        pus_lerescrever_clienteempresausuario_mudarsenha = c.Boolean(nullable: false),
                        pus_lerescrever_perfilusuarioempresa_create = c.Boolean(nullable: false),
                        pus_lerescrever_perfilusuarioempresa_delete = c.Boolean(nullable: false),
                        pus_ler_perfilusuarioempresa_details = c.Boolean(nullable: false),
                        pus_lerescrever_perfilusuarioempresa_edit = c.Boolean(nullable: false),
                        pus_ler_perfilusuarioempresa_index = c.Boolean(nullable: false),
                        pus_lerescrever_perfilusuarioacesso_create = c.Boolean(nullable: false),
                        pus_lerescrever_perfilusuarioacesso_delete = c.Boolean(nullable: false),
                        pus_ler_perfilusuarioacesso_details = c.Boolean(nullable: false),
                        pus_lerescrever_perfilusuarioacesso_edit = c.Boolean(nullable: false),
                        pus_ler_perfilusuarioacesso_index = c.Boolean(nullable: false),
                        pus_lerescrever_perfilusuariogerente_create = c.Boolean(nullable: false),
                        pus_lerescrever_perfilusuariogerente_delete = c.Boolean(nullable: false),
                        pus_ler_perfilusuariogerente_details = c.Boolean(nullable: false),
                        pus_lerescrever_perfilusuariogerente_edit = c.Boolean(nullable: false),
                        pus_ler_perfilusuariogerente_index = c.Boolean(nullable: false),
                        pus_usuariogerente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.pus_codigo)
                .ForeignKey("dbo.usuario_gerente", t => t.pus_usuariogerente, cascadeDelete: true)
                .Index(t => t.pus_usuariogerente);
            
            CreateTable(
                "dbo.usuario_gerente",
                c => new
                    {
                        uss_codigo = c.Int(nullable: false, identity: true),
                        uss_usuario = c.String(nullable: false),
                        uss_senha = c.String(nullable: false),
                        uss_lembrar_me = c.Boolean(nullable: false),
                        uss_nome = c.String(),
                        uss_sobrenome = c.String(),
                        uss_endereco = c.String(),
                        uss_numero = c.String(),
                        uss_complemento = c.String(),
                        uss_bairro = c.String(),
                        uss_cidade = c.String(),
                        uss_estado = c.String(),
                        uss_cep = c.String(),
                        uss_telefone = c.String(),
                        uss_email = c.String(),
                        uss_ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.uss_codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.perfis_usuario", "pus_usuariogerente", "dbo.usuario_gerente");
            DropForeignKey("dbo.perfil_usuario_acesso", "pua_usuariocliente", "dbo.cliente_empresa_usuario");
            DropForeignKey("dbo.cliente_empresa_usuario", "cus_empresa", "dbo.cliente_empresa");
            DropIndex("dbo.perfis_usuario", new[] { "pus_usuariogerente" });
            DropIndex("dbo.perfil_usuario_acesso", new[] { "pua_usuariocliente" });
            DropIndex("dbo.cliente_empresa_usuario", new[] { "cus_empresa" });
            DropTable("dbo.usuario_gerente");
            DropTable("dbo.perfis_usuario");
            DropTable("dbo.perfil_usuario_acesso");
            DropTable("dbo.cliente_empresa_usuario");
            DropTable("dbo.cliente_empresa");
        }
    }
}
