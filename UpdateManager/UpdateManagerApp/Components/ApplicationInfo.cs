namespace UpdateManagerApp
{
    public class ApplicationInfo
    {
        public string Name { get; set; }
        public string CurrentVersion { get; set; }
        public string AvailableVersion { get; set; }
        public bool HasUpdate => !string.IsNullOrEmpty(AvailableVersion) && AvailableVersion != CurrentVersion;
    }
}
