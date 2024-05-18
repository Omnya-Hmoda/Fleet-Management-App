using FPro;
using Npgsql;

using System.Data;



namespace managementclasses
{
    public class Driver
    {
        string connectionString = "Host=localhost;Username=postgres;Password=;Database=Omnya_FMS";

        public GVAR AddDriver(GVAR gvar)
        {
            // Check if gvar and its necessary keys are present
            if (gvar != null && gvar.DicOfDic.ContainsKey("Tags") && gvar.DicOfDic["Tags"] != null)
            {
                // Get vehicle number and type from gvar
                string DriverName = gvar.DicOfDic["Tags"].GetValueOrDefault("DriverName");
                string PhoneNumber = gvar.DicOfDic["Tags"].GetValueOrDefault("PhoneNumber");

                // Check if vehicle number and type are not null or empty
                if (!string.IsNullOrEmpty(DriverName) && !string.IsNullOrEmpty(PhoneNumber))
                {
                    try
                    {
                        // Prepare SQL query
                        string query = "INSERT INTO \"Driver\"(\"DriverName\",\"PhoneNumber\") VALUES (@DriverName,@PhoneNumber)";

                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                        {
                            connection.Open();
                            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                            {
                                // Add parameters to the command
                                command.Parameters.AddWithValue("DriverName", DriverName);
                                command.Parameters.AddWithValue("PhoneNumber", Convert.ToInt64(PhoneNumber));

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

        public GVAR UpdateDriver(GVAR gvar)
        {
            if (gvar != null && gvar.DicOfDic.ContainsKey("Tags") && gvar.DicOfDic["Tags"] != null)
            {
                string DriverID = gvar.DicOfDic["Tags"].GetValueOrDefault("DriverID");
                string DriverName = gvar.DicOfDic["Tags"].GetValueOrDefault("DriverName");
                string PhoneNumber = gvar.DicOfDic["Tags"].GetValueOrDefault("PhoneNumber");


                // Check if vehicle ID, number, and type are not null or empty
                if (!string.IsNullOrEmpty(DriverName) && !string.IsNullOrEmpty(PhoneNumber))
                {
                    try
                    {
                        // Prepare SQL query
                        string query = "UPDATE \"Driver\" SET \"DriverName\" = @DriverName, \"PhoneNumber\" = @PhoneNumber WHERE \"DriverID\" = @DriverID";

                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                        {
                            connection.Open();
                            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                            {
                                // Add parameters to the command
                                command.Parameters.AddWithValue("DriverID", Convert.ToInt64(DriverID));
                                command.Parameters.AddWithValue("DriverName", DriverName);
                                command.Parameters.AddWithValue("PhoneNumber", Convert.ToInt64(PhoneNumber));

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
                        Console.WriteLine($"Error updating Driver: {ex.Message}");
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


        public void DeleteDriver(String DriverID)
        {
            try
            {
                string query2 = "DELETE FROM \"VehiclesInformations\" WHERE \"DriverID\" = @DriverID";
                string query1 = "DELETE FROM \"Driver\" WHERE \"DriverID\" =@DriverID";
              
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query2, connection))
                    {
                        command.Parameters.AddWithValue("DriverID", Convert.ToInt64(DriverID));
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} row(s) deleted1.");
                        using (NpgsqlCommand command1 = new NpgsqlCommand(query1, connection))
                        {
                            command1.Parameters.AddWithValue("DriverID", Convert.ToInt64(DriverID));
                            rowsAffected = command1.ExecuteNonQuery();
                            Console.WriteLine($"{rowsAffected} row(s) deleted.");   
                            return;
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting Driver: {ex.Message}");
            }



            return;
        }

        public GVAR AllDriver()
        {    GVAR gvar =new GVAR();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    string query = "SELECT * FROM \"Driver\"";
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvar.DicOfDT["Driver"] = dataTable;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (if you have a logging mechanism) or handle it as needed
                Console.WriteLine("An error occurred: " + ex.Message);
                gvar.DicOfDic["Tags"]["sts"] = "0";
                
            }
            return gvar;
        }
    }
}
