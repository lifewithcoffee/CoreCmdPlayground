using InfluxDB.Client;
using InfluxDB.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InfluxDBTestLib
{
    public enum QueryRangeUnit
    {
        minute,
        hour,
        day,
    }

    public class QueryRange
    {
        int _number;
        QueryRangeUnit _unit;

        public QueryRange(int number, QueryRangeUnit unit)
        {
            _number = number;
            _unit = unit;
        }

        public string ToFluxString()
        {
            switch (_unit)
            {
                case QueryRangeUnit.minute:
                    return $"{_number}m";
                case QueryRangeUnit.hour:
                    return $"{_number}h";
                case QueryRangeUnit.day:
                    return $"{_number}d";
            }
            return "-1h"; // default return last 1 hour
        }
    }

    public interface IInfluxReader
    {
        Task<List<TMeasurement>> QueryAsync<TMeasurement>(QueryRange range);
    }

    public class InfluxReader : IInfluxReader
    {
        QueryApi _queryApi;

        string _org = "defaultOrg";
        string _bucket = "defaultBucket";

        public InfluxReader(IInfluxDbConnection connection)
        {
            _queryApi = connection.Client.GetQueryApi();
        }

        private string GetQueryString<TMeasurement>(QueryRange range)
        {
            var measurementAttribute = (Measurement)typeof(TMeasurement).GetCustomAttribute(typeof(Measurement));
            string measurementName = measurementAttribute.Name;

            string query = $"from(bucket:\"{_bucket}\") |> range(start: {range.ToFluxString()})" +
                           $"|> filter(fn: (r) => r[\"_measurement\"] == \"{measurementName}\")" +
                           $"|> pivot(rowKey:[\"_time\"], columnKey: [\"_field\"], valueColumn: \"_value\")";

            return query;
        }

        public async Task<List<TMeasurement>> QueryAsync<TMeasurement>(QueryRange range)
        {
            try
            {
                var query = this.GetQueryString<TMeasurement>(range);
                return await _queryApi.QueryAsync<TMeasurement>(query, _org);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
