using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetoDis.Models;

namespace ProjetoDis.ProjectClasses.Iterator
{
    public class Warning
    {
        public Object _warning;

        public Warning (Object warning)
        {
            this._warning = warning;
        }
    }

    public interface IWarningCollection
    {
        IWarningIterator CreateIterator();
    }

    public class WarningCollection : IWarningCollection
    {
        private ArrayList _warnings = new ArrayList();
        public IWarningIterator CreateIterator()
        {
            return new WarningIterator(this);
        }

        public int Count
        {
            get { return _warnings.Count; }
        }
        public object this[int index]
        {
            get { return _warnings[index]; }
            set { _warnings.Add(value); }
        }
    }

    public interface IWarningIterator
    {
        Warning First();
        Warning Next();
        bool IsFinished { get; }
        Warning CurrentWarning { get; }
    }

    public class WarningIterator : IWarningIterator
    {
        private WarningCollection _warnings;
        private int _current = 0;
        //private int _step = 1;

        public WarningIterator(WarningCollection warnings)
        {
            this._warnings = warnings;
        }
        public Warning First()
        {
            _current = 0;
            return _warnings[_current] as Warning;
        }

        public Warning Next()
        {
            _current++;
            if (!IsFinished)
            {
                return _warnings[_current] as Warning;
            }
            else
                return null;
        }

        public bool IsFinished
        {
            get { return _current >= _warnings.Count ; }
        }
        
        public Warning CurrentWarning
        {
            get { return _warnings[_current] as Warning; }
        }
    }
}