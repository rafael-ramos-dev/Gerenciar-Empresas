using ProjetoTesteRafa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjetoTesteRafa.Controllers
{
    public class ManagerController : Controller
    {
        private DataContext db = new DataContext();


        // Parte responsável pelo Login e Registro fora da aplicação

        /*****************************************
         *                                       *
         *                LOGIN                  *
         *                                       *
         *****************************************/

        //GET: Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        //POST: Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel login, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                usuario_gerente gerente_login = new usuario_gerente();
                cliente_empresa_usuario cliente_login = new cliente_empresa_usuario();
                //perfil_usuario_empresa perfil_usuario_empresa = new perfil_usuario_empresa();

                // Chama a função a criação de MD5 para que ambas as senhas (a que já existe no banco e a que está sendo inserida)
                // sejam as mesmas
                var validar_senha = CriarMD5(login.mng_senha);
                var checar_uss_usuario = db.usuario_gerente.Where(m => m.uss_usuario.Equals(login.mng_usuario) && m.uss_senha.Equals(validar_senha) && m.uss_ativo.Equals(true)).FirstOrDefault();


                if (checar_uss_usuario != null)
                {
                    FormsAuthentication.SetAuthCookie(login.mng_usuario, login.mng_lembrar_me);
                    Session["UserId"] = checar_uss_usuario.uss_codigo;
                    Session["UserName"] = checar_uss_usuario.uss_nome;
                    Session["Empresa"] = "0";
                    Session["NomeEmpresa"] = "gerente";


                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    var checar_cli_usuario = db.cliente_empresa_usuario.Where(m => m.cus_usuario.Equals(login.mng_usuario) && m.cus_senha.Equals(validar_senha) && m.cus_ativo.Equals(true)).FirstOrDefault();
                    if (checar_cli_usuario != null)
                    {
                        FormsAuthentication.SetAuthCookie(login.mng_usuario, login.mng_lembrar_me);
                        // Variáveis para a sessão
                        Session["UserId"] = checar_cli_usuario.cus_codigo;
                        Session["UserName"] = checar_cli_usuario.cus_nome;
                        Session["Empresa"] = checar_cli_usuario.cus_empresa;
                        Session["NomeEmpresa"] = checar_cli_usuario.cliente_empresa.cle_razaosocial_principal;

                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Usuário ou Senha Inválidos");
            }
            return View(login);
        }



        /*****************************************
         *                                       *
         *              REGISTRAR                *
         *                                       *
         *****************************************/


        // GET: Usuario_gerente/Registrar  - Mostrar Criação de Usuários (Create) para Login
        [AllowAnonymous]
        public ActionResult Registrar()
        {
            return View();
        }


        // POST: Usuario_gerente/Registrar - Criar no BD à partir das informações do Get Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Registrar([Bind(Include = "uss_codigo,uss_usuario,uss_senha,uss_lembrar_me,uss_perfil,uss_nome")] usuario_gerente usuario_gerente)
        {
            if (ModelState.IsValid)
            {
                //Checa se o usuário já existe no banco de dados para que não seja permitida a criação de dois logins iguais
                var checar = db.usuario_gerente.FirstOrDefault(s => s.uss_usuario == usuario_gerente.uss_usuario);
                if (checar == null)
                {

                    //perfis_usuario perfis_usuario = new perfis_usuario();
                    //perfis_usuario.pus_lerescrever_clienteempresaunidade_create = true;
                    //perfis_usuario.pus_lerescrever_clienteempresaunidade_delete = true;
                    //perfis_usuario.pus_lerescrever_clienteempresaunidade_edit = true;
                    //perfis_usuario.pus_lerescrever_clienteempresausuario_create = true;
                    //perfis_usuario.pus
                    //cliente_empresa cliente_empresa = await db.cliente_empresa.FindAsync(empresa);
                    //var usuario_ativo = cliente_empresa.cle_quantidade_usuario_ativo;
                    //usuario_ativo++;
                    //cliente_empresa.cle_quantidade_usuario_ativo = usuario_ativo;
                    //db.Entry(cliente_empresa).State = EntityState.Modified;




                    //usuario_gerente.uss_perfil = 2;
                    usuario_gerente.uss_senha = CriarMD5(usuario_gerente.uss_senha);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    usuario_gerente.uss_ativo = true;
                    db.usuario_gerente.Add(usuario_gerente);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Usuário Já Existe";
                    return View();
                }
            }

            return View();
        }



        /*****************************************
         *                                       *
         *                LOGOFF                 *
         *                                       *
         *****************************************/


        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            // Previne que o usuário ao utilizar o botão de voltar do navegador, não seja exibida a página anterior do cache
            // e tenha que logar novamente. Limpa a sessão do último usuário
            Session.Clear();
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ProjetoTesteRafa_SessionId", ""));
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            //Response.Cache.SetNoStore();
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Manager");
        }



        /*****************************************
         *                                       *
         *          CRIPTOGRAFIA  MD5            *
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

    }
}
