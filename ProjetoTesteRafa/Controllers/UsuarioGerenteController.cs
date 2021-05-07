using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using ProjetoTesteRafa.Models;
using ProjetoTesteRafa.Models.Comum;

namespace ProjetoTesteRafa.Controllers
{
    public class UsuarioGerenteController : Controller
    {
        private DataContext db = new DataContext();


        /*****************************************
         *                                       *
         *                INDEX                  *
         *                                       *
         *****************************************/


        // GET: Usuariogerente - Página Index onde serão mostrados os usuários
        [Authorize]
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

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pus_ler_usuariogerente_index))
                    {
                        var usuario_gerente = db.usuario_gerente.Include(u => u.perfis_usuario).OrderBy(x => x.uss_nome);
                        return View(await usuario_gerente.ToListAsync());
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


        // GET: Usuariogerente/Details/5 - Mostrar os detalhes do usuário selecionado. A numeração no comentário serve para mostrar qual
        //  o índice (o ID do ítem) no banco.
        [Authorize]
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

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pus_ler_usuariogerente_details))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        usuario_gerente usuario_gerente = await db.usuario_gerente.FindAsync(id);
                        if (usuario_gerente == null)
                        {
                            return HttpNotFound();
                        }

                        return View(usuario_gerente);
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


        // GET: Usuariogerente/Create - Mostrar a criação do usuário.
        [Authorize]
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
                    var acesso_perfil = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pus_lerescrever_usuariogerente_create))
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


        // POST: Usuariogerente/Create - Cria o usuário no banco.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "uss_codigo,uss_usuario,uss_senha,uss_lembrar_me,uss_nome,uss_sobrenome,uss_endereco,uss_numero,uss_complemento,uss_bairro,uss_cidade,uss_estado,uss_cep,uss_telefone,uss_email,uss_ativo,uss_perfil")] usuario_gerente usuario_gerente, string estados)
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

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pus_lerescrever_usuariogerente_create))
                    {
                        if (ModelState.IsValid)
                        {
                            // checa no banco se o usuário já existe e impede a criação de um novo com o mesmo nome de usuário
                            var checar = db.usuario_gerente.FirstOrDefault(s => s.uss_usuario == usuario_gerente.uss_usuario);


                            if (checar == null)
                            {
                                usuario_gerente.uss_estado = estados;
                                usuario_gerente.uss_senha = ManagerController.CriarMD5(usuario_gerente.uss_senha);
                                usuario_gerente.uss_ativo = true;
                                db.usuario_gerente.Add(usuario_gerente);
                                await db.SaveChangesAsync();
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ViewBag.UsuarioErro = "Usuário já cadastrado";
                            }
                        }
                        ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                        return View(usuario_gerente);
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


        // GET: Usuariogerente/Edit/5 - Mostrar a página de edição do usuário selecionado. A numeração no comentário serve para mostrar qual
        //  o índice (o ID do ítem) no banco.
        [Authorize]
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
                    var acesso_perfil = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pus_lerescrever_usuariogerente_edit))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        usuario_gerente usuario_gerente = await db.usuario_gerente.FindAsync(id);
                        if (usuario_gerente == null)
                        {
                            return HttpNotFound();
                        }
                        ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                        return View(usuario_gerente);
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }


        // POST: Usuariogerente/Edit/5 - Salva no banco as edições efetuadas no usuário selecionado. A numeração no comentário serve para mostrar qual
        //  o índice (o ID do ítem) no banco.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit([Bind(Include = "uss_codigo,uss_usuario,uss_senha,uss_lembrar_me,uss_nome,uss_sobrenome,uss_endereco,uss_numero,uss_complemento,uss_bairro,uss_cidade,uss_estado,uss_cep,uss_telefone,uss_email,uss_ativo,uss_perfil")] usuario_gerente usuario_gerente, string estados)
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                if (Session["Empresa"].ToString() != "0")
                {
                    return View("~/Views/Shared/_OutraEmpresa.cshtml");
                }
                else
                {
                    var acesso_perfil = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_lerescrever_usuariogerente_edit));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pus_lerescrever_usuariogerente_edit))
                    {
                        if (ModelState.IsValid)
                        {
                            usuario_gerente.uss_estado = estados;
                            db.Entry(usuario_gerente).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        ViewBag.Estados = new SelectList(new Estados().ListaEstados(), "data_sigla_estado", "data_nome_estado");
                        return View(usuario_gerente);
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


        // GET: Usuariogerente/Delete/5 - Mostrar a página de exclusão do usuário selecionado. A numeração no comentário serve para mostrar qual
        // o índice (o ID do ítem) no banco.
        [Authorize]
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
                        usuario_gerente usuario_gerente = await db.usuario_gerente.FindAsync(id);
                        if (usuario_gerente == null)
                        {
                            return HttpNotFound();
                        }
                        return View(usuario_gerente);
                    }
                    return View("~/Views/Shared/_SemAutorizacao.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }


        // POST: Usuariogerente/Edit/5 - Exclui o usuário selecionado do banco. A numeração no comentário serve para mostrar qual
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
                    return View("~/Views/Shared/_OutraEmpresa.cshtml");
                }
                else
                {
                    var acesso_perfil = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pus_lerescrever_usuariogerente_delete))
                    {
                        usuario_gerente usuario_gerente = await db.usuario_gerente.FindAsync(id);
                        db.usuario_gerente.Remove(usuario_gerente);
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
         *              MUDAR SENHA              *
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
                    return View("~/Views/Shared/_OutraEmpresa.cshtml");
                }
                else
                {
                    var acesso_perfil = new List<perfis_usuario>(db.perfis_usuario.ToList().Where(x => x.pus_usuariogerente == (int)Session["UserId"]));

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pus_lerescrever_usuariogerente_mudarsenha))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        usuario_gerente usuario_gerente = await db.usuario_gerente.FindAsync(id);
                        if (usuario_gerente == null)
                        {
                            return HttpNotFound();
                        }
                        
                        return View(usuario_gerente);
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
        public async Task<ActionResult> MudarSenha([Bind(Include = "uss_codigo,uss_usuario,uss_senha,uss_lembrar_me,uss_nome,uss_sobrenome,uss_endereco,uss_numero,uss_complemento,uss_bairro,uss_cidade,uss_estado,uss_cep,uss_telefone,uss_email,uss_ativo,uss_perfil")] usuario_gerente usuario_gerente)
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

                    if (acesso_perfil != null && acesso_perfil.Any(x => x.pus_lerescrever_usuariogerente_mudarsenha))
                    {
                        if (ModelState.IsValid)
                        {
                            usuario_gerente.uss_senha = ManagerController.CriarMD5(usuario_gerente.uss_senha);
                            db.Entry(usuario_gerente).State = EntityState.Modified;
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        
                        return View(usuario_gerente);
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
