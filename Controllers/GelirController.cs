using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KasaAPI.Data;
using KasaAPI.Models;

namespace KasaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GelirController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GelirController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gelir>>> GetGelirler()
        {
            return await _context.Gelirler.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Gelir>> PostGelir(Gelir gelir)
        {
            _context.Gelirler.Add(gelir);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGelirler), new { id = gelir.Id }, gelir);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> GelirGuncelle(int id, Gelir guncelGelir)
        {
            var gelir = await _context.Gelirler.FindAsync(id);
            if (gelir == null) return NotFound(new { message = "Güncellenecek gelir bulunamadı." });

            gelir.Kaynak = guncelGelir.Kaynak;
            gelir.Tutar = guncelGelir.Tutar;
            gelir.Aciklama = guncelGelir.Aciklama;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Gelir başarıyla güncellendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> GelirSil(int id)
        {
            var gelir = await _context.Gelirler.FindAsync(id);
            if (gelir == null)
            {
                return NotFound(new { message = "Silinecek gelir bulunamadı." });
            }

            _context.Gelirler.Remove(gelir);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Gelir başarıyla silindi." });
        }
    }
}