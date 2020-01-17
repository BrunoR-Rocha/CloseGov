using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetoDis.Models;
using ProjetoDis.ProjectClasses.Iterator;
using ProjetoDis.ProjectClasses.Observer;
using ProjetoDis.ProjectClasses.Templates;

namespace ProjetoDis.Controllers
{
    public class MainController : Controller
    {
        CloseGovDb db = new CloseGovDb();
        
        //se o utilizador for do tipo Common mostra o respetivo menu inicial
        public ActionResult Index()
        {

            WarningIterator alertIterator = AlertIterator();

            WarningIterator reportIterator = ReportIterator();

            ViewBag.alertIterator = alertIterator;

            ViewBag.reportIterator = reportIterator;

            return View();
        }

        public WarningIterator AlertIterator()
        {
            WarningCollection alertCollection = new WarningCollection();

            Alert[] alerts = db.Alerts.ToArray();

            for (int i = 0; i < alerts.Length; i++) // array.Length = 2
            {
                alertCollection[i] = new Warning(alerts[i]);
            }

            WarningIterator alertIterator = alertCollection.CreateIterator() as WarningIterator;

            return alertIterator;
        }

        public WarningIterator ReportIterator()
        {
            WarningCollection reportCollection = new WarningCollection();

            Report[] reports = db.Reports.ToArray();

            for (int i = 0; i < reports.Length; i++) // array.Length = 2
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

                IQueryable<Notification> notifications = db.Notifications.Where(note => note.User == id);

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
            return View();
        }

        [HttpPost]
        public ActionResult Report(Report rep)
        {
            IDictionary<string, string> requestData = new Dictionary<string, string>();

            requestData["title"] = Request["title"];
            requestData["description"] = Request["description"];
            requestData["date"] = Request["date"];
            requestData["local"] = Request["local"];
            

            ReportTemplate reportTemplate = ReportTemplate.Instance;

            if (reportTemplate.Warning_Template(requestData))
            {
                return Redirect("/Main");
            }
            else
            {
                return Redirect("/Main/Report");
            }
            /*
            var check = String.Compare(title, "") == 0 || String.Compare(description, "") == 0;
            check = check || String.Compare(region, "") == 0 || String.Compare(date, "") == 0;

            var val_date = DateTime.TryParse(date, out DateTime data);
            

            //guardar na base de dados respetiva
            if (!val_date || check)
            {
                return Redirect("/Main/Report");
            }
            else
            {
                Subject subject = new Subject();

                IQueryable<User> query = db.Users.Where(user => user.Type == 1);

                foreach (User user in query)
                {
                    UserObserver observer = new UserObserver(user.Id);
                    subject.Attach(observer);
                }

                Report report = new Report();
                report.Title = title;
                report.Description = description;
                report.Location = region;
                report.Date = data;

                db.Reports.Add(report);
                db.SaveChanges();

                NotificationData notification = new NotificationData(report.Id, report.Title, report.Description, "Report");

                subject.Notify(notification);

            }*/
            //return Redirect("/Main");
        }
    }
}