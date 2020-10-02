using InfluxDB.Client;

namespace InfluxDBTestLib
{
    public interface IInfluxDbConnection
    {
        void UpdateSetting(InfluxDbSetting setting);
        InfluxDBClient Client { get; }
    }

    public class InfluxDbConnection : IInfluxDbConnection
    {
        InfluxDBClient _influxDBClient;

        public InfluxDBClient Client => _influxDBClient;

        public InfluxDbConnection(InfluxDbSetting setting)
        {
            this.UpdateSetting(setting);
        }

        public void UpdateSetting(InfluxDbSetting setting)
        {
            if (this.Client != null)
            {
                this.Client.Dispose();
            }

            _influxDBClient = InfluxDBClientFactory.Create($"http://{setting.Host}:{setting.Port}", setting.Token.ToCharArray());
        }
    }
}
