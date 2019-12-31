using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoDis.ProjectClasses.Occurrences
{
    public class Garbage : Ocurrence
    {
        public override void SaveOccurrenceDb()
        {
            throw new NotImplementedException();
        }

        protected override void SendOccurrence()
        {
            throw new NotImplementedException();
        }
    }
}