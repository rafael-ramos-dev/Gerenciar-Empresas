using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using ProjetoTesteRafa.Models;
using ProjetoTesteRafa.Models.Comum;

namespace ProjetoTesteRafa.Controllers
{
    public class ClienteEmpresaController : Controller
    {

        private DataContext db = new DataContext();


        /*****************************************
         *                                       *
         *                  INDEX                *
         *                                       *
         *****************************************/


        // GET: ClienteEmpresa - Página Index onde serão mostrados os usuários
        [Authorize]
        public async Task<ActionResult> Index()
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null)
                    {
                        if (acesso_perfil.Any(x => x.pua_ler_clienteempresa_index))
                        {
                            return View(await db.cliente_empresa.ToListAsync());  
                        }
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_ler_clienteempresa_index))
                    {
                        return View(await db.cliente_empresa.ToListAsync()); 
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


        // GET: ClienteEmpresa/Details/5 - Mostrar os detalhes da Empresa selecionada. A numeração no comentário serve para mostrar qual
        //  o índice (o ID do ítem) no banco.
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null)
                    {
                        if (acesso_perfil.Any(x => x.pua_ler_clienteempresa_details))
                        {
                            if (id == null)
                            {
                                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                            }
                            cliente_empresa cliente_empresa = await db.cliente_empresa.FindAsync(id);
                            if (cliente_empresa == null)
                            {
                                return HttpNotFound();
                            }
                            return View(cliente_empresa); 
                        }
                    }
                        return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_ler_clienteempresa_details))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        cliente_empresa cliente_empresa = await db.cliente_empresa.FindAsync(id);
                        if (cliente_empresa == null)
                        {
                            return HttpNotFound();
                        }
                        return View(cliente_empresa); 
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


        // GET: ClienteEmpresa/Create - Mostrar a criação da Empresa.
        [Authorize]
        public ActionResult Create()
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_clienteempresa_create))
                    {
                        ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                        return View(); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_clienteempresa_create))
                    {
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


        // POST: ClienteEmpresaUnidade/Create - Cria a Empresa no banco.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "cle_codigo,cle_razaosocial_principal,cle_nomefantasia_principal,cle_cnpj_principal,cle_inscricao_municipal,cle_inscricao_estadual,cle_ramo_atividade,cle_numero_funcionario,cle_prazo_validade,cle_endereco,cle_numero,cle_complemento,cle_bairro,cle_cidade,cle_estado,cle_cep,cle_telefone_principal,cle_webProjetoTesteRafae,cle_cnpj_cobranca,cle_razaosocial_cobranca,cle_contato_financeiro,cle_telefone_financeiro,cle_email_financeiro,cle_nome_responsavel,cle_email_responsavel,cle_funcao_responsavel,cle_telefone_responsavel,cle_data_ingresso,cle_data_cancelamento,cle_quantidade_unidade,cle_quantidade_usuario,cle_ativo")] cliente_empresa cliente_empresa, string estados)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_clienteempresa_create))
                    {
                        if (ModelState.IsValid)
                        {
                            cliente_empresa.cle_estado = estados;
                            cliente_empresa.cle_ativo = true;
                            db.cliente_empresa.Add(cliente_empresa);
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }

                        ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                        return View(cliente_empresa); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_clienteempresa_create))
                    {
                        if (ModelState.IsValid)
                        {
                            cliente_empresa.cle_estado = estados;
                            cliente_empresa.cle_ativo = true;
                            db.cliente_empresa.Add(cliente_empresa);
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }

                        ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                        return View(cliente_empresa); 
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
         *                 EDIT                  *
         *                                       *
         *****************************************/


        // GET: ClienteEmpresa/Edit/5 - Mostrar a página de edição da Empresa selecionada. A numeração no comentário serve para mostrar qual
        //  o índice (o ID do ítem) no banco.
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_clienteempresa_edit))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        cliente_empresa cliente_empresa = await db.cliente_empresa.FindAsync(id);
                        if (cliente_empresa == null)
                        {
                            return HttpNotFound();
                        }
                        ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                        return View(cliente_empresa); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_clienteempresa_edit))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        cliente_empresa cliente_empresa = await db.cliente_empresa.FindAsync(id);
                        if (cliente_empresa == null)
                        {
                            return HttpNotFound();
                        }
                        ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                        return View(cliente_empresa); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }


        // POST: ClienteEmpresa/Edit/5 - Salva no banco as edições efetuadas na Empresa selecionada. A numeração no comentário serve para mostrar qual
        //  o índice (o ID do ítem) no banco.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit([Bind(Include = "cle_codigo,cle_razaosocial_principal,cle_nomefantasia_principal,cle_cnpj_principal,cle_inscricao_municipal,cle_inscricao_estadual,cle_ramo_atividade,cle_numero_funcionario,cle_prazo_validade,cle_endereco,cle_numero,cle_complemento,cle_bairro,cle_cidade,cle_estado,cle_cep,cle_telefone_principal,cle_webProjetoTesteRafae,cle_cnpj_cobranca,cle_razaosocial_cobranca,cle_contato_financeiro,cle_telefone_financeiro,cle_email_financeiro,cle_nome_responsavel,cle_email_responsavel,cle_funcao_responsavel,cle_telefone_responsavel,cle_data_ingresso,cle_data_cancelamento,cle_quantidade_unidade,cle_quantidade_usuario,cle_ativo")] cliente_empresa cliente_empresa, string estados)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_clienteempresa_edit))
                    {
                        if (ModelState.IsValid)
                        {
                            cliente_empresa.cle_estado = estados;
                            db.Entry(cliente_empresa).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        return View(cliente_empresa); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_clienteempresa_edit))
                    {
                        if (ModelState.IsValid)
                        {
                            cliente_empresa.cle_estado = estados;
                            db.Entry(cliente_empresa).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        return View(cliente_empresa);
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


        // GET: ClienteEmpresaUnidade/Delete/5 - Mostrar a página de exclusão da Empresa selecionada. A numeração no comentário serve para mostrar qual
        // o índice (o ID do ítem) no banco.
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_clienteempresa_delete))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        cliente_empresa cliente_empresa = await db.cliente_empresa.FindAsync(id);
                        if (cliente_empresa == null)
                        {
                            return HttpNotFound();
                        }
                        ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                        return View(cliente_empresa);   
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_clienteempresa_delete))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        cliente_empresa cliente_empresa = await db.cliente_empresa.FindAsync(id);
                        if (cliente_empresa == null)
                        {
                            return HttpNotFound();
                        }
                        ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                        return View(cliente_empresa);
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }


        // POST: ClienteEmpresa/Edit/5 - Exclui a Empresa selecionada do banco. A numeração no comentário serve para mostrar qual
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

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_clienteempresa_delete))
                    {
                        cliente_empresa cliente_empresa = await db.cliente_empresa.FindAsync(id);
                        db.cliente_empresa.Remove(cliente_empresa);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index"); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_clienteempresa_delete))
                    {
                        cliente_empresa cliente_empresa = await db.cliente_empresa.FindAsync(id);
                        db.cliente_empresa.Remove(cliente_empresa);
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
