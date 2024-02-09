using Microsoft.EntityFrameworkCore;

namespace WsApiexamen
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Examen> Examenes { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Examen>().ToTable("tblExamen");
        }
    }
}
