using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KasaAPI.Data;
using KasaAPI.Models;

namespace KasaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiderTalebiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GiderTalebiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/GiderTalebi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiderTalebi>>> GetGiderTalepleri()
        {
            return await _context.GiderTalepleri.ToListAsync();
        }

        // POST: api/GiderTalebi (Personel yeni talep oluşturur)
        [HttpPost]
        public async Task<ActionResult<GiderTalebi>> PostGiderTalebi(GiderTalebi giderTalebi)
        {
            giderTalebi.Durum = "Bekliyor";
            _context.GiderTalepleri.Add(giderTalebi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGiderTalepleri), new { id = giderTalebi.Id }, giderTalebi);
        }

        // PUT: api/GiderTalebi/5/onayla (Admin talebi onaylar ve kasaya gider olarak işler)
        [HttpPut("{id}/onayla")]
        public async Task<IActionResult> TalepOnayla(int id)
        {
            var talep = await _context.GiderTalepleri.FindAsync(id);
            if (talep == null)
            {
                return NotFound("Talep bulunamadı.");
            }

            talep.Durum = "Onaylandı";

            // Talep onaylandığı an otomatik olarak ana Gider tablosuna eklenir
            var yeniGider = new Gider
            {
                Tarih = talep.Tarih,
                KimeOdendi = talep.KimeOdenecek,
                Kategori = talep.Kategori,
                Tutar = talep.Tutar,
                Aciklama = talep.Aciklama,
                IslemiYapanAdminId = 1, // Şimdilik varsayılan admin ID
                BagliTalepId = talep.Id
            };

        [HttpPut("{id}")]
        async Task<IActionResult> TalepGuncelle(int id, GiderTalebi guncelTalep)
        {
            var talep = await _context.GiderTalepleri.FindAsync(id);
            if (talep == null) return NotFound(new { message = "Güncellenecek talep bulunamadı." });

            talep.KimeOdenecek = guncelTalep.KimeOdenecek;
            talep.Kategori = guncelTalep.Kategori;
            talep.Tutar = guncelTalep.Tutar;
            talep.Aciklama = guncelTalep.Aciklama;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Talep başarıyla güncellendi." });
        }
            _context.Giderler.Add(yeniGider);
            await _context.SaveChangesAsync();

            return Ok(new { mesaj = "Talep onaylandı ve kasaya gider olarak işlendi.", giderId = yeniGider.Id });
        }
    }
}