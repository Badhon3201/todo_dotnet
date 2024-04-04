using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext:IdentityDbContext<ApplicationUser,ApplicationRole,int>{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }

    public DbSet<Person> Persons { get; set; }

    public DbSet<Todo> Todos { get; set; }
}