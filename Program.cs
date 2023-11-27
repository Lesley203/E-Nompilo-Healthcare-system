
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
using E_Nompilo_Healthcare_system.UserClaimsIdentity;
using E_Nompilo_Healthcare_system.Image;
using Microsoft.AspNetCore.Identity.UI.Services;
using E_Nompilo_Healthcare_system.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HealthcareDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserClaimsPrincipalFactory<HealthcareSystemUser>, SystemUserClaims>();


builder.Services.AddIdentity<HealthcareSystemUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
     .AddDefaultUI()
     .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<HealthcareDbContext>();
builder.Services.AddControllersWithViews();

//builder.Services.AddTransient<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.UseEndpoints(enpoint =>
{
    enpoint.MapControllerRoute(
        name: "ContraceptivesRefills",
        pattern: "/ContraceptivesRefills/ContraceptivesReschedule/{ID}",
        defaults: new { controller = "ContraceptivesRefills", action = "ContraceptivesReschedule" }
        );
});
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "ADMIN", "PATIENT", "DOCTOR", "PHARMACIST", "NURSE" };

    foreach (var role in roles)
    {
       if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<HealthcareSystemUser>>();

    string email = "admin@gmail.com";
    string password = "Admin@123";

    if(await userManager.FindByEmailAsync(email) == null)
    {
        var user = new HealthcareSystemUser();
        user.Email = email;
        user.UserName = email;
        user.EmailConfirmed= true;
        user.FirstName = "DANIEL";
        user.LastName = "GASTAVO";
        user.PhoneNumb = "0741234567";
        user.Gender = "MRS";
        user.DateofBirth = DateTime.Now;
        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "ADMIN");
    }
}
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<HealthcareSystemUser>>();

    string email = "NURSE@gmail.com";
    string password = "Nurse@123";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new HealthcareSystemUser();
        user.Email = email;
        user.UserName = email;
        user.EmailConfirmed = true;
        user.FirstName = "Kelvin";
        user.LastName = "Masopo";
        user.PhoneNumb = "0843934567";
        user.Gender = "MR";
        user.DateofBirth = DateTime.Now;
        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "NURSE");
    }
}
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<HealthcareSystemUser>>();

    string email = "patient@gmail.com";
    string password = "Patient@123";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new HealthcareSystemUser();
        user.Email = email;
        user.UserName = email;
        user.EmailConfirmed = true;
        user.FirstName = "RICKY";
        user.LastName = "KOVIC";
        user.PhoneNumb = "0741234567";
        user.Gender = "MR";
        user.DateofBirth = DateTime.Now;
        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "PATIENT");
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<HealthcareSystemUser>>();

    string email = "doctor@gmail.com";
    string password = "Doctor@123";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new HealthcareSystemUser();
        user.Email = email;
        user.UserName = email;
        user.EmailConfirmed = true;
        user.FirstName = "PETER";
        user.LastName = "MAKOVIC"; 
        user.PhoneNumb = "074120986";
        user.Gender = "MRS";
        user.DateofBirth = DateTime.Now;
        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "DOCTOR");

    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<HealthcareSystemUser>>();

    string email = "pharmacist@gmail.com";
    string password = "Pharmacist@123";

    if (await userManager.FindByEmailAsync(email) == null)
    {

        var user = new HealthcareSystemUser();
        user.Email = email;
        user.UserName = email;
        user.EmailConfirmed = true;
        user.FirstName = "JOHN";
        user.LastName = "FARAO";
        user.PhoneNumb = "074120986";
        user.Gender = "MR";
        user.DateofBirth = DateTime.Now;
        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "PHARMACIST");
    }
}
app.Run();
