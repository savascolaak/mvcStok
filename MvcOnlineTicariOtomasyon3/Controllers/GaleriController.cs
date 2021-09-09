﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon3.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon3.Controllers
{
    public class GaleriController : Controller
    {
        // GET: Galeri
        Context c = new Context();
        public ActionResult Index()
        {
            var deger = c.Uruns.ToList();
            return View(deger);
        }
    }
}