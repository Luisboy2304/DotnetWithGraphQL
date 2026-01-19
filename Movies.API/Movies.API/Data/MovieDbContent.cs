using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.API.Data
{
    //public class ManagerContextFactory : IDesignTimeDbContextFactory<ManagerContext>
    //{
    //	public ManagerContext CreateDbContext(string[] args)
    //	{
    //		var optionsBuilder = new DbContextOptionsBuilder<ManagerContext>();

    //		// mesma string de conexão que você usou no OnConfiguring
    //		optionsBuilder.UseSqlServer(
    //			"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MoviesCatalog;User ID=sa;Password=mudar123;TrustServerCertificate=True;");

    //		return new ManagerContext(optionsBuilder.Options);
    //	}
    //}
    public class MovieDbContent:DbContext
    {

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieReview> Reviews { get; set; }
        public MovieDbContent(DbContextOptions<MovieDbContent> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().Property(e => e.Genre).HasConversion<string>();
        }
    }
}
// cria os models e dbcontext depois cria os types depois cria queries depois schema