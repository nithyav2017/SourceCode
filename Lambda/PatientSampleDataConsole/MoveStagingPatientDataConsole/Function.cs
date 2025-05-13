using Amazon.Lambda.Core;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MoveStagingPatientDataConsole
{
    public class ColumnDefinition
    {
        public string Name { get; set; }
        public string Datatype { get; set; }
    }
    public  class Function
    {
        public async Task FunctionHandler(ILambdaContext context)
        {
            using var conn = new SqlConnection("Server=stagingdb.c3yeo4ksyngm.us-east-2.rds.amazonaws.com,1433;Database=stagingdb;User Id=admin;Password=Learning*12;TrustServerCertificate=True;");  // This tells the client to trust the server's certificate.\r\n                       \"Encrypt=True\"");
            {
                await conn.OpenAsync();

                try
                {
                   

                    var selectCmd = new SqlCommand("SELECT * FROM PatientStaging", conn);
                    selectCmd.CommandTimeout = 300;
                    var reader = await selectCmd.ExecuteReaderAsync();

                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    using var bulkCopy = new SqlBulkCopy(conn)
                    {
                        DestinationTableName = "Patient",
                        BulkCopyTimeout = 300,
                        BatchSize = 100
                    };

                    await bulkCopy.WriteToServerAsync(dataTable);

                    // Optional: clean up staging table
                    using var deleteCmd = new SqlCommand("DELETE FROM PatientStaging", conn);
                    await deleteCmd.ExecuteNonQueryAsync();

                    context.Logger.LogInformation("Data Inserted into Patient");

                }
                catch (Exception ex)
                {
                    var cmd = new SqlCommand("SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Patient'", conn);
                    cmd.CommandTimeout = 120;
                    var reader = await cmd.ExecuteReaderAsync();
                    List<ColumnDefinition> lstReaders = new List<ColumnDefinition>();

                    while (await reader.ReadAsync())
                    {
                        lstReaders.Add(new ColumnDefinition
                        {
                            Name = reader["COLUMN_NAME"].ToString()!,
                            Datatype = reader["DATA_TYPE"].ToString()!
                        });

                    }
                    foreach (var col in lstReaders)
                    {
                        context.Logger.LogError($"Column: {col.Name}, Type:{col.Datatype}");
                    }
                    context.Logger.LogError($"Error Processing File:{ex.Message}");
                    throw;
                }
            }


}
    }
}
