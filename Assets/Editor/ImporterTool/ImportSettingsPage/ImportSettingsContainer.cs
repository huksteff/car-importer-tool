using System.Xml.Serialization;
using Editor.ImporterTool.Utilities.Dropdown;
using Editor.ImporterTool.Utilities.Extensions;
using Editor.ImporterTool.Utilities.Manipulator;
using Editor.Utilities;
using UnityEngine.UIElements;

namespace Editor.ImporterTool.ImportSettingsPage
{
    public class ImportSettingsContainer : BaseEditorUnitContainer
    {
        public readonly Label SceneCategory;
        public readonly Label MeshesCategory;
        public readonly Label GeometryCategory;
        public readonly Label MaterialsCategory;
        
        public readonly TextField CarName;
        public readonly FloatField ScaleFactor;
        public readonly Toggle BakeAxisConversionToggle;
        public readonly Toggle ImportVisibilityToggle;
        public readonly Toggle IsReadableToggle;
        public readonly Toggle GenerateCollidersToggle;
        public readonly Toggle KeepQuadsToggle;
        public readonly Toggle WeldVrticesToggle;
        public readonly Toggle GenerateLightmapUVsToggle;

        public readonly DropdownContainer MeshCompressionDropdown;
        public readonly DropdownContainer IndexFormatDropdown;
        public readonly DropdownContainer SelectMaterialDropdown;
        public readonly VisualElement MaterialPanelContainer;

        public ImportSettingsContainer(VisualElement root) : base(root)
        {
            //SCENE
            SceneCategory = Root.CreateLabel("Scene Category", "import-settings-category");
            
            CarName = Root.CreateTextField("Car Name", "text-field", ImporterToolTooltips.CarNameTooltip);
            ScaleFactor = Root.CreateFloatField("Scale Factor", "float-field", ImporterToolTooltips.ScaleFactorTooltip);
            BakeAxisConversionToggle = Root.CreateToggleField("Bake Axis Conversion", "toggle-field", ImporterToolTooltips.BakeAxisConversionTooltip);
            ImportVisibilityToggle = Root.CreateToggleField("Import Visibility", "toggle-field", ImporterToolTooltips.ImportVisibilityTooltip);

            //MESHES
            MeshesCategory = Root.CreateLabel("Meshes Category", "import-settings-category");
            
            IsReadableToggle = Root.CreateToggleField("IsReadable", "toggle-field", ImporterToolTooltips.IsReadableTooltip);
            GenerateCollidersToggle = Root.CreateToggleField("Generate Colliders", "toggle-field", ImporterToolTooltips.GenerateCollidersTooltip);

            //GEOMETRY
            GeometryCategory = Root.CreateLabel("Geometry Category", "import-settings-category");

            MeshCompressionDropdown = new DropdownContainer(Root, "Mesh Compression", DropdownOptionsData.MeshCompressionOption, "TEST TOOLTIP", style:"dropdown");
            KeepQuadsToggle = Root.CreateToggleField("Keep Quads", "toggle-field", ImporterToolTooltips.GenerateCollidersTooltip);
            WeldVrticesToggle = Root.CreateToggleField("Weld Vertices", "toggle-field", ImporterToolTooltips.GenerateCollidersTooltip);
            IndexFormatDropdown = new DropdownContainer(Root, "Index Format", DropdownOptionsData.IndexFormatOption, "TEST TOOLTIP", style:"dropdown");
            GenerateLightmapUVsToggle = Root.CreateToggleField("Generate Lightmap UVs", "toggle-field", ImporterToolTooltips.GenerateCollidersTooltip);

            //MATERIAL
            MaterialsCategory = Root.CreateLabel("Materials Category", "import-settings-category");
            SelectMaterialDropdown = new DropdownContainer(Root, "Materials List", DropdownOptionsData.MaterialsOption, "ALl materials from imported fbx model.", style:"dropdown");

            MaterialPanelContainer = new VisualElement();
            Root.Add(MaterialPanelContainer);
        }
    }
}