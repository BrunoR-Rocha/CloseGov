using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetoDis.Models;

namespace ProjetoDis.ProjectClasses.Iterator
{
    public class AlertIt
    {
        public Alert _alert;

        public AlertIt (Alert alert)
        {
            this._alert = alert;
        }
    }

    public interface IAlertCollection
    {
        IAlertIterator CreateIterator();
    }

    public class AlertCollection : IAlertCollection
    {
        private ArrayList _alerts = new ArrayList();
        public IAlertIterator CreateIterator()
        {
            return new AlertIterator(this);
        }

        public int Count
        {
            get { return _alerts.Count; }
        }
        public object this[int index]
        {
            get { return _alerts[index]; }
            set { _alerts.Add(value); }
        }
    }

    public interface IAlertIterator
    {
        AlertIt First();
        AlertIt Next();
        bool IsFinished { get; }
        AlertIt CurrentAlert { get; }
    }

    public class AlertIterator : IAlertIterator
    {
        private AlertCollection _alerts;
        private int _current = 0;
        //private int _step = 1;

        public AlertIterator(AlertCollection alerts)
        {
            this._alerts = alerts;
        }
        public AlertIt First()
        {
            _current = 0;
            return _alerts[_current] as AlertIt;
        }

        public AlertIt Next()
        {
            _current++;
            if (!IsFinished)
            {
                return _alerts[_current] as AlertIt;
            }
            else
                return null;
        }

        public bool IsFinished
        {
            get { return _current >= _alerts.Count ; }
        }
        
        public AlertIt CurrentAlert {
            get { return _alerts[_current] as AlertIt; }
        }
    }
}