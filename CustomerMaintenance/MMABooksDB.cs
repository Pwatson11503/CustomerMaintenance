using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CustomerMaintenance
{
	class MMABooksDB
	{
		public static SqlConnection GetConnection()
		{
			//Creates a variable containing the connection string to allow the user to connect to the database
			string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
				"AttachDbFilename =|DataDirectory|\\MMABooks.mdf; " +
				"Integrated Security=True";
			//Creates a Sql connection instance
			SqlConnection connection = new SqlConnection(connectionString);
			return connection;
		}


	}
}
