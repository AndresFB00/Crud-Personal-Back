using WebProyect.Models;

namespace WebProyect.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            db.Database.EnsureCreated();

            if (!db.Categories.Any())
            {
                db.Categories.AddRange(
                    new Category { Name = "Electrónica" },
                    new Category { Name = "Hogar" },
                    new Category { Name = "Deporte" },
                    new Category { Name = "Electrodomésticos" },
                    new Category { Name = "Tecnología" }
                );
                db.SaveChanges();
            }

            if (!db.Products.Any())
            {
                var categories = db.Categories.ToList();
                db.Products.AddRange(
                    new Product { Name = "Producto 1", Price = 100, Stock = 10, Description = "Auriculares inalámbricos", CategoryId = categories.First(c => c.Name == "Electrónica").Id },
                    new Product { Name = "Producto 2", Price = 200, Stock = 5, Description = "Lámpara LED", CategoryId = categories.First(c => c.Name == "Hogar").Id },
                    new Product { Name = "Producto 3", Price = 150, Stock = 20, Description = "Balón de fútbol", CategoryId = categories.First(c => c.Name == "Deporte").Id },
                    new Product { Name = "Producto 4", Price = 250, Stock = 8, Description = "Tostadora", CategoryId = categories.First(c => c.Name == "Electrodomésticos").Id },
                    new Product { Name = "Producto 5", Price = 300, Stock = 15, Description = "Tablet 10 pulgadas", CategoryId = categories.First(c => c.Name == "Tecnología").Id }
                );
                db.SaveChanges();
            }
        }
    }
}
