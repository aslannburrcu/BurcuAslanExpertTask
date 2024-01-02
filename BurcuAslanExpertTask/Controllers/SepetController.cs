using BurcuAslanExpertTask.Entities;
using BurcuAslanExpertTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace BurcuAslanExpertTask.Controllers
{
    public class SepetController : Controller
    {
        DataContext _context = new DataContext();


        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
        public class LoginOrRegisterRequiredAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (filterContext.HttpContext.Session["username"] == null)
                {
                    // Kullanıcı girişi yapılmamışsa veya kayıt olmamışsa, Login sayfasına yönlendir
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Auth", action = "Login"}));
                }

                base.OnActionExecuting(filterContext);
               
            }
        }

        public Sepet Sepeti_getir()
        {
            var sepetimiz = (Sepet)Session["sepetimiz"];
            //session içerisinde oluşrurduğumuz sepetimiz nesnemizin adresi var
            if (sepetimiz == null)//1 kez çalışıyor
            {
                sepetimiz = new Sepet();
                Session["sepetimiz"] = sepetimiz;
            }
            return sepetimiz;
        }

        // GET: Sepet
        public ActionResult Index(string siparis_msj)
        {
            var loggedInUser = Session["username"];
            var userFullName = Session["fullName"];

            ViewBag.Username = loggedInUser;
            ViewBag.FullName = userFullName;
            //Session["username"] = loggedInUser.Username;
            ViewBag.siparis_msj = siparis_msj;
            return View(Sepeti_getir());
        }

        [LoginOrRegisterRequired]
        public ActionResult sepete_ekle(int? urunid, byte? adet)
        {
            var loggedInUser = Session["username"];
            var userFullName = Session["fullName"];

            ViewBag.Username = loggedInUser;
            ViewBag.FullName = userFullName;

            var _adet = adet ?? 0;
            var urun = _context.Urunlers.FirstOrDefault(x => x.UrunId == urunid);
            if(urun!= null)
            {
                Sepeti_getir().Sepete_ekle(urun, _adet);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("index");
        }

        public ActionResult sepetten_sil(int? urunid)
        {
            var urun = _context.Urunlers.FirstOrDefault(x => x.UrunId == urunid);
            if(urun!=null)
            {
                Sepeti_getir().Sepetten_sil(urun);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("index");
        }
    }
}