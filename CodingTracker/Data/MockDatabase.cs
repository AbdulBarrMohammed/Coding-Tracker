using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingTracker.Model;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Data
{
    public static class MockDatabase
    {
        public static List<CodeItem> codeItems = new();
        static string connectionString = @"Data Source=coding-Tracker.db";
        //connecting to database

        public static void ConnectToDatabase()
        {
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
