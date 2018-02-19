using System.Collections.Generic;
using System.IO;
using Catel.IoC;
using Catel.Services;
using DockingInfragistics.Helpers;
using DockingInfragistics.Interfaces;
using Infragistics.Windows.DockManager;
using Newtonsoft.Json;

namespace DockingInfragistics.ViewModels
{
    using Catel.MVVM;
    using IF.WPF.Infragistics.Persistence.Extensions;
    using IF.WPF.Infragistics.Persistence.Services.Interfaces;
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public class MainWindowViewModel : ViewModelBase
    {
        private XamDockManager _dockManager;

        // private static string file = @"c:\temp\dockmanager.xml";
        private readonly IDockManagerPersistenceService dockManagerPersistenceService;
        private IDispatcherService dispatcherService;
        private IViewModelFactory viewmodelFactory;
        public ObservableCollection<object> Panes { get; set; }

        public TaskCommand SaveCommand { get; private set; }
        public TaskCommand LoadCommand { get; private set; }
        public TaskCommand NewItemCommand { get; private set; }
        public TaskCommand SystemNewItemCommand { get; private set; }

        public MainWindowViewModel(IDispatcherService dispatcherService, IViewModelFactory viewmodelFactory, IDockManagerPersistenceService dockManagerPersistenceService)
        {
            Panes = new ObservableCollection<object>();

            SaveCommand = new TaskCommand(OnSaveCommandExecute);
            LoadCommand = new TaskCommand(OnLoadCommandExecute);
            NewItemCommand = new TaskCommand(OnNewItemCommandExecute);
            SystemNewItemCommand = new TaskCommand(OnSystemNewItemCommandExecute);

            this.dispatcherService = dispatcherService;
            this.viewmodelFactory = viewmodelFactory;
            this.dockManagerPersistenceService = dockManagerPersistenceService;
        }

        protected override Task InitializeAsync()
        {
            _dockManager = this.GetUIElement<XamDockManager>();
            _dockManager.InitializePaneContent += DockManager_InitializePaneContent;

            return base.InitializeAsync();
        }

        private Task OnNewItemCommandExecute()
        {
            var viewmodel = viewmodelFactory.CreateViewModel<DummyViewModel>(null, null);

            Panes.Add(viewmodel);

            return Task.FromResult(0);
        }

        private Task OnSystemNewItemCommandExecute()
        {
            var viewmodel = viewmodelFactory.CreateViewModel<BottomViewModel>(null, null);

            Panes.Add(viewmodel);

            return Task.FromResult(0);
        }

        private Task OnLoadCommandExecute()
        {
            Panes.Clear();

            using (var file = File.OpenRead("c:\\temp\\prova123.xml"))
            {
                return dockManagerPersistenceService.RestoreGridAsync(_dockManager, file, true);
            }
        }

        private void DockManager_InitializePaneContent(object sender, Infragistics.Windows.DockManager.Events.InitializePaneContentEventArgs e)
        {
            if (e.NewPane.SerializationId != null)
            {
                var contentPane = (string)e.NewPane.GetValue(ContentPane.SerializationIdProperty);

                var dictionary = StringBase64Helper.Base64Decode(contentPane);

                var xx = JsonConvert.DeserializeObject<IDictionary<string, object>>(dictionary);

                IViewModelFactory viewModelFactory = ServiceLocator.Default.ResolveType<IViewModelFactory>();

                var viewmodel = viewModelFactory.CreateViewModel(Type.GetType((string)xx["VM"]), null, null);

                if (viewmodel is IStateAware stateAwareViewModel)
                {
                    stateAwareViewModel.SavedPersistenceBag = xx;
                }

                Panes.Add(viewmodel);

                e.Handled = true;
            }
        }

        private Task OnSaveCommandExecute()
        {
            using (var file = File.OpenWrite("c:\\temp\\prova123.xml"))
            {
                return dockManagerPersistenceService.PersistGridAsync(_dockManager, file, true);
            }
        }
    }
}
