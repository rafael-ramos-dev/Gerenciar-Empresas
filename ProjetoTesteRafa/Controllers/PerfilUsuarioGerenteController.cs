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
    public class PerfilUsuariogerenteController : Controller
    {
        private DataContext db = new DataContext();

        // GET: PerfilUsuariogerente
        public async Task<ActionResult> Index()
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    return View("~/Views/Shared/_OutraEmpresa.cshtml");
                }
                else
                {
                    var acesso_perfil = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pus_ler_perfilusuariogerente_index))
                    {
                        var perfis_usuario = db.perfis_usuario.Include(p => p.usuario_gerente).OrderBy(x => x.usuario_gerente.uss_nome);
                        return View(await perfis_usuario.ToListAsync());
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }

        // GET: PerfilUsuariogerente/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    return View("~/Views/Shared/_OutraEmpresa.cshtml");
                }
                else
                {
                    var acesso_perfil = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pus_ler_perfilusuariogerente_details))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        perfis_usuario perfis_usuario = await db.perfis_usuario.FindAsync(id);
                        if (perfis_usuario == null)
                        {
                            return HttpNotFound();
                        }
                        ViewBag.pus_usuariogerente = new SelectList(db.usuario_gerente, "uss_codigo", "uss_usuario");
                        return View(perfis_usuario);
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }


        // GET: PerfilUsuariogerente/Create
        public ActionResult Create()
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    return View("~/Views/Shared/_OutraEmpresa.cshtml");
                }
                else
                {
                    var user_id = (int)Session["UserId"];
                    var acesso_perfil = db.perfis_usuario.AsNoTracking().SingleOrDefault(x => x.pus_usuariogerente == user_id).pus_lerescrever_usuariogerente_create;

                    if (acesso_perfil)
                    {
                        ViewBag.pus_usuariogerente = new SelectList(db.usuario_gerente, "uss_codigo", "uss_usuario");
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

        // POST: PerfilUsuariogerente/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "pus_codigo,pus_lerescrever_usuariogerente_create,pus_lerescrever_usuariogerente_delete,pus_ler_usuariogerente_details,pus_lerescrever_usuariogerente_edit,pus_ler_usuariogerente_index,pus_lerescrever_usuariogerente_mudarsenha,pus_lerescrever_clienteempresa_create,pus_lerescrever_clienteempresa_delete,pus_ler_clienteempresa_details,pus_lerescrever_clienteempresa_edit,pus_ler_clienteempresa_index,pus_lerescrever_clienteempresaunidade_create,pus_lerescrever_clienteempresaunidade_delete,pus_ler_clienteempresaunidade_details,pus_lerescrever_clienteempresaunidade_edit,pus_ler_clienteempresaunidade_index,pus_lerescrever_clienteempresausuario_create,pus_lerescrever_clienteempresausuario_delete,pus_ler_clienteempresausuario_details,pus_lerescrever_clienteempresausuario_edit,pus_ler_clienteempresausuario_index,pus_lerescrever_clienteempresausuario_mudarsenha,pus_lerescrever_perfilusuarioempresa_create,pus_lerescrever_perfilusuarioempresa_delete,pus_ler_perfilusuarioempresa_details,pus_lerescrever_perfilusuarioempresa_edit,pus_ler_perfilusuarioempresa_index,pus_lerescrever_perfilusuarioacesso_create,pus_lerescrever_perfilusuarioacesso_delete,pus_ler_perfilusuarioacesso_details,pus_lerescrever_perfilusuarioacesso_edit,pus_ler_perfilusuarioacesso_index,pus_lerescrever_perfilusuariogerente_create,pus_lerescrever_perfilusuariogerente_delete,pus_ler_perfilusuariogerente_details,pus_lerescrever_perfilusuariogerente_edit,pus_ler_perfilusuariogerente_index,pus_usuariogerente")] perfis_usuario perfis_usuario)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    return View("~/Views/Shared/_OutraEmpresa.cshtml");
                }
                else
                {
                    //var acesso_perfil = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));
                    var user_id = (int)Session["UserId"];
                    var acesso_perfil = db.perfis_usuario.AsNoTracking().SingleOrDefault(x => x.pus_usuariogerente == user_id).pus_lerescrever_usuariogerente_create;

                    if (acesso_perfil)
                    {
                        if (ModelState.IsValid)
                        {
                            db.perfis_usuario.Add(perfis_usuario);
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }

                        ViewBag.pus_usuariogerente = new SelectList(db.usuario_gerente, "uss_codigo", "uss_usuario", perfis_usuario.pus_usuariogerente);
                        return View(perfis_usuario);
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }

        // GET: PerfilUsuariogerente/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    return View("~/Views/Shared/_OutraEmpresa.cshtml");
                }
                else
                {
                    //var acesso_perfil = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));
                    var user_id = (int)Session["UserId"];
                    var acesso_perfil = db.perfis_usuario.AsNoTracking().SingleOrDefault(x => x.pus_usuariogerente == user_id).pus_lerescrever_usuariogerente_edit;

                    if (acesso_perfil)
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        perfis_usuario perfis_usuario = await db.perfis_usuario.FindAsync(id);
                        if (perfis_usuario == null)
                        {
                            return HttpNotFound();
                        }
                        ViewBag.pus_usuariogerente = new SelectList(db.usuario_gerente, "uss_codigo", "uss_usuario", perfis_usuario.pus_usuariogerente);
                        return View(perfis_usuario);
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }

        // POST: PerfilUsuariogerente/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "pus_codigo,pus_lerescrever_usuariogerente_create,pus_lerescrever_usuariogerente_delete,pus_ler_usuariogerente_details,pus_lerescrever_usuariogerente_edit,pus_ler_usuariogerente_index,pus_lerescrever_usuariogerente_mudarsenha,pus_lerescrever_clienteempresa_create,pus_lerescrever_clienteempresa_delete,pus_ler_clienteempresa_details,pus_lerescrever_clienteempresa_edit,pus_ler_clienteempresa_index,pus_lerescrever_clienteempresaunidade_create,pus_lerescrever_clienteempresaunidade_delete,pus_ler_clienteempresaunidade_details,pus_lerescrever_clienteempresaunidade_edit,pus_ler_clienteempresaunidade_index,pus_lerescrever_clienteempresausuario_create,pus_lerescrever_clienteempresausuario_delete,pus_ler_clienteempresausuario_details,pus_lerescrever_clienteempresausuario_edit,pus_ler_clienteempresausuario_index,pus_lerescrever_clienteempresausuario_mudarsenha,pus_lerescrever_perfilusuarioempresa_create,pus_lerescrever_perfilusuarioempresa_delete,pus_ler_perfilusuarioempresa_details,pus_lerescrever_perfilusuarioempresa_edit,pus_ler_perfilusuarioempresa_index,pus_lerescrever_perfilusuarioacesso_create,pus_lerescrever_perfilusuarioacesso_delete,pus_ler_perfilusuarioacesso_details,pus_lerescrever_perfilusuarioacesso_edit,pus_ler_perfilusuarioacesso_index,pus_lerescrever_perfilusuariogerente_create,pus_lerescrever_perfilusuariogerente_delete,pus_ler_perfilusuariogerente_details,pus_lerescrever_perfilusuariogerente_edit,pus_ler_perfilusuariogerente_index,pus_usuariogerente")] perfis_usuario perfis_usuario)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    return View("~/Views/Shared/_OutraEmpresa.cshtml");
                }
                else
                {
                    //var acesso_perfil = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));
                    var user_id = (int)Session["UserId"];
                    var acesso_perfil = db.perfis_usuario.AsNoTracking().SingleOrDefault(x => x.pus_usuariogerente == user_id).pus_lerescrever_usuariogerente_edit;

                    if (acesso_perfil)
                    {
                        if (ModelState.IsValid)
                        {
                            db.Entry(perfis_usuario).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        ViewBag.pus_usuariogerente = new SelectList(db.usuario_gerente, "uss_codigo", "uss_usuario", perfis_usuario.pus_usuariogerente);
                        return View(perfis_usuario);
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }

        // GET: PerfilUsuariogerente/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    return View("~/Views/Shared/_OutraEmpresa.cshtml");
                }
                else
                {
                    var acesso_perfil = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pus_lerescrever_usuariogerente_delete))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        perfis_usuario perfis_usuario = await db.perfis_usuario.FindAsync(id);
                        if (perfis_usuario == null)
                        {
                            return HttpNotFound();
                        }
                        ViewBag.pus_usuariogerente = new SelectList(db.usuario_gerente, "uss_codigo", "uss_usuario");
                        return View(perfis_usuario);
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }

        // POST: PerfilUsuariogerente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    return View("~/Views/Shared/_OutraEmpresa.cshtml");
                }
                else
                {
                    var acesso_perfil = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pus_lerescrever_usuariogerente_delete))
                    {
                        perfis_usuario perfis_usuario = await db.perfis_usuario.FindAsync(id);
                        db.perfis_usuario.Remove(perfis_usuario);
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
