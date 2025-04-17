using System;
using System.Collections.Generic;
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

        public void ShowCodeItems(string itemType)
        {
            switch (itemType)
            {
                case "test":
                    Console.WriteLine("test");
                    break;
                case "hello":
                    Console.WriteLine("hello");
                    break;
            }
        }

        public int CalculateDuration()
        {
            return -1;
        }


    }



}
