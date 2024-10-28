using Microsoft.EntityFrameworkCore;
using SistemaComprasMVC.Data; // Aseg�rate de que el namespace sea el correcto para tu proyecto

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios para el contexto de la base de datos
builder.Services.AddDbContext<SistemaComprasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SistemaComprasConnection")));

// Agregar servicios a los controladores con vistas (MVC)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuraci�n del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
