using System.Linq;
using Catel.MVVM;
using Catel.Services;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Windows.DataPresenter;

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
        // ReSharper disable once InconsistentNaming
        private static readonly IUIVisualizerService UIVisualizerService;

        private static readonly ISaveFileService SaveFileService;
        private static readonly IOpenFileService OpenFileService;
        //    private static readonly IStorageService StorageService;
        #endregion

        #region Properties
        private static System.Windows.Style BaseStyle { get; set; }
        #endregion

        #region Commands
        public static Command<HeaderCommandParameter> SortAscendingCommand
        {
            get;
        }
        private static void OnSortAscendingExecute(HeaderCommandParameter parameter)
        {
            //Argument.IsNotNull(() => parameter);

            //RemoveColumnDescriptor(parameter);

            //var newDescriptor = new ColumnSortDescriptor
            //{
            //    Column = parameter.Column,
            //    SortDirection = ListSortDirection.Ascending
            //};

            //parameter.Grid.SortDescriptors.Add(newDescriptor);
        }

        public static Command<HeaderCommandParameter> SortDescendingCommand
        {
            get;
        }
        private static void OnSortDescendingExecute(HeaderCommandParameter parameter)
        {
            //Argument.IsNotNull(() => parameter);

            //RemoveColumnDescriptor(parameter);

            //var newDescriptor = new ColumnSortDescriptor
            //{
            //    Column = parameter.Column,
            //    SortDirection = ListSortDirection.Descending
            //};

            //parameter.Grid.SortDescriptors.Add(newDescriptor);
        }

        public static Command<HeaderCommandParameter> SortClearingCommand
        {
            get;
        }
        private static void OnSortClearingExecute(HeaderCommandParameter parameter)
        {
            //Argument.IsNotNull(() => parameter);

            //RemoveColumnDescriptor(parameter);
        }

        public static Command<HeaderCommandParameter> GroupByCommand
        {
            get;
        }
        private static void OnGroupByExecute(HeaderCommandParameter parameter)
        {
            //Argument.IsNotNull(() => parameter);

            //ColumnGroupDescriptor cgd;
            //if (!IsGroupDescriptoDefinedRemoveColumnDescriptor(parameter, out cgd))
            //{
            //    var newDescriptor = new ColumnGroupDescriptor
            //    {
            //        Column = parameter.Column,
            //        SortDirection = ListSortDirection.Ascending
            //    };
            //    parameter.Grid.GroupDescriptors.Add(newDescriptor);
            //}
        }

        public static Command<HeaderCommandParameter> UnGroupByCommand
        {
            get;
        }
        private static void OnUnGroupByExecute(HeaderCommandParameter parameter)
        {
            //Argument.IsNotNull(() => parameter);

            //ColumnGroupDescriptor cgd;

            //if (IsGroupDescriptoDefinedRemoveColumnDescriptor(parameter, out cgd))
            //{
            //    parameter.Grid.GroupDescriptors.Remove(cgd);
            //}
        }

        public static Command<HeaderCommandParameter> DeleteFiltersCommand
        {
            get;
        }
        private static void OnDeleteFiltersExecute(HeaderCommandParameter parameter)
        {
            //Argument.IsNotNull(() => parameter);

            //parameter.Grid.FilterDescriptors.Clear();

            //foreach (var i in parameter.Grid.Columns)
            //{
            //    i.ClearFilters();
            //}
        }

        public static Command<XamDataGrid> ExportGridCommand
        {
            get;
        }

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
            return true;
            //Argument.IsNotNull(() => parameter);

            //return parameter?.Items.Count > 0;
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
        public static Command<HeaderCommandParameter> ClearSettingsCommand { get; set; }
        public static Command<HeaderCommandParameter> SetDefaultCommand { get; set; }

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
            //Argument.IsNotNull(() => parameter);

            return parameter?.Grid != null;
        }

        private static void OnClearSettingsCommandExecute(HeaderCommandParameter parameter)
        {
            //Argument.IsNotNull(() => parameter);
            //IGridInitialStateService gridInitialStateService =
            //    ServiceLocator.Default.ResolveType<IGridInitialStateService>();

            //var param = parameter;

            //var grid = param.Grid;

            //Control control = ParentControlHelper.GetParentControl(grid);

            //string key = control.GetType().FullName;
            //if (gridInitialStateService.ExistKey(key))
            //{
            //    var sortDictionary = gridInitialStateService.GetSortingEntry(key);

            //    if (sortDictionary != null)
            //    {
            //        foreach (var i in sortDictionary)
            //        {
            //            grid.Columns[i.Key].DisplayIndex = i.Value;
            //        }
            //    }
            //}

            //foreach (var gridViewColumn in grid.Columns)
            //{
            //    gridViewColumn.IsVisible = true;
            //    gridViewColumn.Width = GridViewLength.Auto;
            //    //gridViewColumn.CellStyle = new System.Windows.Style(typeof(GridViewCell), BaseStyle);
            //    if (gridViewColumn.CellStyleSelector != null)
            //    {
            //        if (gridViewColumn.CellStyleSelector is CellStyleSelector cellStyleSelector)
            //        {
            //            cellStyleSelector.AdditionalStyles.Remove(gridViewColumn.UniqueName);
            //            cellStyleSelector.CurrentStyles[gridViewColumn.UniqueName] = new System.Windows.Style(typeof(GridViewCell), BaseStyle);
            //        }
            //    }
            //}

            //grid.FontSize = Convert.ToDouble(GridViewFontSizeDefault);
            //grid.FontFamily = new FontFamily(GridViewFontFamilyDefault);
            //grid.FontStyle = GridCustomizationHelpers.GetFontStyleFomString(GridViewFontStyleDefault);

            //grid.RowHeight = GridRowSizeHelper.GetRowHeightFromFontSize(grid.FontSize);

            //grid.SortDescriptors.Clear();
            //grid.FilterDescriptors.Clear();
            //grid.GroupDescriptors.Clear();

            //var rtViewDefaultFlexGridStateService = ServiceLocator.Default.ResolveType<IDefaultGridStateService>();
            //rtViewDefaultFlexGridStateService.ClearDefaultSettings(parameter.ViewModel);

            //grid.Rebind();
        }

        private static bool CanResetSettingsCommandExecute(HeaderCommandParameter parameter)
        {
            return true;
            //Argument.IsNotNull(() => parameter);

            //return parameter?.Grid != null;
        }


        #region Font, Properties ,Color
        public static TaskCommand<HeaderCommandParameter> ChangeFontCommand { get; }
        private static Task OnChangeFontCommandExecute(HeaderCommandParameter parameter)
        {
            var g = parameter.Grid;

            g.SetValue(Control.FontSizeProperty, 30.0);
            g.SetValue(Control.FontFamilyProperty, new FontFamily("Arial"));
            g.SetValue(Control.FontStyleProperty, FontStyles.Italic);

            return Task.FromResult(0);
            //Argument.IsNotNull(() => parameter);

            //var manageFontModel = new ManageFontModelRadGrid { Grid = parameter.Grid };

            //var viewModel = ViewModelFactory.CreateViewModel<ManageFontViewModel>(manageFontModel, null);

            //return UIVisualizerService.ShowDialogAsync(viewModel, OnManageFontCompleted);
        }

        //refactor
        private static void OnManageFontCompleted(object sender, UICompletedEventArgs e)
        {
            //var dataContext = e.DataContext as ManageFontViewModel;
            //if (dataContext == null || !e.Result.HasValue || !e.Result.Value) return;
            //try
            //{
            //    var grid = dataContext.Grid;

            //    grid.FontSize = Convert.ToDouble(dataContext.SelectedFontSize);
            //    grid.FontFamily = new FontFamily(dataContext.SelectedFont);

            //    grid.FontStyle = GridCustomizationHelpers.GetFontStyleFomString(dataContext.SelectedFontStyle);

            //    grid.RowHeight = GridRowSizeHelper.GetRowHeightFromFontSize(grid.FontSize);
            //}
            //catch (Exception ex)
            //{
            //    ServiceLocator.Default.ResolveType<IExceptionService>().Process(() => throw new DefaultException(ex));
            //}
        }

        public static TaskCommand<HeaderCommandParameter> ChangeColorCommand { get; }
        //refactor
        private static Task OnChangeColorCommandExecute(HeaderCommandParameter parameter)
        {
            var g = parameter.Grid;

            var fL = g.FieldLayouts;

            var fLF = fL.First();

            var fields = fLF.Fields;

            var field = fields[1];

            var style = new Style();

            style.Setters.Add(new Setter (Control.BackgroundProperty , new SolidColorBrush(Colors.Red)));
            style.Setters.Add(new Setter (Control.ForegroundProperty , new SolidColorBrush(Colors.Yellow)));

            field.CellValuePresenterStyle = style;
            return Task.FromResult(0);
            //var manageColorModel = new ManageColorModelRadGrid
            //{
            //    Grid = parameter.Grid,
            //    SelectedColumn = (string)parameter.Column.Header
            //};

            //var modello = ViewModelFactory.CreateViewModel<ManageColorViewModel>(manageColorModel, null);

            //return UIVisualizerService.ShowDialogAsync(modello, OnChangeColorCompleted);
        }

        private static void OnChangeColorCompleted(object sender, UICompletedEventArgs e)
        {
            //var dataContext = e.DataContext as ManageColorViewModel;
            //if (dataContext == null || !e.Result.HasValue || !e.Result.Value) return;
            //try
            //{
            //    var grid = dataContext.Grid;

            //    var columnsColors = dataContext.Columns;

            //    foreach (var col in grid.Columns)
            //    {
            //        var columnWithColor = columnsColors.FirstOrDefault(x => x.Name == (string)col.Header);

            //        if (columnWithColor != null)
            //        {
            //            if ((columnWithColor.ForeColor.HasValue || columnWithColor.BackColor.HasValue) && columnWithColor.IsChangedColor)
            //            {
            //                var cellStyleSelector = col.CellStyleSelector as CellStyleSelector;
            //                var cellStyle = new System.Windows.Style(typeof(GridViewCell), BaseStyle);

            //                if (columnWithColor.ForeColor.HasValue)
            //                    cellStyle.Setters.Add(new Setter(Control.ForegroundProperty, new SolidColorBrush(columnWithColor.ForeColor.Value)));
            //                if (columnWithColor.BackColor.HasValue)
            //                    cellStyle.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(columnWithColor.BackColor.Value)));

            //                if (cellStyleSelector != null && !cellStyleSelector.AdditionalStyles.ContainsKey(col.UniqueName))
            //                    cellStyleSelector.AdditionalStyles.Add(col.UniqueName, cellStyle);
            //                else if (cellStyleSelector != null && cellStyleSelector.AdditionalStyles.ContainsKey(col.UniqueName))
            //                    if (!columnWithColor.ForeColor.Equals(System.Windows.Media.Colors.Black) || !columnWithColor.BackColor.Equals(System.Windows.Media.Colors.Transparent))
            //                        cellStyleSelector.AdditionalStyles[col.UniqueName] = cellStyle;
            //                    else if (columnWithColor.ForeColor.Equals(System.Windows.Media.Colors.Black) && columnWithColor.BackColor.Equals(System.Windows.Media.Colors.Transparent))
            //                        cellStyleSelector.AdditionalStyles.Remove(col.UniqueName);
            //            }
            //        }
            //    }

            //    grid.Rebind();
            //}
            //catch (Exception ex)
            //{
            //    ServiceLocator.Default.ResolveType<IExceptionService>().Process(() => throw new DefaultException(ex));
            //}
        }
        #endregion
        #endregion

        #region Ctor
        static DefaultRadGridContextMenuCommands()
        {
            //var servicelocator = ServiceLocator.Default;
            //ViewModelFactory = servicelocator.ResolveType<IViewModelFactory>();
            //UIVisualizerService = servicelocator.ResolveType<IUIVisualizerService>();
            //SaveFileService = servicelocator.ResolveType<ISaveFileService>();
            //OpenFileService = servicelocator.ResolveType<IOpenFileService>();
            //StorageService = servicelocator.ResolveType<IStorageService>();

            //SortAscendingCommand = new Command<HeaderCommandParameter>(OnSortAscendingExecute);
            //SortDescendingCommand = new Command<HeaderCommandParameter>(OnSortDescendingExecute);
            //SortClearingCommand = new Command<HeaderCommandParameter>(OnSortClearingExecute);

            //GroupByCommand = new Command<HeaderCommandParameter>(OnGroupByExecute);
            //UnGroupByCommand = new Command<HeaderCommandParameter>(OnUnGroupByExecute);

            //DeleteFiltersCommand = new Command<HeaderCommandParameter>(OnDeleteFiltersExecute);
            //ExportGridCommand = new Command<RadGridView>(OnExportGridCommand, CanExecuteExportGrid);

            //SaveSettingsCommand = new Command<HeaderCommandParameter>(OnSaveSettingsCommandExecute, CanSaveSettingsCommandExecute);
            //LoadSettingsCommand = new Command<HeaderCommandParameter>(OnLoadSettingsCommandExecute, CanLoadSettingsCommandExecute);

            //ClearSettingsCommand = new Command<HeaderCommandParameter>(OnClearSettingsCommandExecute, CanResetSettingsCommandExecute);
            //SetDefaultCommand = new Command<HeaderCommandParameter>(OnSetDefaultCommandExecute, CanSetDefaultCommandExecute);

            ChangeFontCommand = new TaskCommand<HeaderCommandParameter>(OnChangeFontCommandExecute);
            ChangeColorCommand = new TaskCommand<HeaderCommandParameter>(OnChangeColorCommandExecute);

            //BaseStyle = Application.Current.FindResource("GridViewCellCoreStyle") as System.Windows.Style;
        }

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

        private static bool CanSetDefaultCommandExecute(HeaderCommandParameter parameter)
        {
            var param = parameter;
            return param?.Grid != null;
        }

        #endregion

        #region Internal use methods
        private static void RemoveColumnDescriptor(HeaderCommandParameter parameter)
        {
            //ColumnSortDescriptor sd = (from d in parameter.Grid.SortDescriptors.OfType<ColumnSortDescriptor>()
            //                           where Equals(d.Column, parameter.Column)
            //                           select d).FirstOrDefault();
            //if (sd != null)
            //{
            //    parameter.Grid.SortDescriptors.Remove(sd);
            //}
        }

        //private static bool IsGroupDescriptoDefinedRemoveColumnDescriptor(HeaderCommandParameter parameter, out ColumnGroupDescriptor columnGroupDescriptor)
        //{
        //    //ColumnGroupDescriptor gd = (from d in parameter.Grid.GroupDescriptors.OfType<ColumnGroupDescriptor>()
        //    //                            where Equals(d.Column, parameter.Column)
        //    //                            select d).FirstOrDefault();
        //    //columnGroupDescriptor = gd;
        //    //return gd != null;

        //}
        #endregion
    }

    public class HeaderCommandParameter
    {
        public XamDataGrid Grid { get; set; }
        public string Column { get; set; }
        public string ViewModel { get; set; }
    }
}