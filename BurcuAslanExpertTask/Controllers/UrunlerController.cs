using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BurcuAslanExpertTask.Models;
using System.IO;
using PagedList;
using System.Threading.Tasks;
using BurcuAslanExpertTask.Entities;
using PagedList;
using System.Xml;

namespace BurcuAslanExpertTask.Controllers
{
    public class UrunlerController : Controller
    {
        DataContext _context = new DataContext();
        // GET: Urunler
        public ActionResult Index(int? sayfa)
        {
            var sayfa_no = sayfa ?? 1;
            var urunlerim = _context.Urunlers.ToList().ToPagedList(sayfa_no,10);
            return View(urunlerim);
        }
       
    }
}