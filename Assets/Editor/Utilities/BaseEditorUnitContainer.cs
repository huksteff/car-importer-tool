using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Editor.Utilities
{
    public class BaseEditorUnitContainer
    {
        public readonly VisualElement Root = new();

        protected BaseEditorUnitContainer(VisualElement root)
        {
            root.Add(Root);
        }

        public void Hide()
        {
            Root.style.display = DisplayStyle.None;
        }

        public void Show()
        {
            Root.style.display = DisplayStyle.Flex;
        }

        public void HideElement(VisualElement element)
        {
            element.style.display = DisplayStyle.None;
        }

        public void ShowElement(VisualElement element)
        {
            element.style.display = DisplayStyle.Flex;
        }
    }
}