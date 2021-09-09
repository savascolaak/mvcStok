using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon3.Models.Siniflar;
using PagedList;
using PagedList.Mvc;


namespace MvcOnlineTicariOtomasyon3.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();
        
        public ActionResult Index(int sayfa=1)
        {
            var deger = c.Kategoris.ToList().ToPagedList(sayfa, 5);
            return View(deger);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            c.Kategoris.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult KategorSil(int id)
        {
            var deger = c.Kategoris.Find(id);
            c.Kategoris.Remove(deger);
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult KategoriListele(int id)
        {
            var deger = c.Kategoris.Find(id);

            return View("KategoriListele", deger);
        }
        public ActionResult KategoriGuncelle(Kategori k)
        {
            var deger = c.Kategoris.Find(k.KategoriID);
            deger.KategoriAd = k.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Deneme()
        {
            Class3 cs = new Class3();
            cs.Kategoriler = new SelectList(c.Kategoris, "KategoriID", "KategoriAd");
            cs.Urunler = new SelectList(c.Uruns, "Urunid", "Urunad");
            return View(cs);

        }
        public JsonResult UrunGetir(int p)
        {
            var urunlistesi = (from x in c.Uruns
                               join y in c.Kategoris
                               on x.Kategori.KategoriID equals y.KategoriID
                               where x.Kategori.KategoriID == p
                               select new
                               {
                                   Text = x.UrunAd,
                                   Value = x.Urunid.ToString()
                               }).ToList();
            return Json(urunlistesi, JsonRequestBehavior.AllowGet);
        }
        
    }
}