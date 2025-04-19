using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;
using CodingTracker.Data;
using CodingTracker.Model;
using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace CodingTracker.Controller
{
    public class CodingController
    {
        public void InsertCodeItem(CodeItem codeItem)
        {

            using (var connection = new SqliteConnection(MockDatabase.GetConnectionString())) {


                var sql = @"
                    INSERT INTO coding_track (StartTime, EndTime, Duration)
                    VALUES (@StartTime, @EndTime, @Duration);
                    SELECT last_insert_rowid();";


                    long newId = connection.ExecuteScalar<long>(sql, new {
                    StartTime = codeItem.StartTime,
                    EndTime = codeItem.EndTime,
                    Duration = codeItem.Duration
                });
                codeItem.Id = newId;


            }

        }

        public void DeleteCodeItem()
        {
            int id = GetNumberId("delete");
            using (var connection = new SqliteConnection(MockDatabase.GetConnectionString())) {
                if (id == 0)
                {
                    Console.WriteLine("Invalid ID");
                    DeleteCodeItem();
                }

                var sql = "DELETE FROM coding_track WHERE Id = @Id";
                int rowCount = connection.Execute(sql, new { Id = id});
                if (rowCount == 0)
                {
                    System.Console.WriteLine($"\n\nRecord with Id {id} doesn't exist. \n\n");
                    DeleteCodeItem();
                }
            }
            System.Console.WriteLine($"\n\nRecord with Id {id} was deleted. \n\n");

        }

        public void UpdateCodeItem()
        {
            var id = GetNumberId("update");
            using (var connection = new SqliteConnection(MockDatabase.GetConnectionString()))
            {

                //Get new input
                var startTime = AnsiConsole.Ask<string>("Enter the [green]start time[/] of the book to add:");
                // First check if the start and end time are in the correct format
                while (!isFormattedCorrectly(startTime))
                {
                    startTime = AnsiConsole.Ask<string>("Please enter start time in the format of [green]hh:mm[/]");
                }

                var endTime = AnsiConsole.Ask<string>("Enter the [green]end time[/] of the book:");
                while (!isFormattedCorrectly(endTime))
                {
                    endTime = AnsiConsole.Ask<string>("Please enter end time in the format of [green]hh:mm[/]");
                }

                // caluclate duration
                int duration = CalculateDuration(startTime, endTime);

                // Insert updated code item properties to database

                var sql1 = "UPDATE coding_track SET StartTime = @StartTime, EndTime = @EndTime, Duration = @Duration WHERE Id = @Id";
                connection.Execute(sql1, new {
                    Id = id,
                    StartTime = startTime,
                    EndTime = endTime,
                    Duration = duration
                 });

                Console.WriteLine("Updated Successfully");


            }

        }

        public void GetAllCodeItems()
        {
            Console.Clear();
            using (var connection = new SqliteConnection(MockDatabase.GetConnectionString())) {
                var sql = $"SELECT * FROM coding_track";
                MockDatabase.codeItems = connection.Query<CodeItem>(sql).ToList();
                if (MockDatabase.codeItems.Count == 0) Console.WriteLine("No rows found");
                else
                {
                    foreach (var c in MockDatabase.codeItems)
                    {
                        Console.WriteLine($"Id: {c.Id} Duration in minutes: {c.Duration} Start time: {c.StartTime} End time: {c.EndTime}");
                    }
                    Console.WriteLine("------------------------------------------\n");
                }

            }
        }

        public int GetNumberId(string action)
        {
            var numberInput = AnsiConsole.Ask<string>($"Enter the [green] id [/] of the code you want to {action}:");
            if (numberInput == "0") GetNumberId(action);

            while (!Int32.TryParse(numberInput, out _) || Convert.ToInt32(numberInput) < 0)
            {
                Console.WriteLine("\n\nInvalid number. Try again.\n\n");
                numberInput = Console.ReadLine();
            }

            int finalInput = Convert.ToInt32(numberInput);
            return finalInput;

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

        public int CalculateDuration(string startTime, string endTime)
        {

            TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
            int durationInMin = (int) duration.TotalMinutes;

            return durationInMin;
        }
    }
}
