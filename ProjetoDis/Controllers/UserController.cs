using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetoDis.Models;

namespace ProjetoDis.Controllers
{
    public class UserController : Controller
    {

        CloseGovDb db = new CloseGovDb();

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

            var firstUser = db.Users.Where(user => user.Email == email).FirstOrDefault();

            if (check){
                return Redirect("/User/Login");
            }else if (firstUser != null)
            {
               var verify =  String.Compare(password,firstUser.Password);
               if (verify == 0)
               {
                   Session["id"] = firstUser.Id;
                   Session["name"] = firstUser.Name;

                   return Redirect("/Main");
               }else{
                   return Redirect("/User/Login");
               }
            }
            else
            {
                return Redirect("/User/Register");
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

            var check = String.Compare(name, "") == 0;
            check = check || String.Compare(email, "") == 0;
            check = check || String.Compare(nif, "") == 0;
            check = check || String.Compare(address, "") == 0;
            check = check || String.Compare(region, "") == 0;
            check = check || String.Compare(type, "") == 0;
            check = check || String.Compare(password, "") == 0;

            var isNumeric = int.TryParse(nif, out int number);
            var type_vallidate = int.TryParse(type, out int type_user);

            if (!isNumeric || check || !type_vallidate){
                return Redirect("/User/Register");
            }else if (String.Compare(password, pass2) == 0){ 
                
                User newUser = new User();
                newUser.Email = email;
                newUser.Name = name;
                newUser.Nif = number;
                newUser.Type = type_user;
                newUser.Password = password;
                newUser.Address = address;
                newUser.Region = region;
                db.Users.Add(newUser);

                db.SaveChanges();
                return Redirect("/User/Login");
            }else{
                return Redirect("/User/Register");
            }
        }
        public ActionResult Main()
        {
            return Content("Sessao: "+ Session["id"] +" "+ Session["name"]);
        }

        public ActionResult Logout()
        {
            return Redirect("/User/Login");
        }
    }
}