using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineTicariOtomasyon3.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon3.Controllers
{
    public class CariPanelController : Controller
    {
        Context c = new Context();
        // GET: CariPanel
        
        [Authorize]

        public ActionResult Index()
        {
            var carimail = (string)Session["CariMail"];
            var deger = c.Mesajlars.Where(x => x.Alici == carimail).ToList();
            ViewBag.d1 = carimail;
            var mailid = c.Carilers.Where(x => x.CariMail == carimail).Select(y => y.Cariid).FirstOrDefault();
            ViewBag.mid = mailid;
            var toplamsatis = c.SatisHarekets.Where(x => x.Cariid == mailid).Count();
            ViewBag.toplamsatis = toplamsatis;
            var toplamtutar = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y=>y.ToplamTutar);
            ViewBag.toplamtutar = toplamtutar;
            var toplamurunsayisi = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.Adet);
            ViewBag.ToplamUrunSayisi = toplamurunsayisi;
            var adsoyad = c.Carilers.Where(x => x.CariMail == carimail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            return View(deger);
        }
        [Authorize]
        public ActionResult Siperislerim()
        {
            var carimail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == carimail.ToString()).Select(y => y.Cariid).FirstOrDefault();
            var deger = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(deger);
        }
        [Authorize]
        public ActionResult GelenMesajlar()
        {
            var carimail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Alici == carimail).OrderByDescending(y => y.Tarih).ToList();
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == carimail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Count(x => x.Gonderici == carimail).ToString();
            ViewBag.d2 = gidensayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult GidenMesajlar()
        {
            var carimail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Gonderici == carimail).ToList();
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == carimail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Count(x => x.Gonderici == carimail).ToString();
            ViewBag.d2 = gidensayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult MesajDetay(int id)
        {
            var deger = c.Mesajlars.Where(x => x.MesajID == id).ToList();

            var carimail = (string)Session["CariMail"];
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == carimail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Count(x => x.Gonderici == carimail).ToString();
            ViewBag.d2 = gidensayisi;
            return View(deger);
        }
        [Authorize]
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var carimail = (string)Session["CariMail"];
            var gelensayisi = c.Mesajlars.Count(x => x.Alici == carimail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Count(x => x.Gonderici == carimail).ToString();
            ViewBag.d2 = gidensayisi;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar m)
        {
            var carimail = (string)Session["CariMail"];
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            m.Gonderici = carimail;

            c.Mesajlars.Add(m);
            c.SaveChanges();
            return View();
        }
        [Authorize]
        public ActionResult KargoTakip(string p)
        {
            var deger = from x in c.KargoDetays select x;
            deger = deger.Where(y => y.TakipKodu.Contains(p));
            return View(deger.ToList());

        }
        [Authorize]
        public ActionResult CariKargoTakip(string id)
        {
            var deger = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(deger);
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public PartialViewResult Partial1()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
            var deger = c.Carilers.Find(id);

            return PartialView("Partial1",deger);

        }
        [HttpPost]
       public ActionResult CariPanelGuncelle(Cariler a)
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();

            var deger = c.Carilers.Find(id);
            deger.CariAd = a.CariAd;
            deger.CariSoyad = a.CariSoyad;
            deger.CariMail = a.CariMail;
            deger.CariSehir = a.CariSehir;
            deger.CariSifre = a.CariSifre;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public PartialViewResult Partial2()
        {
            var veriler = c.Mesajlars.Where(x => x.Gonderici == "admin").ToList();
            return PartialView(veriler);

        }
       
    }
}