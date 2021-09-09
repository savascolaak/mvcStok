using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon3.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon3.Controllers
{
    public class SatisController : Controller
    {
        Context c = new Context();
        // GET: Satis
        public ActionResult Index()
        {
            var deger = c.SatisHarekets.ToList();
            return View(deger);

        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> deger1 = (from x in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.Urunid.ToString()
                                           }).ToList();
           
            List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd+ " "+x.CariSoyad,
                                               Value = x.Cariid.ToString()
                                           }).ToList();
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.Personelid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;

            


            return View();

        }
       [HttpPost]
        public ActionResult YeniSatis(SatisHareket s)
        {
            //s.ToplamTutar = s.Fiyat * s.Adet;
            s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(s);
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.Urunid.ToString()
                                           }).ToList();

            List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.Cariid.ToString()
                                           }).ToList();
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.Personelid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;


            
            var deger = c.SatisHarekets.Find(id);
            return View("SatisGetir", deger);

        }
        public ActionResult SatisGuncelle(SatisHareket s)
        {
            var deger = c.SatisHarekets.Find(s.Satisid);
            deger.Cariid = s.Cariid;
            deger.Personel = s.Personel;
            deger.Fiyat = s.Fiyat;
            deger.Tarih = s.Tarih;
            deger.Urun = s.Urun;
            deger.ToplamTutar = s.ToplamTutar;
            deger.Adet = s.Adet;
            c.SaveChanges();
            
            return RedirectToAction("Index");
        }
        public ActionResult SatisDetay(int id)
        {
            var deger = c.SatisHarekets.Where(x => x.Satisid == id).ToList();
            return View(deger);



        }
        public JsonResult UrunListe(string searchTerm)
        {
            var deger = c.Uruns.ToList();
            if (searchTerm != null)
            {
                deger = c.Uruns.Where(x => x.UrunAd.Contains(searchTerm)).ToList();

            }
            var modifiedData = deger.Select(x => new
            {
                id = x.Urunid,
                text = x.UrunAd
            });


            return Json(modifiedData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CariListe(string searchTerm2)
        {
            var deger = c.Carilers.ToList();
            if (searchTerm2 != null)
            {
                deger = c.Carilers.Where(x => x.CariAd.Contains(searchTerm2)).ToList();
                //deger = c.Carilers.Where(x => x.CariSoyad.Contains(searchTerm2)).ToList();

            }
            var modifiedData = deger.Select(x => new
            {
                id = x.Cariid,
                text = x.CariAd + " "+ x.CariSoyad
                
            });


            return Json(modifiedData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PersonelListe(string searchTerm3)
        {
            var deger = c.Personels.ToList();
            if (searchTerm3 != null)
            {
                deger = c.Personels.Where(x => x.PersonelAd.Contains(searchTerm3)).ToList();
                //deger = c.Carilers.Where(x => x.CariSoyad.Contains(searchTerm2)).ToList();

            }
            var modifiedData = deger.Select(x => new
            {
                id = x.Personelid,
                text = x.PersonelAd + " " + x.PersonelSoyad

            });


            return Json(modifiedData, JsonRequestBehavior.AllowGet);
        }
    }
}