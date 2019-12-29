using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoDis.Models
{
    public class Alert
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Important { get; set; }
        public string Description { get; set; }
        public DateTime date { get; set; }
        public string Location { get; set; }
    }
}