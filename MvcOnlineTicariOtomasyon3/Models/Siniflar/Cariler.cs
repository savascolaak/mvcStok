﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon3.Models.Siniflar
{
    public class Cariler
    {
        [Key]
        public int Cariid { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30,ErrorMessage ="En Fazla 30 Karakter Yazabilirsiniz")]
        public string CariAd { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required(ErrorMessage ="Bu Alanı Boş Geçemezsiniz!")]

        public string CariSoyad { get; set; }
        
        [Column(TypeName = "Varchar")]
        [StringLength(13)]

        public string CariSehir { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string CariMail { get; set; }
        
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string CariSifre { get; set; }

        public bool durum { get; set; }
   
        public ICollection<SatisHareket> SatisHarekets { get; set; }

    }
}