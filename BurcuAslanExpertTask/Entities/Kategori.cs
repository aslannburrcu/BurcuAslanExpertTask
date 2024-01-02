using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BurcuAslanExpertTask.Entities
{
    public class Kategori
    {
        public int KategoriId{ get; set; }
        public string KategoriAdi { get; set; }
        public ICollection<Urunler> Urunlers { get; set; }
    }
}