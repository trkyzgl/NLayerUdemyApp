
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NLayer.Repository;
using NLayer.Service.Mapping;
using NLayer.Web.Modules;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



/////



builder.Services.AddAutoMapper(typeof(MapProfile));   /*AutoMapper ı kullanmak için builder ediyoruz*/

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {
        /*
        options.MigrationsAssembly("NLayer.Repository");
        // şeklinde bir kulalnım yapabilirim. Ama projenin ismi değişince burası da değişmesi gerekiyor.
        Bu sebeple aşağıda belirtildiği gibi AppDbContext nesnesinin tanımlandığı projeye git ve o projenin adını al.
        */
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});




builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());  //AutoFac i programa eklediğimiz kısım
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));// modulu dahil ettik
////





//////




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
