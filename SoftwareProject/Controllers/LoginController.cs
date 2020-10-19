using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftwareProject.Models;

namespace SoftwareProject.Controllers
{
    public class LoginController : Controller
    {
        private Context db;

        public LoginController(Context _db)
        {
            db = _db;
        }
  
        public IActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //[Route("")]
        //[Route("~/")]
        //[Route("index")]
        //public IActionResult Index(string username , string password)
        //{
        //    var adm=db.Admins.Where(x => x.username == username && x.password == password).FirstOrDefault();
        //    var usr = db.Users.Where(x => x.username == username && x.password == password).FirstOrDefault();
        //    if (adm!=null)
        //    {

        //        return Json(1);
        //    }
        //    else if (usr!=null)
        //    {

        //        return Json(2);
        //    }
        //    else
        //    {
        //        return Json(0);
        //    }
            
        //}
        public IActionResult LoginCheck(string username, string password)
        {
            var adm = db.Admins.Where(x => x.username == username && x.password == password).FirstOrDefault();
            var usr = db.Users.Where(x => x.username == username && x.password == password).FirstOrDefault();
            if (adm != null)
            {

                return Json(1);
            }
            else if (usr != null)
            {

                return Json(2);
            }
            else
            {
                return Json(0);
            }
        }
        
    }
}