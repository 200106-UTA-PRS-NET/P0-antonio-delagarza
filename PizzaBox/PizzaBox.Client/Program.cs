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
            RepositoryOrdersUserInfo repositoryOrdersUserInfo = new RepositoryOrdersUserInfo(db);
            RepositoryOrdersPizzaInfo repositoryOrdersPizzaInfo = new RepositoryOrdersPizzaInfo(db);
            RepositoryPizzas repositoryPizzas = new RepositoryPizzas(db);
            RepositoryStoreOrdersInfo repositoryStoreOrdersInfo = new RepositoryStoreOrdersInfo(db);
            RepositoryPresetPizzas repositoryPresetPizzas = new RepositoryPresetPizzas(db);
            RepositoryStorePresetPizzas repositoryStorePresetPizzas = new RepositoryStorePresetPizzas(db);

            //All repos instantiated

            int mainMenuChoice = 0;
            while (true)
            {
               // Console.Clear();
                Console.WriteLine("Welcome to EZ Pizza App");
                Console.WriteLine("1. Sign Up as User");
                Console.WriteLine("2. Sign In as User");
                Console.WriteLine("3. Store Manager Mode");
                Console.WriteLine("4. Exit");

                try
                {
                    Console.Write("Select an option: ");
                    mainMenuChoice = Convert.ToInt32(Console.ReadLine());

///////////////////////// Sign Up ////////////////////////////////////////
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
                        UserMenus(ref repositoryUsers, ref repositoryStoreInfo, ref repositoryOrdersUserInfo, ref repositoryOrdersPizzaInfo,
                            ref repositoryPizzas, ref repositoryStoreOrdersInfo, ref repositoryPresetPizzas, ref repositoryStorePresetPizzas);
                    } 

/////////////////////////////////////Exit ////////////////////////////////////////////////
                    else if (mainMenuChoice == 3)
                    {
                        StoreMenus(ref repositoryUsers, ref repositoryStoreInfo, ref repositoryOrdersUserInfo, ref repositoryOrdersPizzaInfo,
                            ref repositoryPizzas, ref repositoryStoreOrdersInfo, ref repositoryPresetPizzas, ref repositoryStorePresetPizzas);
                    }
                    else if (mainMenuChoice == 4)
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

        static void UserMenus(ref RepositoryUsers repositoryUsers,ref RepositoryStoreInfo repositoryStoreInfo,
            ref RepositoryOrdersUserInfo repositoryOrdersUserInfo, ref RepositoryOrdersPizzaInfo repositoryOrdersPizzaInfo, ref RepositoryPizzas repositoryPizzas,
            ref RepositoryStoreOrdersInfo repositoryStoreOrdersInfo, ref RepositoryPresetPizzas repositoryPresetPizzas, ref RepositoryStorePresetPizzas repositoryStorePresetPizzas)
        {
            Users u = null; //user that is to be logged in
            //this ensures that the user signs in correctly
            while (true)
            {
                string email, password;
                Console.Write("Enter email: ");
                email = Console.ReadLine();

                Console.Write("Enter password: ");
                password = Console.ReadLine();

                repositoryUsers.SignIn(email, password, ref u);

                if (u != null)
                {
                    Console.WriteLine("Signed In Successfully!");
                    break;
                }
                else
                {
                    Console.WriteLine("Could not find user. Would you like to try again? y/n: ");
                    string choice = Console.ReadLine();
                    if (choice == "y" || choice == "Y")
                    {
                        Console.WriteLine();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (u != null)



            while (true){
                //Signed In Menu
                int signedInMenuChoice = 0;
                Console.WriteLine("1. Place an order");
                Console.WriteLine("2. View Your Purchace history");
                Console.WriteLine("3. View Your Profile");
                Console.WriteLine("4. Update Your Profile");
                Console.WriteLine("5. Remove Account");
                Console.WriteLine("6. Sign Out");
                try
                {
                    Console.Write("Select an option: ");
                    signedInMenuChoice = Convert.ToInt32(Console.ReadLine());
                    if (signedInMenuChoice == 1)//Place order
                    {
                        if (repositoryStoreInfo.NumStores() == 0)
                        {
                            Console.WriteLine("You must create a store first\n");
                        }
                        else
                        {
                            StoreInfo si = null;
                            Console.WriteLine("Select a store by Id: ");
                            var stores = repositoryStoreInfo.GetItems();
                            int storeChoice;
                            foreach (var st in stores)
                            {
                                Console.WriteLine($"Store # {st.StoreId}\n{st.StoreName}\n{st.Address}\n{st.City}\n{st.State}\n{st.ZipCode}\nPizza starts at ${st.StorePrice}\n");
                            }
                            Console.Write("Selected store: ");
                            try
                            {
                                storeChoice = Convert.ToInt32(Console.ReadLine());
                                repositoryStoreInfo.SetStore(storeChoice, ref si);
                                if (si != null)
                                {
                                        decimal total = 0M;
                                        
                                       OrdersUserInfo ordersUserInfo = new OrdersUserInfo()
                                        {
                                            Email = u.Email,
                                            OrderDateTime = DateTime.Today
                                        };

                                        /////Start of the order
                                        while (true)
                                        {
                                            Pizzas pizza = null;
                                            PresetPizzas presetPizza = null;
                                            OrdersPizzaInfo ordersPizzaInfo = null;
                                            StoreOrdersInfo storeOrdersInfo = null;
                                            //this will return a list of teh pizza names of the store
                                            var storePizzas = repositoryStorePresetPizzas.GetStorePizzas(si.StoreId);
                                            int i = 1;
                                            List<string> storePizzaList = new List<string>();
                                            foreach (StorePresetPizzas st in storePizzas)
                                            {
                                                storePizzaList.Add(st.PizzaName);
                                            }
                                            storePizzaList.Add("Create your own");
                                            for (; i <= storePizzaList.Count(); i++)
                                            {
                                                Console.WriteLine($"{i}. {storePizzaList[i - 1]}");
                                            }
                                            Console.Write("Select an option: ");
                                            int choice = Convert.ToInt32(Console.ReadLine());
                                            //Preset Pizza
                                            if (choice >= 1 && choice < storePizzaList.Count())
                                            {
                                                presetPizza = repositoryPresetPizzas.GetPizza(storePizzaList[choice - 1]);
                                                if (presetPizza.Price + total > 250M)
                                                {
                                                    Console.WriteLine("You can't select this pizza becuase you can only spend 250 dollars per order\n");
                                                    break;
                                                }
                                                if (presetPizza != null)
                                                {
                                                    pizza = new Pizzas()
                                                    {
                                                        Size = presetPizza.Size,
                                                        Crust = presetPizza.Crust,
                                                        CrustFlavor = presetPizza.CrustFlavor,
                                                        Sauce = presetPizza.Sauce,
                                                        SauceAmount = presetPizza.SauceAmount,
                                                        CheeseAmount = presetPizza.CheeseAmount,
                                                        Topping1 = presetPizza.Topping1,
                                                        Topping2 = presetPizza.Topping2,
                                                        Topping3 = presetPizza.Topping3,
                                                        Price = presetPizza.Price
                                                    };
                                                    repositoryPizzas.Add(pizza);
                                                    


                                                    repositoryOrdersUserInfo.Add(ordersUserInfo);
                                                    storeOrdersInfo = new StoreOrdersInfo()
                                                    {
                                                        StoreId = si.StoreId,
                                                        OrderId = ordersUserInfo.OrderId

                                                    };
                                                    repositoryStoreOrdersInfo.Add(storeOrdersInfo);


                                                    ordersPizzaInfo = new OrdersPizzaInfo()
                                                    {
                                                        OrderId = ordersPizzaInfo.OrderId,
                                                        PizzaId = pizza.PizzaId,

                                                    };
                                                    Console.WriteLine("Would you like to add another pizza? y/n: ");
                                                    string select = Console.ReadLine();
                                                    if (select == "y" || select == "Y")
                                                    {
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Selected Pizza was not found on the store\n");
                                                }
                                            }

                                            else if (choice == storePizzaList.Count())//Custom Pizza
                                            {
                                                string size, crust, crustFlavor, sauce, sauceAmount, cheeseAmount, topping1, topping2=null, topping3=null;
                                                decimal price = si.StorePrice;
                                                ////Siza
                                                while (true)
                                                {
                                                    var names = Enum.GetNames(typeof(RepositoryPizzas.SizeAvailable));
                                                    var values = Enum.GetValues(typeof(RepositoryPizzas.SizeAvailable));
                                                    Console.WriteLine("Select Size ");
                                                    for (int j = 0; j < names.Length; j++)
                                                    {
                                                        Console.WriteLine($"{j + 1} {names.GetValue(j)}");
                                                    }
                                                    int sizeSelected = Convert.ToInt32(Console.ReadLine());
                                                    if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), sizeSelected))
                                                    {
                                                        size = names.GetValue(sizeSelected - 1).ToString();
                                                        if (sizeSelected == 3)
                                                        {
                                                            price += 1M;
                                                        }
                                                        else if (sizeSelected == 4)
                                                        {
                                                            price += 2M;
                                                        }
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Option not available. Try again");
                                                    }
                                                }
                                                //Crust
                                                while (true)
                                                {
                                                    var names = Enum.GetNames(typeof(RepositoryPizzas.CrustAvailable));
                                                    var values = Enum.GetValues(typeof(RepositoryPizzas.CrustAvailable));
                                                    Console.WriteLine("Select Crust ");
                                                    for (int j = 0; j < names.Length; j++)
                                                    {
                                                        Console.WriteLine($"{j + 1} {names.GetValue(j)}");
                                                    }
                                                    int crustSelected = Convert.ToInt32(Console.ReadLine());
                                                    if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), crustSelected))
                                                    {
                                                        crust = names.GetValue(crustSelected - 1).ToString();
                                                        if (crustSelected == 1)
                                                        {
                                                            price += 1M;
                                                        }
                                                        else if (crustSelected == 4)
                                                        {
                                                            price += 2M;
                                                        }
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Option not available. Try again");
                                                    }
                                                }
                                                //Crust Flavor
                                                while (true)
                                                {
                                                    var names = Enum.GetNames(typeof(RepositoryPizzas.CrustFlavorAvailable));
                                                    var values = Enum.GetValues(typeof(RepositoryPizzas.CrustFlavorAvailable));
                                                    Console.WriteLine("Select CrustFlavor ");
                                                    for (int j = 0; j < names.Length; j++)
                                                    {
                                                        Console.WriteLine($"{j + 1} {names.GetValue(j)}");
                                                    }
                                                    int crustFlavorSelected = Convert.ToInt32(Console.ReadLine());
                                                    if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), crustFlavorSelected))
                                                    {
                                                        crustFlavor = names.GetValue(crustFlavorSelected - 1).ToString();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Option not available. Try again");
                                                    }
                                                }
                                                //Sauce
                                                while (true)
                                                {
                                                    var names = Enum.GetNames(typeof(RepositoryPizzas.SauceAvailable));
                                                    var values = Enum.GetValues(typeof(RepositoryPizzas.SauceAvailable));
                                                    Console.WriteLine("Select sauce ");
                                                    for (int j = 0; j < names.Length; j++)
                                                    {
                                                        Console.WriteLine($"{j + 1} {names.GetValue(j)}");
                                                    }
                                                    int sauceSelected = Convert.ToInt32(Console.ReadLine());
                                                    if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), sauceSelected))
                                                    {
                                                        sauce = names.GetValue(sauceSelected - 1).ToString();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Option not available. Try again");
                                                    }
                                                }
                                                //Sauce Amount
                                                while (true)
                                                {
                                                    var names = Enum.GetNames(typeof(RepositoryPizzas.AmountsAvailable));
                                                    var values = Enum.GetValues(typeof(RepositoryPizzas.AmountsAvailable));
                                                    Console.WriteLine("Select sauce amount ");
                                                    for (int j = 0; j < names.Length; j++)
                                                    {
                                                        Console.WriteLine($"{j + 1} {names.GetValue(j)}");
                                                    }
                                                    int AmountSelected = Convert.ToInt32(Console.ReadLine());
                                                    if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), AmountSelected))
                                                    {
                                                        sauceAmount = names.GetValue(AmountSelected - 1).ToString();
                                                        if (AmountSelected == 3)
                                                        {
                                                            price += 2M;
                                                        }
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Option not available. Try again");
                                                    }
                                                }
                                                //Cheesse Amount
                                                while (true)
                                                {
                                                    var names = Enum.GetNames(typeof(RepositoryPizzas.AmountsAvailable));
                                                    var values = Enum.GetValues(typeof(RepositoryPizzas.AmountsAvailable));
                                                    Console.WriteLine("Select cheese amount ");
                                                    for (int j = 0; j < names.Length; j++)
                                                    {
                                                        Console.WriteLine($"{j + 1} {names.GetValue(j)}");
                                                    }
                                                    int AmountSelected = Convert.ToInt32(Console.ReadLine());
                                                    if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), AmountSelected))
                                                    {
                                                        cheeseAmount = names.GetValue(AmountSelected - 1).ToString();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Option not available. Try again");
                                                    }
                                                }
                                                //Topping 1
                                                while (true)
                                                {
                                                    var names = Enum.GetNames(typeof(RepositoryPizzas.ToppingsAvailable));
                                                    var values = Enum.GetValues(typeof(RepositoryPizzas.ToppingsAvailable));
                                                    Console.WriteLine("Select topping 1 ");
                                                    for (int j = 0; j < names.Length; j++)
                                                    {
                                                        Console.WriteLine($"{j+1} {names.GetValue(j)}");
                                                    }
                                                    int toppingSelected = Convert.ToInt32(Console.ReadLine());
                                                    if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), toppingSelected))
                                                    {
                                                        topping1 = names.GetValue(toppingSelected - 1).ToString();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Option not available. Try again");
                                                    }
                                                }
                                                //Extra toppings
                                                string tp2, tp3;
                                                Console.WriteLine("Would you like to add a second topping for 50 more cents? y/n:");
                                                tp2 = Console.ReadLine();
                                                if (tp2 == "y" || tp2 == "Y")
                                                {
                                                    while (true)
                                                    {
                                                        var names = Enum.GetNames(typeof(RepositoryPizzas.ToppingsAvailable));
                                                        var values = Enum.GetValues(typeof(RepositoryPizzas.ToppingsAvailable));
                                                        Console.WriteLine("Select topping 2 ");
                                                        for (int j = 0; j < names.Length; j++)
                                                        {
                                                            Console.WriteLine($"{j+1} {names.GetValue(j)}");
                                                        }
                                                        int toppingSelected = Convert.ToInt32(Console.ReadLine());
                                                        if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), toppingSelected))
                                                        {
                                                            topping2 = names.GetValue(toppingSelected - 1).ToString();
                                                            price += 0.50M;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Option not available. Try again");
                                                        }
                                                    }
                                                    Console.WriteLine("Would you like to add a third topping for 50 more cents? y/n:");
                                                    tp3 = Console.ReadLine();
                                                    if (tp3 == "y" || tp3 == "Y")
                                                    {
                                                        while (true)
                                                        {
                                                            var names = Enum.GetNames(typeof(RepositoryPizzas.ToppingsAvailable));
                                                            var values = Enum.GetValues(typeof(RepositoryPizzas.ToppingsAvailable));
                                                            Console.WriteLine("Select topping 1 ");
                                                            for (int j = 0; j < names.Length; j++)
                                                            {
                                                                Console.WriteLine($"{j + 1} {names.GetValue(j)}");
                                                            }
                                                            int toppingSelected = Convert.ToInt32(Console.ReadLine());
                                                            if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), toppingSelected))
                                                            {
                                                                topping3 = names.GetValue(toppingSelected - 1).ToString();
                                                                price += 0.50M;
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Option not available. Try again");
                                                            }
                                                        }
                                                    }
                                               
                                                }
                                               
                                                if (total + price > 250M)
                                                {
                                                    Console.WriteLine("You can't select this pizza becuase you can only spend 250 dollars per order\n");
                                                    break;
                                                }
                                                pizza = new Pizzas()
                                                {
                                                    Size = size,
                                                    Crust = crust,
                                                    CrustFlavor = crustFlavor,
                                                    Sauce = sauce,
                                                    SauceAmount = sauceAmount,
                                                    CheeseAmount = cheeseAmount,
                                                    Topping1 = topping1,
                                                    Topping2 = topping2,
                                                    Topping3 = topping3,
                                                    Price = price
                                                };
                                                repositoryPizzas.Add(pizza);



                                                repositoryOrdersUserInfo.Add(ordersUserInfo);
                                                storeOrdersInfo = new StoreOrdersInfo()
                                                {
                                                    StoreId = si.StoreId,
                                                    OrderId = ordersUserInfo.OrderId

                                                };
                                                repositoryStoreOrdersInfo.Add(storeOrdersInfo);


                                                ordersPizzaInfo = new OrdersPizzaInfo()
                                                {
                                                    OrderId = ordersPizzaInfo.OrderId,
                                                    PizzaId = pizza.PizzaId,

                                                };
                                                repositoryOrdersPizzaInfo.Add(ordersPizzaInfo);

                                                Console.WriteLine("Would you like to add another pizza? y/n: ");
                                                string select = Console.ReadLine();
                                                if (select == "y" || select == "Y")
                                                {
                                                    continue;
                                                }
                                                else
                                                {
                                                    break;
                                                }

                                            }
                                            else
                                            {
                                                Console.WriteLine("Please select a valid option\n");
                                            }

                                        }
                                        
                                        
                                }
                                else
                                {
                                    Console.WriteLine("Store not found\n");
                                }

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Please input a number");
                            }


                        }



                    }
                    else if (signedInMenuChoice == 2)
                    {
                            Console.WriteLine($"Purchase History of {u.Email}");
                            var userOrders = repositoryOrdersUserInfo.GetUserPurchases(u.Email);
                           
                            foreach (OrdersUserInfo temp in userOrders)
                            { 
                                decimal? price = 0M;
                                Console.WriteLine($"Order ID: {temp.OrderId}\n{temp.OrderDateTime}");
                                var usersOrdersPizzas = repositoryOrdersPizzaInfo.GetOrdersPizzas(temp.OrderId);
                                foreach (OrdersPizzaInfo of in usersOrdersPizzas)
                                {
                                    Pizzas p = repositoryPizzas.GetPizzasbyId(of.PizzaId);
                                    Console.Write($"Size: {p.Size} Crust: {p.Crust} Crust Flavor: {p.CrustFlavor} Sauce: {p.Sauce} Sauce Amount: {p.SauceAmount} Cheese Amount: {p.CheeseAmount} Topping 1: {p.Topping1}");
                                    if (p.Topping2 != null)
                                    {
                                        Console.Write($" Topping 2: {p.Topping2}");
                                    }
                                    if (p.Topping3 != null)
                                    {
                                        Console.Write($" Topping 3: {p.Topping3}");
                                    }
                                    Console.WriteLine($" Price: {p.Price}");
                                    price += p.Price;

                                }
                                int storeId = repositoryStoreOrdersInfo.GetStoreId(temp.OrderId);
                                string storename = repositoryStoreInfo.GetStoreName(storeId);
                                Console.WriteLine($"Store: {storename}\n Total: ${price}\n");
                                
                            }
                            
                            
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
                    else if (signedInMenuChoice == 5)
                    {
                        repositoryUsers.Remove(u.Email);
                        u = null;
                        break;
                    }
                    else if (signedInMenuChoice == 6)
                    {
                        
                        u = null;
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
        }

        static void StoreMenus(ref RepositoryUsers repositoryUsers, ref RepositoryStoreInfo repositoryStoreInfo,
            ref RepositoryOrdersUserInfo repositoryOrdersUserInfo, ref RepositoryOrdersPizzaInfo repositoryOrdersPizzaInfo, ref RepositoryPizzas repositoryPizzas,
            ref RepositoryStoreOrdersInfo repositoryStoreOrdersInfo, ref RepositoryPresetPizzas repositoryPresetPizzas, ref RepositoryStorePresetPizzas repositoryStorePresetPizzas)
        {
          
            while (true)
            {
                int StoreChoice = 0;
                Console.WriteLine("1. Create Store");
                Console.WriteLine("2. View All Stores");
                Console.WriteLine("3. View Store Orders");
                Console.WriteLine("4. Modify Store");
                Console.WriteLine("5. Create Preset Pizza");
                Console.WriteLine("6. Add Preset Pizza to Store");
                Console.WriteLine("7. Remove Store");
                Console.WriteLine("8. Exit");

                Console.Write("Selected Option: ");
                StoreChoice = Convert.ToInt32(Console.ReadLine());

                if (StoreChoice == 1) 
                {
                    string storeNamne, address, city, state, zip;
                    decimal storePrice;
                   
                        Console.Write("Store Name: ");
                        storeNamne = Console.ReadLine();

                        Console.Write("Address: ");
                        address = Console.ReadLine();

                        Console.Write("City: ");
                        city = Console.ReadLine();

                        Console.Write("State: ");
                        state = Console.ReadLine();

                        Console.Write("Zip: ");
                        zip = Console.ReadLine();

                        Console.Write("Store Pizza Price: ");
                        storePrice = Convert.ToDecimal(Console.ReadLine());


                        StoreInfo temp = new StoreInfo()
                        {
                            StoreName = storeNamne,
                            Address = address,
                            City = city,
                            State = state,
                            ZipCode = zip,
                            StorePrice = storePrice
                        };
                        repositoryStoreInfo.Add(temp);
                    
                    
                }
                else if (StoreChoice == 2)
                {
                    var stores = repositoryStoreInfo.GetItems();
                    foreach(StoreInfo st in stores)
                    {
                        Console.WriteLine($"Store Id {st.StoreId}\n{st.StoreName}\n{st.Address}\n{st.City}\n{st.State}\n{st.ZipCode}\n");
                    }
                }
                else if (StoreChoice == 3)
                {
                    var stores = repositoryStoreInfo.GetItems();
                    foreach (StoreInfo st in stores)
                    {
                        Console.WriteLine($"Store Id {st.StoreId}\n{st.StoreName}\n{st.Address}\n{st.City}\n{st.State}\n{st.ZipCode}\n");
                    }
                    Console.Write("Select a store ID");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (repositoryStoreInfo.FindStore(choice))
                    {
                        Console.Write($"Store Name: {repositoryStoreInfo.GetStoreName(choice)}");
                        var storeOrders = repositoryStoreOrdersInfo.GetStoreOrders(choice);
                        foreach(StoreOrdersInfo st in storeOrders)
                        {
                            OrdersUserInfo or = repositoryOrdersUserInfo.GetStoreOrderDetails(st.OrderId);
                            Console.WriteLine($"{or.OrderId}\n{or.Email}\n{or.OrderDateTime}");
                            var pizzasInfo = repositoryOrdersPizzaInfo.GetOrdersPizzas(or.OrderId);
                            
                            foreach (OrdersPizzaInfo op in pizzasInfo)
                            {
                                Console.WriteLine($"Pizza number {op.PizzaId}");
                            }

                        }
                    }
                }
                else if (StoreChoice == 4)
                {
                    var stores = repositoryStoreInfo.GetItems();
                    foreach (StoreInfo st in stores)
                    {
                        Console.WriteLine($"Store Id {st.StoreId}\n{st.StoreName}\n{st.Address}\n{st.City}\n{st.State}\n{st.ZipCode}\n");
                    }
                    Console.Write("Select a store ID");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (repositoryStoreInfo.FindStore(choice))
                    {
                        StoreInfo si = null;
                        repositoryStoreInfo.SetStore(choice, ref si);
                        
                    }
                    
                    
                }
                else if (StoreChoice == 5)
                {
                    string name, size, crust, crustFlavor, sauce, sauceAmount, cheeseAmount, topping1, topping2 = null, topping3 = null;
                    decimal price;
                    Console.Write("What is the pizza Name? ");
                    name = Console.ReadLine();
                    ////Siza
                    while (true)
                    {
                        var names = Enum.GetNames(typeof(RepositoryPizzas.SizeAvailable));
                        var values = Enum.GetValues(typeof(RepositoryPizzas.SizeAvailable));
                        Console.WriteLine("Select Size ");
                        for (int j = 0; j < names.Length; j++)
                        {
                            Console.WriteLine($"{j+1} {names.GetValue(j)}");
                        }
                        int sizeSelected = Convert.ToInt32(Console.ReadLine());
                        if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), sizeSelected))
                        {
                            size = names.GetValue(sizeSelected - 1).ToString();
                            
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Option not available. Try again");
                        }
                    }
                    //Crust
                    while (true)
                    {
                        var names = Enum.GetNames(typeof(RepositoryPizzas.CrustAvailable));
                        var values = Enum.GetValues(typeof(RepositoryPizzas.CrustAvailable));
                        Console.WriteLine("Select Crust ");
                        for (int j = 0; j < names.Length; j++)
                        {
                            Console.WriteLine($"{j + 1} {names.GetValue(j)}");
                        }
                        int crustSelected = Convert.ToInt32(Console.ReadLine());
                        if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), crustSelected))
                        {
                            crust = names.GetValue(crustSelected - 1).ToString();
                           
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Option not available. Try again");
                        }
                    }
                    //Crust Flavor
                    while (true)
                    {
                        var names = Enum.GetNames(typeof(RepositoryPizzas.CrustFlavorAvailable));
                        var values = Enum.GetValues(typeof(RepositoryPizzas.CrustFlavorAvailable));
                        Console.WriteLine("Select CrustFlavor ");
                        for (int j = 0; j < names.Length; j++)
                        {
                            Console.WriteLine($"{j+1} {names.GetValue(j)}");
                        }
                        int crustFlavorSelected = Convert.ToInt32(Console.ReadLine());
                        if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), crustFlavorSelected))
                        {
                            crustFlavor = names.GetValue(crustFlavorSelected - 1).ToString();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Option not available. Try again");
                        }
                    }
                    //Sauce
                    while (true)
                    {
                        var names = Enum.GetNames(typeof(RepositoryPizzas.SauceAvailable));
                        var values = Enum.GetValues(typeof(RepositoryPizzas.SauceAvailable));
                        Console.WriteLine("Select sauce ");
                        for (int j = 0; j < names.Length; j++)
                        {
                            Console.WriteLine($"{j + 1} {names.GetValue(j)}");
                        }
                        int sauceSelected = Convert.ToInt32(Console.ReadLine());
                        if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), sauceSelected))
                        {
                            sauce = names.GetValue(sauceSelected - 1).ToString();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Option not available. Try again");
                        }
                    }
                    //Sauce Amount
                    while (true)
                    {
                        var names = Enum.GetNames(typeof(RepositoryPizzas.AmountsAvailable));
                        var values = Enum.GetValues(typeof(RepositoryPizzas.AmountsAvailable));
                        Console.WriteLine("Select sauce amount ");
                        for (int j = 0; j < names.Length; j++)
                        {
                            Console.WriteLine($"{j + 1} {names.GetValue(j)}");
                        }
                        int AmountSelected = Convert.ToInt32(Console.ReadLine());
                        if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), AmountSelected))
                        {
                            sauceAmount = names.GetValue(AmountSelected - 1).ToString();
                            
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Option not available. Try again");
                        }
                    }
                    //Cheesse Amount
                    while (true)
                    {
                        var names = Enum.GetNames(typeof(RepositoryPizzas.AmountsAvailable));
                        var values = Enum.GetValues(typeof(RepositoryPizzas.AmountsAvailable));
                        Console.WriteLine("Select cheese amount ");
                        for (int j = 0; j < names.Length; j++)
                        {
                            Console.WriteLine($"{j + 1} {names.GetValue(j)}");
                        }
                        int AmountSelected = Convert.ToInt32(Console.ReadLine());
                        if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), AmountSelected))
                        {
                            cheeseAmount = names.GetValue(AmountSelected - 1).ToString();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Option not available. Try again");
                        }
                    }
                    //Topping 1
                    while (true)
                    {
                        var names = Enum.GetNames(typeof(RepositoryPizzas.ToppingsAvailable));
                        var values = Enum.GetValues(typeof(RepositoryPizzas.ToppingsAvailable));
                        Console.WriteLine("Select topping 1 ");
                        for (int j = 0; j < names.Length; j++)
                        {
                            Console.WriteLine($"{j + 1} {names.GetValue(j)}");
                        }
                        int toppingSelected = Convert.ToInt32(Console.ReadLine());
                        if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), toppingSelected))
                        {
                            topping1 = names.GetValue(toppingSelected - 1).ToString();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Option not available. Try again");
                        }
                    }
                    //Extra toppings
                    string tp2, tp3;
                    Console.WriteLine("Would you like to add a second topping for 50 more cents? y/n:");
                    tp2 = Console.ReadLine();
                    if (tp2 == "y" || tp2 == "Y")
                    {
                        while (true)
                        {
                            var names = Enum.GetNames(typeof(RepositoryPizzas.ToppingsAvailable));
                            var values = Enum.GetValues(typeof(RepositoryPizzas.ToppingsAvailable));
                            Console.WriteLine("Select topping 2 ");
                            for (int j = 0; j < names.Length; j++)
                            {
                                Console.WriteLine($"{j + 1} {names.GetValue(j)}");
                            }
                            int toppingSelected = Convert.ToInt32(Console.ReadLine());
                            if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), toppingSelected))
                            {
                                topping2 = names.GetValue(toppingSelected - 1).ToString();
                               
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Option not available. Try again");
                            }
                        }
                        Console.WriteLine("Would you like to add a third topping for 50 more cents? y/n:");
                        tp3 = Console.ReadLine();
                        if (tp3 == "y" || tp3 == "Y")
                        {
                            while (true)
                            {
                                var names = Enum.GetNames(typeof(RepositoryPizzas.ToppingsAvailable));
                                var values = Enum.GetValues(typeof(RepositoryPizzas.ToppingsAvailable));
                                Console.WriteLine("Select topping 1 ");
                                for (int j = 0; j < names.Length; j++)
                                {
                                    Console.WriteLine($"{j + 1} {names.GetValue(j)}");
                                }
                                int toppingSelected = Convert.ToInt32(Console.ReadLine());
                                if (Enum.IsDefined(typeof(RepositoryPizzas.SizeAvailable), toppingSelected))
                                {
                                    topping3 = names.GetValue(toppingSelected - 1).ToString();
                                    
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Option not available. Try again");
                                }
                            }
                        }

                    }
                    Console.Write("What is the pizza price?: ");
                    price = Convert.ToDecimal(Console.ReadLine());

                    PresetPizzas pizza = new PresetPizzas()
                    {
                        PizzaName = name,
                        Size = size,
                        Crust = crust,
                        CrustFlavor = crustFlavor,
                        Sauce = sauce,
                        SauceAmount = sauceAmount,
                        CheeseAmount = cheeseAmount,
                        Topping1 = topping1,
                        Topping2 = topping2,
                        Topping3 = topping3,
                        Price = price
                    };
                    repositoryPresetPizzas.Add(pizza);
                }
                else if (StoreChoice == 6)
                {
                    StorePresetPizzas storePresetPizzas = null;
                    var stores = repositoryStoreInfo.GetItems();
                    foreach (StoreInfo st in stores)
                    {
                        Console.WriteLine($"Store Id {st.StoreId}\n{st.StoreName}\n{st.Address}\n{st.City}\n{st.State}\n{st.ZipCode}\n");
                    }
                    Console.Write("Select a store ID");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (repositoryStoreInfo.FindStore(choice))
                    {
                        Console.Write("Select a Pizza name: ");
                        var allPizzas = repositoryPresetPizzas.GetItems();
                        foreach (PresetPizzas pz in allPizzas)
                        {
                            Console.WriteLine($"{pz.PizzaName}");
                        }
                        string name = Console.ReadLine();
                        PresetPizzas preset = repositoryPresetPizzas.GetPizza(name);
                        if (preset != null)
                        {
                            storePresetPizzas = new StorePresetPizzas()
                            {
                                StoreId = choice,
                                PizzaName = name
                            };
                            repositoryStorePresetPizzas.Add(storePresetPizzas);
                        }
                    }

                }
                else if (StoreChoice == 7)
                {
                    
                }
                else if (StoreChoice == 8)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("You must type a valid option!\n");
                }
            }
            
            
        }
        
    }

}
