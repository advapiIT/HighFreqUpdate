using System.Linq;
using Catel.MVVM;
using Catel.Services;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Windows.DataPresenter;
using Catel;
using HighFreqUpdate.Models;
using HighFreqUpdate.ViewModels;
using Catel.IoC;
using System;
using HighFreqUpdate.Helpers;

namespace HighFreqUpdate.Behaviors
{
    public class DefaultRadGridContextMenuCommands
    {
        private const string GridViewSettingFile = "Gridview setting file|*.grid";
        private const double GridViewFontSizeDefault = 12;
        private const string GridViewFontFamilyDefault = "Sagoe UI";
        private const string GridViewFontStyleDefault = "Normal";

        #region Variables
        private static readonly IViewModelFactory ViewModelFactory;
        private static readonly IUIVisualizerService UIVisualizerService;

        private static readonly ISaveFileService SaveFileService;
        private static readonly IOpenFileService OpenFileService;
        //    private static readonly IStorageService StorageService;
        #endregion

        #region Properties
        private static System.Windows.Style BaseStyle { get; set; }
        #endregion

        #region Commands
        public static Command<HeaderCommandParameter> SortAscendingCommand { get; }
        private static void OnSortAscendingExecute(HeaderCommandParameter parameter)
        {
            Argument.IsNotNull(() => parameter);

            RemoveColumnDescriptor(parameter);

            parameter.Grid.FieldLayouts[0].SortedFields.Add(new FieldSortDescription
            {
                Direction = System.ComponentModel.ListSortDirection.Ascending,
                FieldName = parameter.Column
            });
        }

        public static Command<HeaderCommandParameter> SortDescendingCommand { get; }
        private static void OnSortDescendingExecute(HeaderCommandParameter parameter)
        {
            Argument.IsNotNull(() => parameter);

            RemoveColumnDescriptor(parameter);

            parameter.Grid.FieldLayouts[0].SortedFields.Add(new FieldSortDescription
            {
                Direction = System.ComponentModel.ListSortDirection.Descending,
                FieldName = parameter.Column
            });
        }

        public static Command<HeaderCommandParameter> SortClearingCommand { get; }
        private static void OnSortClearingExecute(HeaderCommandParameter parameter)
        {
            Argument.IsNotNull(() => parameter);

            RemoveColumnDescriptor(parameter);
        }

        public static Command<HeaderCommandParameter> GroupByCommand { get; }
        private static void OnGroupByExecute(HeaderCommandParameter parameter)
        {
            Argument.IsNotNull(() => parameter);

            parameter.Grid.FieldLayouts[0].SortedFields.Add(new FieldSortDescription
            {
                Direction = System.ComponentModel.ListSortDirection.Ascending,
                FieldName = parameter.Column,
                IsGroupBy = true
            });
        }

        public static Command<HeaderCommandParameter> UnGroupByCommand { get; }
        private static void OnUnGroupByExecute(HeaderCommandParameter parameter)
        {
            Argument.IsNotNull(() => parameter);

            RemoveColumnDescriptor(parameter);
        }

        public static Command<HeaderCommandParameter> DeleteFiltersCommand { get; }
        private static void OnDeleteFiltersExecute(HeaderCommandParameter parameter)
        {
            Argument.IsNotNull(() => parameter);

            foreach (var fieldlayout in parameter.Grid.FieldLayouts)
            {
                fieldlayout.RecordFilters.Clear();
            }
        }

