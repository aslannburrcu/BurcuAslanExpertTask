using BurcuAslanExpertTask.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BurcuAslanExpertTask.Models 
{
    public class Sepetlik
    {
        public Urunler urun { get; set; }
        public byte adet { get; set; }
    }
}