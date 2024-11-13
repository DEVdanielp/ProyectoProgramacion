using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Identity;
using Hospital.Web.Data;
using Hospital.Web.Data.Entities;
using Hospital.Web.Helpers;
using Hospital.Web.Services;
using Microsoft.EntityFrameworkCore;
using Hospital.Web.Data.Seeders;

namespace Hospital.Web
{
    public static class CustomConfiguration
    {
        public static WebApplicationBuilder AddCustomBuilderConfiguration(this WebApplicationBuilder builder)
        {
            //Se conecta con la base de datos
            builder.Services.AddDbContext<DataContext>(Configuration =>
            {
                Configuration.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
            });

            //
            builder.Services.AddHttpContextAccessor();  

            //Se instancian los servicios 
            AddServices(builder);

            //Identity and Acces Managment
            AddIAM(builder);

            //Toast Notificaciòn 
            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.IsDismissable = true;
                config.Position = NotyfPosition.BottomRight;
            });

            return builder;
        }

        private static void AddIAM(WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<User, IdentityRole>(conf =>
            {
                //Configuracion de la FUerza de la contraseña para los Usuarios
                conf.User.RequireUniqueEmail = true;
                conf.Password.RequireDigit = false;
                conf.Password.RequiredUniqueChars = 0;
                conf.Password.RequireLowercase = false;
                conf.Password.RequireUppercase = false;
                conf.Password.RequireNonAlphanumeric = false;
                conf.Password.RequiredLength = 4;

            }).AddEntityFrameworkStores<DataContext>()
              .AddDefaultTokenProviders();

            //Las cookies
            builder.Services.ConfigureApplicationCookie(conf =>
            {
                conf.Cookie.Name = "Auth";
                conf.ExpireTimeSpan = TimeSpan.FromDays(100);
                conf.LoginPath = "/Account/Login";
                conf.AccessDeniedPath = "/Account/NotAuthorized";
            });

        }

        //Se llaman a los servicios a usar
        public static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAppoimentServices, AppoimentServices>(); //Servicios de citas
            builder.Services.AddScoped<IUsersService, UsersService>(); //Servicios de Usuario
            builder.Services.AddScoped<IStatusServices, StatusServices>(); //Servicios de citas
            builder.Services.AddScoped<IMedicalSpeServices, MedicalSpeServices>(); //Servicios de Especialidad Medica
            builder.Services.AddScoped<IMedicationsServices, MedicationService>();//Servicios de medicamentos
            builder.Services.AddScoped<IMedicalOrdersServices, MedicalOrdersServices>();//Servicios de Orden Medica
            builder.Services.AddScoped<IMedicalHistoryServices, MedicalHistoryService>(); //Servicios de Historia Clínica
            builder.Services.AddScoped<IRolesServices, RolService>(); //Servicios de Historia Clínica
            builder.Services.AddTransient<SeedDb>();



            //Helpers
            builder.Services.AddScoped<IConverterHelper, ConverterHelper>();
            builder.Services.AddScoped<ICombosHelpers, CombosHelper>();
        }


        public static WebApplication AddCustomWebAppConfiguration(this WebApplication app)
        {
            app.UseNotyf();
            SeedData(app);
            return app;
        }

        private static void SeedData(WebApplication app)
        {
            IServiceScopeFactory scopeFactory = app.Services.GetService<IServiceScopeFactory>();

            using (IServiceScope scope = scopeFactory!.CreateScope())
            {
                SeedDb service = scope.ServiceProvider.GetService<SeedDb>();
                service!.SeedAsync().Wait();
            }
        }
    }
}
