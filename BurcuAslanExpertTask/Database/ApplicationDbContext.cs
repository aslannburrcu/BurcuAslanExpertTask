using BurcuAslanExpertTask.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BurcuAslanExpertTask.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnetion")
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Urunler> Urunlers { get; set; }
     
        public DbSet<Kategori> Kategoris { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}