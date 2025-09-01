namespace InventoryManagementSystem
{
    public class Product
    {
        private static int nextId = 1;

        public int Id { get; private set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(string name, decimal price, int quantity)
        {
            Id = nextId++;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Price: ${Price:F2}, Quantity: {Quantity}";
        }
    }
}