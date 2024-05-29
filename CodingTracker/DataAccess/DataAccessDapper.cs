using CodingTracker.CodingTrackerLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.DataAccess;

public class DataAccessDapper
{
    static string connectionString = ConfigurationManager.AppSettings.Get("SqlConnectionString");

    static List<TimeEntry> GetAllRecords()
    {
        var sql = "select * from products";
        List<TimeEntry> entries= new List<TimeEntry>();
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    TimeEntry entry = new TimeEntry
                    {
                        Id = reader.GetInt32(0),
                        StartTime = reader.GetDateTime(1),
                        EndTime = reader.GetDateTime(2),
                        Duration = reader.GetTimeSpan(3)
                    };
                    entries.Add(entry);
                }
            }

        }
        return entries;
    }

    static void InsertRecord()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

        }
    }
}
