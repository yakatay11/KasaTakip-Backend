using Microsoft.EntityFrameworkCore;
using KasaAPI.Data;
using KasaAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Tek ve kapsamlı CORS politikası (Vercel ve localhost'u tamamen destekler)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS middleware'i yönlendirmelerden ÖNCE gelmelidir
app.UseCors("AllowAll");

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