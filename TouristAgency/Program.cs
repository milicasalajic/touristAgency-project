using Microsoft.EntityFrameworkCore;
using TouristAgency;
using TouristAgency.Controllers;
using TouristAgency.Data;
using TouristAgency.Repositories;
using TouristAgency.RepositoryInterfaces;
using TouristAgency.ServiceInterfaces;
using TouristAgency.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
// dodaj seed
builder.Services.AddTransient<Seed>();
// uvedi bazu ovde
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDatabase"));
});


// Registracija servisa i repozitorijuma
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); // Repozitorijum za Category
builder.Services.AddScoped<ICategoryService, CategoryService>(); // Servis za Category
builder.Services.AddScoped<CategoryController>(); // Kontroler za Category

// Registracija servisa i repozitorijuma
builder.Services.AddScoped<ITouristPackageRepository, TouristPackageRepository>(); // Repozitorijum za Category
builder.Services.AddScoped<ITouristPackageService, TouristPackageService>(); // Servis za Category
builder.Services.AddScoped<TouristPackageController>(); // Kontroler za Category

// Registracija servisa i repozitorijuma
builder.Services.AddScoped<IReservationRepository, ReservationRepository>(); // Repozitorijum za Category
builder.Services.AddScoped<IReservationService, ReservationService>(); // Servis za Category
builder.Services.AddScoped<ReservationController>(); // Kontroler za Category



var app = builder.Build();

// nakon dodavanja builder seed dodaj ovo, popunjava bazu podataka pocetnim podacima
// proverava argumente pri pokretanju programa
if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);



// iHost da bi se dobio pristup bp
void SeedData(IHost app) 
{   // uzima servis za kreiranje scopa (rad s servisima koji imaju kratak vek trajanja npr bp)
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope()) 
    {   // dohvati seed koji popunjava pocetnim podacima
        var service = scope.ServiceProvider.GetService<Seed>();
        // pozovi popunjavanje
        service.SeedDataContext();
    }
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapRazorPages();
app.MapControllers(); // Mapiranje ruta kontrolera

app.Run();
