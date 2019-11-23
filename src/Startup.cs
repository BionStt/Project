using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Project
{
    public class Startup
    {
        public void Configure(IApplicationBuilder application, Context context)
        {
            application.UseCors(cors => cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            application.UseHsts();
            application.UseHttpsRedirection();
            application.UseRouting();
            application.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Seed();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddDbContextPool<Context>(options => options.UseSqlite("Data Source = Database.db"));
        }
    }
}
