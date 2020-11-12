namespace OneSplash.Domain.Settings
{
    public class AppSettings
    {
        public string DbConnection = "Data Source=default.sqlite3";
        public string AccessKey { get; set; }
        public string Secret { get; set; }
    }
}
