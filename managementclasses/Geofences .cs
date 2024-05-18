using FPro;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace managementclasses
{

    public class Geofences
    {
        string connectionString = "Host=localhost;Username=postgres;Password=;Database=Omnya_FMS";
        public GVAR GetAllGeofences()
        {
            GVAR gvar = new GVAR();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                    SELECT 
                        ""GeofenceID"",
                        ""GeofenceType"",
                        ""AddedDate"",
                        ""StrokeColor"",
                        ""StrokeOpacity"",
                        ""StrokeWeight"",
                        ""FillColor"",
                        ""FillOpacity""
                    FROM 
                        ""Geofences""";

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvar.DicOfDT["Geofences"] = dataTable;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (if you have a logging mechanism) or handle it as needed
                Console.WriteLine("An error occurred: " + ex.ToString());

                gvar.DicOfDic["Tags"]["sts"] = "0";
                gvar.DicOfDic["Tags"]["error"] = ex.Message;
            }
            return gvar;
        }

        public GVAR GetAllCircularGeofences()
        {
            GVAR gvar = new GVAR();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                    SELECT 
                        ""GeofenceID"",
                        ""Radius"",
                        ""Latitude"",
                        ""Longitude""
                    FROM 
                        ""CircleGeofence""";

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvar.DicOfDT["CircleGeofence"] = dataTable;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (if you have a logging mechanism) or handle it as needed
                Console.WriteLine("An error occurred: " + ex.ToString());

                gvar.DicOfDic["Tags"]["sts"] = "0";
                gvar.DicOfDic["Tags"]["error"] = ex.Message;
            }
            return gvar;
        }



        public GVAR GetAllRectangleGeofence()
        {
            GVAR gvar = new GVAR();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                    SELECT 
                        ""GeofenceID"",
                        ""North"",
                        ""East"",
                        ""West"",
                        ""South""
                    FROM 
                        ""RectangleGeofence""";

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvar.DicOfDT["RectangleGeofence"] = dataTable;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (if you have a logging mechanism) or handle it as needed
                Console.WriteLine("An error occurred: " + ex.ToString());

                gvar.DicOfDic["Tags"]["sts"] = "0";
                gvar.DicOfDic["Tags"]["error"] = ex.Message;
            }
            return gvar;
        }

        public GVAR GetPolygonGeofence()
        {
            GVAR gvar = new GVAR();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                    SELECT 
                        ""GeofenceID"",
                        ""Latitude"",
                        ""Longitude""
                    FROM 
                        ""PolygonGeofence""";

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvar.DicOfDT["PolygonGeofence"] = dataTable;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (if you have a logging mechanism) or handle it as needed
                Console.WriteLine("An error occurred: " + ex.ToString());

                gvar.DicOfDic["Tags"]["sts"] = "0";
                gvar.DicOfDic["Tags"]["error"] = ex.Message;
            }
            return gvar;
        }
    }
}
