using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Writes;
using System;
using System.Threading.Tasks;

namespace InfluxDBTestLib
{
    public class InfluxdbCommandBase
    {
        // find tokens from InfluxDB UI | Data | Tokens
        private static readonly char[] Token = "4R1aL7t1hZolnMQezXQxkhhMGlqYUBy7g5Ue8RQAQ9wHn_XIHJN_2EpFqaYcD9F2wv_lt-kHqP8Ym99c7Gv5pw==".ToCharArray();

        public void WriteRawApi()
        {
            Console.WriteLine("InfluxdbCommandBase.Test1() :> creating influx db client ...");

            /**
             * or use username/password:
             * var influxDBClient = InfluxDBClientFactory.Create("http://localhost:9999", "ron", "open".ToCharArray());
             */
            var influxDBClient = InfluxDBClientFactory.Create("http://localhost:9999", Token);
            string orgId = "defaultOrg";
            string bucketName = "defaultBucket";

            Console.WriteLine("InfluxdbCommandBase.Test1() :> start writing data ...");

            // Write Data
            using (var writeApi = influxDBClient.GetWriteApi())
            {
                // Write by Point
                var point = PointData.Measurement("temperature")
                    .Tag("location", "west")
                    .Field("value", 55D)
                    .Timestamp(DateTime.UtcNow.AddSeconds(-10), WritePrecision.Ns);

                writeApi.WritePoint(bucketName, orgId, point);
                Console.WriteLine("InfluxdbCommandBase.Test1() :> Write by Point completed");

                // Write by LineProtocol
                writeApi.WriteRecord(bucketName, orgId, WritePrecision.Ns, "temperature,location=north value=60.0");
                Console.WriteLine("InfluxdbCommandBase.Test1() :> Write by LineProtocol completed");

                // Write by POCO
                var temperature = new Temperature { Location = "south", Value = 94D, RLTestColumn = 100D, Time = DateTime.UtcNow };
                writeApi.WriteMeasurement(bucketName, orgId, WritePrecision.Ns, temperature);
                Console.WriteLine("InfluxdbCommandBase.Test1() :> Write by POCO completed");
            }

            influxDBClient.Dispose();
        }

        public async Task ReadRawApi()
        {
            try
            {
                Console.WriteLine("InfluxdbCommandBase.Test1() :> start querying ...");

                var influxDBClient = InfluxDBClientFactory.Create("http://localhost:9999", Token);
                Console.WriteLine("InfluxdbCommandBase.ReadRawApi() :> client created");

                string orgId = "defaultOrg";

                // Query data
                var queryApi = influxDBClient.GetQueryApi();
                var query = $"from(bucket:\"defaultBucket\") |> range(start: 0)";
                var tables = await queryApi.QueryAsync(query, orgId);
                if(tables != null)
                {
                    Console.WriteLine("InfluxdbCommandBase.ReadRawApi() :> got tables");

                    tables.ForEach(fluxTable =>
                    {
                        var fluxRecords = fluxTable.Records;
                        fluxRecords.ForEach(fluxRecord =>
                        {
                            Console.WriteLine($"{fluxRecord.GetTime()}: {fluxRecord.GetValue()}");
                            Console.WriteLine($"    Field       : {fluxRecord.GetField()}");
                            Console.WriteLine($"    Measurement : {fluxRecord.GetMeasurement()}");
                        });
                    });
                }

                influxDBClient.Dispose();
                Console.WriteLine("InfluxdbCommandBase.ReadRawApi() :> Client disposed");
            }catch(Exception ex)
            {
                Console.WriteLine($"Exception:\n{ex.Message}\n{ex.InnerException.Message}");
            }
        }
    }

    [Measurement("temperature")]
    class Temperature
    {
        [Column(IsTimestamp = true)] public DateTime Time;
        [Column("location", IsTag = true)] public string Location { get; set; }
        [Column("value")] public double Value { get; set; }
        [Column] public double RLTestColumn { get; set; }
    }
}
