using Editor.ImporterTool.ComponentsPage;
using Editor.ImporterTool.ImportBlock;
using Editor.ImporterTool.ImportSettingsPage;
using Editor.ImporterTool.ModelSettings;
using Editor.ImporterTool.Utilities.Dropdown;

namespace Editor.ImporterTool
{
    public class ImporterToolContext
    {
        public readonly ImportBlockModel ImportBlockModel = new ();
        public readonly CarSettingsModel CarSettingsModel = new ();
        public readonly ImportSettingsModel ImportSettingsModel = new ();
        public readonly ComponentPageModel ComponentPageModel = new();
    }
}