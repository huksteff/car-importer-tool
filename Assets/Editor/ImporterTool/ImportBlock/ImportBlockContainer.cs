using Editor.ImporterTool.Utilities.Extensions;
using Editor.ImporterTool.Utilities.Manipulator;
using Editor.Utilities;
using log4net.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.ImporterTool.ImportBlock
{
    public class ImportBlockContainer : BaseEditorUnitContainer
    {
        public readonly Label Text;
        public readonly Button NewButton;
        public readonly Vector2Field NewVector2Field;
        public readonly ImportManipulator Manipulator;

        public readonly VisualElement ImportField;

        public ImportBlockContainer(VisualElement root) : base(root)
        {
            Text = Root.CreateLabel("Drop file into this field", "field-label");
            
            ImportField = new VisualElement();
            ImportField.AddStyle("import-field");
            ImportField.Add(Text);
            root.Add(ImportField);
            Manipulator = new ImportManipulator(ImportField);
        }
    }
}