using Individuellt_projekt___Databaser.Models2;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individuellt_projekt___Databaser
{
    internal class InputManagement
    {
        public static int UserInput(int lowerRange, int upperRange)
        {
            int numberInput;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out numberInput))
                {
                    if (numberInput >= lowerRange && numberInput <= upperRange)
                    { 
                        return numberInput;
                    }
                }
                Console.WriteLine("Ogiltig input");
            }
        }

        public static void ReturnToMenu()
        {
            Console.SetCursorPosition(1, 28);
            Console.WriteLine("[ENTER] Återvänd till menyn");
            Console.ReadLine();
        }

        public static int SecureInput(SqlConnection connection)
        {
            while (true)
            {
                var userChoice = Console.ReadKey(true).Key;

                if (userChoice == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Console.WriteLine("Exiting program...");
                    connection.Close();
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                if (userChoice >= ConsoleKey.D1 && userChoice <= ConsoleKey.D8)
                {
                    int keyConvertToInt = userChoice - ConsoleKey.D0; //Gives a numerical value thanks to subtraction
                    Console.Clear();
                    return keyConvertToInt;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    ReturnToMenu();
                }
            }
        }
    }
}
