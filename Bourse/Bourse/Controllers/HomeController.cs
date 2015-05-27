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

        [HttpGet]
        public ActionResult Subscribe()
        {
            return View(new UsersModel());
        }
        [HttpPost]

        public ActionResult Subscribe(UsersModel newUser)
        {
            UsersModel users = new UsersModel(Session["MainDB"]);
            if (!String.IsNullOrEmpty(newUser.UserName))
            {
                if (!users.Exist(newUser.UserName))
                {
                    if (!String.IsNullOrEmpty(newUser.Password))
                    {
                        users.UserName = newUser.UserName;
                        users.Password = newUser.Password;
                        users.FullName = newUser.FullName;
                        users.EMail = newUser.EMail;
                        users.CarteCredit = newUser.CarteCredit;
                        users.Solde = double.Parse(newUser.Solde.ToString());

                        users.Insert();
                        return RedirectToAction("Index", "Home"); ;
                    }
                    else
                    {
                        TempData["Notice"] = "Le mot de passe est vide...";
                    }
                }
                else
                {
                    TempData["Notice"] = "Cet usager existe déjà...";
                }
            }
            return View(newUser);
        }

        [HttpGet]
        public ActionResult Profil()
        {
            if ((bool)Session["UserValid"])
            {
                UsersModel users = new UsersModel(Session["MainDB"]);
                users.SelectByID(Session["UserId"].ToString());
                users.Next();
                users.EndQuerySQL();
                return View(users);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Profil(UsersModel users)
        {
            // users est une nouvelle instance peuplée par le formulaire
            UsersModel updatedUser = new UsersModel(Session["MainDB"]);
            updatedUser.SelectByID(Session["UserId"].ToString());
            updatedUser.EndQuerySQL();

            updatedUser.UserName = users.UserName;
            updatedUser.FullName = users.FullName;
            updatedUser.Password = users.Password;
            updatedUser.EMail = users.EMail;
            updatedUser.CarteCredit = users.CarteCredit;
            if (users.Solde > 0)
                updatedUser.Solde = users.Solde + updatedUser.Solde;

            updatedUser.Update();
            Session["FullName"] = updatedUser.FullName;
            Session["Solde"] = updatedUser.Solde;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Achat(string id)
        {
            BourseModel bourse = new BourseModel(Session["MainDB"]);
            if (string.IsNullOrWhiteSpace(id) || !bourse.Exist(id))
                return View("Error");
            else
            {
                ViewData["id"] = id;
                YahooFinance yf = new YahooFinance();
                ViewData["Prix"] = yf.GetStockPriceFromSymbol(id);
                return View();
            }

        }
    }
}