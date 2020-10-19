using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using System.Net;
using System.Text;
using SoftwareProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SoftwareProject.Controllers
{
    public class HomeController : Controller
    {
        WebClient client = new WebClient();
        private Context db;

        public HomeController(Context _db)
        {
            db = _db;
        }
        public ActionResult Index()
        {
                var headers = new List<string>(VeriAl("https://www.wired.com/", "h2"));
                var links = new List<string>(LinkAl("https://www.wired.com", "a", headers));
 
                ViewBag.head = new SelectList(headers);
            
            
                return View();
        }
        public ActionResult GetExamList()
        {
            var exams = db.Exams.ToList();
            return View(exams);
        }
        public ActionResult DeleteExam(int id)
        {
            var ques = db.Questions.Where(x => x.ExamID == id).ToList();
            var ex = db.Exams.Find(id);
            foreach(var x in ques)
            {
                db.Questions.Remove(x);
            }
            db.SaveChanges();
            db.Exams.Remove(ex);
            db.SaveChanges();
            return RedirectToAction("GetExamList");
        }
        [HttpPost]
        public ActionResult CreateExam(Exam ex)
        {
            DateTime dt = DateTime.Today;
            ex.Date = dt;
            db.Exams.Add(ex);
            db.SaveChanges();
            var jsonIcerik = JsonConvert.SerializeObject(ex.id);
            return Json(jsonIcerik);
        }
        [HttpPost]
        public ActionResult AddQuestion(Questions qs)
        {
         
            db.Questions.Add(qs);
            db.SaveChanges();
            var jsonIcerik = JsonConvert.SerializeObject(qs.id);
            return Json(jsonIcerik);
        }
        public IActionResult icerikGetir(string headerName)
        {
            if (headerName != null) { 
            var headers = new List<string>(VeriAl("https://www.wired.com/", "h2"));
            var index = headers.IndexOf(headerName);
            var links = new List<string>(LinkAl("https://www.wired.com", "a", headers));
            var icerikler = (icerikAl("https://www.wired.com" + links[index], "p"));
            ViewBag.HName = headerName;
            ViewBag.ic = icerikler;


            var jsonIcerik = JsonConvert.SerializeObject(icerikler);
            return Json(jsonIcerik);
            }
            else
            {
                return Json(null);
            }
        }

        public List<string> VeriAl(string url, string tag)
        {
            var list = new List<string>();
            string html = client.DownloadString(url);
            HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();
            dokuman.LoadHtml(html);
            HtmlNodeCollection basliklar = dokuman.DocumentNode.SelectNodes("//" + tag);
            if (basliklar != null )
            {
                foreach (var baslik in basliklar)
                {
                    list.Add(baslik.InnerText);
                }
            }
            return list;
        }
        public List<string> LinkAl(string url, string tag, List<string> basliklars)
        {
            var list = new List<string>();
            string html = client.DownloadString(url);
            HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();
            dokuman.LoadHtml(html);
            HtmlNodeCollection basliklar = dokuman.DocumentNode.SelectNodes("//" + tag);
            if (basliklar != null)
            {
                foreach (var baslik in basliklar)
                {
                    string link = baslik.Attributes["href"].Value;
                    if (link != null)
                    {
                        //list.Add(baslik.InnerText);

                        for (int i = 0; i < 5; i++)
                        {
                            if (baslik.InnerText == basliklars[i])
                            {
                                list.Add(link);
                            }
                        }
                    }
                }
            }
            return list;
        }
        public string icerikAl(string url, string tag)
        {
            var icerikler = new List<string>();
            string paragraf = "";
            string html = client.DownloadString(url);
            HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();
            dokuman.LoadHtml(html);
            HtmlNodeCollection basliklar = dokuman.DocumentNode.SelectNodes("//" + tag);
            if (basliklar != null)
            {
                foreach (var baslik in basliklar)
                {

                    icerikler.Add(baslik.InnerText);
                }
            }
            for (int i = 7; i < 15; i++)
            {
                paragraf = paragraf + icerikler[i];
            }
            return paragraf;
        }
    }
}