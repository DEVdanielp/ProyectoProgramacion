using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Hospital.Web.Data;
using Hospital.Web.Helpers;
using Hospital.Web.Services;
using Microsoft.EntityFrameworkCore;

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

            //Se instancian los servicios 
            AddServices(builder);

            //Toast Notificaciòn 
            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.IsDismissable = true;
                config.Position = NotyfPosition.BottomRight;
            });

            return builder;
        }

        //Se llaman a los servicios a usar
        public static void AddServices(WebApplicationBuilder builder)
        {
           //Services
            builder.Services.AddScoped<IRolesServices, RolService>(); //Servicios de roles
            builder.Services.AddScoped<IAppoimentServices, AppoimentServices>(); //Servicios de citas
            builder.Services.AddScoped<IUsersServices, UserServices>(); //Servicios de Usuario
            builder.Services.AddScoped<IStatusServices, StatusServices>(); //Servicios de citas
            builder.Services.AddScoped<IMedicalSpeServices, MedicalSpeServices>(); //Servicios de Especialidad Medica
            builder.Services.AddScoped<IMedicationsServices, MedicationService>();//Servicios de medicamentos
            builder.Services.AddScoped<IMedicalOrdersServices, MedicalOrdersServices>();//Servicios de Orden Medica
            builder.Services.AddScoped<IPermissionsServices, PermissionsService>(); //Servicios de Permisos
            builder.Services.AddScoped<IMedicalHistoryServices, MedicalHistoryService>(); //Servicios de Historia Clínica
            builder.Services.AddScoped<IRolPermissionsServices, RolPermissionServices>(); //Servicios de RolesPermisos

            //Helpers
            builder.Services.AddScoped<IConvertHelper, ConvertHelper>();
            builder.Services.AddScoped<ICombosHelpers, CombosHelper>();
        }


        public static WebApplication AddCustomWebAppConfiguration(this WebApplication app)
        {
            app.UseNotyf();
            return app;
        }


    }
}
