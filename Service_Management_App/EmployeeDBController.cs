using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Service_Management_App.Data;

namespace Service_Management_App
{
    class EmployeeDBController
    {
        private string conString;

        public EmployeeDBController()
        {
            GetConnectionString();
            //Trace.WriteLine(conString);
        }

        public string GetConnectionString()
        {
            conString = ConfigurationManager.ConnectionStrings["Lacalhost_connection"].ToString();
            return conString;
        }

        public int SearchEmployee(string iCNumber)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SearchEmployee", connection);
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Identity_Card_Number", SqlDbType.VarChar, 20).Value = iCNumber;

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int i = reader.GetInt32(0);
                        reader.Close();
                        return i;
                    }
                    return 0;
                }
                catch (SqlException)
                {
                    Trace.WriteLine("Problem here!");
                    return 0;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public int InsertEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("spEmployeeInsert");
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@First_Name", SqlDbType.VarChar, 20).Value = employee.FirstName;
                cmd.Parameters.Add("@Last_Name", SqlDbType.VarChar, 20).Value = employee.LastName;
                cmd.Parameters.Add("@Identity_Card_Number", SqlDbType.VarChar, 20).Value = employee.ICNumber;
                cmd.Parameters.Add("@Hiring_Date", SqlDbType.DateTime).Value = employee.HiringDate;
                cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 20).Value = employee.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 20).Value = employee.Email;
                Trace.WriteLine(employee);
                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    int i = reader.GetInt32(0);
                    reader.Close();
                    return i;
                }
                catch (SqlException)
                {
                    Trace.WriteLine("Problem here!");
                    return 0;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("GetEmployees", connection);
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        employees.Add(new Employee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4), reader.GetString(5), reader.GetString(6), reader.GetBoolean(7)));
                    }
                    reader.Close();
                    return employees;
                }
                catch (SqlException)
                {
                    Trace.WriteLine("Problem here!");
                    return employees;
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        public int UpdateEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("UpdateEmployee", connection);
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Employee", SqlDbType.Int).Value = employee.Id;
                cmd.Parameters.Add("@First_Name", SqlDbType.VarChar, 20).Value = employee.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 20).Value = employee.LastName;
                cmd.Parameters.Add("@Identity_Card_Number", SqlDbType.VarChar, 20).Value = employee.ICNumber;
                cmd.Parameters.Add("@Hiring_Date", SqlDbType.DateTime).Value = employee.HiringDate;
                cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 20).Value = employee.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 20).Value = employee.Email;
                Trace.WriteLine(employee);
                try
                {
                    connection.Open();
                    int recordsAffected = cmd.ExecuteNonQuery();
                    Trace.WriteLine(recordsAffected);
                    return recordsAffected;
                }
                catch (SqlException)
                {
                    Trace.WriteLine("Problem here!");
                    return 0;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public int RemoveEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                Trace.WriteLine(employee);
                SqlCommand cmd = new SqlCommand("RemoveEmployee", connection);
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Employee", SqlDbType.Int).Value = employee.Id;
                try
                {
                    connection.Open();
                    int recordsAffected = cmd.ExecuteNonQuery();
                    Trace.WriteLine(recordsAffected);
                    return recordsAffected;
                }
                catch (SqlException)
                {
                    Trace.WriteLine("Problem here!");
                    return 0;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}
