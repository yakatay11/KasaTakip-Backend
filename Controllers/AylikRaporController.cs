using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KasaAPI.Data;
using KasaAPI.Models;

namespace KasaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AylikRaporController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AylikRaporController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AylikRapor>>> GetRaporlar()
        {
            return await _context.AylikRaporlar.OrderByDescending(r => r.Id).ToListAsync();
        }

        // Belirli bir ayın raporunu hesaplayıp arşive kaydeden endpoint
        [HttpPost("olustur")]
        public async Task<IActionResult> RaporOlustur([FromBody] AylikRapor raporDto)
        {
            // Aynı aya ait rapor daha önce eklenmiş mi kontrol et
            var mevcutRapor = await _context.AylikRaporlar.FirstOrDefaultAsync(r => r.AyYil == raporDto.AyYil);
            if (mevcutRapor != null)
            {
                mevcutRapor.ToplamGelir = raporDto.ToplamGelir;
                mevcutRapor.ToplamGider = raporDto.ToplamGider;
                mevcutRapor.NetBakiye = raporDto.NetBakiye;
            }
            else
            {
                _context.AylikRaporlar.Add(raporDto);
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Aylık rapor başarıyla arşivlendi." });
        }
    }
}