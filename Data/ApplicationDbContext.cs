using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext:DbContext{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }

    public DbSet<Person> Persons { get; set; }

    public DbSet<Todo> Todos { get; set; }
}