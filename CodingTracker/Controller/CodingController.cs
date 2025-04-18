using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;
using CodingTracker.Data;
using CodingTracker.Model;
using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace CodingTracker.Controller
{
    public class CodingController
    {
        public void InsertCodeItem(CodeItem codeItem)
        {

            int duration = codeItem.Duration;
            string startTime = codeItem.StartTime.ToString();
            string endTime = codeItem.EndTime.ToString();

            using (var connection = new SqliteConnection(MockDatabase.GetConnectionString())) {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"INSERT INTO coding_track(StartTime, EndTime, Duration) VALUES('{startTime}', '{endTime}', {duration})";
                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void DeleteCodeItem()
        {
            int id = GetNumberId("delete");
            using (var connection = new SqliteConnection(MockDatabase.GetConnectionString())) {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"DELETE from coding_track WHERE Id = '{id}'";

                int rowCount = tableCmd.ExecuteNonQuery();
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

        }

        public void GetAllCodeItems()
        {
            Console.Clear();
            using (var connection = new SqliteConnection(MockDatabase.GetConnectionString())) {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"SELECT * FROM coding_track";
                tableCmd.ExecuteNonQuery();
                SqliteDataReader reader = tableCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MockDatabase.codeItems.Add(
                            new CodeItem(reader.GetInt32(3), reader.GetString(1), reader.GetString(2))
                        );
                    }
                } else {
                    Console.WriteLine("No rows found");
                }
                connection.Close();
                Console.WriteLine("------------------------------------------\n");
                foreach (var c in MockDatabase.codeItems)
                {
                    Console.WriteLine($"Duration in minutes: {c.Duration} Start time: {c.StartTime} End time: {c.EndTime}");
                }
                Console.WriteLine("------------------------------------------\n");
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
    }
}
