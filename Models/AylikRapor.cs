namespace KasaAPI.Models
{
    public class AylikRapor
    {
        public int Id { get; set; }
        public string AyYil { get; set; } = string.Empty;
        public decimal ToplamGelir { get; set; }
        public decimal ToplamGider { get; set; }
        public decimal NetBakiye { get; set; }
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
    }
}