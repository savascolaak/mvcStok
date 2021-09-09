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
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();
        public ActionResult Index(string p)
        {
            var deger = from x in c.Uruns select x;
            if (!string.IsNullOrEmpty(p))
            {
                deger = deger.Where(y => y.UrunAd.Contains(p));
            }
            return View(deger.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();

        }
        [HttpPost]
        public ActionResult YeniUrun(Urun p)
        {
            c.Uruns.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var deger = c.Uruns.Find(id);
            c.Uruns.Remove(deger);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            var deger = c.Uruns.Find(id);
            return View("UrunGetir", deger);
        }
        public ActionResult UrunGuncelle(Urun p)
        {
            var deger = c.Uruns.Find(p.Urunid);
            deger.UrunAd = p.UrunAd;
            deger.Marka = p.Marka;
            deger.SatisFiyat = p.SatisFiyat;
            deger.Stok = p.Stok;
            deger.Durum = p.Durum;
            deger.AlisFiyat = p.AlisFiyat;
            deger.UrunGorsel = p.UrunGorsel;
            deger.Kategori = p.Kategori;
            c.SaveChanges();

            return RedirectToAction("Index");

        }
        public ActionResult UrunListesi()
        {
            var deger = c.Uruns.ToList();
            return View(deger);
        }
        public JsonResult KategoriListe(string searchTerm)
        {
            var deger = c.Kategoris.ToList();
            if (searchTerm != null)
            {
                deger = c.Kategoris.Where(x => x.KategoriAd.Contains(searchTerm)).ToList();

            }
            var modifiedData = deger.Select(x => new
            {
                id = x.KategoriID,
                text = x.KategoriAd
            });


            return Json(modifiedData, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + x.PersonelSoyad,
                                               Value = x.Personelid.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            var deger1 = c.Uruns.Find(id);
            ViewBag.dgr1 = deger1.Urunid;
            ViewBag.dgr2 = deger1.SatisFiyat;

            var deger4 = c.Uruns.Where(x=>x.Urunid == deger1.Urunid).Select(y=>y.Stok).FirstOrDefault();
            ViewBag.d4 = deger4;

            return View();
            
        }
        [HttpPost]
        public ActionResult SatisYap(SatisHareket p)
        {
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(p);
            c.SaveChanges();
            return View("Index","Satis");

        }
    }

}

