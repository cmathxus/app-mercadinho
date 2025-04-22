using Mercadinho.models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext {
    
    public DbSet<Produto> Produtos {get; set;}
    public DbSet<Log> Logs {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=banco.sqlite");
}