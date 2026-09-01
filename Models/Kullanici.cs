namespace KasaAPI.Models
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; } = string.Empty;
        public string KullaniciAdi { get; set; } = string.Empty;
        public string Sifre { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty; // Varsayılanı boş bırakıyoruz ki frontend'den gelen rol (Yonetici, Muhasebe, Personel) tam olarak işlensin.
    }
}