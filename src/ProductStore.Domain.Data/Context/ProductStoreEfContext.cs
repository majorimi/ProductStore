using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using ProductStore.Domain.Models;

namespace ProductStore.Domain.Data.Context
{
	public partial class ProductStoreEfContext : DbContext, IProductStoreContext
	{
		public IDbSet<ProductCategory> ProductCategories { get; set; }
		public IDbSet<Product> Products { get; set; }
		public IDbSet<ProductImage> ProductImages { get; set; }
		public IDbSet<ProductRating> ProductRatings { get; set; }

		public DbContext DbContext => this;

		public ProductStoreEfContext()
			: base("name=ProductStoreDb")
		{
			Configuration.LazyLoadingEnabled = false;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(100).IsUnicode(true).IsVariableLength().IsRequired();
			modelBuilder.Entity<Product>().Property(p => p.Description).HasMaxLength(200).IsUnicode(true).IsVariableLength().IsOptional();
			modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired().HasPrecision(7, 2);
			modelBuilder.Entity<Product>().Property(p => p.CreatedAtUtc).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).IsRequired();
			modelBuilder.Entity<Product>().Property(p => p.Deleted).IsRequired();
			modelBuilder.Entity<Product>()
				.HasKey(k => k.Id)
				.ToTable("Products")
				.HasRequired(r => r.ProductCategory).WithMany(m => m.Products).WillCascadeOnDelete(false);


			modelBuilder.Entity<ProductCategory>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			modelBuilder.Entity<ProductCategory>().Property(p => p.Name).HasMaxLength(100).IsUnicode(true).IsVariableLength().IsRequired();
			modelBuilder.Entity<ProductCategory>().Property(p => p.CreatedAtUtc).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
			modelBuilder.Entity<ProductCategory>()
				.HasKey(k => k.Id)
				.ToTable("ProductCategories");

			modelBuilder.Entity<ProductRating>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			modelBuilder.Entity<ProductRating>().Property(p => p.Rating).IsRequired();
			modelBuilder.Entity<ProductRating>().Property(p => p.CustomerId).IsRequired();
			modelBuilder.Entity<ProductRating>().Property(p => p.CreatedAtUtc).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
			modelBuilder.Entity<ProductRating>()
				.HasKey(k => k.Id)
				.ToTable("ProductRatings")
				.HasRequired(r => r.Product).WithMany(m => m.ProductRatings).WillCascadeOnDelete(false);

			modelBuilder.Entity<ProductImage>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			modelBuilder.Entity<ProductImage>().Property(p => p.Image).IsRequired();
			modelBuilder.Entity<ProductImage>().Property(p => p.CreatedAtUtc).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
			modelBuilder.Entity<ProductImage>()
				.HasKey(k => k.Id)
				.ToTable("ProductImages")
				.HasRequired(r => r.Product).WithMany(m => m.ProductImages).WillCascadeOnDelete(false);
		}
	}
}