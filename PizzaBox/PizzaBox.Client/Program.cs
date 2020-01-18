using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PizzaBox.Domain.Models;
using Microsoft.EntityFrameworkCore;
using PizzaBox.Storing.Repositories;
using System.Linq;

namespace PizzaBox.Client
{
    class Program
    {

        static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = configBuilder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<PizzaDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaDB"));
            var options = optionsBuilder.Options;
            PizzaDBContext db = new PizzaDBContext(options);

            RepositoryUsers repositoryUsers = new RepositoryUsers(db);
            RepositoryStoreInfo repositoryStoreInfo = new RepositoryStoreInfo(db);

            Users u = null; //user that is to be logged in

            int mainMenuChoice = 0;
            while (true)
            {
               // Console.Clear();
                Console.WriteLine("Welcome to EZ Pizza App");
                Console.WriteLine("1. Sign Up");
                Console.WriteLine("2. Sign In");
                Console.WriteLine("3. Exit");

                try
                {
                    Console.Write("Select an option: ");
                    mainMenuChoice = Convert.ToInt32(Console.ReadLine());
                    if (mainMenuChoice == 1)
                    {
                        string email, password, fname, lname, phone;
                        Console.Write("Enter email: ");
                        email = Console.ReadLine();

                        Console.Write("Enter password (must be 6 or more characters): ");
                        password = Console.ReadLine();

                        Console.Write("Enter first name: ");
                        fname = Console.ReadLine();

                        Console.Write("Enter last name: ");
                        lname = Console.ReadLine();

                        Console.Write("Enter phone: ");
                        phone = Console.ReadLine();

                        Users temp = new Users()
                        {
                            Email = email,
                            Password = password,
                            FirstName = fname,
                            LastName = lname,
                            Phone = phone
                        };

                        repositoryUsers.Add(temp);
                    }







 ////////////////////////// SIGNED IN MENU    /////////////////////////////////
                    else if (mainMenuChoice == 2)
                    {
                        //this ensures that the user signs in correctly
                        while (true)
                        {
                            string email, password;
                            Console.Write("Enter email: ");
                            email = Console.ReadLine();

                            Console.Write("Enter password (must be 6 or more characters): ");
                            password = Console.ReadLine();

                            repositoryUsers.SignIn(email, password, ref u);

                            if (u != null)
                            {
                                Console.WriteLine("Signed In Successfully!");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Could not find user. Please try again.");
                            }
                        }
                        //Signed In Menu
                        int signedInMenuChoice = 0;
                        Console.WriteLine("1. Place an order");
                        Console.WriteLine("2. View Purchace history");
                        Console.WriteLine("3. View Profile");
                        Console.WriteLine("4. Update Profile");
                        Console.WriteLine("5. Exit");
                        try
                        {
                            Console.Write("Select an option: ");
                            signedInMenuChoice = Convert.ToInt32(Console.ReadLine());
                            if (signedInMenuChoice == 1)
                            {
                               

                            }
                            else if (signedInMenuChoice == 2)
                            {
                                
                            }
                            else if (signedInMenuChoice == 3)
                            {
                                Console.WriteLine($"Email: {u.Email}");
                                Console.WriteLine($"Password: {u.Password}");
                                Console.WriteLine($"First Name: {u.FirstName}");
                                Console.WriteLine($"Last Name: {u.LastName}");
                                Console.WriteLine($"Phone: {u.Phone}\n");
                            }
                            else if (signedInMenuChoice == 4)
                            {
                                string password = u.Password;
                                string fname = u.FirstName;
                                string lname = u.LastName;
                                string phone = u.Phone;

                                string decision;
                                while (true)
                                {
                                    Console.Write("Change password? y/n ");
                                    decision = Console.ReadLine();
                                    if (decision == "y" || decision == "Y")
                                    {
                                        Console.Write("New Password: ");
                                        password = Console.ReadLine();
                                        break;
                                    }
                                    else if (decision == "n" || decision == "N")
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid option\n");
                                    }
                                }

                                while (true)
                                {
                                    Console.Write("Change First Name? y/n ");
                                    decision = Console.ReadLine();
                                    if (decision == "y" || decision == "Y")
                                    {
                                        Console.Write("New First Name: ");
                                        fname = Console.ReadLine();
                                        break;
                                    }
                                    else if (decision == "n" || decision == "N")
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid option\n");
                                    }
                                }

                                while (true)
                                {
                                    Console.Write("Change Last Name? y/n ");
                                    decision = Console.ReadLine();
                                    if (decision == "y" || decision == "Y")
                                    {
                                        Console.Write("New Last Name: ");
                                        lname = Console.ReadLine();
                                        break;
                                    }
                                    else if (decision == "n" || decision == "N")
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid option\n");
                                    }
                                }

                                while (true)
                                {
                                    Console.Write("Change Phone? y/n ");
                                    decision = Console.ReadLine();
                                    if (decision == "y" || decision == "Y")
                                    {
                                        Console.Write("New Phone: ");
                                        phone = Console.ReadLine();
                                        break;
                                    }
                                    else if (decision == "n" || decision == "N")
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid option\n");
                                    }
                                }

                                Users temp = new Users()
                                {
                                    Email = u.Email,
                                    Password = password,
                                    FirstName = fname,
                                    LastName = lname,
                                    Phone = phone
                                };

                                repositoryUsers.Modify(temp);

                            }
                            else if(signedInMenuChoice == 5)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("You must type a valid option!\n");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Please input a number\n");
                        }


                    }


//////////////////////////////////////////////////////////////////////////////////////

                    else if (mainMenuChoice == 3)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You must type a valid option!");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Please input a number");
                }
               


            }

           

        }

        
    }

}
