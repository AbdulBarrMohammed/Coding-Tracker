using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingTracker.Model;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CodingTracker.Data
{
    public static class MockDatabase
    {
        public static List<CodeItem> codeItems = new();
        private static string connectionString = @"Data Source=coding-Tracker.db";

        public static string GetConnectionString()
        {
            return connectionString;
        }


        //connecting to database

        public static void ConnectToDatabase()
        {

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            string connectionString = config.GetConnectionString("Default");
            Console.WriteLine($"Connection string: {connectionString}");

            using (var connection = new SqliteConnection(connectionString)) {
            connection.Open();
            var tableCmd = connection.CreateCommand();

            tableCmd.CommandText =
            @"CREATE TABLE IF NOT EXISTS coding_track (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                StartTime TEXT,
                EndTime TEXT,
                Duration INTEGER
            )";
            tableCmd.ExecuteNonQuery(); // means we dont want the database to return any values

            connection.Close();

            }
        }
    }
}
