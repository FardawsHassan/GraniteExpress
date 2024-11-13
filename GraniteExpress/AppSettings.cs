namespace GraniteExpress.Models
{
    public static class AppSettings
    {
        public static Settings Settings { get; set; } = new();

        public static Dictionary<string, string> Components = new()
        {
            ["JournalViewAdd"] = "Journal Add Permission",
            ["JournalViewEdit"] = "Journal Edit Permission",
            ["JournalViewDelete"] = "Journal Delete Permission",
            ["JournalViewTable"] = "Can See Journals",
            ["JournalViewPrint"] = "Can Print Journal List",
            ["JournalViewExport"] = "Can Export Journal List",

            //Menu
            ["ReferenceMenu"] = "Can See Reference Menu",
            ["ChartOfAccountMenu"] = "Can See Chart Of Account Menu",
            ["ClientMenu"] = "Can See Client Menu",
            ["TemplateMenu"] = "Can See Template Menu",

            ["JournalMenu"] = "Can See Journal Menu",
            ["TransactionMenu"] = "Can See Transaction Menu",
            ["ViewMenu"] = "Can See View Menu",

            ["ReportMenu"] = "Can See Report Menu",
            ["ListOfTransactionMenu"] = "Can See List Of Transaction Menu",

            ["AdminMenu"] = "Can See Admin Menu",
            ["UserMenu"] = "Can See User Menu",
            ["RoleMenu"] = "Can See Role Menu",
            ["ClaimMenu"] = "Can See Claim Menu",

            ["SettingsMenu"] = "Can See Settings Menu",
            ["ChangePasswordMenu"] = "Can See Change Password Menu",
        };

        public static Dictionary<string, string> Databases = new()
        {
            // ["GraniteExpress1"] = "Primary Database",
            // ["GraniteExpress2"] = "Backup Database",
            // ["GraniteExpress3"] = "Database 1",
            // ["GraniteExpress4"] = "Database 2",
            ["shuvropust18_"] = "Database 2",
        };
    }

    public class Settings
    {
        // Database Information
        public string ServerName { get; set; } = "sql.bsite.net\MSSQL2016";
        public string DatabaseUserId { get; set; } = "shuvropust18_";
        public string DatabasePassword { get; set; } = "shuvropust18";

        // Email Setitngs
        public string AppUrl { get; set; } = "https://localhost:7054";
        public string ProtectionKey { get; set; } = "kdPzE27PwAON4RDcNIEyClVzBw9VCaGw";
        public string AdminEmail { get; set; } = "admin@gmail.com";
        public string AdminPassword { get; set; } = "admin123@";
    }
}
