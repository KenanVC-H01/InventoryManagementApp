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

    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nInventory Management System");
                Console.WriteLine("1. Add New Product");
                Console.WriteLine("2. Remove Product");
                Console.WriteLine("3. Search for Product");
                Console.WriteLine("4. Update Product Information");
                Console.WriteLine("5. Display All Products");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewProduct(inventory);
                        break;
                    case "2":
                        RemoveProduct(inventory);
                        break;
                    case "3":
                        SearchProduct(inventory);
                        break;
                    case "4":
                        UpdateProductInformation(inventory);
                        break;
                    case "5":
                        inventory.DisplayAllProducts();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddNewProduct(Inventory inventory)
        {
            Console.Write("Enter Product ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Product Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Product Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("Enter Product Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Product product = new Product(id, name, description, quantity, price);
            inventory.AddProduct(product);
        }

        static void RemoveProduct(Inventory inventory)
        {
            Console.Write("Enter Product ID to remove: ");
            int id = int.Parse(Console.ReadLine());
            inventory.RemoveProduct(id);
        }

        static void SearchProduct(Inventory inventory)
        {
            Console.Write("Enter Product ID to search or Product Name: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int id))
            {
                var product = inventory.SearchProduct(id);
                if (product != null)
                {
                    Console.WriteLine(product);
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            else
            {
                var products = inventory.SearchProduct(input);
                if (products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        Console.WriteLine(product);
                    }
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
        }

        static void UpdateProductInformation(Inventory inventory)
        {
            Console.Write("Enter Product ID to update: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter New Product Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter New Product Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter New Product Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("Enter New Product Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            inventory.UpdateProduct(id, name, description, quantity, price);
        }
    }
}