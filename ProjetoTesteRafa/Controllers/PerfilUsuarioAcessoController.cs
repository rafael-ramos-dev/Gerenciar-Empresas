using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetoTesteRafa.Models;

namespace ProjetoTesteRafa.Controllers
{
    public class PerfilUsuarioAcessoController : Controller
    {
        private DataContext db = new DataContext();


        /*****************************************
         *                                       *
         *                  INDEX                *
         *                                       *
         *****************************************/


        // GET: PerfilUsuarioAcesso - Página Index onde serão mostrados os Acessos dos Usuários
        public async Task<ActionResult> Index()
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null)
                    {
                        if (acesso_perfil.Any(x => x.pua_ler_perfilusuarioacesso_index == true))
                        {
                            var perfil_usuario_acesso = db.perfil_usuario_acesso.Include(p => p.cliente_empresa_usuario);

                            var empresa = (int)Session["Empresa"];
                            var acesso_empresa = perfil_usuario_acesso.Where(x => x.cliente_empresa_usuario.cus_empresa == empresa);

                            return View(await acesso_empresa.ToListAsync());
                        }
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_ler_perfilusuarioacesso_index))
                    {
                        var perfil_usuario_acesso = db.perfil_usuario_acesso.Include(p => p.cliente_empresa_usuario).OrderBy(x => x.cliente_empresa_usuario.cliente_empresa.cle_razaosocial_principal);
                        return View(await perfil_usuario_acesso.ToListAsync()); 
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


        // GET: PerfilUsuarioAcesso/Details/5 - Mostrar os detalhes dos acessos do usuário selecionado. A numeração no comentário 
        //  serve para mostrar qual o índice (o ID do ítem) no banco.
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_ler_perfilusuarioacesso_details == true))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        var perfil_usuario_acesso = await db.perfil_usuario_acesso.FindAsync(id);
                        if (perfil_usuario_acesso == null)
                        {
                            return HttpNotFound();
                        }

                        var empresa = (int)Session["Empresa"];
                        var acesso_empresa = perfil_usuario_acesso.cliente_empresa_usuario.cus_empresa.Equals(empresa);

                        if (!acesso_empresa)
                        {
                            return View("~/Views/Shared/_OutraEmpresa.cshtml");
                        }
                        ViewBag.pua_usuariocliente = new SelectList(db.cliente_empresa_usuario, "cus_codigo", "cus_usuario");
                        return View(perfil_usuario_acesso);
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_ler_perfilusuarioacesso_details))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        var perfil_usuario_acesso = await db.perfil_usuario_acesso.FindAsync(id);
                        if (perfil_usuario_acesso == null)
                        {
                            return HttpNotFound();
                        }
                        ViewBag.pua_usuariocliente = new SelectList(db.cliente_empresa_usuario, "cus_codigo", "cus_usuario");
                        return View(perfil_usuario_acesso); 
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


        // GET: PerfilUsuarioAcesso/Create - Mostrar a criação dos acessos do usuário.
        public ActionResult Create()
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_perfilusuarioacesso_create == true))
                    {
                        var empresa = (int)Session["Empresa"];
                        ViewBag.pua_usuariocliente = new SelectList(db.cliente_empresa_usuario.Where(x => x.cus_empresa == empresa), "cus_codigo", "cus_usuario");
                        return View();
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_perfilusuarioacesso_create))
                    {
                        ViewBag.pua_usuariocliente = new SelectList(db.cliente_empresa_usuario, "cus_codigo", "cus_usuario");
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


        // POST: PerfilUsuarioAcesso/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "pua_codigo,pua_lerescrever_clienteempresaunidade_create,pua_lerescrever_clienteempresaunidade_delete,pua_ler_clienteempresaunidade_details,pua_lerescrever_clienteempresaunidade_edit,pua_ler_clienteempresaunidade_index,pua_lerescrever_clienteempresausuario_create,pua_lerescrever_clienteempresausuario_delete,pua_ler_clienteempresausuario_details,pua_lerescrever_clienteempresausuario_edit,pua_ler_clienteempresausuario_index,pua_lerescrever_clienteempresausuario_mudarsenha,pua_lerescrever_perfilusuarioempresa_create,pua_lerescrever_perfilusuarioempresa_delete,pua_ler_perfilusuarioempresa_details,pua_lerescrever_perfilusuarioempresa_edit,pua_ler_perfilusuarioempresa_index,pua_lerescrever_perfilusuarioacesso_create,pua_lerescrever_perfilusuarioacesso_delete,pua_ler_perfilusuarioacesso_details,pua_lerescrever_perfilusuarioacesso_edit,pua_ler_perfilusuarioacesso_index,pua_usuariocliente")] perfil_usuario_acesso perfil_usuario_acesso) // pua_lerescrever_clienteempresa_create,pua_lerescrever_clienteempresa_delete,pua_ler_clienteempresa_details,pua_lerescrever_clienteempresa_edit,pua_ler_clienteempresa_index
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_perfilusuarioacesso_create == true))
                    {
                        if (ModelState.IsValid)
                        {
                            db.perfil_usuario_acesso.Add(perfil_usuario_acesso);
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }

                        ViewBag.pua_usuariocliente = new SelectList(db.cliente_empresa_usuario, "cus_codigo", "cus_usuario", perfil_usuario_acesso.pua_usuariocliente);
                        return View(perfil_usuario_acesso);
                    }

                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_perfilusuarioacesso_create))
                    {
                        if (ModelState.IsValid)
                        {
                            db.perfil_usuario_acesso.Add(perfil_usuario_acesso);
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }

                        ViewBag.pua_usuariocliente = new SelectList(db.cliente_empresa_usuario, "cus_codigo", "cus_usuario", perfil_usuario_acesso.pua_usuariocliente);
                        return View(perfil_usuario_acesso); 
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


        // GET: PerfilUsuarioAcesso/Edit/5 - Mostrar a página de edição de acesso do usuário selecioinado. A numeração no comentário
        // serve para mostrar qual o índice (o ID do ítem) no banco.
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    //var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));
                    var user_id = (int)Session["UserId"];
                    var acesso_perfil = db.perfil_usuario_acesso.AsNoTracking().SingleOrDefault(x => x.pua_usuariocliente == user_id).pua_lerescrever_perfilusuarioacesso_edit;

                    if (acesso_perfil)
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        perfil_usuario_acesso perfil_usuario_acesso = await db.perfil_usuario_acesso.FindAsync(id);
                        if (perfil_usuario_acesso == null)
                        {
                            return HttpNotFound();
                        }

                        var empresa = (int)Session["Empresa"];
                        var acesso_empresa = perfil_usuario_acesso.cliente_empresa_usuario.cus_empresa.Equals(empresa);

                        if (!acesso_empresa)
                        {
                            return View("~/Views/Shared/_OutraEmpresa.cshtml");
                        }

                        ViewBag.pua_usuariocliente = new SelectList(db.cliente_empresa_usuario, "cus_codigo", "cus_usuario", perfil_usuario_acesso.pua_usuariocliente);
                        return View(perfil_usuario_acesso);
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_perfilusuarioacesso_edit))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        perfil_usuario_acesso perfil_usuario_acesso = await db.perfil_usuario_acesso.FindAsync(id);
                        if (perfil_usuario_acesso == null)
                        {
                            return HttpNotFound();
                        }

                        ViewBag.pua_usuariocliente = new SelectList(db.cliente_empresa_usuario, "cus_codigo", "cus_usuario", perfil_usuario_acesso.pua_usuariocliente);
                        return View(perfil_usuario_acesso); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }


        // POST: PerfilUsuarioAcesso/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "pua_codigo,pua_lerescrever_clienteempresaunidade_create,pua_lerescrever_clienteempresaunidade_delete,pua_ler_clienteempresaunidade_details,pua_lerescrever_clienteempresaunidade_edit,pua_ler_clienteempresaunidade_index,pua_lerescrever_clienteempresausuario_create,pua_lerescrever_clienteempresausuario_delete,pua_ler_clienteempresausuario_details,pua_lerescrever_clienteempresausuario_edit,pua_ler_clienteempresausuario_index,pua_lerescrever_clienteempresausuario_mudarsenha,pua_lerescrever_perfilusuarioempresa_create,pua_lerescrever_perfilusuarioempresa_delete,pua_ler_perfilusuarioempresa_details,pua_lerescrever_perfilusuarioempresa_edit,pua_ler_perfilusuarioempresa_index,pua_lerescrever_perfilusuarioacesso_create,pua_lerescrever_perfilusuarioacesso_delete,pua_ler_perfilusuarioacesso_details,pua_lerescrever_perfilusuarioacesso_edit,pua_ler_perfilusuarioacesso_index,pua_usuariocliente")] perfil_usuario_acesso perfil_usuario_acesso) // pua_lerescrever_clienteempresa_create,pua_lerescrever_clienteempresa_delete,pua_ler_clienteempresa_details,pua_lerescrever_clienteempresa_edit,pua_ler_clienteempresa_index
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    //var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    // Traz o valor de pua_lerescrever_usuarioacesso_edit sem trazer o objeto inteiro
                    var user_id = (int)Session["UserId"];
                    var acesso_perfil = db.perfil_usuario_acesso.AsNoTracking().SingleOrDefault(x => x.pua_usuariocliente == user_id).pua_lerescrever_perfilusuarioacesso_edit;

                    if (acesso_perfil)
                    {
                        if (ModelState.IsValid)
                        {
                            db.Entry(perfil_usuario_acesso).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }

                        ViewBag.pua_usuariocliente = new SelectList(db.cliente_empresa_usuario, "cus_codigo", "cus_usuario", perfil_usuario_acesso.pua_usuariocliente);
                        return View(perfil_usuario_acesso);
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_perfilusuarioacesso_edit))
                    {
                        if (ModelState.IsValid)
                        {
                            db.Entry(perfil_usuario_acesso).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }

                        ViewBag.pua_usuariocliente = new SelectList(db.cliente_empresa_usuario, "cus_codigo", "cus_usuario", perfil_usuario_acesso.pua_usuariocliente);
                        return View(perfil_usuario_acesso); 
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


        // GET: PerfilUsuarioAcesso/Delete/5 - Mostrar a página de exclusão dos acessos do usuário selecionado. A numeração no 
        // comentário serve para mostrar qual o índice (o ID do ítem) no banco.
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_perfilusuarioacesso_delete == true))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        perfil_usuario_acesso perfil_usuario_acesso = await db.perfil_usuario_acesso.FindAsync(id);
                        if (perfil_usuario_acesso == null)
                        {
                            return HttpNotFound();
                        }

                        var empresa = (int)Session["Empresa"];
                        var acesso_empresa = perfil_usuario_acesso.cliente_empresa_usuario.cus_empresa.Equals(empresa);
                        
                        if (!acesso_empresa)
                        {
                            return View("~/Views/Shared/_OutraEmpresa.cshtml");
                        }
                        ViewBag.pua_usuariocliente = new SelectList(db.cliente_empresa_usuario, "cus_codigo", "cus_usuario");
                        return View(perfil_usuario_acesso);
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_perfilusuarioacesso_delete))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        perfil_usuario_acesso perfil_usuario_acesso = await db.perfil_usuario_acesso.FindAsync(id);
                        if (perfil_usuario_acesso == null)
                        {
                            return HttpNotFound();
                        }
                        ViewBag.pua_usuariocliente = new SelectList(db.cliente_empresa_usuario, "cus_codigo", "cus_usuario");
                        return View(perfil_usuario_acesso); 
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }


        // POST: PerfilUsuarioAcesso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    var acesso_perfil = new List<perfil_usuario_acesso>(db.perfil_usuario_acesso.ToList().Where(x => x.pua_usuariocliente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pua_lerescrever_perfilusuarioacesso_delete == true))
                    {
                        perfil_usuario_acesso perfil_usuario_acesso = await db.perfil_usuario_acesso.FindAsync(id);
                        db.perfil_usuario_acesso.Remove(perfil_usuario_acesso);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
                else
                {
                    var acesso_perfil_gerente = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil_gerente != null && acesso_perfil_gerente.Any(x => x.pus_lerescrever_perfilusuarioacesso_delete))
                    {
                        perfil_usuario_acesso perfil_usuario_acesso = await db.perfil_usuario_acesso.FindAsync(id);
                        db.perfil_usuario_acesso.Remove(perfil_usuario_acesso);
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
