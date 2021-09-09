using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon3.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon3.Controllers
{
    
    public class FaturaController : Controller
    {
        Context c = new Context();
        // GET: Fatura
        public ActionResult Index()
        {
            var liste = c.Faturalars.ToList();
            return View(liste);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar f)
        {
            c.Faturalars.Add(f);
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult FaturaGetir(int id)
        {
            var fatura = c.Faturalars.Find(id);
            return View("FaturaGetir", fatura);
        }
        public ActionResult FaturaGuncelle(Faturalar f)
        {
            var deger = c.Faturalars.Find(f.Faturaid);
            deger.FaturaSeriNo = f.FaturaSeriNo;
            deger.FaturaSıraNo = f.FaturaSıraNo;
            deger.Saat = f.Saat;
            deger.Tarih = f.Tarih;
            deger.TeslimAlan = f.TeslimAlan;
            deger.TeslimEden = f.TeslimEden;
            deger.VergiDairesi = f.VergiDairesi;

            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetay(int id)
        {
            var deger = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();
            
            return View(deger);
        }
        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }
        public ActionResult YeniKalem(FaturaKalem p)
        {
            c.FaturaKalems.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        
        public ActionResult Dinamik()
        {
            int SiraNo;
            Class4 cs = new Class4();
            cs.deger1 = c.Faturalars.OrderByDescending(x=>x.Tarih).ToList();
            cs.deger2 = c.FaturaKalems.ToList();
           
            Random rd = new Random();
            //int SiraNo = rd.Next(100000, 1000000);


            Random rnd = new Random();
            int degisken = rnd.Next(0, 3);
            string[] serino = { "A", "B", "C" };
            string SeriNo = serino[degisken];
            
            var deger = c.Uruns.ToList();
            ViewBag.urunler = deger;
            
            var personel = c.Personels.ToList();
            ViewBag.personel = personel;
            
            var cari = c.Carilers.ToList();
            ViewBag.cari = cari;
            
            var deger1 = c.Uruns.Select(x => x.UrunAd).FirstOrDefault();
            var saat = DateTime.Now.ToShortTimeString();
            var tarih = DateTime.Now.ToLongDateString();
           
            ViewBag.tarih = tarih;
            ViewBag.saat = saat;
            ViewBag.SiraNo = SiraNo;
            ViewBag.SeriNo = SeriNo;
            var urun = c.Uruns.Select(x => x.UrunAd).ToList();
            SiraNo++;
            List<vs> vss = new List<vs>();
            bool selo = true;
            for (int i = 0; i < deger.Count(); i++)
            {
                for (int a = 0; a < deger.Count(); a++)
                {
                    string e = deger.ElementAt(i).UrunAd;

                    string d = deger.ElementAt(a).UrunAd;

                    if (a != i)
                    {
                        if (e == d)
                        {
                            vss.Add(new vs { UrunAd = deger.ElementAt(i).UrunAd, UrunMarka = deger.ElementAt(i).Marka });

                            selo = false;
                        }

                        
                    }


                }
                if (selo)
                {
                    vss.Add(new vs { UrunAd = deger.ElementAt(i).UrunAd, UrunMarka = " " });
                }
                selo = true;
            }

            ViewBag.vs = vss;

            var modifiedData = deger.Select(x => new
            {
                id = x.Urunid,
                text = x.UrunAd
            });

            return View(cs);
        }
        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSıraNo,DateTime Tarih,
            string VergiDairesi, string Saat, string TeslimEden,string TeslimAlan, string Toplam, FaturaKalem[] kalemler)
        {
            
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSıraNo = FaturaSıraNo;
            f.Tarih = DateTime.Now;
            f.VergiDairesi = VergiDairesi;
            f.Saat = DateTime.Now.ToShortTimeString();
            f.TeslimEden = TeslimEden;
            f.TeslimAlan = TeslimAlan;
            f.Toplam = decimal.Parse(Toplam);
            c.Faturalars.Add(f);
            
            foreach (var x in kalemler)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama = x.Aciklama;
                fk.BirimFiyat = x.BirimFiyat;
                fk.Faturaid = x.FaturaKalemid;
                fk.Miktar = x.Miktar;
                fk.Tutar = x.Tutar;
                c.FaturaKalems.Add(fk);

            }
            c.SaveChanges();
           
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);


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
       
    }
}