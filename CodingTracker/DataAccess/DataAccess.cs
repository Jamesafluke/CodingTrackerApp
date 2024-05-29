
using CodingTracker.CodingTrackerLibrary;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using System.Configuration;
using System.Collections.Specialized;
using System;

namespace CodingTracker.DataAccess;

static public class DataAccess
{
    static string connectionString = ConfigurationManager.AppSettings.Get("connectionString");
    

    static public void InitizeDatabase()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();

            tableCmd.CommandText =
                @"CREATE TABLE IF NOT EXISTS TimeEntries (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    StartTime TEXT,
                    EndTime TEXT,
                    Duration TEXT
                    )";

            tableCmd.ExecuteNonQuery(); //Execute but don't return values.

            connection.Close();
        }
    }

    static public void CreateRecord(TimeEntry timeEntry)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var tableCommand = connection.CreateCommand();

            tableCommand.CommandText =
                $"INSERT INTO TimeEntries(StartTime, EndTime, Duration)" +
                $"VALUES('{timeEntry.StartTime}', '{timeEntry.EndTime}', '{timeEntry.Duration}');";

            tableCommand.ExecuteNonQuery();

            connection.Close();
        }
    }

    static public List<TimeEntry> GetAllRecords()
    {
        List<TimeEntry> timeEntries = new List<TimeEntry>();

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var tableCommand = connection.CreateCommand();

            tableCommand.CommandText = "SELECT * FROM TimeEntries";

            var reader = tableCommand.ExecuteReader();

            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    TimeEntry timeEntry = new TimeEntry();
                    timeEntry.Id = reader.GetInt32(0);
                    timeEntry.StartTime = reader.GetDateTime(1);
                    timeEntry.EndTime = reader.GetDateTime(2);
                    timeEntry.Duration = reader.GetTimeSpan(3);
                    timeEntries.Add(timeEntry);
                }
            }
            connection.Close();
        }
        return timeEntries;
    }

    static public TimeEntry GetRecord(int id)
    {
        TimeEntry timeEntry = new TimeEntry();

        using (var connection = new SqliteConnection(connectionString)){
            connection.Open();

            var tableCommand = connection.CreateCommand();

            tableCommand.CommandText = $"SELECT * FROM TimeEntries WHERE ID = {id}";

            SqliteDataReader reader = tableCommand.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                timeEntry.Id = reader.GetInt32(0);
                timeEntry.StartTime = DateTime.ParseExact(reader.GetString(1), "F", null);
                timeEntry.EndTime = DateTime.ParseExact(reader.GetString(2), "F", null);
                timeEntry.Duration = TimeSpan.FromMinutes(reader.GetInt32(3));
                
            }
        }

        return timeEntry;
    }
    internal static void UpdateRecord(int id, TimeEntry timeEntry)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var tableCommand = connection.CreateCommand();

            tableCommand.CommandText = $"UPDATE TimeEntries SET startTime = '{timeEntry.StartTime}', endTime = '{timeEntry.EndTime}', duration = '{timeEntry.Duration}' WHERE Id = {id}";

            tableCommand.ExecuteNonQuery();

            connection.Close();
        }
    }

    static public void DeleteRecord(int id)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var tableCommand = connection.CreateCommand();

            tableCommand.CommandText = $"DELETE FROM TimeEntries WHERE ID = {id}";

            tableCommand.ExecuteNonQuery();

            connection.Close();
        }
    }


}
