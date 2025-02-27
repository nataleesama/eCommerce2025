
using System;
using System.ComponentModel.Design;
using System.Data;
using System.Reflection;
using eCommerce.Models;
using Library.eCommerce.Services;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product?> inventory = ProductServiceProxy.Current.Products;
            Console.WriteLine("Welcome to eCommerce!");
            char mode;
            do
            {
                Console.WriteLine("Do you want to manage inventory(m), shop at the store(s), or exit(e)?");
                string? modeChoice = Console.ReadLine();
                mode = modeChoice[0];
                switch (mode)
                {
                    case 'M':
                    case 'm':
                        char choice;
                        do
                        {
                            Console.WriteLine("\n\n");
                            Console.WriteLine("C. Create a product.");
                            Console.WriteLine("R. Read all items.");
                            Console.WriteLine("U. Update product details.");
                            Console.WriteLine("D. Delete a product from inventory.");
                            Console.WriteLine("Q. Quit.");
                            string? input = Console.ReadLine();
                            choice = input[0];
                            switch (choice)
                            {
                                case 'C':
                                case 'c':
                                    Console.WriteLine("Enter Product Name: ");
                                    string? name = Console.ReadLine();
                                    Console.WriteLine("Enter Product Price: ");
                                    float priceFlt = float.Parse(Console.ReadLine() ?? "-1");
                                    double price = Math.Round(priceFlt, 2);
                                    Console.WriteLine("Enter Product Quantity: ");
                                    int quantity = int.Parse(Console.ReadLine() ?? "-1");
                                    ProductServiceProxy.Current.AddOrUpdate(new Product
                                    {

                                        Name = name,
                                        Price = price,
                                        Quantity = quantity

                                    });

                                    break;
                                case 'R':
                                case 'r':
                                    inventory.ForEach(Console.WriteLine);
                                    break;
                                case 'U':
                                case 'u':
                                    Console.WriteLine("Which product would you like to update?");
                                    int selection = int.Parse(Console.ReadLine() ?? "-1");
                                    var selectedProd = inventory.FirstOrDefault(p => p.Id == selection);
                                    if (selectedProd != null)
                                    {
                                        Console.WriteLine("Would you like to update the name(n), price(p), or quantity(q):");
                                        string? edit = Console.ReadLine();
                                        char updating = edit[0];
                                        switch (updating)
                                        {
                                            case 'n':
                                            case 'N':
                                                Console.WriteLine("Enter new name:");
                                                selectedProd.Name = Console.ReadLine() ?? $"{selectedProd.Name}";
                                                break;
                                            case 'p':
                                            case 'P':
                                                Console.WriteLine("Enter new price:");
                                                selectedProd.Price = float.Parse(Console.ReadLine() ?? $"{selectedProd.Price}");
                                                break;
                                            case 'q':
                                            case 'Q':
                                                Console.WriteLine("Enter new quantity:");
                                                selectedProd.Quantity = int.Parse(Console.ReadLine() ?? $"{selectedProd.Quantity}");
                                                break;
                                            default:
                                                Console.WriteLine("Invalid Choice");
                                                break;
                                        }

                                        ProductServiceProxy.Current.AddOrUpdate(selectedProd);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input");
                                    }

                                    break;
                                case 'D':
                                case 'd':
                                    Console.WriteLine("Which product would you like to delete?");
                                    selection = int.Parse(Console.ReadLine() ?? "-1");
                                    selectedProd = inventory.FirstOrDefault(p => p.Id == selection);
                                    if (selectedProd != null)
                                    {
                                        ProductServiceProxy.Current.Delete(selection);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Item");
                                    }
                                    break;
                                case 'Q':
                                case 'q':
                                    break;
                                default:
                                    Console.WriteLine("ERROR");
                                    break;
                            }
                        } while (choice != 'Q' && choice != 'q');
                        break;
                    case 'S':
                    case 's':
                        Dictionary<Product?, int> cart = CartServiceProxy.Current.Cart;
                        char action;
                        do
                        {
                            Console.WriteLine("\n\n");
                            Console.WriteLine("******INVENTORY******");
                            inventory.ForEach(Console.WriteLine);
                            Console.WriteLine("*********************");
                            Console.WriteLine("C. Add item to cart.");
                            Console.WriteLine("R. Read all items in cart.");
                            Console.WriteLine("U. Update product quantity in cart.");
                            Console.WriteLine("D. Delete all instance of item in cart.");
                            Console.WriteLine("Q. Checkout.");
                            string? input = Console.ReadLine();
                            action = input[0];
                            switch (action)
                            {
                                case 'c':
                                case 'C':

                                    Console.WriteLine("Which product would you like to add to your cart?");
                                    int selection = int.Parse(Console.ReadLine() ?? "-1");
                                    var selectedProd = inventory.FirstOrDefault(p => p.Id == selection);
                                    if (selectedProd == null)
                                    {
                                        Console.WriteLine("Invalid Item");
                                        break;
                                    }
                                    var cartCount = 1;
                                    if (selectedProd.Quantity > 0)
                                    {
                                        selectedProd.Quantity = selectedProd.Quantity - 1;
                                        CartServiceProxy.Current.AddToCart(selectedProd, 1);
                                        ProductServiceProxy.Current.AddOrUpdate(selectedProd);
                                    }
                                    else
                                    {
                                        Console.WriteLine("This item is currently sold out.");
                                    }
                                    break;
                                case 'r':
                                case 'R':
                                    Console.WriteLine("========CART=========");
                                    foreach (var pair in cart)
                                    {
                                        Console.WriteLine($"{pair.Key?.Id}) {pair.Value} {pair.Key?.Name}: ${pair.Key?.Price * pair.Value}");
                                    }
                                    Console.WriteLine("=====================");
                                    break;
                                case 'u':
                                case 'U':
                                    Console.WriteLine("Which product would you like to update?");
                                    selection = int.Parse(Console.ReadLine() ?? "-1");
                                    selectedProd = inventory.FirstOrDefault(p => p.Id == selection);
                                    if (selectedProd == null || cart.ContainsKey(selectedProd) == false)
                                    {
                                        Console.WriteLine("Invalid Item");
                                        break;
                                    }
                                    Console.WriteLine("How many would you like to have in your cart?");
                                    var amount = int.Parse(Console.ReadLine() ?? "-1");
                                    if (cart[selectedProd] > amount)
                                    {
                                        int add = CartServiceProxy.Current.Delete(selectedProd, amount);
                                        selectedProd.Quantity += add;
                                        ProductServiceProxy.Current.AddOrUpdate(selectedProd);
                                    }
                                    else
                                    {
                                        if (selectedProd.Quantity - (amount - cart[selectedProd]) >= 0)
                                        {
                                            int subtract = CartServiceProxy.Current.AddToCart(selectedProd, amount);
                                            selectedProd.Quantity -= subtract;
                                            ProductServiceProxy.Current.AddOrUpdate(selectedProd);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid Amount");
                                        }

                                    }
                                    break;
                                case 'd':
                                case 'D':
                                    Console.WriteLine("Which product would you like to delete?");
                                    selection = int.Parse(Console.ReadLine() ?? "-1");
                                    var selProd = inventory.FirstOrDefault(p => p.Id == selection);
                                    if (selProd == null || cart.ContainsKey(selProd) == false)
                                    {
                                        Console.WriteLine("Invalid Item");
                                        break;
                                    }
                                    int total = CartServiceProxy.Current.Delete(selProd, -1);
                                    selProd.Quantity += total;
                                    ProductServiceProxy.Current.AddOrUpdate(selProd);
                                    break;
                                case 'q':
                                case 'Q':
                                    double sum = 0;
                                    Console.WriteLine("\n");
                                    Console.WriteLine("#######RECEIPT#######");
                                    foreach (var pair in cart)
                                    {
                                        Console.WriteLine($"{pair.Value} {pair.Key.Name}: ${pair.Key.Price * pair.Value}");
                                        sum += pair.Key.Price * pair.Value;
                                    }
                                    double tax = Math.Round(sum * 0.07, 2);
                                    Console.WriteLine($"7% Sales Tax: {Math.Round(tax, 2)}");
                                    Console.WriteLine($"Total: ${Math.Round(sum + tax, 2)}");
                                    Console.WriteLine("#####################");
                                    CartServiceProxy.Current.Drop();
                                    break;
                                default:
                                    Console.WriteLine("ERROR");
                                    break;
                            }

                        } while (action != 'Q' && action != 'q');

                        break;
                    case 'e':
                    case 'E':
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
                Console.WriteLine("\n\n");
            } while (mode != 'e' && mode != 'E');
        }
    }
}