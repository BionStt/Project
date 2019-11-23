using Microsoft.EntityFrameworkCore;

namespace Project
{
    public sealed class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
