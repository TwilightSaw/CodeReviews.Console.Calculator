﻿// Program.cs

using System.Text.RegularExpressions;
using static CalculatorLibrary.CalculatorLibrary;

namespace CalculatorProgram;

internal class Program
{
    private static void Main(string[] args)
    {
        var endApp = false;
        // count the amount of times the calculator was used
        var calculatorUse = 0;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        var calculator = new Calculator();

        while (!endApp)
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            var numInput1 = "";
            var numInput2 = "";

            double result;

            // Showing history
            Console.WriteLine("History of last 3 elements: ");
            if (calculator.history.Count == 0)
                Console.WriteLine("Empty\n");
            foreach (var x in calculator.history) Console.WriteLine($"{x.Item1} {x.Item4} {x.Item2} = {x.Item3}");

            Console.WriteLine("\this - History ");
            Console.WriteLine("\tcl - Clear History");
            Console.WriteLine("\tc - Continue");
            Console.Write("Your option? ");

            // Read from user first variable for first menu
            var opFirst = Console.ReadLine();

            switch (opFirst)
            {
                case "his":
                    Console.WriteLine("Choose calculation from 1 to 3: ");
                    // Read from user history calculation choice
                    var r = Convert.ToInt32(Console.ReadLine());
                    // Check if calculation exists
                    if (r > calculator.history.Count)
                    {
                        Console.WriteLine("History is empty yet or you chose wrong number\n");
                        continue;
                    }
                    switch (r)
                    {
                        case 1:
                            numInput1 = calculator.history[0].Item3.ToString();
                            break;
                        case 2:
                            numInput1 = calculator.history[1].Item3.ToString();
                            break;
                        case 3:
                            numInput1 = calculator.history[2].Item3.ToString();
                            break;
                    }

                    break;
                case "cl":
                    // Clear history
                    calculator.history.Clear();
                    continue;
                case "c":
                    // Continue to the main calculator usage
                    break;
            }

            
            double cleanNum1 = 0;

            // Checking if earlier history calculation was chosen 
            if (opFirst != "his")
            {
                // Ask the user to type the first number.
                opFirst = "";
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }
            }
            else
            {
                double.TryParse(numInput1, out cleanNum1);
            }


            // Ask the user to type the second number.
            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput2 = Console.ReadLine();
            }

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("\tmod - Modulo");

            Console.Write("Your option? ");

            var opSecond = Console.ReadLine();

            // Validate input is not null, and matches the pattern
            if (opSecond == null || !Regex.IsMatch(opSecond, "[a|s|m|d|p|mod]"))
                Console.WriteLine("Error: Unrecognized input.");
            else
                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, opSecond);
                    if (double.IsNaN(result))
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                    calculatorUse++;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }

        calculator.Finish();
        Console.WriteLine($"The calculator was used {calculatorUse} times");
    }
}