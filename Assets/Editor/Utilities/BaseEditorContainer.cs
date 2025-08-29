using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Editor.Utilities
{
    public class BaseEditorContainer
    {
        public readonly VisualElement Root = new();

        protected BaseEditorContainer()
        {
            Root.style.flexGrow = 1;
        }
    }
}