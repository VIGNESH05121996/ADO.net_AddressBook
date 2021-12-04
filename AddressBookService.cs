﻿using System;
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
    }
}
