using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BurcuAslanExpertTask.Entities
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnetion")
        {

        }
        public DbSet<Urunler> Urunlers { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<User> Users { get; set; }
    }
}