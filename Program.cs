using System;
using System.Collections.Generic;
using System.Linq;

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

    public class Inventory
    {
        private List<Product> products = new List<Product>();

        public void AddProduct(Product product)
        {
            products.Add(product);
            Console.WriteLine("Product added successfully.");
        }

        public void RemoveProduct(int productId)
        {
            var product = products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                products.Remove(product);
                Console.WriteLine("Product removed successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        public Product SearchProduct(int productId)
        {
            var product = products.FirstOrDefault(p => p.ProductID == productId);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }
            return product;
        }

        public List<Product> SearchProduct(string name)
        {
            return products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void UpdateProduct(int productId, string name, string description, int quantity, decimal price)
        {
            var product = products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                product.Name = name;
                product.Description = description;
                product.Quantity = quantity;
                product.Price = price;
                Console.WriteLine("Product updated successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        public void DisplayAllProducts()
        {
            if (products.Count == 0)
            {
                Console.WriteLine("No products in inventory.");
            }
            else
            {
                foreach (var product in products)
                {
                    Console.WriteLine(product);
                }
            }
        }
    }
}