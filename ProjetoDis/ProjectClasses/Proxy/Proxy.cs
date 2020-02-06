using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetoDis.Models;

namespace ProjetoDis.ProjectClasses.Proxy
{
        public interface IDatabaseSubject
        {
            void AddAlert(Alert alert);
            void AddReport(Report report);
            void AddUser(User user);

            void AddNotification(Notification note);

            void RemoveAlert(Alert alert);
            void RemoveReport(Report report);

            //void RemoveUser(User user);

            Alert[] GetAlerts();
            Report[] GetReports();
            User[] GetUsers(Func<User, bool>predicate);
            Notification[] GetNotifications(Func<Notification, bool>predicate);
            User GetUser(string email);
            void UpdateAlert(int id, string value, string type);

    }

    public class RealDatabaseSubject : IDatabaseSubject
        {
            CloseGovDb db = new CloseGovDb();

            public void AddAlert(Alert alert)
            {
                db.Alerts.Add(alert);
                db.SaveChanges();
            }

            public void AddReport(Report report)
            {
                db.Reports.Add(report);
                db.SaveChanges();
            }

            public void AddUser(User user)
            {
                db.Users.Add(user);
                db.SaveChanges();
            }

            public void AddNotification(Notification note)
            {
                db.Notifications.Add(note);
                db.SaveChanges();
            }

            public void RemoveAlert(Alert alert)
            {
                db.Alerts.Remove(alert);
                db.SaveChanges();
            }

            public void RemoveReport(Report report)
            {
                db.Reports.Remove(report);
                db.SaveChanges();
            }

            public Alert[] GetAlerts()
            {
                return db.Alerts.ToArray();
            }

            public Report[] GetReports()
            {
                return db.Reports.ToArray();
            }

            public User[] GetUsers(Func<User, bool> predicate)
            {
                return db.Users.Where(predicate).ToArray();
            }

            public Notification[] GetNotifications(Func<Notification, bool> predicate)
            {
                return db.Notifications.Where(predicate).ToArray();
            }

            public User GetUser(string email)
            {
                return db.Users.Where(user => user.Email == email).FirstOrDefault();
            }


            public void UpdateAlert(int id, string value, string type)
            {
            if (type == "0")
            {
                db.Alerts.FirstOrDefault(x => x.Id == id).Status = value;
            }
            else
            {
                db.Alerts.FirstOrDefault(x => x.Id == id).Comment = value;
            }
                db.SaveChanges();
            }


        }

        public class ProxyDB : IDatabaseSubject
        {
            RealDatabaseSubject _db = new RealDatabaseSubject();

            public void AddAlert(Alert alert)
            {
                _db.AddAlert(alert);
            }

            public void AddReport(Report report)
            {
                _db.AddReport(report);
            }

            public void AddUser(User user)
            {
                _db.AddUser(user);
            }

            public void AddNotification(Notification note)
            {
                _db.AddNotification(note);
            }

            public void RemoveAlert(Alert alert)
            {
                _db.RemoveAlert(alert);
            }

            public void RemoveReport(Report report)
            {
                _db.RemoveReport(report);
            }

            public Alert[] GetAlerts()
            {
                return _db.GetAlerts();
            }

            public Report[] GetReports()
            {
                return _db.GetReports();
            }

            public User[] GetUsers(Func<User, bool> predicate)
            {
                return _db.GetUsers(predicate);
            }

            public Notification[] GetNotifications(Func<Notification, bool> predicate)
            {
                return _db.GetNotifications(predicate);
            }

            public User GetUser(string email)
            {
                return _db.GetUser(email);
            }

            public void UpdateAlert(int id, string value, string type)
            {
                _db.UpdateAlert(id, value, type);
            }

        }



}