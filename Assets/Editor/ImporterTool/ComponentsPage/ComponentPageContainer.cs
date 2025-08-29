using Editor.ImporterTool.Utilities.Dropdown;
using Editor.ImporterTool.Utilities.Extensions;
using Editor.Utilities;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.ImporterTool.ComponentsPage
{
    public class ComponentPageContainer : BaseEditorUnitContainer
    {
        public readonly Label TestLabel;
        public readonly DropdownContainer ScriptDropdown;
        public readonly VisualElement ConfigContainer;
        public readonly Button RemoveComponentsButton;
        
        public ComponentPageContainer(VisualElement root) : base(root)
        {
            TestLabel = Root.CreateLabel("Component Settings", "import-settings-category");

            ScriptDropdown = new DropdownContainer(Root, "Select Component", DropdownOptionsData.ComponentsOption, "TEST TOOLTIP", style:"dropdown");
            ConfigContainer = new VisualElement();
            Root.Add(ConfigContainer);
            RemoveComponentsButton = Root.CreateButton("Remove Component", "button");
        }
    }
}