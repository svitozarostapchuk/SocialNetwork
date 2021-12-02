using DAL.EF.Configurations;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts
{
    public class SocialNetworkDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public SocialNetworkDbContext(DbContextOptions<SocialNetworkDbContext> options)
            : base(options)
        {
        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Friendship> Frienships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}

