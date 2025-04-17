using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Spectre.Console;
using static CodingTracker.Enums;

namespace CodingTracker
{
    public class UserInput {


        internal void Main()
        {

            while (true)
            {
                Console.Clear();
                var actionChoice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuAction>()
                .Title("What do you want to do next?")
                .AddChoices(Enum.GetValues<MenuAction>()));

                switch(actionChoice)
                {
                    case MenuAction.AddCodeItem:
                        Console.WriteLine("You want to add code");
                        break;
                    case MenuAction.ViewCodeItems:
                        Console.WriteLine("You want to view code");
                        break;
                    case MenuAction.DeleteCodeItem:
                        Console.WriteLine("You want to delete code code");
                        break;
                    case MenuAction.UpdateCodeItem:
                        Console.WriteLine("You want to update code");
                        break;
                }

                // Wait for user input before continuing
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
            }

        }

        public void AddCodeItems()
        {
            // Get user input with specture console for each code item property
            // Add to database
            var startTime = AnsiConsole.Ask<string>("Enter the [green]start time[/] of the book to add:");
            var endTime = AnsiConsole.Ask<string>("Enter the [green]end time[/] of the book:");

            // First check if the start and end time are in the correct format

            /*
            bool passed = false;
            string s = String.Empty;
            DateTime dt;
            try{
                s = "23:45"; //Whatever you are getting the time from
                dt = Convert.ToDateTime(s);
                s = dt.ToString("HH:mm"); //if you want 12 hour time  ToString("hh:mm")
                passed = true;
            }
            catch(Exception ex)
            {

            } */


        }

        public int CalculateDuration()
        {

            /*
            string startTime = "7:00";
            string endTime = "14:00";

            TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
            int x = (int) duration.TotalMinutes;
            Console.WriteLine(x)

            Console.WriteLine(duration.ToString()); */
            return -1;
        }


    }



}
