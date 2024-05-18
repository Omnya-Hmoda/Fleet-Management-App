using FPro;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace managementclasses
{
    public class RouteHistory
    {
        string connectionString = "Host=localhost;Username=postgres;Password=;Database=Omnya_FMS";

        public GVAR AddHistoricalPoint(GVAR gvar)
        {
            string VehicleID = gvar.DicOfDic["Tags"].GetValueOrDefault("VehicleID");
            string VehicleDirection = gvar.DicOfDic["Tags"].GetValueOrDefault("VehicleDirection");
            string Status = gvar.DicOfDic["Tags"].GetValueOrDefault("Status");
            string VehicleSpeed = gvar.DicOfDic["Tags"].GetValueOrDefault("VehicleSpeed");
            string Epoch = gvar.DicOfDic["Tags"].GetValueOrDefault("Epoch");
            string Address = gvar.DicOfDic["Tags"].GetValueOrDefault("Address");
            string Latitude = gvar.DicOfDic["Tags"].GetValueOrDefault("Latitude");
            string Longitude = gvar.DicOfDic["Tags"].GetValueOrDefault("Longitude");

            string query = @"INSERT INTO ""RouteHistory"" (""VehicleID"", ""VehicleDirection"", ""Status"", ""VehicleSpeed"", ""Epoch"", ""Address"", ""Latitude"", ""Longitude"")
                         VALUES (@VehicleID, @VehicleDirection, @Status, @VehicleSpeed, @Epoch, @Address, @Latitude, @Longitude);";

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("VehicleID", Convert.ToInt64(VehicleID));
                        command.Parameters.AddWithValue("VehicleDirection", Convert.ToInt64(VehicleDirection));
                        command.Parameters.AddWithValue("Status", Status);
                        command.Parameters.AddWithValue("VehicleSpeed", VehicleSpeed);
                        command.Parameters.AddWithValue("Epoch", Convert.ToInt64(Epoch));
                        command.Parameters.AddWithValue("Address", Address);
                        command.Parameters.AddWithValue("Latitude", Convert.ToSingle(Latitude));
                        command.Parameters.AddWithValue("Longitude", Convert.ToSingle(Longitude));

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} row(s) inserted.");
                        gvar.DicOfDic["Tags"]["sts"] = "1";
                        return gvar;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting vehicle information : {ex.Message}");
            }



            // If execution reaches here, there was an error or missing data
            // Update gvar status accordingly
            if (gvar != null && gvar.DicOfDic.ContainsKey("Tags"))
            {
                gvar.DicOfDic["Tags"]["sts"] = "0";
            }
            return gvar;
        }

        public GVAR GetVehicleRouteHistory(string vehicleID, long startEpoch, long endEpoch)
        {
            GVAR gvar = new GVAR();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT 
                    v.""VehicleID"",
                    v.""VehicleNumber"",
                    rh.""Address"",
                    rh.""Status"",
                    rh.""Latitude"",
                    rh.""Longitude"",
                    rh.""VehicleDirection"",
                    rh.""VehicleSpeed""
                FROM 
                    ""Vehicles"" v
                JOIN 
                    ""RouteHistory"" rh ON v.""VehicleID"" = rh.""VehicleID""
                WHERE 
                    v.""VehicleID"" = @VehicleID
                    AND rh.""Epoch"" BETWEEN @StartEpoch AND @EndEpoch";

                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("@VehicleID", Convert.ToInt64(vehicleID));
                    command.Parameters.AddWithValue("@StartEpoch", startEpoch);
                    command.Parameters.AddWithValue("@EndEpoch", endEpoch);

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvar.DicOfDT["RouteHistory"] = dataTable;
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
     
