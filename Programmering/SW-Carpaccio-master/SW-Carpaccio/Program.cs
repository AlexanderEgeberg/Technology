using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SW_Carpaccio
{
    public class Program
    {
        //1
        //Bruger skal kunne inputte en mængde
        //2
        //bruger skal kunne inputte en pris
        //3
        //Bruger skal kunne inputte en to-bogstavs stat kode
        //4
        //program skal regne den totale pris ud, uden discount og uden skat
        //5
        //program skal kunne finde ud af hvilken discount rate der skal bruges
        //6
        //program skal kunne trække rabatten fra
        //7
        //program skal kunne finde procenten på skatten ud fra statskoden
        //8
        //program skal ligge statsskat oveni prisen
        //9
        //Bruger skal kunne se resultat i kr/$
        //10
        //program skal være klar til næste input

        static void Main()
        {
            Console.WriteLine("Input your amount of items:");
            int AmountOfItems = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Input the price:");
            double PriceOfItem = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Input 2-letter state code:");
            string stateCode = Console.ReadLine();
            if (stateCode.Length != 2)
            {
                Console.WriteLine("Input 2-letter state code:");
                stateCode = Console.ReadLine();
            }
            double taxPercentage = Calculator.GetTax(stateCode);
            double totalWithDiscount = Calculator.ResultDiscount(AmountOfItems, PriceOfItem);
            double totalAmount = totalWithDiscount * taxPercentage;
            Console.WriteLine($"The total amount is: {totalAmount}");
            Console.WriteLine("\nPress enter to input a product");
            Console.ReadLine();
            Console.WriteLine();
            Main();
        }
    }

    public class Calculator
    {
        /// <summary>
        /// Gets the total price without discount and taxes
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static double ResultNoTax(int amount, double price)
        {
            return amount * price;
        }

        /// <summary>
        /// Gets the total price with discount but without taxes.
        /// Inside the function theres a switch statement that calculates the discount based on the price
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static double ResultDiscount(int amount, double price)
        {

            double result = ResultNoTax(amount, price);


            switch (result)
            {
                case var n when (n < 1000):
                    return result;
                    break;
                case var n when (n <= 1000 && n < 5000):
                    return result * 0.97;
                    break;
                case var n when (n <= 5000 && n < 7000):
                    return result * 0.95;
                    break;
                case var n when (n <= 7000 && n < 10000):
                    return result * 0.93;
                    break;
                case var n when (n >= 10000 && n < 50000):
                    return result * 0.90;
                    break;
                case var n when (n >= 50000):
                    return result * 0.85;
                    break;
                default:
                    return result;
                    break;
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        public static double GetTax(string stateCode)
        {
            double tax = 0;

            switch (stateCode.ToUpper())
            {
                case "UT":
                    tax = 6.85;
                    break;
                case "NV":
                    tax = 8.00;
                    break;
                case "TX":
                    tax = 6.25;
                    break;
                case "AL":
                    tax = 4.00;
                    break;
                case "CA":
                    tax = 8.25;
                    break;
                default:
                    return 1;
            }

            return (tax / 100) + 1;
        }
    }
}
