using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AirPollutionWebApi.Models;

namespace AirPollutionWebApi.Singletons
{
	public class Singleton
	{
		static Singleton instance;
		const string ConnectionString = "Server=tcp:forschool.database.windows.net,1433;Initial Catalog=schooldb;Persist Security Info=False;User ID=***REMOVED***;Password=***REMOVED***;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

		public List<Reading> Readings = new List<Reading>();

		Singleton()
		{
			GetData();
		}

		public static Singleton Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Singleton();
				}
				return instance;
			}
		}

		void GetData()
		{
			Readings = new List<Reading>();
			using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
			{
				string command = "SELECT * FROM Readings";
				databaseConnection.Open();
				SqlCommand selectCommand = new SqlCommand(command, databaseConnection);
				var reader = selectCommand.ExecuteReader();
				while (reader.Read())
				{
					Readings.Add(new Reading
					{
                        TimeStamp = reader.GetDateTime(0),
						Co = reader.GetInt32(1),
						No = reader.GetInt32(2),
						So = reader.GetInt32(3)
					});
				}
			}
		}

		//public void PutData(int id, Reading reading)
		//{
		//	using (SqlConnection dbCon = new SqlConnection(ConnectionString))
		//	{
		//		dbCon.Open();
		//		string query = $"UPDATE Readings SET FirstName='{reading.FirstName}', LastName='{reading.LastName}', Year='{reading.Year}' WHERE Id={id};";
		//		var cmd = new SqlCommand(query, dbCon);
		//		cmd.ExecuteNonQuery();
		//		dbCon.Close();
		//	}
		//	GetData();
		//}

		public void PostData(Reading reading)
		{
			using (SqlConnection dbCon = new SqlConnection(ConnectionString))
			{
				dbCon.Open();
                string query = $"INSERT INTO Readings (TimeStamp,Co,No,So) VALUES('{reading.TimeStamp}',{reading.Co},{reading.No},{reading.So});";
				var cmd = new SqlCommand(query, dbCon);
				cmd.ExecuteNonQuery();
				dbCon.Close();
			}
			GetData();
		}

		public void DeleteData(int id)
		{
			using (SqlConnection dbCon = new SqlConnection(ConnectionString))
			{
				dbCon.Open();
				string query = $"DELETE FROM Readings WHERE Id={id};";
				var cmd = new SqlCommand(query, dbCon);
				cmd.ExecuteNonQuery();
				dbCon.Close();
			}
			GetData();
		}
	}
}