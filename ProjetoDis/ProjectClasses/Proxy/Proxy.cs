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

            void RemoveAlert(Alert alert);
            void RemoveReport(Report report);

            //void RemoveUser(User user);

            Alert[] GetAlerts();
            Report[] GetReports();
            User[] GetUsers(Func<User, bool>predicate);
            Notification[] GetNotifications(Func<Notification, bool>predicate);
            User GetUser(string email);
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
        }


    
}