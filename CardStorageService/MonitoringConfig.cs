namespace CardStorageService
{
    public class MonitoringConfig
    {
        public bool EnableRPSLog { get; set; }
        public bool EnableStorageStatistic { get; set; }
        public TimeSpan StartTime { get; set; }
    }
}
