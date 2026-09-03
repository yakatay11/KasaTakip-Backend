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

        // POST: api/Kullanici/giris
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

        // PUT: api/Kullanici/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKullanici(int id, Kullanici kullanici)
        {
            if (id != kullanici.Id)
            {
                return BadRequest(new { message = "ID uyuşmazlığı." });
            }

            var mevcutKullanici = await _context.Kullanicilar.FindAsync(id);
            if (mevcutKullanici == null)
            {
                return NotFound(new { message = "Kullanıcı bulunamadı." });
            }

            mevcutKullanici.AdSoyad = kullanici.AdSoyad;
            mevcutKullanici.KullaniciAdi = kullanici.KullaniciAdi;
            mevcutKullanici.Rol = kullanici.Rol;

            if (!string.IsNullOrEmpty(kullanici.Sifre))
            {
                mevcutKullanici.Sifre = kullanici.Sifre;
            }

            _context.Entry(mevcutKullanici).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Kullanicilar.Any(e => e.Id == id))
                {
                    return NotFound(new { message = "Kullanıcı bulunamadı." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Kullanici/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKullanici(int id)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici == null)
            {
                return NotFound(new { message = "Kullanıcı bulunamadı." });
            }

            _context.Kullanicilar.Remove(kullanici);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}