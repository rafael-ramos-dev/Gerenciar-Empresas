using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ProjetoTesteRafa.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            if (Session["Empresa"] != null || Session["UserId"] != null)
            {
                return View(); 
            }
            else
            {
                return RedirectToAction("Login", "Manager");
            }
        }

        
    }
}