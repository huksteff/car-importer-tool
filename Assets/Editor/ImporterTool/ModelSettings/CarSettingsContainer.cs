using Editor.ImporterTool.ComponentsPage;
using Editor.ImporterTool.ImportSettingsPage;
using Editor.ImporterTool.StatsPage;
using Editor.ImporterTool.Utilities.Dropdown;
using Editor.ImporterTool.Utilities.Extensions;
using Editor.ImporterTool.Utilities.Manipulator;
using Editor.Utilities;
using UnityEngine.UIElements;

namespace Editor.ImporterTool.ModelSettings
{
    public class CarSettingsContainer : BaseEditorUnitContainer
    {
        public readonly Label TestLabel;
        public readonly VisualElement MenuContainer;
        public readonly VisualElement MainContainer;
        
        public readonly Button BackButton;
        public readonly Button ImportSettingsButton;
        public readonly Button ComponentEditorButton;
        public readonly Button StatsButton;

        public readonly ImportSettingsContainer ImportSettingsContainer;
        public readonly ComponentPageContainer ComponentPageContainer;
        public readonly StatsContainer StatsContainer;
        
        public readonly Button ApplyButton;
        public readonly Button PickObjectButton;
        public readonly Button ShowExplorerButton;
        public readonly Label TestMaterialName;
        public readonly VisualElement ButtonsHorizContainer;

        public CarSettingsContainer(VisualElement root) : base(root)
        {
            MenuContainer = new VisualElement();
            MenuContainer.AddStyle("horizontal-menu");

            BackButton = Root.CreateButton("\u27b2", "circle-button");
            MenuContainer.Add(BackButton);
            ImportSettingsButton = Root.CreateButton("\u2699", "circle-button");
            MenuContainer.Add(ImportSettingsButton);
            ComponentEditorButton = Root.CreateButton("\u26cf", "circle-button");
            MenuContainer.Add(ComponentEditorButton);
            StatsButton = Root.CreateButton("\u2634", "circle-button");
            MenuContainer.Add(StatsButton);
            
            root.Add(MenuContainer);

            MainContainer = new VisualElement();
            MainContainer.AddStyle("main-container");

            ImportSettingsContainer = new ImportSettingsContainer(MainContainer);
            ComponentPageContainer = new ComponentPageContainer(MainContainer);
            StatsContainer = new StatsContainer(MainContainer);
            
            root.Add(MainContainer);
            
            ButtonsHorizContainer = new VisualElement();
            ButtonsHorizContainer.AddStyle("horizontal-container");
            MainContainer.Add(ButtonsHorizContainer);
            
            PickObjectButton = Root.CreateButton("Ping Object", "other-button");
            ShowExplorerButton = Root.CreateButton("Open Folder", "other-button");
            ApplyButton = Root.CreateButton("Integrate", "apply-button", ImporterToolTooltips.UpdateButtonTooltip);
            
            ButtonsHorizContainer.Add(PickObjectButton);
            ButtonsHorizContainer.Add(ShowExplorerButton);
            ButtonsHorizContainer.Add(ApplyButton);
        }
    }
}