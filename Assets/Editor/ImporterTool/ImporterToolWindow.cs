using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.ImporterTool
{
    public class ImporterToolWindow : EditorWindow
    {
        [SerializeField] private VisualTreeAsset _visualTreeAsset;
        private ImporterToolContext _context;
        private ImporterToolContainer _container;
        private ImporterToolController _controller;

        [MenuItem("Tools/Car Importer")]
        public static void ShowWindow()
        {
            ImporterToolWindow window = GetWindow<ImporterToolWindow>();
            window.titleContent = new GUIContent("Car Importer");
            
            window.minSize = new Vector2(500, 500);
        }

        public void CreateGUI()
        {
            var uss = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/ImporterTool/Styles/WindowStyle.uss");
            rootVisualElement.styleSheets.Add(uss);

            _context = new ImporterToolContext();
            _container = new ImporterToolContainer();
            _controller = new ImporterToolController(_container, _context);
            
            rootVisualElement.Add(_container.Root);
            _controller.Init();
        }

        public void OnDisable()
        {
            _controller?.Dispose();
            _controller = null;
            _container = null;
            _context = null;
        }
    }
}
