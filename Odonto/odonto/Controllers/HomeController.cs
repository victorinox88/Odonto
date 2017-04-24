using odonto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace odonto.Controllers
{
    public class HomeController : BaseController
    {

        private NEOODONTOEntities db = new NEOODONTOEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            DOCTOR doc = db.DOCTORs.SqlQuery("select * from DOCTOR where LOGIN ='" + username + "' and PASSWORD = '" + password+"'").SingleOrDefault();
            if (doc != null)
            {
                List<int> bolas = new List<int>();
                bolas.Count();
                HttpCookie cookie = new HttpCookie("usuario");
                cookie["id"] = doc.ID.ToString();
                cookie["nombre"] = doc.NOMBRE;
                cookie["apellido"] = doc.APELLIDO;
                cookie["rol"] = doc.ID_ROL.ToString();
                cookie["calendario"] = doc.NOMBRE;
                Response.Cookies.Remove("usuario");
                Response.Cookies.Add(cookie);

                return RedirectToAction("Login", "Home");
            }
            else
            {
                SECRETARIA sec = db.SECRETARIAs.SqlQuery("select * from SECRETARIA where LOGIN ='" + username + "' and PASSWORD = '" + password + "'").SingleOrDefault();
                if (sec != null)
                {
                    List<CITA> cit = db.CITAs.SqlQuery("select * from CITA").ToList<CITA>();
                    string nombre = (cit.Count != 0 ? "Luisa2" : "Luisa");
                    HttpCookie cookie = new HttpCookie("usuario");
                    cookie["id"] = "all";
                    cookie["nombre"] = sec.NOMBRE;
                    cookie["apellido"] = sec.APELLIDO;
                    cookie["rol"] = sec.ID_ROL.ToString();
                    cookie["calendario"] = nombre;
                    Response.Cookies.Remove("usuario");
                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Login", "Home");
                }

                ViewBag.message = "El usuario o contraseña no son incorrectos";
                return View();
            }

        }

        public ActionResult CloseSession()
        {
            //bool validation = this.operationCookie.DeleteCookie(this.cookieName, Response);
            //Session.Clear();
            return RedirectToAction("index", "Home");
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

        public ActionResult Login()
        {
            ViewBag.nombre = cookieName.Values["nombre"];
            ViewBag.apellido = cookieName.Values["apellido"];
            ViewBag.rol = cookieName.Values["rol"];
            ViewBag.calendario = cookieName.Values["calendario"];
            return View();
        }


    }
}