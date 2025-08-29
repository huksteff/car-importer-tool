using UnityEngine.UIElements;

namespace Editor.ImporterTool.Utilities.Extensions
{
    public static class StyleExtensions
    {
        public static TVisualElement AddStyle<TVisualElement>(this TVisualElement visualElement, string styles) where TVisualElement : VisualElement
        {
            foreach (var style in styles.Split(' '))
            {
                visualElement.AddToClassList(style);
            }

            return visualElement;
        }

        public static TVisualElement RemoveStyle<TVisualElement>(this TVisualElement visualElement, string styles) where TVisualElement : VisualElement
        {
            foreach (var style in styles.Split(' '))
            {
                visualElement.RemoveFromClassList(style);
            }

            return visualElement;
        }
    }
}