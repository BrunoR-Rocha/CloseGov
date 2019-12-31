using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetoDis.Models;

namespace ProjetoDis.Controllers
{
    public class MainController : Controller
    {
        //se o utilizador for do tipo Common mostra o respetivo menu inicial
        public ActionResult Index()
        {
            return View();
        }

        //se o utilizador autenticado for do tipo Employee mostra o respetivo menu inicial
        public ActionResult IndexEmployee()
        {
            return View();
        }

        //mostra aos utilizador os dados enviados, dando a possibilidade de efetuar alguma alteraçao
        public ActionResult Show()
        {
            return View();
        }

        public ActionResult Alert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Alert(Alert alert)
        {
            alert = new Alert();
            
            //valores do formulario de alerta

            var title = Request["title"];
            var description = Request["description"];
            var date = Request["date"];
            var region = Request["local"];
            var address = Request["address"];
            var danger = Request["perigo"];
            //talvez acrescentar um selector para um dos 3 tipos de ocorrencia

            var check = String.Compare(title, "") == 0;
            check = check || String.Compare(description, "") == 0 || String.Compare(region, "") == 0;
            check = check || String.Compare(date, "") == 0 || String.Compare(address, "") == 0;
            check = check || String.Compare(danger, "") == 0;

            var num_danger = int.TryParse(danger, out int dangerous);
            var val_date = DateTime.TryParse(date, out DateTime data);

            if (val_date || check || num_danger)
            {
                return Redirect("/Main/Alert");
            }
            else
            {
                //guardar na base de dados correta conforme o tipo de ocorrencia

            }
            return Redirect("/Main");
        }

        public ActionResult Report()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Report(Report rep)
        {
            rep = new Report();

            var title = Request["title"];
            var description = Request["description"];
            var date = Request["date"];
            var region = Request["local"];

            var check = String.Compare(title, "") == 0 || String.Compare(description, "") == 0;
            check = check || String.Compare(region, "") == 0 || String.Compare(date, "") == 0;

            var val_date = DateTime.TryParse(date, out DateTime data);

            //guardar na base de dados respetiva

            return Redirect("/Main");
        }
    }
}