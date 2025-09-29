using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagementSystem.Enums;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem
{
    public class Inventory
    {
        private readonly ApplicationDbContext context;

        public Inventory()
        {
            context = new ApplicationDbContext();
        }

        public void AddProduct(string name, decimal price, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Product name cannot be empty.");
                return;
            }

           
            bool exists = context.Products
                .Any(p => p.Name.ToLower() == name.ToLower());

            if (exists)
            {
                Console.WriteLine("Product with this name already exists.");
                return;
            }

            if (price < 0 || quantity < 0)
            {
                Console.WriteLine("Price and Quantity cannot be negative.");
                return;
            }

            var product = new Product { Name = name, Price = price, Quantity = quantity };
            context.Products.Add(product);
            context.SaveChanges();

            Console.WriteLine($"Product '{name}' added successfully with ID: {product.Id}");
        }

        public void ViewAllProducts(SortOption sortBy = SortOption.Id)
        {
            var products = context.Products.AsQueryable();

            if (!products.Any())
            {
                Console.WriteLine("No products in inventory.");
                return;
            }

            products = sortBy switch
            {
                SortOption.Name => products.OrderBy(p => p.Name),
                SortOption.Price => products.OrderBy(p => p.Price),
                SortOption.Quantity => products.OrderBy(p => p.Quantity),
                _ => products.OrderBy(p => p.Id)
            };

            Console.WriteLine("\nInventory:");
            Console.WriteLine("==========================================");
            foreach (var product in products.ToList())
            {
                Console.WriteLine(product);
            }
            Console.WriteLine("==========================================");
        }

        public void EditProduct(int id, string? newName = null, decimal? newPrice = null, int? newQuantity = null)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(newName))
            {
                bool exists = context.Products
                    .Any(p => p.Name.ToLower() == newName.ToLower() && p.Id != id);

                if (exists)
                {
                    Console.WriteLine("Another product with this name already exists.");
                    return;
                }

                product.Name = newName;
            }

            if (newPrice.HasValue)
            {
                if (newPrice.Value < 0)
                {
                    Console.WriteLine("Price cannot be negative.");
                    return;
                }
                product.Price = newPrice.Value;
            }

            if (newQuantity.HasValue)
            {
                if (newQuantity.Value < 0)
                {
                    Console.WriteLine("Quantity cannot be negative.");
                    return;
                }
                product.Quantity = newQuantity.Value;
            }

            context.SaveChanges();
            Console.WriteLine($"Product with ID {id} updated successfully.");
        }

        public void DeleteProduct(int id)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            context.Products.Remove(product);
            context.SaveChanges();
            Console.WriteLine($"Product '{product.Name}' deleted successfully.");
        }

        public void SearchProduct(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Console.WriteLine("Search term cannot be empty.");
                return;
            }

           
            var foundProducts = context.Products
                .Where(p => p.Name.ToLower().Contains(searchTerm.ToLower())
                         || p.Id.ToString().Contains(searchTerm)).AsNoTracking()
                .ToList();

            if (!foundProducts.Any())
            {
                Console.WriteLine("No products found matching your search.");
                return;
            }

            Console.WriteLine($"\n Found {foundProducts.Count} product(s):");
            Console.WriteLine("==========================================");
            foreach (var product in foundProducts)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine("==========================================");
        }

        public Product? FindProductById(int id)
        {
            return context.Products
                .FirstOrDefault(p => p.Id == id);
        }

        public Product? FindProductByName(string name)
        {
            return context.Products
                .FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
        }
    }
}
