using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon3.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon3.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        Context c = new Context();
        public ActionResult Index()
        {
            var deger = c.Carilers.Where(x=>x.durum==true).ToList();
            return View(deger);
        }
        [HttpGet]
        public ActionResult YeniCari()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult YeniCari(Cariler p)
        {
            p.durum = true;
            c.Carilers.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Sil(int id)
        {
            var deger = c.Carilers.Find(id);
            deger.durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariGetir(int id)
        {
            var deger = c.Carilers.Find(id);
            return View("CariGetir",deger);
        }
        public ActionResult CariGuncelle(Cariler p)
        {
            if (!ModelState.IsValid)
            {
                return View("CariGetir");
            }
            var deger = c.Carilers.Find(p.Cariid);
            deger.CariAd = p.CariAd;
            deger.CariMail = p.CariMail;
            deger.CariSehir = p.CariSehir;
            deger.CariSoyad = p.CariSoyad;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult MusteriSatis(int id)
        {
            var deger = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            var cr = c.Carilers.Where(x => x.Cariid == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.cari = cr;

            return View(deger);

        }

    }
}