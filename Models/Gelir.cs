namespace KasaAPI.Models
{
    public class Gelir
    {
        public int Id { get; set; }
        public string Kaynak { get; set; } = string.Empty;
        public decimal Tutar { get; set; }
        public string Aciklama { get; set; } = string.Empty;
        public DateTime Tarih { get; set; } = DateTime.Now;
    }
}