using System;
using System.IO;

namespace UpdateSydneyCoffee
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 0;
            // Get number of customers with validation
            do
            {
                Console.Write("Enter number of customers: ");
                string inputN = Console.ReadLine();
                if (!int.TryParse(inputN, out n) || n <= 0)
                {
                    Console.WriteLine("Invalid input! Please enter a positive integer.");
                    n = 0; // reset
                }
            } while (n <= 0);

            // Arrays to store data
            string[] name = new string[n];
            int[] quantity = new int[n];
            string[] reseller = new string[n];
            double[] charge = new double[n];

            double min = double.MaxValue;
            string minName = "";
            double max = double.MinValue;
            string maxName = "";

            Console.WriteLine("\n\t\t\t\tWelcome to Sydney Coffee Program\n");

            for (int i = 0; i < n; i++)
            {
                // Get customer name (non-empty)
                do
                {
                    Console.Write($"Enter customer {i + 1} name: ");
                    name[i] = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(name[i]))
                    {
                        Console.WriteLine("Name cannot be empty. Please enter again.");
                    }
                } while (string.IsNullOrWhiteSpace(name[i]));

                // Get quantity with validation and error handling
                quantity[i] = 0;
                do
                {
                    Console.Write("Enter the number of coffee beans bags (1-200): ");
                    string qtyInput = Console.ReadLine();
                    if (!int.TryParse(qtyInput, out quantity[i]) || quantity[i] < 1 || quantity[i] > 200)
                    {
                        Console.WriteLine("Invalid Input! Coffee bags between 1 and 200 can be ordered.");
                        quantity[i] = 0; // reset to continue loop
                    }
                } while (quantity[i] == 0);

                // Calculate price
                double price;
                if (quantity[i] <= 5)
                    price = 36 * quantity[i];
                else if (quantity[i] <= 15)
                    price = 34.5 * quantity[i];
                else
                    price = 32.7 * quantity[i];

                // Get reseller status, only accept yes/no (case insensitive)
                do
                {
                    Console.Write("Are you a reseller? (yes/no): ");
                    reseller[i] = Console.ReadLine().Trim().ToLower();
                    if (reseller[i] != "yes" && reseller[i] != "no")
                    {
                        Console.WriteLine("Invalid input! Please enter 'yes' or 'no'.");
                    }
                } while (reseller[i] != "yes" && reseller[i] != "no");

                // Calculate final charge
                if (reseller[i] == "yes")
                    charge[i] = price * 0.8; // 20% discount
                else
                    charge[i] = price;

                Console.WriteLine($"The total sales value from {name[i]} is ${charge[i]:F2}");
                Console.WriteLine("-----------------------------------------------------------------------------");

                // Track min and max spending
                if (charge[i] < min)
                {
                    min = charge[i];
                    minName = name[i];
                }

                if (charge[i] > max)
                {
                    max = charge[i];
                    maxName = name[i];
                }
            }

            // Display summary
            Console.WriteLine("\n\t\t\t\tSummary of sales\n");
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine(String.Format("{0,15}{1,10}{2,10}{3,15}", "Name", "Quantity", "Reseller", "Charge"));
            Console.WriteLine("-----------------------------------------------------------------------------");

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(String.Format("{0,15}{1,10}{2,10}{3,15:C2}",
                    name[i], quantity[i], reseller[i], charge[i]));
            }

            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine($"The customer spending most is {maxName} with ${max:F2}");
            Console.WriteLine($"The customer spending least is {minName} with ${min:F2}");

            // Save summary to file
            try
            {
                using (StreamWriter sw = new StreamWriter("SydneyCoffeeSalesSummary.txt"))
                {
                    sw.WriteLine("Summary of Sydney Coffee Sales");
                    sw.WriteLine("-------------------------------------------------");
                    sw.WriteLine(String.Format("{0,15}{1,10}{2,10}{3,15}", "Name", "Quantity", "Reseller", "Charge"));
                    sw.WriteLine("-------------------------------------------------");

                    for (int i = 0; i < n; i++)
                    {
                        sw.WriteLine(String.Format("{0,15}{1,10}{2,10}{3,15:C2}",
                            name[i], quantity[i], reseller[i], charge[i]));
                    }

                    sw.WriteLine("-------------------------------------------------");
                    sw.WriteLine($"The customer spending most is {maxName} with ${max:F2}");
                    sw.WriteLine($"The customer spending least is {minName} with ${min:F2}");
                }
                Console.WriteLine("\nSummary saved to file: SydneyCoffeeSalesSummary.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}

