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
        public int Important { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public int UserID { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
    }
}