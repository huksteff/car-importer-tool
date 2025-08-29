using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.ImporterTool.Utilities.Manipulator
{
    public class ImportManipulator : PointerManipulator
    {
        public Action<string> OnAddFile;
        
        public ImportManipulator(VisualElement element)
        {
            target = element.Query<VisualElement>(className: "import-field");
        }
        
        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<PointerDownEvent>(OnPointerDownEvent);
            target.RegisterCallback<DragEnterEvent>(OnDragEnterEvent);
            target.RegisterCallback<DragLeaveEvent>(OnDragLeaveEvent);
            target.RegisterCallback<DragUpdatedEvent>(OnDragUpdatedEvent);
            target.RegisterCallback<DragPerformEvent>(OnDragPerformEvent);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<PointerDownEvent>(OnPointerDownEvent);
            target.UnregisterCallback<DragEnterEvent>(OnDragEnterEvent);
            target.UnregisterCallback<DragLeaveEvent>(OnDragLeaveEvent);
            target.UnregisterCallback<DragUpdatedEvent>(OnDragUpdatedEvent);
            target.UnregisterCallback<DragPerformEvent>(OnDragPerformEvent);
        }

        private void OnPointerDownEvent(PointerDownEvent evt)
        {
            // Debug.Log("PointerDownEvent");
            DragAndDrop.PrepareStartDrag();
            
            DragAndDrop.StartDrag(string.Empty);
        }

        private void OnDragEnterEvent(DragEnterEvent evt)
        {
            // Debug.Log("DragEnterEvent");
            target.AddToClassList("activate");
        }

        private void OnDragLeaveEvent(DragLeaveEvent evt)
        {
            // Debug.Log("DragLeaveEvent");
            target.RemoveFromClassList("activate");
        }

        private void OnDragUpdatedEvent(DragUpdatedEvent evt)
        {
            // Debug.Log("DragUpdatedEvent");
            DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
        }

        private void OnDragPerformEvent(DragPerformEvent evt)
        {
            // Debug.Log("DragPerformEvent");
            if (DragAndDrop.paths.Length > 1)
            {
                Debug.LogError("Hui");
                return;
            }

            var path = DragAndDrop.paths[0];
            
            if (!path.Contains(".fbx"))
            {
                Debug.LogError("Imported file is not FBX model type.");
                return;
            }
            
            OnAddFile?.Invoke(path);
            
            target.RemoveFromClassList("activate");
        }
    }
}