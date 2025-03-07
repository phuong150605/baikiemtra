using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HangHoaManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HangHoaManagement.Data
{
    public class GoodDbContext : DbContext
    {
        public GoodDbContext(DbContextOptions<GoodDbContext> options) : base(options)
        {
        }

        public DbSet<HangHoaManagement.Models.hang_hoa> hang_hoa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình thêm nếu cần
            modelBuilder.Entity<hang_hoa>()
                .HasKey(h => h.ma_hanghoa);

            modelBuilder.Entity<hang_hoa>()
                .Property(h => h.ten_hanghoa)
                .IsRequired();
        }
    }
}