namespace InventoryManagementSystem
{
    public class Product
    {
       

        public int Id { get;set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

     
        public Product() { }

       
        public Product(string name, decimal price, int quantity)
        {
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