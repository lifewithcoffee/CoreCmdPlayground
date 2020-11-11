namespace HttpClientLib
{
    public class HttpClientOptions
    {
        public bool ByPassCertificate { get; set; } = false;
        public string MimeType { get; set; } = "application/json";
    }
}