        public static Command<XamDataGrid> ExportGridCommand { get; }
        private static void OnExportGridCommand(XamDataGrid parameter)
        {
            //Argument.IsNotNull(() => parameter);

            //IExceptionService exceptionService = ServiceLocator.Default.ResolveType<IExceptionService>();
            //var grid = parameter;

            //grid.IsBusy = true;

            //Workbook workbookAspose = ExportWorkbook.Export(grid);

            //string filename = ExportHelper.AddIndexToFileNameIfNeeded();

            //try
            //{
            //    workbookAspose.Save(filename);

            //    System.Diagnostics.Process.Start(filename);
            //}
            //catch (Exception ex)
            //{
            //    exceptionService.HandleException(ex);
            //}
            //finally
            //{
            //    grid.IsBusy = false;
            //}

        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private static bool CanExecuteExportGrid(XamDataGrid parameter)
        {
            Argument.IsNotNull(() => parameter);

            return parameter.DataItems.Count > 0;
        }

        internal static Command<HeaderCommandParameter> SaveSettingsCommand { get; }
        private static void OnSaveSettingsCommandExecute(HeaderCommandParameter parameter)
        {
            //Argument.IsNotNull(() => parameter);

            //var param = parameter.Grid;

            //SaveFileService.Filter = GridViewSettingFile;
            //if (SaveFileService.DetermineFile())
            //{
            //    // User selected a file
            //    using (var fs = File.OpenWrite(SaveFileService.FileName))
            //    {
            //        GridViewPersistenceHelper.SaveLayout(param, fs);
            //    }
            //}
        }

        private static bool CanSaveSettingsCommandExecute(HeaderCommandParameter parameter)
        {
            return parameter?.Grid != null;
        }

        internal static Command<HeaderCommandParameter> LoadSettingsCommand { get; }
        private static void OnLoadSettingsCommandExecute(HeaderCommandParameter parameter)
        {
            //Argument.IsNotNull(() => parameter);

            //var param = (parameter).Grid;

            //OpenFileService.Filter = GridViewSettingFile;
            //if (OpenFileService.DetermineFile())
            //{
            //    // User selected a file
            //    using (var fs = File.OpenRead(OpenFileService.FileName))
            //    {
            //        GridViewPersistenceHelper.LoadLayout(param, fs);
            //    }

            //    param.Rebind();
            //}
        }

        private static bool CanLoadSettingsCommandExecute(HeaderCommandParameter parameter)
        {
            Argument.IsNotNull(() => parameter);

            return parameter?.Grid != null;
        }

        public static Command<HeaderCommandParameter> ClearSettingsCommand { get; }
        private static void OnClearSettingsCommandExecute(HeaderCommandParameter parameter)
        {
            Argument.IsNotNull(() => parameter);

            var grid = parameter.Grid;

            if (grid.FieldLayouts.FirstOrDefault() != null)
            {
                var fields = grid.FieldLayouts.First().Fields;

                foreach (var field in fields)
                {
                    field.Visibility = Visibility.Visible;
                    field.Width = FieldLength.Auto;
                    field.CellValuePresenterStyle = null;
                }
            }

            grid.SetValue(Control.FontSizeProperty, Convert.ToDouble(GridViewFontSizeDefault));
            grid.SetValue(Control.FontFamilyProperty, new FontFamily(GridViewFontFamilyDefault));
            grid.SetValue(Control.FontStyleProperty, GridCustomizationHelpers.GetFontStyleFomString(GridViewFontStyleDefault));

            grid.FieldSettings.CellHeight = GridRowSizeHelper.GetRowHeightFromFontSize(grid.FontSize);

            foreach (var fieldlayout in parameter.Grid.FieldLayouts)
            {
                fieldlayout.RecordFilters.Clear();
            }

            RemoveColumnDescriptor(parameter);
        }

        private static bool CanClearSettingsCommandExecute(HeaderCommandParameter parameter)
        {
            Argument.IsNotNull(() => parameter);

            return parameter?.Grid != null;
        }

        public static Command<HeaderCommandParameter> SetDefaultCommand { get; }
        private static void OnSetDefaultCommandExecute(HeaderCommandParameter parameter)
        {
            //Argument.IsNotNull(() => parameter);

            //using (var ms = new MemoryStream())
            //{
            //    GridViewPersistenceHelper.SaveLayout(parameter.Grid, ms);

            //    ms.Seek(0, SeekOrigin.Begin);

            //    var content = ms.GetString(Encoding.UTF8);

            //    var encoded = StringBase64Helper.Base64Encode(content);

            //    StorageService.PushEntry(parameter.ViewModel, encoded, IsolatedStorageHelper.ViewModels);
            //}
        }

        private static bool CanSetDefaultCommandExecute(object parameter)
        {
            return true;
        }

        #region Font, Properties ,Color
        public static TaskCommand<HeaderCommandParameter> ChangeFontCommand { get; }
        private static Task OnChangeFontCommandExecute(HeaderCommandParameter parameter)
        {
            return Task.FromResult(0);
        }

        private static void OnManageFontCompleted(object sender, UICompletedEventArgs e)
        {
            return;
            
        }

        public static TaskCommand<HeaderCommandParameter> ChangeColorCommand { get; }
        private static Task OnChangeColorCommandExecute(HeaderCommandParameter parameter)
        {
            return Task.FromResult(0);
        }

        private static void OnChangeColorCompleted(object sender, UICompletedEventArgs e)
        {
           return;
            
        }

        public static TaskCommand<HeaderCommandParameter> PropertiesCommand { get; }
        private static Task OnPropertiesCommandExecute(HeaderCommandParameter parameter)
        {
            return Task.FromResult(0);
        }

        private static void OnPropertiesCommandCompleted(object sender, UICompletedEventArgs e)
        {
          
            
        }
        #endregion
        #endregion

        #region Ctor
        static DefaultRadGridContextMenuCommands()
        {
            var servicelocator = ServiceLocator.Default;
            ViewModelFactory = servicelocator.ResolveType<IViewModelFactory>();
            UIVisualizerService = servicelocator.ResolveType<IUIVisualizerService>();
            //SaveFileService = servicelocator.ResolveType<ISaveFileService>();
            //OpenFileService = servicelocator.ResolveType<IOpenFileService>();
            //StorageService = servicelocator.ResolveType<IStorageService>();

            SortAscendingCommand = new Command<HeaderCommandParameter>(OnSortAscendingExecute);
            SortDescendingCommand = new Command<HeaderCommandParameter>(OnSortDescendingExecute);
            SortClearingCommand = new Command<HeaderCommandParameter>(OnSortClearingExecute);

            GroupByCommand = new Command<HeaderCommandParameter>(OnGroupByExecute);
            UnGroupByCommand = new Command<HeaderCommandParameter>(OnUnGroupByExecute);

            DeleteFiltersCommand = new Command<HeaderCommandParameter>(OnDeleteFiltersExecute);
            //ExportGridCommand = new Command<RadGridView>(OnExportGridCommand, CanExecuteExportGrid);

            //SaveSettingsCommand = new Command<HeaderCommandParameter>(OnSaveSettingsCommandExecute, CanSaveSettingsCommandExecute);
            //LoadSettingsCommand = new Command<HeaderCommandParameter>(OnLoadSettingsCommandExecute, CanLoadSettingsCommandExecute);

            ClearSettingsCommand = new Command<HeaderCommandParameter>(OnClearSettingsCommandExecute);
            SetDefaultCommand = new Command<HeaderCommandParameter>(OnSetDefaultCommandExecute);

            ChangeColorCommand = new TaskCommand<HeaderCommandParameter>(OnChangeColorCommandExecute);
            ChangeFontCommand = new TaskCommand<HeaderCommandParameter>(OnChangeFontCommandExecute);
            PropertiesCommand = new TaskCommand<HeaderCommandParameter>(OnPropertiesCommandExecute);

            //BaseStyle = Application.Current.FindResource("GridViewCellCoreStyle") as System.Windows.Style;
        }
        #endregion

        #region Internal use methods
        private static void RemoveColumnDescriptor(HeaderCommandParameter parameter)
        {
            if (parameter.Grid.FieldLayouts[0].SortedFields.FirstOrDefault(x => x.Field.Name == parameter.Column) != null)
            {
                parameter.Grid.FieldLayouts[0].SortedFields.Remove(parameter.Grid.FieldLayouts[0].SortedFields.FirstOrDefault(x => x.Field.Name == parameter.Column));
            }
        }
        #endregion
    }

    public class HeaderCommandParameter
    {
        public XamDataGrid Grid { get; set; }
        public string Column { get; set; }
        public string ViewModel { get; set; }
    }
}