using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Product
{
    public int ProductID { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

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
    }

    public bool RemoveProduct(int productId)
    {
        var product = products.FirstOrDefault(p => p.ProductID == productId);
        if (product != null)
        {
            products.Remove(product);
            return true;
        }
        return false;
    }

    public Product? SearchProduct(int productId)
    {
        return products.FirstOrDefault(p => p.ProductID == productId);
    }

    public List<Product> SearchProduct(string name)
    {
        return products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public void UpdateProduct(Product updatedProduct)
    {
        var product = products.FirstOrDefault(p => p.ProductID == updatedProduct.ProductID);
        if (product != null)
        {
            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.Quantity = updatedProduct.Quantity;
            product.Price = updatedProduct.Price;
        }
    }

    public List<Product> GetAllProducts()
    {
        return new List<Product>(products);
    }

    public void SaveToFile(string filePath)
    {
        var json = JsonSerializer.Serialize(products);
        File.WriteAllText(filePath, json);
    }

    public void LoadFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            products = JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }
    }
}

public class Program
{
    private static void Main()
    {
        Inventory inventory = new Inventory();
        const string filePath = "inventory.json";
        inventory.LoadFromFile(filePath);

        while (true)
        {
            Console.WriteLine("\nInventory Management System");
            Console.WriteLine("1. Add New Product");
            Console.WriteLine("2. Remove Product");
            Console.WriteLine("3. Search for Product");
            Console.WriteLine("4. Update Product Information");
            Console.WriteLine("5. Display All Products");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    AddProduct(inventory);
                    break;
                case "2":
                    RemoveProduct(inventory);
                    break;
                case "3":
                    SearchProduct(inventory);
                    break;
                case "4":
                    UpdateProduct(inventory);
                    break;
                case "5":
                    DisplayAllProducts(inventory);
                    break;
                case "6":
                    inventory.SaveToFile(filePath);
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void AddProduct(Inventory inventory)
    {
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine()!, out int productId))
        {
            Console.WriteLine("Invalid Product ID.");
            return;
        }

        Console.Write("Enter Product Name: ");
        string productName = Console.ReadLine()!;
        Console.Write("Enter Product Description: ");
        string productDescription = Console.ReadLine()!;
        Console.Write("Enter Product Quantity: ");
        if (!int.TryParse(Console.ReadLine()!, out int productQuantity))
        {
            Console.WriteLine("Invalid Product Quantity.");
            return;
        }

        Console.Write("Enter Product Price: ");
        if (!decimal.TryParse(Console.ReadLine()!, out decimal productPrice))
        {
            Console.WriteLine("Invalid Product Price.");
            return;
        }

        var product = new Product
        {
            ProductID = productId,
            Name = productName,
            Description = productDescription,
            Quantity = productQuantity,
            Price = productPrice
        };

        inventory.AddProduct(product);
        Console.WriteLine("Product added successfully.");
    }

    static void RemoveProduct(Inventory inventory)
    {
        Console.Write("Enter Product ID to remove: ");
        if (!int.TryParse(Console.ReadLine()!, out int productId))
        {
            Console.WriteLine("Invalid Product ID.");
            return;
        }

        if (inventory.RemoveProduct(productId))
        {
            Console.WriteLine("Product removed successfully.");
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    static void SearchProduct(Inventory inventory)
    {
        Console.Write("Enter Product ID to search or Product Name: ");
        string input = Console.ReadLine()!;
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

    static void UpdateProduct(Inventory inventory)
    {
        Console.Write("Enter Product ID to update: ");
        if (!int.TryParse(Console.ReadLine()!, out int productId))
        {
            Console.WriteLine("Invalid Product ID.");
            return;
        }

        var product = inventory.SearchProduct(productId);
        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        Console.Write("Enter new Product Name: ");
        string productName = Console.ReadLine()!;
        Console.Write("Enter new Product Description: ");
        string productDescription = Console.ReadLine()!;
        Console.Write("Enter new Product Quantity: ");
        if (!int.TryParse(Console.ReadLine()!, out int productQuantity))
        {
            Console.WriteLine("Invalid Product Quantity.");
            return;
        }

        Console.Write("Enter new Product Price: ");
        if (!decimal.TryParse(Console.ReadLine()!, out decimal productPrice))
        {
            Console.WriteLine("Invalid Product Price.");
            return;
        }

        var updatedProduct = new Product
        {
            ProductID = productId,
            Name = productName,
            Description = productDescription,
            Quantity = productQuantity,
            Price = productPrice
        };

        inventory.UpdateProduct(updatedProduct);
        Console.WriteLine("Product updated successfully.");
    }

    static void DisplayAllProducts(Inventory inventory)
    {
        var products = inventory.GetAllProducts();
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
