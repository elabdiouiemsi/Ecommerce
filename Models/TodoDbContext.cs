using Microsoft.EntityFrameworkCore;

public class TodoDbContext : DbContext
{
    public DbSet<Task> Tasks { get; set; }

    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }
}
