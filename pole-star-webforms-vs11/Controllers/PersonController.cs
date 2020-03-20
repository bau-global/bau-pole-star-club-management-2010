using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PoleStarWebVS11.Models;

namespace PoleStarWebVS11.Controllers
{
    public class PersonController : Controller
    {
        KutupYildiziDBEntities1 db = new KutupYildiziDBEntities1();
        
        
        public ActionResult Login(LoginModel model)
        {
            try
            {
                if (Session["userinfo"] == null)//giriş yapmamışsa
                {
                    var query = (from p in db.Person
                                 where model.PersonEmail == p.PersonEmail && model.PersonPassword == p.PersonPassword
                                 select p);
                    int count = query.Count();
                    if (count == 0)
                    {
                        //kullanıcı adı veya şifre yanlış
                    }
                    else
                    {
                        //session oluştur
                        var loggedin = query.First();
                        Session["userinfo"] = loggedin.PersonEmail + "-" + loggedin.PersonFName + "-" + loggedin.PersonLName + "-" + loggedin.RoleID;
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home"); // zaten giriş yapmışsa
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
            
            
            return View(model);
        }
        
        public ActionResult Register(RegisterModel model) {
            try
            {
                if (Session["userinfo"] == null) // giriş yapmamışsa
                {
                    var userCount =(from p in db.Person where p.PersonEmail == model.PersonEmail select p).Count();
                    if (userCount > 0)
                    {
                        //bu email adresi zaten kayıtlı !
                        
                    }
                    else
                    {
                        Person p = new Person();
                        p.PersonEmail = model.PersonEmail;
                        p.PersonPassword = model.PersonPassword;
                        db.AddToPerson(p);
                        db.SaveChanges();
                        //gelen bilgilerle yeni person oluşturup db ye ekledik ve kaydettik.
                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home"); // zaten giriş yapmışsa ana sayfaya yönlendir
                }
            }
            catch (Exception)
            {
                
                throw;
            }
           
            
            return View(model);
        }

        public ActionResult Logout() {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}





















/* Fatih MVC'ci dayidan deneme
public ActionResult Login(LoginModel model)
{
    if (!ModelState.IsValidField("PersonEmail") || !ModelState.IsValidField("PersonPassword"))
    {
        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return View(model);
    }
    try
    {
        //string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(model.PersonPassword, "md5").ToLower().ToString();
        string pass = model.PersonPassword;

        Person query = (from p in db.Person
                        where p.PersonEmail == model.PersonEmail && p.PersonPassword == pass
                        select p).SingleOrDefault();

        if (query == null)
        {
            ModelState.AddModelError("uyari", "bilgileri kontrol ediniz");
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return View(model);
        }
        else
        {
            Session["PersonLoginInfo"] = query;

            if (model.RememberMe == true)
            {
                HttpCookie PersonRememberMe = new HttpCookie("myRememberCookie");
                PersonRememberMe.Values.Add("personEmail", query.PersonEmail);
                PersonRememberMe.Values.Add("personPassword", query.PersonPassword);
                PersonRememberMe.Expires = DateTime.Now.AddDays(365);
                Response.Cookies.Add(PersonRememberMe);
            }
        }

    }
    catch (Exception ex)
    {
    }

    // If we got this far, something failed, redisplay form
    return View();
}
*/