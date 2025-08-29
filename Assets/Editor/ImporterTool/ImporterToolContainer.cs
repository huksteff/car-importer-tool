using Editor.ImporterTool.ImportBlock;
using Editor.ImporterTool.ModelSettings;
using Editor.Utilities;

namespace Editor.ImporterTool
{
    public class ImporterToolContainer : BaseEditorContainer
    {
        public ImportBlockContainer ImportBlockContainer;
        public CarSettingsContainer CarSettingsContainer;
        
        public ImporterToolContainer()
        {
            Root.AddToClassList("window");

            ImportBlockContainer = new ImportBlockContainer(Root);
            CarSettingsContainer = new CarSettingsContainer(Root);
        }
    }
}