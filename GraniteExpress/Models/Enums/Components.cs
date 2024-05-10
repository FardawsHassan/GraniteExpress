using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.Models.Enums
{
    public enum Components
    {
        [Description("JournalViewTable")]
        JournalViewTable,

        [Description("JournalViewEdit")]
        JournalViewEdit,

        [Description("JournalViewAdd")]
        JournalViewAdd,

        [Description("JournalViewDelete")]
        JournalViewDelete,
    }
}
