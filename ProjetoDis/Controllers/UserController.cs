using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetoDis.Models;
using ProjetoDis.ProjectClasses.Proxy;

namespace ProjetoDis.Controllers
{
    public class UserController : Controller
    {

        ProxyDB db = new ProxyDB();

        //metodos GET
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User u)
        {
            var email = Request["email"];       // ou talvez utilizar o nif - tmbm pode ser chave primaria
            var password = Request["Password"];

            var check = String.Compare(email, "") == 0;
            check = check || String.Compare(password, "") == 0;

            var firstUser = db.GetUser(email);

            if (check)
            {
                ViewBag.alerts = "missing fields";
                return View();
            }
            else if (firstUser != null)
            {
               var verify =  String.Compare(password,firstUser.Password);
               if (verify == 0)
               {
                   Session["id"] = firstUser.Id;
                   Session["name"] = firstUser.Name;
                   Session["type"] = firstUser.Type;

                   return Redirect("/Main");
               }
               else
               {
                   ViewBag.alerts = "wrong password";
                   return View();
                }
            }
            else
            {
                ViewBag.alerts = "wrong email";
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            var name = Request["name"];
            var email = Request["email"];
            var nif = Request["nif"];
            var address = Request["address"];
            var region = Request["region"];
            var type = Request["type"];
            var password = Request["password"];
            var pass2 = Request["confirmPassword"];

            var check = verifyData(name, email, nif, address, region, type, password);

            var isNumeric = int.TryParse(nif, out int number);
            var type_vallidate = int.TryParse(type, out int type_user);

            if (!isNumeric || check || !type_vallidate)
            {
                ViewBag.alerts = "missing fields";
                return View();
            }
            else if (String.Compare(password, pass2) == 0){ 
                
                User newUser = new User();
                newUser.Email = email;
                newUser.Name = name;
                newUser.Nif = number;
                newUser.Type = type_user;
                newUser.Password = password;
                newUser.Address = address;
                newUser.Region = region;

                db.AddUser(newUser);

                return Redirect("/User/Login");
            }
            else
            {
                ViewBag.alerts = "matched passwords";
                return View();
            }
        }

        public bool verifyData(string name, string email, string nif, string address, string region, string type, string password)
        {
            var check = String.Compare(name, "") == 0;
            check = check || String.Compare(email, "") == 0;
            check = check || String.Compare(nif, "") == 0;
            check = check || String.Compare(address, "") == 0;
            check = check || String.Compare(region, "") == 0;
            check = check || String.Compare(type, "") == 0;
            check = check || String.Compare(password, "") == 0;

            return check;
        }

        public ActionResult Main()
        {
            return Content("Sessao: "+ Session["id"] +" "+ Session["name"]);
        }

        public ActionResult Logout()
        {
            Session["id"] = null;
            Session["name"] = null;
            Session["type"] = null;

            return Redirect("/User/Login");
        }
    }
}