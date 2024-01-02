using BurcuAslanExpertTask.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BurcuAslanExpertTask.Models;
using System.Xml;

namespace BurcuAslanExpertTask.Controllers
{
    public class HomeController : Controller
    {
        DataContext _context = new DataContext();
        public ActionResult Index()
        {
            var loggedInUser = Session["username"];
            var userFullName = Session["fullName"];

            ViewBag.Username = loggedInUser;
            ViewBag.FullName = userFullName;
            return View();
        }

        public ActionResult kategori_doldur()
        {
            var kategorilerim = _context.Kategoris.ToList();
            return PartialView(kategorilerim);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
        public class LoginOrRegisterRequiredAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (filterContext.HttpContext.Session["username"] == null)
                {
                    // Kullanıcı girişi yapılmamışsa veya kayıt olmamışsa, Login sayfasına yönlendir
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Auth", action = "Login" }));
                }
                else
                {
                    // Kullanıcı giriş yapmışsa, ViewBag'e USD bilgisini ekleyerek taşı
                    string link = "https://www.tcmb.gov.tr/kurlar/today.xml";
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(link);
                    string USD = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
                    filterContext.Controller.ViewBag.USD = USD;
                }

                base.OnActionExecuting(filterContext);
            }
        }


        [LoginOrRegisterRequired]
        public ActionResult Kurlar()
        {
            string link = "https://www.tcmb.gov.tr/kurlar/today.xml";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(link);
            string USD = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
            string EURO = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;
            string Sterlin = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='GBP']/BanknoteSelling").InnerXml;

            ViewBag.usd = USD;
            ViewBag.euro = EURO;
            ViewBag.sterlin = Sterlin;

            return View();
        }
    }
}