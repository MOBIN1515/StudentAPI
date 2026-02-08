using Microsoft.EntityFrameworkCore;
using StudentAPI.Entity;
namespace StudentAPI.Data;

public class AppDbContext:DbContext
{
   public AppDbContext(DbContextOptions<AppDbContext> option ) : base( option ) { }
  
    public DbSet<Student>Students { get; set; }
    public DbSet<Course>Course { get; set; }
    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
                    .Property(s => s.RowVersion)
                    .IsRowVersion();


        //modelBuilder.Entity<Student>()
        //    .Property(s => s.CreatedAt)
        //    .HasDefaultValueSql("GETUTCDATE()");





        //modelBuilder.Entity<Student>().Property<DateTime>("CreatedAt");

        //modelBuilder.Entity<Student>().HasQueryFilter(s => s.IsDeleted);

        //modelBuilder.Entity<Student>().HasIndex(c => c.CourseId);

        //modelBuilder.Entity<Student>().Property(r => r.RowVersion)
        //    .IsRowVersion();
    }
}
