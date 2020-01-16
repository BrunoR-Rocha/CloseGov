using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetoDis.Models;

namespace ProjetoDis.ProjectClasses.Observer
{
    public interface IObserver
    {
        void Update(ISubject subject, NotificationData data);

    }

    public interface ISubject
    { 
        //adicionar um observer 
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(NotificationData data);
    }

    public class Subject : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();


        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(NotificationData data)
        {
            foreach (var observer in _observers)
            {
                observer.Update(this, data);
            }
        }
    }

    public class NotificationData
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string type { get; set; }

        public NotificationData(int id, string title, string description, string type)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.type = type;
        }
    }
    public class UserObserver : IObserver
    {
        private int employeeId;

        public UserObserver(int EmployeeId)
        {
            this.employeeId = EmployeeId;
        }

        public void Update(ISubject subject, NotificationData data)
        {
            CloseGovDb db = new CloseGovDb();

            Notification note = new Notification();
            switch (data.type)
            {
                case "Alert":
                    note.Alert = data.id;
                    break;
                case "Report":
                    note.Report = data.id;
                    break;
                default:
                    break;
            }

            note.Description = data.description;
            note.Title = data.title;
            note.User = employeeId;

            db.Notifications.Add(note);
            db.SaveChanges();

        }
    }

    public class CommonObserver : IObserver
    {
        public void Update(ISubject subject, NotificationData data)
        {
            //por decidir o que vai acontecer
        }
    }


}