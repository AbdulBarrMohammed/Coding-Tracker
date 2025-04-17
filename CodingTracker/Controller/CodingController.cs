using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingTracker.Data;
using CodingTracker.Model;
using Microsoft.Data.Sqlite;

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

        public void RemoveCodeItem()
        {

        }

        public void GetCodeItem()
        {

        }

        public void UpdateCodeItem()
        {

        }
    }
}
