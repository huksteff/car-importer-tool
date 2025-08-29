using UnityEditor;
using UnityEngine.UIElements;

namespace Editor.ImporterTool.Utilities.Extensions
{
    public static class VisualElementExtensions
    {
        public static Label CreateLabel(this VisualElement root, string contentText = null, string styles = null)
        {
            var label = new Label(contentText);
            label.AddStyle(styles);
            root.Add(label);
            
            return label;
        }
        
        public static Button Button(this VisualElement root, string contentText, string styles = null, string tooltip = null)
        {
            var button = new Button()
            {
                text = contentText
            };
            button.AddStyle(styles);
            button.tooltip = tooltip;
            root.Add(button);

            return button;
        }

        public static Button CreateButton(this VisualElement root, string contentText, string styles = null, string tooltip = null)
        {
            return Button(root, contentText, StyleConst.CreateButton + ' ' + styles, tooltip);
        }

        public static TextField CreateTextField(this VisualElement root, string contentText, string styles = null,
            string tooltip = null)
        {
            var textField = new TextField()
            {
                label = contentText
            };
            textField.AddStyle(styles);
            textField.tooltip = tooltip;
            root.Add(textField);

            return textField;
        }
        
        public static FloatField CreateFloatField(this VisualElement root, string contentText, string styles = null,
            string tooltip = null)
        {
            var floatField = new FloatField()
            {
                label = contentText
            };
            floatField.AddStyle(styles);
            floatField.tooltip = tooltip;
            root.Add(floatField);

            return floatField;
        }

        public static Toggle CreateToggleField(this VisualElement root, string contentText, string styles = null,
            string tooltip = null)
        {
            var toggle = new Toggle()
            {
                label = contentText
            };
            toggle.AddStyle(styles);
            toggle.tooltip = tooltip;
            root.Add(toggle);

            return toggle;
        }
    }
}