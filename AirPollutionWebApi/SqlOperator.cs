using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AirPollutionWebApi.Models;

namespace AirPollutionWebApi.Singletons
{
	public static class SqlOperator
	{
		const string ConnectionString = 
            "Server=tcp:forschool.database.windows.net,1433;" +
            "Initial Catalog=schooldb;" +
            "Persist Security Info=False;" +
            "User ID=***REMOVED***;" +
            "Password=***REMOVED***;" +
            "MultipleActiveResultSets=False;" +
            "Encrypt=True;" +
            "TrustServerCertificate=False;" +
            "Connection Timeout=30;";

        public static List<Reading> GetReadings(string command)
		{
			var readings = new List<Reading>();

			using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
			{
				databaseConnection.Open();
				SqlCommand selectCommand = new SqlCommand(command, databaseConnection);
				var reader = selectCommand.ExecuteReader();
				while (reader.Read())
				{
					readings.Add(new Reading
					{
                        Id = reader.GetInt32(0),
                        TimeStamp = reader.GetInt32(1),
                        Co = reader.GetDecimal(2),
						No = reader.GetDecimal(3),
						So = reader.GetDecimal(4)
					});
				}
			}

            return readings;
		}

        public static void PutReading(int id, Reading reading)
		{
			using (SqlConnection dbCon = new SqlConnection(ConnectionString))
			{
				dbCon.Open();
                string query = $"UPDATE Readings SET TimeStamp='{reading.TimeStamp}'," +
                    $"Co='{reading.Co}', No='{reading.No}', So='{reading.So}' WHERE Id={id};";
				var cmd = new SqlCommand(query, dbCon);
				cmd.ExecuteNonQuery();
				dbCon.Close();
			}
		}

        public static void PostReading(Reading reading)
		{
			using (SqlConnection dbCon = new SqlConnection(ConnectionString))
			{
				dbCon.Open();
                string query = $"INSERT INTO Readings (TimeStamp,Co,No,So)" +
                    $"VALUES('{reading.TimeStamp}',{reading.Co},{reading.No},{reading.So});";
				var cmd = new SqlCommand(query, dbCon);
				cmd.ExecuteNonQuery();
				dbCon.Close();
			}
		}

        public static void DeleteReading(int id)
		{
			using (SqlConnection dbCon = new SqlConnection(ConnectionString))
			{
				dbCon.Open();
				string query = $"DELETE FROM Readings WHERE Id={id};";
				var cmd = new SqlCommand(query, dbCon);
				cmd.ExecuteNonQuery();
				dbCon.Close();
			}
		}
	}
}