using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoftwareProject.Models;

namespace SoftwareProject.Controllers
{
    public class UserController : Controller
    {
        private Context db;

        public UserController(Context _db)
        {
            db = _db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var exams = db.Exams.ToList();
            return View(exams);
        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(user usr)
        {
            db.Users.Add(usr);
            db.SaveChanges();

            return View();
        }
   
        public IActionResult StartExam(int id)
        {
            var ques = db.Questions.Where(x => x.ExamID == id).ToList();
            ViewBag.Header = db.Exams.Find(id).Header;
            ViewBag.Content = db.Exams.Find(id).Content;

            return View(ques);
        }
        public IActionResult CheckQuestion(int qsID , string qsAnswer)
        {
            var qs=db.Questions.Find(qsID);
            if (qs.Answer == qsAnswer)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
            
        }
    }
}