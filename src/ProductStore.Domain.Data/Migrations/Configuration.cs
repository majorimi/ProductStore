using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using ProductStore.Domain.Data.Context;
using ProductStore.Domain.Data.Extensions;
using ProductStore.Domain.Models;

namespace ProductStore.Domain.Data.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<ProductStoreEfContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
			SetSqlGenerator("System.Data.SqlClient", new ProductStoreEfContext.CustomSqlServerMigrationSqlGenerator());
			ContextType = typeof(ProductStoreEfContext);
		}

		protected override void Seed(ProductStoreEfContext context)
		{
			var insertTestData = bool.Parse(ConfigurationManager.AppSettings["InsertTestData"]);
			if (insertTestData)
			{
				var baseDir = ConfigurationManager.AppSettings["TestImagesPath"];
				byte[] bytes1 = null;
				byte[] bytes2 = null;
				byte[] bytes3 = null;

				if (Directory.Exists(ConfigurationManager.AppSettings["TestImagesPath"]))
				{
					var converter = new ImageConverter();

					var img1 = Bitmap.FromFile(Path.Combine(baseDir, "Nokia7Plus_1.jpg"));
					var img2 = Bitmap.FromFile(Path.Combine(baseDir, "Nokia7Plus_2.jpg"));
					var img3 = Bitmap.FromFile(Path.Combine(baseDir, "SamsungGalaxy.jpg"));

					bytes1 = (byte[])converter.ConvertTo(img1, typeof(byte[]));
					bytes2 = (byte[])converter.ConvertTo(img2, typeof(byte[]));
					bytes3 = (byte[])converter.ConvertTo(img3, typeof(byte[]));
				}

				var prod1 = new Product()
				{
					Name = "Samsung Galaxy",
					Description = "Samsung Galaxy mobile phone",
					Price = 299.89m,
					Deleted = false,
					ProductRatings = new List<ProductRating>()
					{
						new ProductRating() {CustomerId = 2, Rating = 4.5, CreatedAtUtc = DateTime.UtcNow},
						new ProductRating() {CustomerId = 3, Rating = 3, CreatedAtUtc = DateTime.UtcNow}
					},
					CreatedAtUtc = DateTime.UtcNow,
				};
				var prod2 = new Product()
				{
					Name = "Nokia 7 Plus",
					Description = "Nokia 7 Plus mobile phone",
					Price = 99.89m,
					Deleted = false,
					ProductRatings = new List<ProductRating>()
					{
						new ProductRating() {CustomerId = 3, Rating = 4, CreatedAtUtc = DateTime.UtcNow}
					},
					CreatedAtUtc = DateTime.UtcNow,
				};

				if (bytes1 != null && bytes2 != null)
				{
					prod2.ProductImages.Add(new ProductImage() { Image = bytes1, CreatedAtUtc =  DateTime.UtcNow});
					prod2.ProductImages.Add(new ProductImage() { Image = bytes2, CreatedAtUtc =  DateTime.UtcNow });
				}
				if (bytes3 != null)
				{
					prod1.ProductImages.Add(new ProductImage() { Image = bytes3, CreatedAtUtc = DateTime.UtcNow });
				}


				var cat1 = new ProductCategory()
				{
					Name = "Mobile Phones",
					Products = new List<Product>()
					{
						prod1, prod2
					},
					CreatedAtUtc = DateTime.UtcNow
				};
				context.ProductCategories.AddIfNotExists(p => p.Name, cat1);

				context.ProductCategories.AddIfNotExists(p => p.Name,
					new ProductCategory()
					{
						Name = "Tablets",
						CreatedAtUtc = DateTime.UtcNow
					});
				context.ProductCategories.AddIfNotExists(p => p.Name,
					new ProductCategory()
					{
						Name = "PC Games",
						Products = new List<Product>()
						{
							new Product()
							{
								Name = "Civilization VI",
								Description = "Civilization VI Gold edition",
								Price = 20m,
								Deleted = false,
								CreatedAtUtc = DateTime.UtcNow,
							}
						},
						CreatedAtUtc = DateTime.UtcNow
					});
			}
		}
	}
}
