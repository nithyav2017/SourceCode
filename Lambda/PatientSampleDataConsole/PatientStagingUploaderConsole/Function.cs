using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.S3.Events;
using clsArthritisPatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PatientStagingUploaderConsole
{
    public class Function
    {
        public async Task FunctionHandler(Amazon.Lambda.S3Events.S3Event evnt, ILambdaContext context)
        {
            IAmazonS3 _s3Client = new AmazonS3Client();

            var S3Event = evnt.Records[0].S3;
            string bucket = S3Event.Bucket.Name;
            string key = Uri.EscapeDataString( S3Event.Object.Key);

            try
            {
                var response = await _s3Client.GetObjectAsync(bucket, key);
                using var reader = new StreamReader(response.ResponseStream);

                var json = await reader.ReadToEndAsync();
                var patients = JsonSerializer.Deserialize<List<Patient>>(json);

                using var conn = new Microsoft.Data.SqlClient.SqlConnection("Server=stagingdb.c3yeo4ksyngm.us-east-2.rds.amazonaws.com,1433;Database=stagingdb;User Id=admin;Password=Learning*12;");
                conn.Open();

                foreach (var p in patients)
                {

                    var cmd = new Microsoft.Data.SqlClient.SqlCommand(@"
                    INSERT INTO PatientStaging (
                        Id, FirstName,LastName, DateOfBirth, Email, Phone, HcpSpecialty,
                        Indication, InsuranceType, ConsentToEmail, ConsentToText,
                        PinHash, CopayCardNumber)
                    VALUES (@Id, @FirstName,@LastName, @DOB, @Email, @Phone, @HcpSpecialty,
                            @Indication, @InsuranceType, @ConsentToEmail, @ConsentToText,
                            @PinHash, @CopayCardNumber)", conn);

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@FirstName", p.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", p.LastName);
                    cmd.Parameters.AddWithValue("@DOB", p.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Email", p.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", p.Phone ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@HcpSpecialty", p.HcpSpecialty ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Indication", p.Indication ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@InsuranceType", p.InsuranceType ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ConsentToEmail", p.ConsentToEmail);
                    cmd.Parameters.AddWithValue("@ConsentToText", p.ConsentToText);
                    cmd.Parameters.AddWithValue("@PinHash", p.PinHash ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CopayCardNumber", p.CopayCardNumber ?? (object)DBNull.Value);
                    await cmd.ExecuteNonQueryAsync();
                }
                context.Logger.LogInformation("Data Inserted into PatientStaging");
                context.Logger.LogInformation($"Bucket: {bucket}, Key: {key}");
            }
            catch (Exception ex)
            {

                context.Logger.LogError($"Error Processing File:{ex.Message}");
                throw;
            }

        }
    }
}