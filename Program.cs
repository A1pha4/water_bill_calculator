using System;
using System.IO;

class Program
{
    static void Main()
    {
        double lastMonthReading = GetLastMonthReading();
        showWelcomeMessage();

        do
        {
            

            Console.Write("Enter this month's meter reading (in cubic meters): ");
            if (!double.TryParse(Console.ReadLine(), out double thisMonthReading))
            {
                Console.WriteLine("Invalid input. Please enter a numeric value for the reading.");
                return;
            }

            double consumption = thisMonthReading - lastMonthReading;

            Console.WriteLine($"This month's consumption: {consumption} cubic meters");

            Console.Write("Enter type of usage (residential/commercial/industrial): ");
            string usageType = Console.ReadLine().ToLower();

            double baseRate = 0;
            double consumptionCharge = GetConsumptionCharge(usageType);
            double totalBill = baseRate + (consumption * consumptionCharge);

            Console.WriteLine($"Consumption Charge: KES {consumption * consumptionCharge:F2}");
            Console.WriteLine($"Total Bill: KES {totalBill:F2}");

            StoreReadings(thisMonthReading, usageType);

            lastMonthReading = thisMonthReading;

            Console.Write("Do you want to calculate another bill? (yes/no): ");
        } while (Console.ReadLine().ToLower() == "yes");
        Console.WriteLine("\t\t\t\t\t\t\t\t\tKaribu tena wakati mwingine!!!");
    }

    static double GetConsumptionCharge(string usageType)
    {
        switch (usageType)
        {
            case "residential":
                return 25.0;
            case "commercial":
                return 30.50;
            case "industrial":
                return 35.75;
            default:
                throw new ArgumentException("Invalid usage type. Please enter 'residential', 'commercial', or 'industrial'.");
        }
    }

    static void StoreReadings(double consumption, string usageType)
    {
        string filePath = "meter_readings.txt";

        using (StreamWriter writer = File.AppendText(filePath))
        {
            writer.WriteLine($"{DateTime.Now}: {usageType} - {consumption} cubic meters");
        }

        Console.WriteLine("Meter reading stored successfully.");
    }

    static double GetLastMonthReading()
    {
        string filePath = "meter_readings.txt";

        if (File.Exists(filePath))
        {
            string[] allLines = File.ReadAllLines(filePath);

            if (allLines.Length > 0)
            {
                string lastLine = allLines[allLines.Length - 1];

                if (lastLine != null)
                {
                    string[] parts = lastLine.Split(':');
                    if (parts.Length == 2 && double.TryParse(parts[1].Trim().Split('-')[1].Split(' ')[1], out double lastMonthReading))
                    {
                        return lastMonthReading;
                    }
                }
            }
        }

        return -1;
    }
    static void showWelcomeMessage()
    {
        Console.WriteLine("\t\t\t\t\t\t\t\t****************************************************************************************");
        Console.WriteLine("\t\t\t\t\t\t\t\t*                             Welcome to the                                           *");
        Console.WriteLine("\t\t\t\t\t\t\t\t*                          Water Bill Calculator                                       *");
        Console.WriteLine("\t\t\t\t\t\t\t\t****************************************************************************************");
    }
}
