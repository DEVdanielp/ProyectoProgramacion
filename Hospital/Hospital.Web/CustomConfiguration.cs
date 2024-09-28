using Hospital.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Web
{
    public static class CustomConfiguration
    {
        public static WebApplicationBuilder AddCustomBuilderConfiguration(this WebApplicationBuilder builder)
        {
            //Data Context
            builder.Services.AddDbContext<DataContext>(Configuration =>
            {
                Configuration.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
            });
            return builder;
        }
    }
}
