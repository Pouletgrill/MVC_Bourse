using Bourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bourse.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(String UserName, String Password)
        {
            UsersModel users = new UsersModel(Session["MainDB"]);
            if (users.Exist(UserName))
            {
                if (users.Password == Password)
                {
                    TempData["Notice"] = "Vous êtes maintenant connecté...";
                    Session["UserValid"] = true;
                    Session["UserId"] = users.ID;
                    Session["FullName"] = users.FullName;
                    Session["Solde"] = users.Solde;
                    return RedirectToAction("Marche", "Home");
                }
                else
                {
                    TempData["Notice"] = "Mot de passe incorrect...";
                }
            }
            else
                TempData["Notice"] = "Cet usager n'existe pas...";
            return View();
        }

        public ActionResult Marche()
        {
            return View();
        }

        public ActionResult PartielMarche()
        {
            BourseModel bourse = new BourseModel(Session["MainDB"]); 
            return PartialView(bourse);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Subscribe()
        {
            ViewBag.Message = "Your Subscribe page.";

            return View();
        }
    }
}