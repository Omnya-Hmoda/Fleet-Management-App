using FPro;
using Npgsql;
using System;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace managementclasses
{
    public class Vehicle
    {
        string connectionString = "Host=localhost;Username=postgres;Password=;Database=Omnya_FMS";

        public GVAR AddVehicle(GVAR gvar)
        {
            // Check if gvar and its necessary keys are present
            if (gvar != null && gvar.DicOfDic.ContainsKey("Tags") && gvar.DicOfDic["Tags"] != null)
            {
                // Get vehicle number and type from gvar
                string vehicleNumber = gvar.DicOfDic["Tags"].GetValueOrDefault("VehicleNumber");
                string vehicleType = gvar.DicOfDic["Tags"].GetValueOrDefault("VehicleType");

                // Check if vehicle number and type are not null or empty
                if (!string.IsNullOrEmpty(vehicleNumber) && !string.IsNullOrEmpty(vehicleType))
                {
                    try
                    {
                        // Prepare SQL query
                        string query = "INSERT INTO \"Vehicles\"(\"VehicleNumber\",\"VehicleType\") VALUES (@VehicleNumber,@VehicleType)";

                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                        {
                            connection.Open();
                            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                            {
                                // Add parameters to the command
                                command.Parameters.AddWithValue("VehicleNumber", Convert.ToInt64(vehicleNumber));
                                command.Parameters.AddWithValue("VehicleType", vehicleType);

                                // Execute the command
                                int rowsAffected = command.ExecuteNonQuery();
                                Console.WriteLine($"{rowsAffected} row(s) inserted.");

                                // Update gvar status
                                gvar.DicOfDic["Tags"]["sts"] = "1";
                                return gvar;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error inserting vehicle: {ex.Message}");
                    }
                }
            }

            // If execution reaches here, there was an error or missing data
            // Update gvar status accordingly
            if (gvar != null && gvar.DicOfDic.ContainsKey("Tags"))
            {
                gvar.DicOfDic["Tags"]["sts"] = "0";
            }

            return gvar;
        }

        public GVAR UpdateVehicle(GVAR gvar)
        {
            if (gvar != null && gvar.DicOfDic.ContainsKey("Tags") && gvar.DicOfDic["Tags"] != null)
            {   string vehicleID = gvar.DicOfDic["Tags"].GetValueOrDefault("VehicleID");
                string vehicleNumber = gvar.DicOfDic["Tags"].GetValueOrDefault("VehicleNumber");
                string vehicleType = gvar.DicOfDic["Tags"].GetValueOrDefault("VehicleType");

                // Check if vehicle ID, number, and type are not null or empty
                if (!string.IsNullOrEmpty(vehicleID) && !string.IsNullOrEmpty(vehicleNumber) && !string.IsNullOrEmpty(vehicleType))
                {
                    try
                    {
                        // Prepare SQL query
                        string query = "UPDATE \"Vehicles\" SET \"VehicleNumber\" = @VehicleNumber, \"VehicleType\" = @VehicleType WHERE \"VehicleID\" = @VehicleID";

                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                        {
                            connection.Open();
                            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                            {
                                // Add parameters to the command
                                command.Parameters.AddWithValue("VehicleID", Convert.ToInt64(vehicleID));
                                command.Parameters.AddWithValue("VehicleNumber", Convert.ToInt64(vehicleNumber));
                                command.Parameters.AddWithValue("VehicleType", vehicleType);

                                // Execute the command
                                int rowsAffected = command.ExecuteNonQuery();
                                Console.WriteLine($"{rowsAffected} row(s) updated.");

                                // Update gvar status
                                gvar.DicOfDic["Tags"]["sts"] = "1";
                                return gvar;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating vehicle: {ex.Message}");
                    }
                }
            }

            // If execution reaches here, there was an error or missing data
            // Update gvar status accordingly
            if (gvar != null && gvar.DicOfDic.ContainsKey("Tags"))
            {
                gvar.DicOfDic["Tags"]["sts"] = "0";
            }

            return gvar;
        }


        public void DeleteVehicle(String vehicleId)
        {
            try
                    {
                        string query2 = "DELETE FROM \"VehiclesInformations\" WHERE \"VehicleID\" = @VehicleID";
                        string query1 = "DELETE FROM \"RouteHistory\" WHERE \"VehicleID\" =@VehicleID"; 
                        string query = "DELETE FROM \"Vehicles\" WHERE\"VehicleID\" =@VehicleID";
                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                        {
                            connection.Open();
                            using (NpgsqlCommand command = new NpgsqlCommand(query2, connection))
                            {
                                command.Parameters.AddWithValue("VehicleID", Convert.ToInt64(vehicleId));
                                int rowsAffected = command.ExecuteNonQuery();
                                Console.WriteLine($"{rowsAffected} row(s) deleted1.");
                                using (NpgsqlCommand command1 = new NpgsqlCommand(query1, connection))
                                {
                                    command1.Parameters.AddWithValue("VehicleID", Convert.ToInt64(vehicleId));
                                     rowsAffected = command1.ExecuteNonQuery();
                                    Console.WriteLine($"{rowsAffected} row(s) deleted.");
                                    using (NpgsqlCommand command2 = new NpgsqlCommand(query, connection))
                                    {
                                        command2.Parameters.AddWithValue("VehicleID", Convert.ToInt64(vehicleId));
                                        rowsAffected = command2.ExecuteNonQuery();
                                        Console.WriteLine($"{rowsAffected} row(s) deleted.");
                                        return ;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting vehicle: {ex.Message}");
                    }
           
           

            return ;
        }


    
}

    public class VehicleInformation
    {
        string connectionString = "Host=localhost;Username=postgres;Password=;Database=Omnya_FMS";
        public GVAR AddVehicleInformation(GVAR gvar)
        {
            // Check if gvar and its necessary keys are present
            if (gvar != null && gvar.DicOfDic.ContainsKey("Tags") && gvar.DicOfDic["Tags"] != null)
            {
                // Get vehicle number and type from gvar
                string VehicleID = gvar.DicOfDic["Tags"].GetValueOrDefault("VehicleID");
                string DriverID = gvar.DicOfDic["Tags"].GetValueOrDefault("DriverID");
                string VehicleMake = gvar.DicOfDic["Tags"].GetValueOrDefault("VehicleMake");
                string VehicleModel = gvar.DicOfDic["Tags"].GetValueOrDefault("VehicleModel");
                string PurchaseDate = gvar.DicOfDic["Tags"].GetValueOrDefault("PurchaseDate");

                // Check if vehicle number and type are not null or empty
                if (!string.IsNullOrEmpty(VehicleID) && !string.IsNullOrEmpty(DriverID))
                {
                    try
                    {
                        // Prepare SQL query
                        string query = "INSERT INTO \"VehiclesInformations\"(\"VehicleID\",\"DriverID\",\"VehicleMake\",\"VehicleModel\",\"PurchaseDate\") VALUES (@VehicleID,@DriverID,@VehicleMake,@VehicleModel,@PurchaseDate)";

                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                        {
                            connection.Open();
                            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                            {
                                // Add parameters to the command
                                command.Parameters.AddWithValue("VehicleID", Convert.ToInt64(VehicleID));
                                command.Parameters.AddWithValue("DriverID", Convert.ToInt64(DriverID));
                                command.Parameters.AddWithValue("VehicleMake", VehicleMake);
                                command.Parameters.AddWithValue("VehicleModel", VehicleModel);
                                command.Parameters.AddWithValue("PurchaseDate", Convert.ToInt64(PurchaseDate));
                                // Execute the command
                                int rowsAffected = command.ExecuteNonQuery();
                                Console.WriteLine($"{rowsAffected} row(s) inserted.");

                                // Update gvar status
                                gvar.DicOfDic["Tags"]["sts"] = "1";
                                return gvar;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error inserting vehicle information : {ex.Message}");
                    }
                }
            }

            // If execution reaches here, there was an error or missing data
            // Update gvar status accordingly
            if (gvar != null && gvar.DicOfDic.ContainsKey("Tags"))
            {
                gvar.DicOfDic["Tags"]["sts"] = "0";
            }

            return gvar;
        }

        public GVAR UpdateVehicleInformation(GVAR gvar)
        {
            if (gvar != null && gvar.DicOfDic.ContainsKey("Tags") && gvar.DicOfDic["Tags"] != null)
            {
                string ID = gvar.DicOfDic["Tags"].GetValueOrDefault("ID");
                string VehicleID = gvar.DicOfDic["Tags"].GetValueOrDefault("VehicleID");
                string DriverID = gvar.DicOfDic["Tags"].GetValueOrDefault("DriverID");
                string VehicleMake = gvar.DicOfDic["Tags"].GetValueOrDefault("VehicleMake");
                string VehicleModel = gvar.DicOfDic["Tags"].GetValueOrDefault("VehicleModel");
                string PurchaseDate = gvar.DicOfDic["Tags"].GetValueOrDefault("PurchaseDate");

                // Check if ID is not null or empty
                if (!string.IsNullOrEmpty(ID))
                {
                    try
                    {
                        // Prepare SQL query
                        string query = "UPDATE \"VehiclesInformations\" " +
                                       "SET \"VehicleID\" = @VehicleID, " +
                                           "\"DriverID\" = @DriverID, " +
                                           "\"VehicleMake\" = @VehicleMake, " +
                                           "\"VehicleModel\" = @VehicleModel, " +
                                           "\"PurchaseDate\" = @PurchaseDate " +
                                       "WHERE \"ID\" = @ID";

                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                        {
                            connection.Open();
                            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                            {
                                // Add parameters to the command
                                command.Parameters.AddWithValue("ID", Convert.ToInt64(ID));
                                command.Parameters.AddWithValue("VehicleID", Convert.ToInt64(VehicleID));
                                command.Parameters.AddWithValue("DriverID", Convert.ToInt64(DriverID));
                                command.Parameters.AddWithValue("VehicleMake", VehicleMake);
                                command.Parameters.AddWithValue("VehicleModel", VehicleModel);
                                command.Parameters.AddWithValue("PurchaseDate", Convert.ToInt64(PurchaseDate));

                                // Execute the command
                                int rowsAffected = command.ExecuteNonQuery();
                                Console.WriteLine($"{rowsAffected} row(s) updated.");

                                // Update gvar status
                                gvar.DicOfDic["Tags"]["sts"] = "1";
                                return gvar;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating vehicle: {ex.Message}");
                    }
                }
            }

            // If execution reaches here, there was an error or missing data
            // Update gvar status accordingly
            if (gvar != null && gvar.DicOfDic.ContainsKey("Tags"))
            {
                gvar.DicOfDic["Tags"]["sts"] = "0";
            }

            return gvar;
        }
      
        public void DeleteVehicleInformation(String ID)
        {
           // bool doneDelete = false;


            try
            {
                string query = "DELETE FROM \"VehiclesInformations\" WHERE \"ID\" = @ID";
                ;
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                        using (NpgsqlCommand command2 = new NpgsqlCommand(query, connection))
                        {
                            command2.Parameters.AddWithValue("ID", Convert.ToInt64(ID));
                           int rowsAffected = command2.ExecuteNonQuery();
                            Console.WriteLine($"{rowsAffected} row(s) deleted.");

                        return;
                        }
                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting vehicle: {ex.Message}");
            }



            return;
        }

        public GVAR AllVehicleDetails()
        {
            GVAR gvar = new GVAR();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"WITH LastRouteHistory AS (SELECT *, ROW_NUMBER() OVER (PARTITION BY ""VehicleID"" ORDER BY ""Epoch"" DESC) AS RowNum FROM ""RouteHistory"") SELECT v.""VehicleID"", v.""VehicleNumber"", v.""VehicleType"", lrh.""VehicleDirection"", lrh.""Status"", lrh.""Address"", lrh.""Latitude"", lrh.""Longitude"" FROM ""Vehicles"" v JOIN LastRouteHistory lrh ON v.""VehicleID"" = lrh.""VehicleID"" WHERE lrh.RowNum = 1";

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvar.DicOfDT["Vehicles"] = dataTable;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (if you have a logging mechanism) or handle it as needed
                Console.WriteLine("An error occurred: " + ex.Message);
                 gvar.DicOfDic["Tags"]["sts"] = "0";
                gvar.DicOfDic["Tags"]["error"] = ex.Message;
            }
            return gvar;
        }


        public GVAR GetVehicleInformation(String vehicleID)
        {
            GVAR gvar = new GVAR();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT 
                    v.""VehicleNumber"",
                    v.""VehicleType"",
                    d.""DriverName"",
                    d.""PhoneNumber"",
                    CONCAT(rh.""Latitude"", ',', rh.""Longitude"") AS ""Position"",
                    vi.""VehicleMake"",
                    vi.""VehicleModel"",
                    rh.""Epoch"",
                    rh.""VehicleSpeed"",
                    rh.""Address""
                FROM 
                    ""Vehicles"" v
                JOIN 
                    ""VehiclesInformations"" vi ON v.""VehicleID"" = vi.""VehicleID""
                JOIN 
                    ""Driver"" d ON vi.""DriverID"" = d.""DriverID""
                JOIN 
                    (
                        SELECT 
                            *,
                            ROW_NUMBER() OVER (PARTITION BY ""VehicleID"" ORDER BY ""Epoch"" DESC) AS rn
                        FROM 
                            ""RouteHistory""
                    ) rh ON v.""VehicleID"" = rh.""VehicleID""
                WHERE 
                    v.""VehicleID"" = @VehicleID
                    AND rh.rn = 1";

                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("@VehicleID", Convert.ToInt64(vehicleID));

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvar.DicOfDT["VehicleInformation"] = dataTable;
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
