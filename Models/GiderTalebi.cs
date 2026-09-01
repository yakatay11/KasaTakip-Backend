using System;

namespace KasaAPI.Models
{
    public class GiderTalebi
    {
        public int Id { get; set; }
        public int TalepEdenPersonelId { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
        public string KimeOdenecek { get; set; } = string.Empty;
        public string Kategori { get; set; } = string.Empty;
        public decimal Tutar { get; set; }
        public string Aciklama { get; set; } = string.Empty;
        public string Durum { get; set; } = "Bekliyor"; // Bekliyor, Onaylandı, Reddedildi
    }
}