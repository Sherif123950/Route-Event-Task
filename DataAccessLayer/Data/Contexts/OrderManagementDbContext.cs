using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Contexts
{
	public class OrderManagementDbContext:IdentityDbContext<User>
	{
        public OrderManagementDbContext()
        {
            
        }
        public OrderManagementDbContext(DbContextOptions<OrderManagementDbContext> options):base(options)
        {
            
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order>  Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<Product>()
			.Property(p => p.Price)
			.HasColumnType("decimal(18,2)");

			builder.Entity<OrderItem>()
				.Property(oi => oi.UnitPrice)
				.HasColumnType("decimal(18,2)");

			builder.Entity<OrderItem>()
				.Property(oi => oi.Discount)
				.HasColumnType("decimal(18,2)");

			builder.Entity<Order>()
				.Property(o => o.TotalAmount)
				.HasColumnType("decimal(18,2)");

			builder.Entity<Invoice>()
				.Property(i => i.TotalAmount)
				.HasColumnType("decimal(18,2)");
			builder.Entity<Order>()
			.HasKey(o => o.Id);
			builder.Entity<Order>()
				.Property(o => o.Id)
				.ValueGeneratedOnAdd();
			builder.Entity<Product>()
				.Property(p => p.Id)
				.ValueGeneratedOnAdd();
			builder.Entity<Customer>()
				.Property(c => c.Id)
				.ValueGeneratedOnAdd();
		
		}
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
  //          if (!optionsBuilder.IsConfigured)
  //          {
  //              optionsBuilder.UseInMemoryDatabase("OrderManagementSystem");
  //          }
  //          base.OnConfiguring(optionsBuilder);
		//}
	}
    
}
