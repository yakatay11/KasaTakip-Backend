using Microsoft.EntityFrameworkCore;
using KasaAPI.Data;
using KasaAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// CORS Servis Kaydı
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ÇOK ÖNEMLİ: CORS middleware'i diğer yönlendirmelerden ve controller'lardan ÖNCE gelmelidir!
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();
// Uygulama ilk çalıştığında örnek admin kullanıcı oluşturur (Eğer yoksa)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    if (!context.Kullanicilar.Any())
    {
        context.Kullanicilar.Add(new Kullanici
        {
            AdSoyad = "Sistem Yöneticisi",
            KullaniciAdi = "admin",
            Sifre = "1234",
            Rol = "Yonetici"
        });
        context.SaveChanges();
    }
}
app.Run();