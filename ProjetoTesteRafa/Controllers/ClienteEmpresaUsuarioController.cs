using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using ProjetoTesteRafa.Models;
using ProjetoTesteRafa.Models.Comum;

namespace ProjetoTesteRafa.Controllers
{
    public class ClienteEmpresaUsuarioController : Controller
    {
        private DataContext db = new DataContext();


        /*****************************************
         *                                       *
         *                INDEX                  *
         *                                       *
         *****************************************/


        // GET: ClienteEmpresaUsuario - Página Index onde serão mostrados os usuários
        [Authorize]
        public async Task<ActionResult> Index()
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_ler_clienteempresausuario_index))
                    {
                        // Funciona
                        // a variável em Session precisa ser posta em outra variável com seu tipo antes de ser passada
                        // para ser feito o comparativo e buscar apenas as empresas que fazem parte do usuário em questão
                        var empresa = (int)Session["Empresa"];

                        // busca no banco incluindo a tabela cliente_empresa
                        var cliente_empresa_usuario = db.cliente_empresa_usuario.Include(c => c.cliente_empresa).OrderBy(x => x.cus_nome);

                        //verifica o que cliente_empresa_usuario trouxe do banco e filtra pra mostrar apenas
                        // a empresa que é comparável ao usuário logado
                        var usuario_final = cliente_empresa_usuario.Where(x => x.cus_empresa == empresa);

                        // devolve para a View de forma Assíncrona os dados já filtrados para serem mostrados na View
                        return View(await usuario_final.ToListAsync());
                        
                        // funcional
                        //var cliente_empresa_usuario = new List<cliente_empresa_usuario>(db.cliente_empresa_usuario.Include(c => c.cliente_empresa).ToList().Where(x => x.cus_empresa == (int)Session["Empresa"]));
                        //return View(cliente_empresa_usuario);

                        //Padrão
                        //return View(await cliente_empresa_usuario.ToListAsync());
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_ler_clienteempresausuario_index))
                    {
                        var cliente_empresa_usuario = db.cliente_empresa_usuario.Include(c => c.cliente_empresa).OrderBy(x => x.cliente_empresa.cle_razaosocial_principal);
                        return View(await cliente_empresa_usuario.ToListAsync()); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }



        /*****************************************
         *                                       *
         *                DETAILS                *
         *                                       *
         *****************************************/


        // GET: ClienteEmpresaUsuario/Details/5 - Mostrar os detalhes do usuário selecionado. A numeração no comentário serve para mostrar qual
        //  o índice (o ID do ítem) no banco.
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_ler_clienteempresausuario_details))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        cliente_empresa_usuario cliente_empresa_usuario = await db.cliente_empresa_usuario.FindAsync(id);
                        if (cliente_empresa_usuario == null)
                        {
                            return HttpNotFound();
                        }

                        // Valida se o id procurado faz parte da empresa e exiba. Caso contrário, mostra uma
                        // página dizendo que não faz parte da empresa.
                        var empresa = (int)Session["Empresa"];
                        var acesso_empresa = cliente_empresa_usuario.cus_empresa.Equals(empresa);

                        if (!acesso_empresa)
                        {
                            return View("~/Views/Shared/_OutraEmpresa.cshtml");
                        }

                        // Término da validação da empresa

                        return View(cliente_empresa_usuario);
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_ler_clienteempresausuario_details))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        cliente_empresa_usuario cliente_empresa_usuario = await db.cliente_empresa_usuario.FindAsync(id);
                        if (cliente_empresa_usuario == null)
                        {
                            return HttpNotFound();
                        }
                        return View(cliente_empresa_usuario); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }



        /*****************************************
         *                                       *
         *                CREATE                 *
         *                                       *
         *****************************************/


        // GET: ClienteEmpresaUsuario/Create - Mostrar a criação do Usuário da Empresa
        [Authorize]
        public async Task<ActionResult> Create()
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var empresa = (int)Session["Empresa"];
                    cliente_empresa cliente_empresa = await db.cliente_empresa.FindAsync(empresa);

                    if (cliente_empresa.cle_quantidade_usuario_ativo < cliente_empresa.cle_quantidade_usuario)
                    {
                        var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                        if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_clienteempresausuario_create))
                        {
                            //ViewBag.cus_empresa = new SelectList(db.cliente_empresa, "cle_codigo", "cle_razaosocial_principal");
                            ViewBag.cus_empresa = new SelectList(db.cliente_empresa.ToList().Where(x => x.cle_codigo == (int)Session["Empresa"]), "cle_codigo", "cle_razaosocial_principal");
                            ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                            return View();
                        }
                        return View("~/Views/Shared/_SemAutorizacao.cshtml"); 
                    }
                    return View("~/Views/Shared/_LimiteExcedido.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_clienteempresausuario_create))
                    {
                        // só põe na lista as empresas onde a quantidade de usuários ativos sejam menores que a quantidade máxima
                        // de usuários que a empresa pode criar, impedindo assim de que seja criados novos usuários sem que
                        // a empresa tenha contratado tal quantidade
                        var items = db.cliente_empresa.ToList().Where(x => x.cle_quantidade_usuario_ativo < x.cle_quantidade_usuario);

                        ViewBag.cus_empresa = new SelectList(items, "cle_codigo", "cle_razaosocial_principal");
                        ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                        return View(); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }


        // POST: ClienteEmpresaUsuario/Create - Cria o Usuário da Empresa no banco.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "cus_codigo,cus_usuario,cus_senha,cus_lembrar_me,cus_nome,cus_sobrenome,cus_endereco,cus_numero,cus_complemento,cus_bairro,cus_cidade,cus_estado,cus_cep,cus_telefone,cus_email,cus_ativo,cus_empresa,cus_empresa_unidade")] cliente_empresa_usuario cliente_empresa_usuario, string estados)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_clienteempresausuario_create))
                    {
                        var empresa = (int)Session["Empresa"];

                        if (ModelState.IsValid)
                        {
                            // Adiciona 1 ao número de usuários ativos
                            //var empresa = (int)Session["Empresa"];
                            cliente_empresa cliente_empresa = await db.cliente_empresa.FindAsync(empresa);
                            var usuario_ativo = cliente_empresa.cle_quantidade_usuario_ativo;
                            usuario_ativo++;
                            cliente_empresa.cle_quantidade_usuario_ativo = usuario_ativo;
                            db.Entry(cliente_empresa).State = EntityState.Modified;

                            // Permite que o Usuário só seja criado nessa empresa caso não seja criado ou editado pela gerente
                            cliente_empresa_usuario.cus_empresa = (int)Session["Empresa"];
                            cliente_empresa_usuario.cus_estado = estados;
                            cliente_empresa_usuario.cus_senha = CriarMD5(cliente_empresa_usuario.cus_senha);
                            cliente_empresa_usuario.cus_ativo = true;
                            db.cliente_empresa_usuario.Add(cliente_empresa_usuario);
                            
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }

                        
                        ViewBag.cus_empresa = new SelectList(db.cliente_empresa, "cle_codigo", "cle_razaosocial_principal", cliente_empresa_usuario.cus_empresa);
                        ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                        return View(cliente_empresa_usuario);
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_clienteempresausuario_create))
                    {
                        if (ModelState.IsValid)
                        {
                            // Adiciona 1 ao número de usuários ativos
                            var empresa = cliente_empresa_usuario.cus_empresa;
                            cliente_empresa cliente_empresa = await db.cliente_empresa.FindAsync(empresa);
                            var usuario_ativo = cliente_empresa.cle_quantidade_usuario_ativo;
                            usuario_ativo++;
                            cliente_empresa.cle_quantidade_usuario_ativo = usuario_ativo;
                            db.Entry(cliente_empresa).State = EntityState.Modified;


                            cliente_empresa_usuario.cus_estado = estados;
                            cliente_empresa_usuario.cus_senha = CriarMD5(cliente_empresa_usuario.cus_senha);
                            cliente_empresa_usuario.cus_ativo = true;
                            db.cliente_empresa_usuario.Add(cliente_empresa_usuario);
                            
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        ViewBag.cus_empresa = new SelectList(db.cliente_empresa, "cle_codigo", "cle_razaosocial_principal", cliente_empresa_usuario.cus_empresa);
                        ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                        return View(cliente_empresa_usuario); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }



        /*****************************************
         *                                       *
         *                EDIT                   *
         *                                       *
         *****************************************/


        // GET: ClienteEmpresaUsuario/Edit/5 - Mostrar a página de edição do Usuário da Empresa selecionado. A numeração no comentário serve para mostrar qual
        //  o índice (o ID do ítem) no banco.
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_clienteempresaunidade_edit))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        cliente_empresa_usuario cliente_empresa_usuario = await db.cliente_empresa_usuario.FindAsync(id);
                        if (cliente_empresa_usuario == null)
                        {
                            return HttpNotFound();
                        }

                        var empresa = (int)Session["Empresa"];
                        var acesso_empresa = cliente_empresa_usuario.cus_empresa.Equals(empresa);

                        if (!acesso_empresa)
                        {
                            return View("~/Views/Shared/_OutraEmpresa.cshtml");
                        }

                        // envia para a view o valor original de cus_ativo (pra ver se estava ativo ou não) para depois
                        // retornar o valor original ao posto para que sejam feitas as comparações necessárias
                        cliente_empresa_usuario.cus_ativo_anterior = cliente_empresa_usuario.cus_ativo;

                        ViewBag.cus_empresa = new SelectList(db.cliente_empresa.ToList().Where(x => x.cle_codigo == (int)Session["Empresa"]), "cle_codigo", "cle_razaosocial_principal");
                        ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                        return View(cliente_empresa_usuario); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_clienteempresausuario_edit))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        cliente_empresa_usuario cliente_empresa_usuario = await db.cliente_empresa_usuario.FindAsync(id);
                        if (cliente_empresa_usuario == null)
                        {
                            return HttpNotFound();
                        }

                        // envia para a view o valor original de cus_ativo (pra ver se estava ativo ou não) para depois
                        // retornar o valor original ao posto para que sejam feitas as comparações necessárias
                        cliente_empresa_usuario.cus_ativo_anterior = cliente_empresa_usuario.cus_ativo;

                        ViewBag.cus_empresa = new SelectList(db.cliente_empresa, "cle_codigo", "cle_razaosocial_principal", cliente_empresa_usuario.cus_empresa);
                        ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                        return View(cliente_empresa_usuario); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }


        // POST: ClienteEmpresaUsuario/Edit/5 - Salva no banco as edições efetuadas no Usuário da Empresa selecionado. A numeração no comentário serve para mostrar qual
        //  o índice (o ID do ítem) no banco.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit([Bind(Include = "cus_codigo,cus_usuario,cus_senha,cus_lembrar_me,cus_nome,cus_sobrenome,cus_endereco,cus_numero,cus_complemento,cus_bairro,cus_cidade,cus_estado,cus_cep,cus_telefone,cus_email,cus_ativo,cus_empresa,cus_ativo_anterior")] cliente_empresa_usuario cliente_empresa_usuario, string estados)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_clienteempresausuario_edit))
                    {
                        if (ModelState.IsValid)
                        {
                            cliente_empresa_usuario.cus_estado = estados;
                            db.Entry(cliente_empresa_usuario).State = EntityState.Modified;

                            if (cliente_empresa_usuario.cus_ativo_anterior && !cliente_empresa_usuario.cus_ativo || !cliente_empresa_usuario.cus_ativo_anterior && cliente_empresa_usuario.cus_ativo)
                            {
                                var empresa = cliente_empresa_usuario.cus_empresa;
                                cliente_empresa cliente_empresa = await db.cliente_empresa.FindAsync(empresa);
                                var usuario_ativo = cliente_empresa.cle_quantidade_usuario_ativo;

                                if (cliente_empresa_usuario.cus_ativo)
                                {
                                    usuario_ativo++;
                                    cliente_empresa.cle_quantidade_usuario_ativo = usuario_ativo;
                                    db.Entry(cliente_empresa).State = EntityState.Modified;
                                }

                                if (!cliente_empresa_usuario.cus_ativo)
                                {
                                    usuario_ativo--;
                                    cliente_empresa.cle_quantidade_usuario_ativo = usuario_ativo;
                                    db.Entry(cliente_empresa).State = EntityState.Modified;
                                }
                            }

                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        ViewBag.cus_empresa = new SelectList(db.cliente_empresa, "cle_codigo", "cle_razaosocial_principal", cliente_empresa_usuario.cus_empresa);                        
                        ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                        return View(cliente_empresa_usuario); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_clienteempresausuario_edit))
                    {
                        if (ModelState.IsValid)
                        {
                            cliente_empresa_usuario.cus_estado = estados;
                            db.Entry(cliente_empresa_usuario).State = EntityState.Modified;

                            if (cliente_empresa_usuario.cus_ativo_anterior && !cliente_empresa_usuario.cus_ativo || !cliente_empresa_usuario.cus_ativo_anterior && cliente_empresa_usuario.cus_ativo)
                            {
                                var empresa = cliente_empresa_usuario.cus_empresa;
                                cliente_empresa cliente_empresa = await db.cliente_empresa.FindAsync(empresa);
                                var usuario_ativo = cliente_empresa.cle_quantidade_usuario_ativo;

                                if (cliente_empresa_usuario.cus_ativo)
                                {
                                    usuario_ativo++;
                                    cliente_empresa.cle_quantidade_usuario_ativo = usuario_ativo;
                                    db.Entry(cliente_empresa).State = EntityState.Modified;
                                }

                                if (!cliente_empresa_usuario.cus_ativo)
                                {
                                    usuario_ativo--;
                                    cliente_empresa.cle_quantidade_usuario_ativo = usuario_ativo;
                                    db.Entry(cliente_empresa).State = EntityState.Modified;
                                }
                            }

                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        ViewBag.cus_empresa = new SelectList(db.cliente_empresa, "cle_codigo", "cle_razaosocial_principal", cliente_empresa_usuario.cus_empresa);                        
                        ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                        return View(cliente_empresa_usuario); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }



        /*****************************************
         *                                       *
         *                DELETE                 *
         *                                       *
         *****************************************/


        // GET: ClienteEmpresaUsuario/Delete/5 - Mostrar a página de exclusão do Usuário da Empresa selecionado. A numeração no comentário serve para mostrar qual
        // o índice (o ID do ítem) no banco.
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_clienteempresausuario_delete))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        cliente_empresa_usuario cliente_empresa_usuario = await db.cliente_empresa_usuario.FindAsync(id);
                        if (cliente_empresa_usuario == null)
                        {
                            return HttpNotFound();
                        }

                        var empresa = (int)Session["Empresa"];
                        var acesso_empresa = cliente_empresa_usuario.cus_empresa.Equals(empresa);

                        if (!acesso_empresa)
                        {
                            return View("~/Views/Shared/_OutraEmpresa.cshtml");
                        }

                        return View(cliente_empresa_usuario); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_clienteempresausuario_delete))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        cliente_empresa_usuario cliente_empresa_usuario = await db.cliente_empresa_usuario.FindAsync(id);
                        if (cliente_empresa_usuario == null)
                        {
                            return HttpNotFound();
                        }
                        return View(cliente_empresa_usuario); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }


        // POST: ClienteEmpresaUsuario/Edit/5 - Exclui o Usuário da Empresa selecionado do banco. A numeração no comentário serve para mostrar qual
        // o índice (o ID do ítem) no banco.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_clienteempresausuario_delete))
                    {
                        cliente_empresa_usuario cliente_empresa_usuario = await db.cliente_empresa_usuario.FindAsync(id);
                        db.cliente_empresa_usuario.Remove(cliente_empresa_usuario);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index"); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_clienteempresausuario_delete))
                    {
                        cliente_empresa_usuario cliente_empresa_usuario = await db.cliente_empresa_usuario.FindAsync(id);
                        db.cliente_empresa_usuario.Remove(cliente_empresa_usuario);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index"); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }



        /*****************************************
         *                                       *
         *             MUDAR SENHA               *
         *                                       *
         *****************************************/


        // GET: Usuariogerente/MudarSenha/5 - Mostrar a página de mudança da senha do usuário selecionado. A numeração no comentário serve para mostrar qual
        //  o índice (o ID do ítem) no banco.
        [Authorize]
        public async Task<ActionResult> MudarSenha(int? id)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_clienteempresausuario_mudarsenha))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        cliente_empresa_usuario cliente_empresa_usuario = await db.cliente_empresa_usuario.FindAsync(id);
                        if (cliente_empresa_usuario == null)
                        {
                            return HttpNotFound();
                        }

                        var empresa = (int)Session["Empresa"];
                        var acesso_empresa = cliente_empresa_usuario.cus_empresa.Equals(empresa);

                        if (!acesso_empresa)
                        {
                            return View("~/Views/Shared/_OutraEmpresa.cshtml");
                        }

                        return View(cliente_empresa_usuario); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_clienteempresausuario_mudarsenha))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        cliente_empresa_usuario cliente_empresa_usuario = await db.cliente_empresa_usuario.FindAsync(id);
                        if (cliente_empresa_usuario == null)
                        {
                            return HttpNotFound();
                        }
                        return View(cliente_empresa_usuario); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }


        // POST: Usuariogerente/MudarSenha/5 - Salva no banco a mudança da senha efetuada no usuário selecionado. A numeração no comentário serve para mostrar qual
        //  o índice (o ID do ítem) no banco.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> MudarSenha([Bind(Include = "cus_codigo,cus_usuario,cus_senha,cus_lembrar_me,cus_nome,cus_sobrenome,cus_endereco,cus_numero,cus_complemento,cus_bairro,cus_cidade,cus_estado,cus_cep,cus_telefone,cus_email,cus_ativo,cus_empresa")] cliente_empresa_usuario cliente_empresa_usuario)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_clienteempresausuario_mudarsenha))
                    {
                        if (ModelState.IsValid)
                        {
                            cliente_empresa_usuario.cus_senha = CriarMD5(cliente_empresa_usuario.cus_senha);
                            db.Entry(cliente_empresa_usuario).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        return View(cliente_empresa_usuario); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_clienteempresausuario_mudarsenha))
                    {
                        if (ModelState.IsValid)
                        {
                            cliente_empresa_usuario.cus_senha = CriarMD5(cliente_empresa_usuario.cus_senha);
                            db.Entry(cliente_empresa_usuario).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        return View(cliente_empresa_usuario); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }



        /*****************************************
         *                                       *
         *         CRIPTOGRAFIA  MD5             *
         *                                       *
         *****************************************/


        //Criar uma string MD5 para adicionar um pouco de segurança
        public static string CriarMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }



        /*****************************************
         *                                       *
         *                DISPOSE                *
         *                                       *
         *****************************************/


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
