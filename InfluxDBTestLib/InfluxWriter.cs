using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using System.Threading.Tasks;

namespace InfluxDBTestLib
{
    public interface IInfluxWriter
    {
        void ChangeOrganizationBucket(string org, string bucket);
        Task WriteAsync<TMeasurement>(TMeasurement measurement);
    }

    public class InfluxWriter : IInfluxWriter
    {
        WriteApiAsync _writeApiAsync;

        string _org = "defaultOrg";
        string _bucket = "defaultBucket";

        public InfluxWriter(IInfluxDbConnection connection)
        {
            _writeApiAsync = connection.Client.GetWriteApiAsync();
        }

        public void ChangeOrganizationBucket(string org, string bucket)
        {
            _org = org;
            _bucket = bucket;
        }

        public async Task WriteAsync<TMeasurement>(TMeasurement measurement)
        {
            await _writeApiAsync.WriteMeasurementAsync(_bucket, _org, WritePrecision.Ns, measurement);
        }
    }
}
