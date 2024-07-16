namespace UpdateManagerApp
{
    public class DriverInfo
    {
        public string Name { get; set; } = string.Empty;
        public string CurrentVersion { get; set; } = string.Empty;
        public bool IsUpToDate { get; set; }
    }
}
