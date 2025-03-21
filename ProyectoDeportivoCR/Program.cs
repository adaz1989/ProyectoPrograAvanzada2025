using ProyectoDeportivoCR.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IGeneral, General>();

builder.Services.AddScoped<IEncriptacionService, EncriptacionService>();
builder.Services.AddScoped<IUsuarioRepositorie, UsuarioRepositorie>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseSession();
app.UseExceptionHandler("/Error/CapturarError");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
