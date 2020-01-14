using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjetoDis.Models
{
    public class CloseGovDb : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Alert> Alerts { get; set; }

        //public DbSet<RestaurantReview> Reviews { get; set; }

    }
}