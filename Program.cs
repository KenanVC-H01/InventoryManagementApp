using System;

namespace InventoryManagement
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Product(int id, string name, string description, int quantity, decimal price)
        {
            ProductID = id;
            Name = name;
            Description = description;
            Quantity = quantity;
            Price = price;
        }

        public override string ToString()
        {
            return $"ID: {ProductID}, Name: {Name}, Description: {Description}, Quantity: {Quantity}, Price: {Price:C}";
        }
    }
}