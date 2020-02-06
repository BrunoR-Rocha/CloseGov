using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoDis.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int User { get; set; }
        public int? Alert { get; set; }
        public int? Report { get; set; }
        public int? Garbage { get; set; }
        public int? Vegetation { get; set; }
        public int? Road { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

    }
}