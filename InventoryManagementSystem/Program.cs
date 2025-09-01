
using InventoryManagementSystem.Enums;

namespace InventoryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            bool running = true;

            Console.WriteLine("=== Inventory Management System ===");

            while (running)
            {
                Console.WriteLine("\nMain Menu:");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. View All Products");
                Console.WriteLine("3. Edit Product");
                Console.WriteLine("4. Delete Product");
                Console.WriteLine("5. Search Product");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option (1-6): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProductMenu(inventory);
                        break;
                    case "2":
                        ViewProductsMenu(inventory);
                        break;
                    case "3":
                        EditProductMenu(inventory);
                        break;
                    case "4":
                        DeleteProductMenu(inventory);
                        break;
                    case "5":
                        SearchProductMenu(inventory);
                        break;
                    case "6":
                        running = false;
                        Console.WriteLine("Thank you for using the Inventory Management System!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddProductMenu(Inventory inventory)
        {
            Console.WriteLine("\n--- Add New Product ---");

            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            Console.Write("Enter product price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price format.");
                return;
            }

            Console.Write("Enter product quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity format.");
                return;
            }

            inventory.AddProduct(name, price, quantity);
        }

        static void ViewProductsMenu(Inventory inventory)
        {
            Console.WriteLine("\n--- View Products ---");
            Console.WriteLine("Sort by: 1. ID (default) 2. Name 3. Price 4. Quantity");
            Console.Write("Choose sorting option (1-4): ");

            string sortOption = Console.ReadLine();
            SortOption sortBy = sortOption switch
            {
                "2" => SortOption.Name,
                "3" => SortOption.Price,
                "4" => SortOption.Quantity,
                _ => SortOption.Id
            };

            inventory.ViewAllProducts(sortBy);
        }

        static void EditProductMenu(Inventory inventory)
        {
            Console.WriteLine("\n--- Edit Product ---");
            Console.Write("Enter product ID to edit: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            var product = inventory.FindProductById(id);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.WriteLine($"Editing product: {product}");
            Console.WriteLine("Leave field blank to keep current value.");

            Console.Write($"New name (current: {product.Name}): ");
            string newName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newName)) newName = null;

            Console.Write($"New price (current: {product.Price}): ");
            string priceInput = Console.ReadLine();
            decimal? newPrice = null;
            if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal tempPrice))
            {
                newPrice = tempPrice;
            }

            Console.Write($"New quantity (current: {product.Quantity}): ");
            string quantityInput = Console.ReadLine();
            int? newQuantity = null;
            if (!string.IsNullOrWhiteSpace(quantityInput) && int.TryParse(quantityInput, out int tempQuantity))
            {
                newQuantity = tempQuantity;
            }

            inventory.EditProduct(id, newName, newPrice, newQuantity);
        }

        static void DeleteProductMenu(Inventory inventory)
        {
            Console.WriteLine("\n--- Delete Product ---");
            Console.Write("Enter product ID to delete: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            Console.Write("Are you sure you want to delete this product? (y/n): ");
            string confirmation = Console.ReadLine();

            if (confirmation.ToLower() == "y")
            {
                inventory.DeleteProduct(id);
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }
        }

        static void SearchProductMenu(Inventory inventory)
        {
            Console.WriteLine("\n--- Search Product ---");
            Console.Write("Enter product name or ID to search: ");
            string searchTerm = Console.ReadLine();

            inventory.SearchProduct(searchTerm);
        }
    }
}