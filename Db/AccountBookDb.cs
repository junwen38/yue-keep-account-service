using Microsoft.EntityFrameworkCore;
using YueKeepAccountService.Models;

namespace YueKeepAccountService.Db
{
  public class AccountBookDb : DbContext
  {
    public DbSet<Category> Categories
    {
      get;
      set;
    }

    public DbSet<Book> Books
    {
      get;
      set;
    }

    public DbSet<Item> Items
    {
      get;
      set;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseMySQL($"server={DbHost};user id={DbUsername};password={DbPassword};database={DbName}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Category>().HasOne<Category>(r => r.Parent).WithMany(r => r.Children).HasForeignKey(r => r.ParentId);
      modelBuilder.Entity<Category>().HasOne<Book>(r => r.Book).WithMany().HasForeignKey(r => r.BookId);
      modelBuilder.Entity<Item>().HasOne<Category>(r => r.Category1).WithMany().HasForeignKey(r => r.Category1Id);
      modelBuilder.Entity<Item>().HasOne<Category>(r => r.Category2).WithMany().HasForeignKey(r => r.Category2Id);
    }

    public static string DbHost { get; set; }

    public static string DbName { get; set; }

    public static string DbUsername { get; set; }

    public static string DbPassword { get; set; }
  }
}
