using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CustomerMaintenance
{
	class StateDB
	{
		public static List<State> GetStates()
		{
			//Creates a list of states for the state names to be held in.
			List<State> states = new List<State>();
			SqlConnection connection = MMABooksDB.GetConnection();
			//Don't forget spaces after each line!
			string selectStatement = "Select StateCode, StateName " 
				+ "From States " 
				+ "Order By StateName";
			SqlCommand selectCommand = new SqlCommand(selectStatement, connection);

			try
			{
				//Try to open connection
				connection.Open();
				//Create a SQL Reader that will read through the States
				SqlDataReader stateReader = selectCommand.ExecuteReader();
				//While the stateReader reads more states, add them to the list of states
				while (stateReader.Read())
				{
					State s = new State();
					s.StateCode = stateReader["StateCode"].ToString();
					s.StateName = stateReader["StateName"].ToString();
					states.Add(s);
				}
				stateReader.Close();
			}
			//if the connection open fails
			catch (SqlException ex)
			{
				throw ex;
			}
			finally
			{
				//Once the reader is finished adding all of the states to the list, close the connection.
				connection.Close();
			}
			return states;
		}
	}
}
