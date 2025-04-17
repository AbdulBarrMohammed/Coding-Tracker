using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CodingTracker.Controller;
using CodingTracker.Model;
using Microsoft.VisualBasic;
using Spectre.Console;
using static CodingTracker.Enums;

namespace CodingTracker
{
    public class UserInput {

        private CodingController codingController = new();

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
                        AddCodeItems();
                        break;
                    case MenuAction.ViewCodeItems:
                        ViewCodeItems();
                        break;
                    case MenuAction.DeleteCodeItem:
                        RemoveCodeItem();
                        break;
                    case MenuAction.UpdateCodeItem:
                        EditCodeItem();
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
            while (!isFormattedCorrectly(startTime))
            {
                startTime = AnsiConsole.Ask<string>("Please enter time in the format of [green]hh:mm[/]");
            }

            while (!isFormattedCorrectly(endTime))
            {
                endTime = AnsiConsole.Ask<string>("Please enter time in the format of [green]hh:mm[/]");
            }

            // caluclate duration
            int duration = CalculateDuration(startTime, endTime);

            //insert new code item to coding controller
            codingController.InsertCodeItem(new CodeItem(duration, startTime, endTime));

        }

        public void RemoveCodeItem()
        {
            int id = codingController.GetNumberId();
            codingController.DeleteCodeItem(id);
        }

        public void ViewCodeItems()
        {
            codingController.GetAllCodeItems();
        }

        public void EditCodeItem()
        {
            codingController.UpdateCodeItem();
        }

        public int CalculateDuration(string startTime, string endTime)
        {

            TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
            int durationInMin = (int) duration.TotalMinutes;

            return durationInMin;
        }

        public bool isFormattedCorrectly(string timeStr) {

            bool isFormatted;
            string s = String.Empty;
            DateTime dt;
            try{
                dt = Convert.ToDateTime(timeStr);
                s = dt.ToString("hh:mm"); // 12 hour
                isFormatted = true;
            }
            catch(Exception ex)
            {
                isFormatted = false;
            }

            return isFormatted;
        }


    }



}
