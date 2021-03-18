using Microsoft.EntityFrameworkCore;
using YueKeepAccountService.Models;

namespace YueKeepAccountService.Db {
    public class AccountBookDb : DbContext {
        public DbSet<Category> Categories {
            get;
            set;
        }

        public DbSet<Book> Books {
            get;
            set;
        }

        public DbSet<Item> Items {
            get;
            set;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySQL("server=<数据库服务器>;user id=<数据库用户名>;password=<数据库密码>;database=<数据库名>");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Category>().HasOne<Category>(r=>r.Parent).WithMany(r=>r.Children).HasForeignKey(r=>r.ParentId);
            modelBuilder.Entity<Category>().HasOne<Book>(r=>r.Book).WithMany().HasForeignKey(r=>r.BookId);
            modelBuilder.Entity<Item>().HasOne<Category>(r=>r.Category1).WithMany().HasForeignKey(r=>r.Category1Id);
            modelBuilder.Entity<Item>().HasOne<Category>(r=>r.Category2).WithMany().HasForeignKey(r=>r.Category2Id);
        }
    }
}
