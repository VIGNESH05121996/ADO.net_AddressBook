using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;

namespace ADO.netAddressBook
{
    public class AddressBookService
    {
        public static string connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AddressBook;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Create Connection String
        SqlConnection sqlConnection = new SqlConnection(connection);

        //Table creation
        public void CreateTable()
        {
            try
            {
                Contact contact = new Contact();
                string query = @"select * from  addresstable"; //query for feteching the database
                SqlCommand command = new SqlCommand(query, sqlConnection); //command object to execute query on database
                this.sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows) //command to check rows present or not in selected table
                {
                    while(reader.Read()) //if present then read rows
                    {
                        contact.FirstName = Convert.ToString(reader["FirstName"] == DBNull.Value ? default : reader["FirstName"]);
                        contact.LastName = Convert.ToString(reader["LastName"] == DBNull.Value ? default : reader["LastName"]);
                        contact.Address = Convert.ToString(reader["Address"] == DBNull.Value ? default : reader["Address"]);
                        contact.City = Convert.ToString(reader["City"] == DBNull.Value ? default : reader["City"]);
                        contact.State = Convert.ToString(reader["States"] == DBNull.Value ? default : reader["States"]);
                        contact.Zip = Convert.ToDouble(reader["Zip"] == DBNull.Value ? default : reader["Zip"]);
                        contact.PhoneNumber = Convert.ToDouble(reader["PhoneNumber"] == DBNull.Value ? default : reader["PhoneNumber"]);
                        contact.Email = Convert.ToString(reader["Email"] == DBNull.Value ? default : reader["Email"]);

                        Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}", contact.FirstName, contact.LastName, contact.Address, contact.City, contact.State, contact.Zip, contact.PhoneNumber, contact.Email);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        public void DisplayContact(SqlDataReader reader)
        {
            Contact contact = new Contact();
            contact.FirstName = Convert.ToString(reader["FirstName"]);
            contact.LastName = Convert.ToString(reader["LastName"]);
            contact.Address = Convert.ToString(reader["Address"]);
            contact.City = Convert.ToString(reader["City"]);
            contact.State = Convert.ToString(reader["States"]);
            contact.Zip = Convert.ToDouble(reader["Zip"]);
            contact.PhoneNumber = Convert.ToDouble(reader["PhoneNumber"]);
            contact.Email = Convert.ToString(reader["Email"]);

            Console.WriteLine("\n{0},{1},{2},{3},{4},{5},{6},{7}", contact.FirstName, contact.LastName, contact.Address, contact.City, contact.State, contact.Zip, contact.PhoneNumber, contact.Email);
        }

        //Insert New Contact
        public void InsertNewContact(Contact contact)
        {
            try
            {
                using (this.sqlConnection)
                {
                    SqlCommand command = new SqlCommand("dbo.spAddAddressBookDetails", this.sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                    command.Parameters.AddWithValue("@LastName", contact.LastName);
                    command.Parameters.AddWithValue("@Address", contact.Address);
                    command.Parameters.AddWithValue("@City", contact.City);
                    command.Parameters.AddWithValue("@States", contact.State);
                    command.Parameters.AddWithValue("@Zip", contact.Zip);
                    command.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                    command.Parameters.AddWithValue("@Email", contact.Email);

                    this.sqlConnection.Open();
                    var result = command.ExecuteNonQuery();
                    if (result == 0)
                    {
                        Console.WriteLine("No Data Added");
                    }
                    else
                    {
                        Console.WriteLine("Employee Data Added");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
               this.sqlConnection.Close();
            }
        }

        public void EditContact()
        {
            try
            {
                this.sqlConnection.Open();
                string query = @"Update addresstable set Address = 'NGO Colony' where FirstName='Praveen'";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine("Updated");
                    }
                    else
                    {
                        Console.WriteLine("Not Updated");
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        public void DeleteContact()
        {
            try
            {
                this.sqlConnection.Open();
                string query = @"delete from addresstable where FirstName='Praveen'";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    Console.WriteLine("Deleted");
                }
                else
                {
                    Console.WriteLine("Not Deleted");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        public void RetrivePerson()
        {
            try
            {
                string nameList = "";
                this.sqlConnection.Open();
                string query = @"select * from addresstable where City='Chennai' or States='Tamil Nadu' ";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        DisplayContact(reader);
                        nameList += reader["FirstName"].ToString() + " ";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        public void SizeOfCityState()
        {
            try
            {
                string nameList = "";
                this.sqlConnection.Open();
                string query = @"select Count(*),States,City from addresstable Group by States,City";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} \t {1} \t {2}", reader[0], reader[1], reader[2]);
                        nameList += reader[0].ToString() + " ";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

        public void SortPersonByCity()
        {
            try
            {
                string nameList = "";
                this.sqlConnection.Open();
                string query = @"select * from addresstable order by City,FirstName";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DisplayContact(reader);
                        nameList += reader["FirstName"].ToString() + " ";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
    }
}
