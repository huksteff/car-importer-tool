using Editor.ImporterTool.Utilities.Extensions;
using Editor.Utilities;
using UnityEngine.UIElements;

namespace Editor.ImporterTool.StatsPage
{
    public class StatsContainer : BaseEditorUnitContainer
    {
        public readonly Label Stat;
        
        public StatsContainer(VisualElement root) : base(root)
        {
            Stat = Root.CreateLabel("Settings menu: ", "label");
            Stat.style.fontSize = 24;
        }
    }
}