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
        private static readonly char[] Token = "4lBCCgkkF9Eh2o7F-6wSE68J5SSl9D3DUlTFlzCz4nAr6DkwfpNtw5osl3iu_z3EmviwTF0rYWFcOHtBHbO-bA==".ToCharArray();

        public async Task Test1()
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

            Console.WriteLine("InfluxdbCommandBase.Test1() :> start querying ...");

            // Query data
            var query = $"from(bucket:\"defaultBucket\") |> range(start: -1h)";
            var tables = await influxDBClient.GetQueryApi().QueryAsync(query, orgId);

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

            Console.WriteLine("InfluxdbCommandBase.Test1() :> disposing influx db client ...");
            influxDBClient.Dispose();
        }

        [Measurement("temperature")]
        private class Temperature
        {
            [Column("location", IsTag = true)] public string Location { get; set; }
            [Column("value")] public double Value { get; set; }
            [Column("RLTestColumn")] public double RLTestColumn { get; set; }
            [Column(IsTimestamp = true)] public DateTime Time;
        }
    }
}
