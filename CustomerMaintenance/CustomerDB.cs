using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CustomerMaintenance
{
	class CustomerDB
	{
		public static Customer GetCustomer(int customerID)
		{
			SqlConnection connection = MMABooksDB.GetConnection();
			//Make sure to have a space at the end of each line so the statement works correctly!
			string selectStatement = "SELECT CustomerID, Name, Address, City, State, Zipcode " + "From Customers "
				+ "Where CustomerID = @CustomerID";
			SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
			selectCommand.Parameters.AddWithValue("@CustomerID", customerID);

			try
			{
				//Try to open connection
				connection.Open();
				//Create a SQL Reader that will output a single row.
				SqlDataReader custReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
				//If the custReader object Reads a row that is equal to the customerID inputted, return these customer values.
				if (custReader.Read())
				{
					Customer customer = new Customer();
					customer.CustomerID = (int)custReader["CustomerID"];
					customer.Name = custReader["Name"].ToString();
					customer.Address = custReader["Address"].ToString();
					customer.City = custReader["City"].ToString();
					customer.State = custReader["State"].ToString();
					customer.ZipCode = custReader["ZipCode"].ToString();
					return customer;
				}
				//If no record is found, return null
				else
				{
					return null;
				}
			}
			//if the connection open fails
			catch (SqlException ex)
			{
				throw ex;
			}
			finally
			{
				//Regardless if a customer was found with the user inputted ID number, the connection will close.
				connection.Close();
			}
		}

		public static bool UpdateCustomer(Customer oldCustomer, Customer newCustomer)
		{
			SqlConnection connection = MMABooksDB.GetConnection();
			//Make sure to have a space at the end of each line so the statement works correctly!
			string updateStatement =
		   "UPDATE Customers SET " + "Name = @NewName, " + "Address = @NewAddress, " + "City = @NewCity, "
		   + "State = @NewState, " + "ZipCode = @NewZipCode "
		   + "WHERE CustomerID = @oldCustomerID " + "AND Name = @OldName " +
		   "AND Address = @OldAddress " + "AND City = @OldCity " + "AND State = @OldState " + "AND ZipCode = @OldZipCode";
			SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
			updateCommand.Parameters.AddWithValue("@NewName", newCustomer.Name);
			updateCommand.Parameters.AddWithValue("@NewAddress", newCustomer.Address);
			updateCommand.Parameters.AddWithValue("@NewCity", newCustomer.City);
			updateCommand.Parameters.AddWithValue("@NewState", newCustomer.State);
			updateCommand.Parameters.AddWithValue("@NewZipCode", newCustomer.ZipCode);
			updateCommand.Parameters.AddWithValue("@OldCustomerID", oldCustomer.CustomerID);
			updateCommand.Parameters.AddWithValue("@OldName", oldCustomer.Name);
			updateCommand.Parameters.AddWithValue("@OldAddress", oldCustomer.Address);
			updateCommand.Parameters.AddWithValue("@OldCity", oldCustomer.City);
			updateCommand.Parameters.AddWithValue("@OldState", oldCustomer.State);
			updateCommand.Parameters.AddWithValue("@OldZipCode", oldCustomer.ZipCode);

			try
			{
				//Try to open connection
				connection.Open();
				//Returns the number of rows affected
				int count = updateCommand.ExecuteNonQuery();
				//if the number of rows affected is greater than 1, return true, else false.
				if (count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			//if the connection open fails
			catch (SqlException ex)
			{
				throw ex;
			}
			finally
			{
				//Regardless if a customer was actually modified or not, close the connection.
				connection.Close();
			}
		}

		public static int AddCustomer(Customer customer)
		{
			SqlConnection connection = MMABooksDB.GetConnection();
			string insertStatement = "Insert Customers " +
				"(Name, Address, City, State, ZipCode) " +
				"Values (@Name, @Address, @City, @State, @ZipCode) ";
			SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
			insertCommand.Parameters.AddWithValue("@Name", customer.Name);
			insertCommand.Parameters.AddWithValue("@Address", customer.Address);
			insertCommand.Parameters.AddWithValue("@City", customer.City);
			insertCommand.Parameters.AddWithValue("@State", customer.State);
			insertCommand.Parameters.AddWithValue("@ZipCode", customer.ZipCode);
			try
			{
				//Try to open connection
				connection.Open();
				insertCommand.ExecuteNonQuery();
				string selectStatement = "Select Ident_Current('Customers') From Customers";
				SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
				int customerID = Convert.ToInt32(selectCommand.ExecuteScalar());
				return customerID;
			}
			//if the connection open fails
			catch (SqlException ex)
			{
				throw ex;
			}
			finally
			{
				//Regardless if a customer was actually added or not, close the connection.
				connection.Close();
			}
		}

		public static bool DeleteCustomer(Customer customer)
		{
			SqlConnection connection = MMABooksDB.GetConnection(); string deleteStatement =
			"DELETE FROM Customers " 
			+ "WHERE CustomerID = @CustomerID " + "AND Name = @Name " + "AND Address = @Address " 
			+ "AND City = @City " + "AND State = @State " + "AND ZipCode = @ZipCode";
			SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
			deleteCommand.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
			deleteCommand.Parameters.AddWithValue("@Name", customer.Name);
			deleteCommand.Parameters.AddWithValue("@Address", customer.Address);
			deleteCommand.Parameters.AddWithValue("@City", customer.City);
			deleteCommand.Parameters.AddWithValue("@State", customer.State);
			deleteCommand.Parameters.AddWithValue("@ZipCode", customer.ZipCode);
			try
			{
				//Try to open connection
				connection.Open();
				int count = deleteCommand.ExecuteNonQuery();
				if (count > 0)
				{
					return true;
				}
				else
				{
					return false;
				}

			}
			//if the connection open fails
			catch (SqlException ex)
			{
				throw ex;
			}
			finally
			{
				//Regardless if a customer was actually added or not, close the connection.
				connection.Close();
			}
		}
	}
}
