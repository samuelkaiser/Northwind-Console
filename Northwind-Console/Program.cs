using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
// using NLog;
using System.Text;
using System.Threading.Tasks;
using NorthwindConsole.Models;

namespace NorthwindConsole
{
    class MainClass
    {
        //private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static object NLog { get; private set; }

        public static void Main(string[] args)
        {
            // logger.Info("Program started");
            try
            {
                string choice;
                do
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("1) Display Categories");
                    Console.WriteLine("2) Add Category");
                    Console.WriteLine("3) Display Category and related products");
                    Console.WriteLine("4) Display all Categories and their related products");
                    Console.WriteLine("5) Display all products.");
                    Console.WriteLine("6) Display product by ID");
                    Console.WriteLine("7) Add new product");
                    Console.WriteLine("8) Edit product by ID");
                    Console.WriteLine("9) Delete category by ID (and all containing products)");
                    Console.WriteLine("10) Delete a product by ID");
                    Console.WriteLine("\"q\" to quit");
                    choice = Console.ReadLine();
                    Console.Clear();

                    // logger.Info($"Option {choice} selected");

                    switch (choice) {
                        case "1": displayAllCategories(); break; // name and description
                        case "2": displayAllCategoriesAndProducts(); break;
                        case "3": displayCategoryAndProducts(); break;
                        case "4": addNewCategory(); break; 
                        case "5": displayAllProducts(); break; // all, active, or discontinued (if all, distinguish between active and disc.)
                        case "6": displayProductById(); break; // all relevant product details
                        case "7": addNewProduct(); break; // as it sounds
                        case "8": editExistingProductById(); break; // as it sounds
                        case "9": deleteCategoryAndAllChildProducts(); break; // as it sounds
                        case "10": break;
                        case "q": Console.WriteLine("thank you come again..."); break; // quietly mumble it back
                        case "Q": Console.WriteLine("THANK YOU COME AGAIN!!!"); break; // YELL IT BACK AT THEM
                        default: Console.WriteLine("Please choose a valid option."); break;
                    }
                    

                } while (choice.ToLower() != "q");
            }
            catch (Exception ex)
            {
               // logger.Error(ex.Message);
            }
            // logger.Info("Program ended");
        }
        public static void displayProductById() {

        }

        public static void editExistingProductById() {

        }

        public static void addNewProduct() {
            string response;
            
            // do
            // {
            Product product = new Product();

            do {
                Console.WriteLine("Enter Product Name:");
                response = Console.ReadLine();

                // set a minimum character length on product names
                if (response.Length < 5)
                {
                    Console.WriteLine("Product names must be atleast 5 characters. Try again.");
                }
                else {
                    product.ProductName = response;
                }

            } while (response.Length < 5);

            product.ProductName = Console.ReadLine();

            do
            {
                Console.WriteLine("Enter unit price.");
                response = Console.ReadLine();
                if (!((Convert.ToDecimal(response) % 1) > 0)) {
                    Console.WriteLine("Please enter a valid decimal");
                }
            } while (!((Convert.ToDecimal(response) % 1) > 0));

            // reusable "short" data type for various upcoming checks
            short numericCheck;
            do
            {
                // figuring out the units on hand using a short data type
                short stock = 0;
                Console.WriteLine("Enter units on hand");
                response = Console.ReadLine();
                if (!(short.TryParse(response, out numericCheck)))
                {
                    Console.WriteLine("Please enter a valid number.");
                }
                else {
                    short.TryParse(response, out stock);
                    product.UnitsInStock = stock;
                }
            } while (!(short.TryParse(response, out numericCheck)));

            do
            {
                // figuring out the units on hand using a short data type
                short onOrder = 0;

                Console.WriteLine("Enter units on order");
                response = Console.ReadLine();

                if (!(short.TryParse(response, out numericCheck)))
                {
                    Console.WriteLine("Please enter a valid number.");
                }
                else
                {
                    short.TryParse(response, out onOrder);
                    product.UnitsOnOrder = onOrder;
                }
            } while (!(short.TryParse(response, out numericCheck)));

            do
            {
                // figuring out the reorder level (don't actually know what it is just know it's a smallint
                short reorderLevel = 0;

                Console.WriteLine("Please enter Reorder level.");
                response = Console.ReadLine();

                if (!(short.TryParse(response, out numericCheck)))
                {
                    Console.WriteLine("Please enter a valid number.");
                }
                else
                {
                    short.TryParse(response, out reorderLevel);
                    product.UnitsOnOrder = reorderLevel;
                }
            } while (!(short.TryParse(response, out numericCheck)));

            do {
                Console.WriteLine("Is this product discontinued? y/n");
                response = Console.ReadLine();
            } while (response.ToLower() != "y" && response.ToLower() != "n");

            int categoryID;
            do {
                Console.WriteLine("Please enter a category ID:");
                response = Console.ReadLine();
                if (int.TryParse(response, out categoryID)) {

                }
            }while(!(int.TryParse(response, out categoryID)));

            var db = new NorthwindContext();
            db.Products.Add(product);
            db.SaveChanges();
                /**
                 * public int ProductID { get; set; } check
        public string ProductName { get; set; } check
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; } check
        public Int16? UnitsInStock { get; set; }
        public Int16? UnitsOnOrder { get; set; }
        public Int16? ReorderLevel { get; set; } check
        public bool Discontinued { get; set; } check

        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }
            */
                // logger.Info($"User has added a category called {category.CategoryName}.");

                /*Console.WriteLine("Add additional category? (y/n)");
                response = Console.ReadLine();
                counter++;
                // check if they'd like to add additional categories
                do
                {
                    Console.WriteLine("Please enter a valid response.  Add additional category? (y/n)");
                    response = Console.ReadLine();
                } while (response.ToLower() != "y" && response.ToLower() != "n");*/


          //  } while (response.ToLower() != "n");

            // logger.Info($"User has added {counter} total categories.");
        }

