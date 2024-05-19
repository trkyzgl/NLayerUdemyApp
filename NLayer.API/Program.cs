using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.API.Filters;
using NLayer.API.Middlewares;
using NLayer.API.Modules;
using NLayer.Core.Repositoties;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using NLayer.Service.Validations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());// FluentValidation eklendi // ValidateFilterAttribute Eklendi

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true; // Framework ün default filtresini inaktif yapıyoruz. Çünkü biz hazirlamış olduğumuz ValidateFilterAttribute filtresini kullanacaz. Fakat Bu builder ın MVC tarafında olmasına gerek yok çünkü MVC bunu kendisi yapıyor olacak
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//.net core 6 ile beraber StartUp dosyası kalktı. orada kodlar Program dosyasına eklendi.
/*Bizde EFCore a yapmış olduğumuz ConnectionStringi Kullanma bilgisini vereceğiz*/

/////////
builder.Services.AddMemoryCache();  /// Cache yapma işlemini burada ekledik.


builder.Services.AddScoped(typeof(NotFoundFilter<>)); // NotFound için tasarlamış olduğumuz hata Filtresini ekledik

//builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();                     Gerek yok çünkü RepoServiceModule class ında eklemiş oldu
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));    Gerek yok çünkü RepoServiceModule class ında eklemiş oldu
//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));      Gerek yok çünkü RepoServiceModule class ında eklemiş oldu
builder.Services.AddAutoMapper(typeof(MapProfile));   /*AutoMapper ı kullanmak için builder ediyoruz*/

//builder.Services.AddScoped<IProductRepository, ProductRepository>();    Gerek yok çünkü RepoServiceModule class ında eklemiş olduk
//builder.Services.AddScoped<IProductService, ProductService>();          Gerek yok çünkü RepoServiceModule class ında eklemiş olduk

//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();  Gerek yok çünkü RepoServiceModule class ında eklemiş olduk
//builder.Services.AddScoped<ICategoryService, CategoryService>();        Gerek yok çünkü RepoServiceModule class ında eklemiş olduk


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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException();// Hatalarımızı yakalayıp döneceğimiz ve kendimizin yaptığı tasarım. NOT: HATA olduğu için UseAuthorization, MapControllers gibi eklemelerden önce yapılması gerekiyor

app.UseAuthorization();

app.MapControllers();

app.Run();
