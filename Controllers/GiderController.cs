using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KasaAPI.Data;
using KasaAPI.Models;

namespace KasaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GiderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gider>>> GetGiderler()
        {
            return await _context.Giderler.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Gider>> PostGider(Gider gider)
        {
            _context.Giderler.Add(gider);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGiderler), new { id = gider.Id }, gider);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> GiderGuncelle(int id, Gider guncelGider, [FromQuery] string rol)
        {
            if (rol != "Yonetici")
            {
                return Unauthorized(new { message = "Bu işlem için yönetici yetkisi gerekiyor." });
            }

            var gider = await _context.Giderler.FindAsync(id);
            if (gider == null)
            {
                return NotFound(new { message = "Güncellenecek gider bulunamadı." });
            }

            gider.KimeOdendi = guncelGider.KimeOdendi; 
            gider.Kategori = guncelGider.Kategori;
            gider.Tutar = guncelGider.Tutar;
            gider.Aciklama = guncelGider.Aciklama;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Gider başarıyla güncellendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> GiderSil(int id, [FromQuery] string rol)
        {
            if (rol != "Yonetici")
            {
                return Unauthorized(new { message = "Bu işlem için yönetici yetkisi gerekiyor." });
            }

            var gider = await _context.Giderler.FindAsync(id);
            if (gider == null)
            {
                return NotFound(new { message = "Silinecek gider bulunamadı." });
            }

            _context.Giderler.Remove(gider);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Gider başarıyla silindi." });
        }
    }
}