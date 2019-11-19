using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NLog;
using NorthwindConsole.Models;

namespace NorthwindConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            logger.Info("Program started");
            try
            {
                string choice;
                do
                {
                    Console.WriteLine("1) Display Categories");
                    Console.WriteLine("2) Add Category");
                    Console.WriteLine("3) Display Category and related products");
                    Console.WriteLine("4) Display all Categories and their related products");
                    Console.WriteLine("5) Display all products.");
                    Console.WriteLine("6) Something else");
                    Console.WriteLine("7) Something else entirely");
                    Console.WriteLine("\"q\" to quit");
                    choice = Console.ReadLine();
                    Console.Clear();
                    logger.Info($"Option {choice} selected");
                    if (choice == "1")
                    {
                        displayAllCategories();
                    }
                    else if (choice == "2")
                    {
                        addNewCategory();
                    }
                    else if (choice == "3")
                    {
                        displayCategoryAndProducts();
                        logger.Info($"User has displayed all products by a specific category.");

                    }
                    else if (choice == "4")
                    {
                        displayCategoryAndProducts();
                        logger.Info($"User has displayed all categories and their products.");

                    }
                    else if (choice == "5")
                    {
                        displayAllProducts();
                    }
                    Console.WriteLine();

                } while (choice.ToLower() != "q");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            logger.Info("Program ended");
        }

        public static void displayAllCategories() {
            var db = new NorthwindContext();
            var query = db.Categories.OrderBy(p => p.CategoryName);

            Console.WriteLine($"{query.Count()} records returned");
            foreach (var item in query)
            {
                Console.WriteLine($"{item.CategoryId}) {item.CategoryName} - {item.Description}");
            }
            logger.Info($"User has displayed all categories.");
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

                logger.Info($"User has added a category called {category.CategoryName}.");

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

            logger.Info($"User has added {counter} total categories.");
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
            logger.Info($"CategoryId {id} selected");
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
            do
            {
                Console.WriteLine("1) Display all products.");
                Console.WriteLine("2) Display active products.");
                Console.WriteLine("3) Display discontinued products.");
                response = Console.ReadLine();
            } while (response != "1" && response != "2" && response != "3");
        }
    }
}
