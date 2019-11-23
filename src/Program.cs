using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Project
{
    public static class Program
    {
        public static void Main()
        {
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(x => x.UseStartup<Startup>()).Build().Run();
        }
    }
}
