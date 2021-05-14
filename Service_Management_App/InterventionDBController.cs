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
    class InterventionDBController
    {
        private string conString;

        public InterventionDBController()
        {
            GetConnectionString();
            //Trace.WriteLine(conString);
        }

        public string GetConnectionString()
        {
            conString = ConfigurationManager.ConnectionStrings["Lacalhost_connection"].ToString();
            return conString;
        }

        public List<Intervention> GetInterventions()
        {
            List<Intervention> interventions = new List<Intervention>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("GetInterventions", connection);
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        interventions.Add(new Intervention(reader.GetInt32(0), new Car(reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6), new Owner(reader.GetInt32(7))), reader.GetDateTime(8), reader.GetString(9), reader.GetBoolean(10)));
                    }
                    reader.Close();
                    return interventions;
                }
                catch (SqlException)
                {
                    Trace.WriteLine("Problem here!");
                    return interventions;
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        public List<Service> GetServiceForInterventions(Intervention intervention)
        {
            List<Service> services = new List<Service>();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("GetServiceForIntervention", connection);
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Intervention", SqlDbType.Int).Value = intervention.Id;
                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //services.Add(new Intervention(reader.GetInt32(0), new Car(reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6), new Owner(reader.GetInt32(7))), reader.GetDateTime(8), reader.GetString(9), reader.GetBoolean(10)));
                    }
                    reader.Close();
                    return services;
                }
                catch (SqlException)
                {
                    Trace.WriteLine("Problem here!");
                    return services;
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        //public int SearchOwner(string iCNumber)
        //{
        //    using (SqlConnection connection = new SqlConnection(conString))
        //    {
        //        SqlCommand cmd = new SqlCommand("SearchOwner", connection);
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@Identity_Card_Number", SqlDbType.VarChar, 20).Value = iCNumber;

        //        try
        //        {
        //            connection.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            if (reader.Read())
        //            {
        //                int i = reader.GetInt32(0);
        //                reader.Close();
        //                return i;
        //            }
        //            return 0;
        //        }
        //        catch (SqlException)
        //        {
        //            Trace.WriteLine("Problem here!");
        //            return 0;
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //}



        //public int InsertOwner(Owner owner)
        //{
        //    using (SqlConnection connection = new SqlConnection(conString))
        //    {
        //        SqlCommand cmd = new SqlCommand("InsertOwner", connection);
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@First_Name", SqlDbType.VarChar, 20).Value = owner.FirstName;
        //        cmd.Parameters.Add("@Last_Name", SqlDbType.VarChar, 20).Value = owner.LastName;
        //        cmd.Parameters.Add("@Identity_Card_Number", SqlDbType.VarChar, 20).Value = owner.ICNumber;
        //        cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 20).Value = owner.Phone;
        //        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 20).Value = owner.Email;

        //        try
        //        {
        //            connection.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            reader.Read();
        //            int i = reader.GetInt32(0);
        //            reader.Close();
        //            return i;
        //        }
        //        catch (SqlException)
        //        {
        //            Trace.WriteLine("Problem here!");
        //            return 0;
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //}

        //public List<Car> GetCars()
        //{
        //    List<Car> cars = new List<Car>();
        //    using (SqlConnection connection = new SqlConnection(conString))
        //    {
        //        SqlCommand cmd = new SqlCommand("GetCars", connection);
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        try
        //        {
        //            connection.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                cars.Add(new Car(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), new Owner(reader.GetInt32(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11))));
        //            }
        //            reader.Close();
        //            return cars;
        //        }
        //        catch (SqlException)
        //        {
        //            Trace.WriteLine("Problem here!");
        //            return cars;
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //}

        //public int SearchCar(string licensePlate)
        //{
        //    using (SqlConnection connection = new SqlConnection(conString))
        //    {
        //        SqlCommand cmd = new SqlCommand("SearchCar", connection);
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@License_Plate", SqlDbType.VarChar, 20).Value = licensePlate;

        //        try
        //        {
        //            connection.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            if (reader.Read())
        //            {
        //                int i = reader.GetInt32(0);
        //                reader.Close();
        //                return i;
        //            }
        //            return 0;
        //        }
        //        catch (SqlException)
        //        {
        //            Trace.WriteLine("Problem here!");
        //            return 0;
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //}

        //public int InsertCar(Car car)
        //{
        //    using (SqlConnection connection = new SqlConnection(conString))
        //    {
        //        SqlCommand cmd = new SqlCommand("InsertCar", connection);
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@Brand", SqlDbType.VarChar, 20).Value = car.Brand;
        //        cmd.Parameters.Add("@Model_Name", SqlDbType.VarChar, 20).Value = car.ModelName;
        //        cmd.Parameters.Add("@Model_Year", SqlDbType.Int).Value = car.ModelYear;
        //        cmd.Parameters.Add("@Color", SqlDbType.VarChar, 20).Value = car.Color;
        //        cmd.Parameters.Add("@License_Plate", SqlDbType.VarChar, 20).Value = car.LicensePlate;
        //        cmd.Parameters.Add("@Owner_ID", SqlDbType.Int).Value = car.Owner.ID;

        //        try
        //        {
        //            connection.Open();
        //            int recordsAffected = cmd.ExecuteNonQuery();
        //            return recordsAffected;
        //        }
        //        catch (SqlException)
        //        {
        //            Trace.WriteLine("Problem here!");
        //            return 0;
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //}

        //public int RemoveCar(Car car)
        //{
        //    using (SqlConnection connection = new SqlConnection(conString))
        //    {
        //        Trace.WriteLine(car);
        //        SqlCommand cmd = new SqlCommand("RemoveCar", connection);
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@Id_Car", SqlDbType.Int).Value = car.Id;
        //        try
        //        {
        //            connection.Open();
        //            int recordsAffected = cmd.ExecuteNonQuery();
        //            Trace.WriteLine(recordsAffected);
        //            return recordsAffected;
        //        }
        //        catch (SqlException)
        //        {
        //            Trace.WriteLine("Problem here!");
        //            return 0;
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //}

        //public int UpdateCar(Car car)
        //{
        //    using (SqlConnection connection = new SqlConnection(conString))
        //    {
        //        SqlCommand cmd = new SqlCommand("UpdateCar", connection);
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@Id_Car", SqlDbType.Int).Value = car.Id;
        //        cmd.Parameters.Add("@Brand", SqlDbType.VarChar, 20).Value = car.Brand;
        //        cmd.Parameters.Add("@Model_Name", SqlDbType.VarChar, 20).Value = car.ModelName;
        //        cmd.Parameters.Add("@Model_Year", SqlDbType.Int).Value = car.ModelYear;
        //        cmd.Parameters.Add("@Color", SqlDbType.VarChar, 20).Value = car.Color;
        //        cmd.Parameters.Add("@License_Plate", SqlDbType.VarChar, 20).Value = car.LicensePlate;
        //        cmd.Parameters.Add("@Owner_ID", SqlDbType.Int).Value = car.Owner.ID;
        //        cmd.Parameters.Add("@First_Name", SqlDbType.VarChar, 20).Value = car.Owner.FirstName;
        //        cmd.Parameters.Add("@Last_Name", SqlDbType.VarChar, 20).Value = car.Owner.LastName;
        //        cmd.Parameters.Add("@Identity_Card_Number", SqlDbType.VarChar, 20).Value = car.Owner.ICNumber;
        //        cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 20).Value = car.Owner.Phone;
        //        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 20).Value = car.Owner.Email;

        //        try
        //        {
        //            connection.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            if (reader.Read())
        //            {
        //                int i = reader.GetInt32(0);
        //                reader.Close();
        //                return i;
        //            }
        //            return 0;
        //        }
        //        catch (SqlException)
        //        {
        //            Trace.WriteLine("Problem here!");
        //            return 0;
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //}
    }
}
