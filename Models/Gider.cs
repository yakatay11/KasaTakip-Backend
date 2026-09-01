using System;

namespace KasaAPI.Models
{
    public class Gider
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
        public string KimeOdendi { get; set; } = string.Empty;
        public string Kategori { get; set; } = string.Empty;
        public decimal Tutar { get; set; }
        public string Aciklama { get; set; } = string.Empty;
        public int IslemiYapanAdminId { get; set; }
        public int? BagliTalepId { get; set; } // Talepten gelmediyse boş (null) olabilir
    }
}