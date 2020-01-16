using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetoDis.Models;
using ProjetoDis.ProjectClasses.Iterator;
using ProjetoDis.ProjectClasses.Observer;

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

            if (!val_date || check || !num_danger)
            {
                return Redirect("/Main/Alert");
            }
            else
            {
                Subject subject = new Subject();

                IQueryable<User> query = db.Users.Where(user => user.Type == 2);

                foreach (User user in query)
                {
                    UserObserver observer = new UserObserver(user.Id);
                    subject.Attach(observer);
                }

                Alert alert = new Alert();
                alert.Title = title;
                alert.Description = description;
                alert.Location = region;
                alert.Date = data;
                alert.Important = dangerous;

                db.Alerts.Add(alert);
                db.SaveChanges();
                
                NotificationData notification = new NotificationData(alert.Id, alert.Title, alert.Description, "Alert");

                subject.Notify(notification);
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
            var title = Request["title"];
            var description = Request["description"];
            var date = Request["date"];
            var region = Request["local"];

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

            }
            return Redirect("/Main");
        }
    }
}