        public static void deleteCategoryAndAllChildProducts() {

        }
        public static void displayAllCategories() {
            var db = new NorthwindContext();
            var query = db.Categories.OrderBy(p => p.CategoryName);

            Console.WriteLine($"{query.Count()} records returned");
            foreach (var item in query)
            {
                Console.WriteLine($"{item.CategoryId}) {item.CategoryName} - {item.Description}");
            }
            // logger.Info($"User has displayed all categories.");
        }

        public static void addNewCategory() {
            string response;
            int counter = 0;
            do
            {
                
                Category category = new Category();
                Console.WriteLine("Enter Category Name:");
                category.CategoryName = Console.ReadLine();
                Console.WriteLine("Enter the Category Description:");
                category.Description = Console.ReadLine();

                var db = new NorthwindContext();
                db.Categories.Add(category);
                db.SaveChanges();

                // logger.Info($"User has added a category called {category.CategoryName}.");

                Console.WriteLine("Add additional category? (y/n)");
                response = Console.ReadLine();
                counter++;
                // check if they'd like to add additional categories
                do
                {
                    Console.WriteLine("Please enter a valid response.  Add additional category? (y/n)");
                    response = Console.ReadLine();
                } while (response.ToLower() != "y" && response.ToLower() != "n");

               
            } while (response.ToLower() != "n");

            // logger.Info($"User has added {counter} total categories.");
        }

        public static void displayCategoryAndProducts() {
            var db = new NorthwindContext();
            var query = db.Categories.OrderBy(p => p.CategoryId);

            Console.WriteLine("Select the category whose products you want to display:");
            foreach (var item in query)
            {
                Console.WriteLine($"{item.CategoryId}) {item.CategoryName}");
            }
            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            // logger.Info($"CategoryId {id} selected");
            Category category = db.Categories.FirstOrDefault(c => c.CategoryId == id);

            Console.WriteLine($"{category.CategoryName} - {category.Description}");

            foreach (Product p in category.Products)
            {
                Console.WriteLine(p.ProductName);
            }
        }

        public static void displayAllCategoriesAndProducts() {
            var db = new NorthwindContext();
            var query = db.Categories.Include("Products").OrderBy(p => p.CategoryId);
            foreach (var item in query)
            {
                Console.WriteLine($"{item.CategoryId}) {item.CategoryName}");
                foreach (Product p in item.Products)
                {
                    Console.WriteLine($"\t{p.ProductID}) {p.ProductName}");
                }
            }
        }

        public static void displayAllProducts() {
            string response;
            var db = new NorthwindContext();
            var query = db.Products.OrderBy(p => p.ProductID);
            do
            {
                Console.ForegroundColor = ConsoleColor.White; // relatively neutral color to represent both active and discontinued as an option
                Console.WriteLine("1) Display all products.");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("2) Display active products."); // differentiating active

                Console.ForegroundColor = ConsoleColor.Red; // differentiating discontinued
                Console.WriteLine("3) Display discontinued products.");

                Console.ForegroundColor = ConsoleColor.Cyan;

                response = Console.ReadLine();
            } while (response != "1" && response != "2" && response != "3");

            switch (response) {
                case "1": // all products by name
                    var allProducts = db.Products.OrderBy(p => p.ProductID);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Active");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" | ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Discontinued\n\n");
                    foreach (var item in allProducts) {
                        if (item.Discontinued)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("{0}", item.ProductName);
                        }
                        else {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("{0}", item.ProductName);
                        }
                    }
                    break;
                case "2": // all active products by name
                    var activeProducts = db.Products.OrderBy(p => p.ProductID).Where(p => p.Discontinued == false);
                    foreach (var item in activeProducts)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("{0}", item.ProductName);
                    }
                    break;
                case "3": // all discontinued products by name
                    var discontinuedProducts = db.Products.OrderBy(p => p.ProductID).Where(p => p.Discontinued == true);
                    foreach (var item in discontinuedProducts)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{0}", item.ProductName);
                    }
                    break;
                default: Console.WriteLine("Please choose a valid option."); break;
            }
        }
    }
}
