using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KasaAPI.Data;
using KasaAPI.Models;

namespace KasaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KullaniciController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Kullanici
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kullanici>>> GetKullanicilar()
        {
            return await _context.Kullanicilar.ToListAsync();
        }

        // POST: api/Kullanici
        [HttpPost]
        public async Task<ActionResult<Kullanici>> PostKullanici(Kullanici kullanici)
        {
            _context.Kullanicilar.Add(kullanici);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetKullanicilar), new { id = kullanici.Id }, kullanici);
        }

        // POST: api/Kullanici/giris (Kullanıcı giriş kontrolü)
        [HttpPost("giris")]
        public async Task<IActionResult> GirisYap([FromBody] Kullanici girisModel)
        {
            var k = await _context.Kullanicilar
                .FirstOrDefaultAsync(x => x.KullaniciAdi == girisModel.KullaniciAdi && x.Sifre == girisModel.Sifre);

            if (k == null)
            {
                return Unauthorized(new { message = "Kullanıcı adı veya şifre hatalı." });
            }

            return Ok(new { message = "Giriş başarılı", rol = k.Rol, adSoyad = k.AdSoyad, id = k.Id });
        }
    }
}