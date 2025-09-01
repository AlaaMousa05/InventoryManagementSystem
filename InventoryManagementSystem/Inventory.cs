using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagementSystem.Enums;

namespace InventoryManagementSystem
{
    public class Inventory
    {
        private List<Product> products;

        public Inventory()
        {
            products = new List<Product>();
        }

        public void AddProduct(string name, decimal price, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: Product name cannot be empty.");
                return;
            }

            if (products.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Error: Product with this name already exists.");
                return;
            }

            if (price < 0)
            {
                Console.WriteLine("Error: Price cannot be negative.");
                return;
            }

            if (quantity < 0)
            {
                Console.WriteLine("Error: Quantity cannot be negative.");
                return;
            }

            products.Add(new Product(name, price, quantity));
            Console.WriteLine($"Product '{name}' added successfully with ID: {products.Last().Id}");
        }

        public void ViewAllProducts(SortOption sortBy = SortOption.Id)
        {
            if (products.Count == 0)
            {
                Console.WriteLine("No products in inventory.");
                return;
            }

            var sortedProducts = sortBy switch
            {
                SortOption.Name => products.OrderBy(p => p.Name),
                SortOption.Price => products.OrderBy(p => p.Price),
                SortOption.Quantity => products.OrderBy(p => p.Quantity),
                _ => products.OrderBy(p => p.Id)
            };

            Console.WriteLine("\nInventory:");
            Console.WriteLine("==========================================");
            foreach (var product in sortedProducts)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine("==========================================");
        }

        public void EditProduct(int id, string? newName = null, decimal? newPrice = null, int? newQuantity = null)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine("Error: Product not found.");
                return;
            }

            if (newName != null)
            {
                if (string.IsNullOrWhiteSpace(newName))
                {
                    Console.WriteLine("Error: Product name cannot be empty.");
                    return;
                }

                if (products.Any(p => p.Name.Equals(newName, StringComparison.OrdinalIgnoreCase) && p.Id != id))
                {
                    Console.WriteLine("Error: Another product with this name already exists.");
                    return;
                }
                product.Name = newName;
            }

            if (newPrice.HasValue)
            {
                if (newPrice.Value < 0)
                {
                    Console.WriteLine("Error: Price cannot be negative.");
                    return;
                }
                product.Price = newPrice.Value;
            }

            if (newQuantity.HasValue)
            {
                if (newQuantity.Value < 0)
                {
                    Console.WriteLine("Error: Quantity cannot be negative.");
                    return;
                }
                product.Quantity = newQuantity.Value;
            }

            Console.WriteLine($"Product with ID {id} updated successfully.");
        }

        public void DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine("Error: Product not found.");
                return;
            }

            products.Remove(product);
            Console.WriteLine($"Product '{product.Name}' deleted successfully.");
        }

        public void SearchProduct(string searchTerm)
        {
            var foundProducts = products.Where(p =>
                p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Id.ToString().Contains(searchTerm)
            ).ToList();

            if (foundProducts.Count == 0)
            {
                Console.WriteLine("No products found matching your search.");
                return;
            }

            Console.WriteLine($"\nFound {foundProducts.Count} product(s):");
            Console.WriteLine("==========================================");
            foreach (var product in foundProducts)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine("==========================================");
        }

        public Product? FindProductById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        public Product? FindProductByName(string name)
        {
            return products.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}