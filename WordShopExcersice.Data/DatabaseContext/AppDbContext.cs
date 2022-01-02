using Microsoft.EntityFrameworkCore;
using WordShopExcersice.Data.Models;

namespace WordShopExcersice.Data.DatabaseContext;

public class AppDbContext : DbContext
{
    private static bool _created;

    public AppDbContext()
    {
        if (!File.Exists("../PostCode.db"))
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        
    }

    public AppDbContext(DbContextOptions opt) : base(opt)
    {
    }

    public virtual DbSet<PostCodeModel?> PostCode { get; set; }

   
    protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
    {
        optionbuilder.UseSqlite(@"Data Source=../PostCode.db;");
    }
}