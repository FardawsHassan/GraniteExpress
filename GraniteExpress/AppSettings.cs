namespace GraniteExpress.Models
{
    public static class AppSettings
    {
        public static Settings Settings { get; set; } = new();

        public static Dictionary<string,string> Components = new()
        {
            ["JournalViewAdd"] = "Journal Add Permission",
            ["JournalViewEdit"] = "Journal Edit Permission",
            ["JournalViewDelete"] = "Journal Delete Permission",
            ["JournalViewTable"] = "Can See Journals",
            ["JournalViewPrint"] = "Can Print Journal List",
            ["JournalViewExport"] = "Can Export Journal List",
            ["AdminMenuPermission"] = "Can See Admin Menu",
            ["ReferenceMenuPermission"] = "Can See Reference Menu",
            ["ReportMenuPermission"] = "Can See Report Menu",
        };
    }

    public class Settings
    {
        public string AppUrl { get; set; } = "https://localhost:7239";
        public string DBConnection { get; set; }
        public string ProtectionKey { get; set; } = "kdPzE27PwAON4RDcNIEyClVzBw9VCaGw";
        public string AdminEmail { get; set; } = "admin@gmail.com";
        public string AdminPassword { get; set; } = "admin123@";
    }
}
