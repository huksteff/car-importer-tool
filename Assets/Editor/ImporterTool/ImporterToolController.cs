using Editor.ImporterTool.ImportBlock;
using Editor.ImporterTool.ModelSettings;
using Editor.Utilities;

namespace Editor.ImporterTool

{
    public class ImporterToolController : IController
    {
        private readonly ImporterToolContainer _container;
        private readonly ImporterToolContext _context;
        private readonly ControllersList _controllersList = new();

        public ImporterToolController(ImporterToolContainer container, ImporterToolContext context)
        {
            _container = container;
            _context = context;
        }
        
        public void Init()
        {
            _controllersList.Add(new ImportBlockController(_context, _context.ImportBlockModel, _container.ImportBlockContainer));
            _controllersList.Add(new CarSettingsController(_context, _context.CarSettingsModel, _container.CarSettingsContainer));
            _controllersList.Init();
        }

        public void Dispose()
        {
            _controllersList.Dispose();
            _controllersList.Clear();
        }
    }
}