using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon3.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon3.Controllers
{
    [Authorize]
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context c = new Context();
        
        public ActionResult Index()
        {
            var deger = c.Departmans.Where(x => x.Durum == true).ToList();
            return View(deger);
        }
        [Authorize(Roles ="A")]
        [HttpGet]
        public ActionResult DepartmanEkle()
        {

            return View();

        }
        [HttpPost]
       
        public ActionResult DepartmanEkle(Departman d)
        {
            c.Departmans.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult DepartmanSil(int id)
        {
            var deger = c.Departmans.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult DepartmanListele(int id)
        {
            var deger = c.Departmans.Find(id);
            return View("DepartmanListele", deger);

        }
        public ActionResult DepartmanGuncelle(Departman d)
        {
            var deger = c.Departmans.Find(d.Departmanid);
            deger.DepartmanAd = d.DepartmanAd;
            deger.Durum = d.Durum;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult DepartmanDetay(int id)
        {
            var deger = c.Personels.Where(x => x.Departmanid == id).ToList();
            var dpt = c.Departmans.Where(x => x.Departmanid == id).Select(y => y.DepartmanAd).FirstOrDefault();
            ViewBag.d = dpt;

            return View(deger);
        }
        public ActionResult DepartmanPersonelSatis(int id)
        {
            var deger = c.SatisHarekets.Where(x => x.Personelid == id).ToList();
            var per = c.Personels.Where(x => x.Personelid == id).Select(y => y.PersonelAd + y.PersonelSoyad).FirstOrDefault();
            ViewBag.dp = per;
            return View(deger);
        }
    }
}