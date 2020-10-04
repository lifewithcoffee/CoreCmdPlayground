using InfluxDB.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InfluxDBTestLib
{
    public interface IInfluxReader
    {
        Task QueryAsync(string measurement);
    }

    public class InfluxReader : IInfluxReader
    {
        QueryApi _queryApi;

        string _org = "defaultOrg";

        public InfluxReader(IInfluxDbConnection connection)
        {
            _queryApi = connection.Client.GetQueryApi();
        }

        public async Task QueryAsync(string measurement)
        {
            var query = $"from(bucket:\"defaultBucket\") |> range(start: -1h) |> filter(fn: (r) => r[\"_measurement\"] == \"{measurement}\")";
            var tables = await _queryApi.QueryAsync(query, _org);
            tables.ForEach(fluxTable =>
            {
                var fluxRecords = fluxTable.Records;
                fluxRecords.ForEach(fluxRecord =>
                {
                    Console.WriteLine($"{fluxRecord.GetTime()}: {fluxRecord.GetValue()}");
                    Console.WriteLine($"    Field       : {fluxRecord.GetField()}");
                    Console.WriteLine($"    Measurement : {fluxRecord.GetMeasurement()}");
                    Console.WriteLine("------------------");
                    Console.WriteLine(string.Join(",", fluxRecord.Values.Select(v => v.ToString())));
                    Console.WriteLine("------------------");
                    Console.WriteLine(fluxRecord.ToString());
                    Console.WriteLine("==================\n");
                });
            });
        }
    }
}
