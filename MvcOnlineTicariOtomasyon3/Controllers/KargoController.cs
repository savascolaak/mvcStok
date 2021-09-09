using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon3.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon3.Controllers
{
    public class KargoController : Controller
    {
        // GET: Kargo
        Context c = new Context();
        public ActionResult Index(string p)
        {
            var deger = from x in c.KargoDetays select x;
            if (!string.IsNullOrEmpty(p))
            {
                deger = deger.Where(y => y.TakipKodu.Contains(p));
            }
            return View(deger.ToList());
        }
        [HttpGet]
        public ActionResult YeniKargo()
        {
            Random rd = new Random();
            string[] karakterler = { "A", "B", "C", "D" };
            int k1, k2, k3;
            k1 = rd.Next(0, 4);
            k2 = rd.Next(0, 4);
            k3 = rd.Next(0, 4);
            int s1, s2, s3;
            s1 = rd.Next(100, 1000);
            s2 = rd.Next(10, 99);
            s3 = rd.Next(10, 99);


            string kod = s1.ToString() + karakterler[k1] + s2 + karakterler[k2] + s3 + karakterler[k3];
            ViewBag.takipkod = kod;


            return View();

        }
        [HttpPost]
        public ActionResult YeniKargo(KargoDetay d)
        {
            c.KargoDetays.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult KargoDetay(string id)
        {
            var deger = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(deger);
        }
        public JsonResult UrunListe(string searchTerm)
        {
            var deger = c.Carilers.ToList();
            if (searchTerm != null)
            {
                deger = c.Carilers.Where(x => x.CariAd.Contains(searchTerm)).ToList();

            }
            var modifiedData = deger.Select(x => new
            {
                id = x.Cariid,
                text = x.CariAd
            });


            return Json(modifiedData, JsonRequestBehavior.AllowGet);
        }

    }
}