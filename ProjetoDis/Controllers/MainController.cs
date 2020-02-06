using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetoDis.Models;
using ProjetoDis.ProjectClasses.Iterator;
using ProjetoDis.ProjectClasses.Observer;
using ProjetoDis.ProjectClasses.Proxy;
using ProjetoDis.ProjectClasses.Templates;

using System.Web.Script.Serialization;

namespace ProjetoDis.Controllers
{
    public class MainController : Controller
    {
        ProxyDB db = new ProxyDB();
        
        //se o utilizador for do tipo Common mostra o respetivo menu inicial
        public ActionResult Index()
        {
            WarningIterator alertIterator = AlertIterator();

            WarningIterator reportIterator = ReportIterator();

            ViewBag.alertIterator = alertIterator;

            ViewBag.reportIterator = reportIterator;

            Notifications();

            return View();
        }

        public WarningIterator AlertIterator()
        {
            WarningCollection alertCollection = new WarningCollection();

            Alert[] alerts = db.GetAlerts();

            for (int i = 0; i < alerts.Length; i++) 
            {
                alertCollection[i] = new Warning(alerts[i]);
            }

            WarningIterator alertIterator = alertCollection.CreateIterator() as WarningIterator;

            return alertIterator;
        }

        public WarningIterator ReportIterator()
        {
            WarningCollection reportCollection = new WarningCollection();

            Report[] reports = db.GetReports();

            for (int i = 0; i < reports.Length; i++) 
            {
                reportCollection[i] = new Warning(reports[i]);
            }

            WarningIterator reportIterator = reportCollection.CreateIterator() as WarningIterator;

            return reportIterator;
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

        public ActionResult Notifications()
        {
            if (Session["id"] != null)
            {
                int id = (int) Session["id"];

                Notification[] notifications = db.GetNotifications(note => note.User == id);

                List<Notification> listNotifications = new List<Notification>();

                foreach(Notification notification in notifications)
                {
                    listNotifications.Add(notification);

                }

                ViewBag.notifications = listNotifications;
            }

            return View();
        }

        public ActionResult Alert()
        {
            Notifications();
            return View();
        }       



        public string GetAlerts()
        {
            WarningIterator iterator = AlertIterator();

            List<Alert> alertsList = new List<Alert>();

            for (Warning warning = iterator.First(); !iterator.IsFinished; warning = iterator.Next())
            {
                alertsList.Add((Alert)warning._warning);
            }

            return new JavaScriptSerializer().Serialize(alertsList);
        }



        public string GetReports()
        {
            WarningIterator iterator = ReportIterator();

            List<Report> reportsList = new List<Report>();

            for (Warning warning = iterator.First(); !iterator.IsFinished; warning = iterator.Next())
            {
                reportsList.Add((Report)warning._warning);
            }

            return new JavaScriptSerializer().Serialize(reportsList);

        }


        public ActionResult Calendar()
        {
            Notifications();
            return View();
        }        
        public ActionResult Map()
        {
            Notifications();
            return View();
        }
        
        public ActionResult MapReport()
        {
            Notifications();
            return View();
        }
                
        public ActionResult Profile()
        {
            Notifications();
            return View();
        }

        [HttpPost]
        public ActionResult Alert(Alert al)
        {
            IDictionary<string, string> requestData = new Dictionary<string, string>();

            //valores do formulario de alerta

            requestData["title"] = Request["title"];
            requestData["description"] = Request["description"];
            requestData["date"] = Request["date"];
            requestData["local"] = Request["local"];
            requestData["address"] = Request["address"];
            requestData["perigo"] = Request["perigo"];
            requestData["user"] = "" + Session["id"];

            //talvez acrescentar um selector para um dos 3 tipos de ocorrencia
            AlertTemplate alertTemplate = AlertTemplate.Instance;


            if (alertTemplate.Warning_Template(requestData))
            {
                return Redirect("/Main");
            }
            else
            {
                return Redirect("/Main/Alert");
            }
        }

        public ActionResult Report()
        {
            Notifications();
            return View();
        }

        [HttpPost]
        public ActionResult Report(Report rep)
        {
            IDictionary<string, string> requestData = new Dictionary<string, string>();

            requestData["title"] = Request["title"];
            requestData["description"] = Request["description"];
            requestData["date"] = Request["date"];
            requestData["address"] = Request["address"];
            requestData["local"] = Request["local"];
            requestData["user"] = "" + Session["id"];

            ReportTemplate reportTemplate = ReportTemplate.Instance;

            if (reportTemplate.Warning_Template(requestData))
            {
                return Redirect("/Main");
            }
            else
            {
                return Redirect("/Main/Report");
            }
        }
    }
}