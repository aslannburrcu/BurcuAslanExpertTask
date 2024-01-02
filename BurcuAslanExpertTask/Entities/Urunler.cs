using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BurcuAslanExpertTask.Entities
{
    public class Urunler
    {
        [Key]
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public string UrunMarka { get; set; }
        public string UrunKategori { get; set; }
        public int UrunStok { get; set; }
        public string Aciklama { get; set; }
        public Kategori Kategori { get; set; }
        

    }
}