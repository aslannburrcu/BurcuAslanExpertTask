using BurcuAslanExpertTask.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BurcuAslanExpertTask.Models
{
    public class Sepet
    {
        private List<Sepetlik> _sepetim = new List<Sepetlik>();

        public List<Sepetlik> Sepetim { get => _sepetim; }

        public void Sepete_ekle(Urunler gelen_urun, byte adet)
        {

            var varolan_urun = _sepetim.FirstOrDefault(x => x.urun.UrunId == gelen_urun.UrunId);
            if (varolan_urun == null)
            {
                _sepetim.Add(new Sepetlik { urun = gelen_urun, adet = 1 });
            }
            else if (adet == 0) varolan_urun.adet += 1;
            else varolan_urun.adet = adet;
        }

        public void Sepetten_sil(Urunler silinecek_urun)
        {
            _sepetim.RemoveAll(x => x.urun.UrunId == silinecek_urun.UrunId);
        }

        public void Sepeti_temizle()
        {
            _sepetim.Clear();
        }
    }
}