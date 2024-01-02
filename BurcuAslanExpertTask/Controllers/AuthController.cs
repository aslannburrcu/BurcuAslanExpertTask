using BurcuAslanExpertTask.Models;
using BurcuAslanExpertTask.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace BurcuAslanExpertTask.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService authService;

        public AuthController()
        {
            authService = new AuthService();
        }
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
            {
            if(ModelState.IsValid) // ModelState.IsValid, modelin geçerli olup olmadığını kontrol eder.
                                   // Kullanıcı tarafından girilen verilerin doğruluğunu kontrol eder.

            {
                var result = await authService.Authenticate(model);
                // authService, giriş işlemlerini yürüten bir AuthService sınıfıdır.
                // Authenticate metodu, kullanıcının giriş bilgilerini alır ve doğrular.

                if (result !=null)
                {
                    if(result.IsSuccessful)
                    {
                        var loggedInUser = JsonConvert.DeserializeObject<UserModel>(JsonConvert.SerializeObject(result.Data));// Giriş başarılı olduğunda, kullanıcı bilgilerini result.Data'dan alır.
                                                                                                                              // Bu bilgileri UserModel sınıfına dönüştürür.

                        Session["username"] = loggedInUser.Username;
                        Session["fullName"] = string.Format("{0} {1}", loggedInUser.FirstName, loggedInUser.LastName);// Kullanıcının tam adını oturum değişkenine atar.

                        return RedirectToAction("Index", "Urunler"); // Giriş başarılıysa, "Index" action'ına ve "Urunler" controller'ına yönlendirme yapar.

                    }
                    else
                    {
                        ViewBag.ErrorMessage = result.Message;// Hata mesajını ViewBag üzerinden view'e iletilir.
                    }
                }
            }

            return View();

            }

        public ActionResult LogOut()
        {
            Session["username"] = null;
            Session["fullName"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            var model = new RegisterModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model) //Bu metot, kullanıcının kayıt bilgilerini alır,
                                                                      //doğrular ve başarılı ise kullanıcıyı sistemde kaydeder. Sonuçları ekrana gösterir.
        {
            if (ModelState.IsValid)
            {
                var result = await authService.Register(model);

                if (result.IsSuccessful)
                {
                    ViewBag.Message = result.Message;
                    return RedirectToAction("Index", "Urunler");
                }
                else
                {
                    ViewBag.Message = result.Message;
                    return View(model);
                }
            }
            return View(model);
        }
    }
}