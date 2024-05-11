using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.Models
{
    public static class AppSettings
    {
        public static Dictionary<string,string> Components = new()
        {
            ["JournalViewAdd"] = "Journal Add Permission",
            ["JournalViewEdit"] = "Journal Edit Permission",
            ["JournalViewDelete"] = "Journal Delete Permission",
            ["JournalViewTable"] = "Can See Journals",
            ["JournalViewPrint"] = "Can Print Journal List",
            ["JournalViewExport"] = "Can Export Journal List",
        };
    }
}
