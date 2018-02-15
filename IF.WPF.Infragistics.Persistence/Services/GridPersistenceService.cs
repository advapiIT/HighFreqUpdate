using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Catel;
using Catel.Runtime.Serialization.Xml;
using Catel.Services;
using Infragistics.Windows.DataPresenter;

namespace IF.WPF.Infragistics.Persistence
{
    public class GridPersistenceService : IGridPersistenceService
    {
        private readonly IDispatcherService dispatcherService;
        private readonly IXmlSerializer xmlSerializer;

        private const string GridSaves = "c:\\temp\\prova123.xml";

        public GridPersistenceService(IDispatcherService dispatcherService, IXmlSerializer xmlSerializer)
        {
            Argument.IsNotNull(() => dispatcherService);
            Argument.IsNotNull(() => xmlSerializer);

            this.dispatcherService = dispatcherService;
            this.xmlSerializer = xmlSerializer;
        }

        #region IGridPersistenceService region
        public Task PersistGrid(XamDataGrid grid)
        {
            return dispatcherService.InvokeAsync(() =>
            {
                GridCustomizations gridCustomizations = null;

                string layout = string.Empty;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    grid.SaveCustomizations(memoryStream);

                    byte[] bytes = memoryStream.ToArray();

                    layout = Encoding.UTF8.GetString(bytes);
                }

                gridCustomizations = GetGridExternalInformations(grid);
                gridCustomizations.GridLayout = layout;

                using (var memoryStream = new MemoryStream())
                {
                    xmlSerializer.Serialize(gridCustomizations, memoryStream);

                    byte[] bytes = memoryStream.ToArray();

                    File.WriteAllBytes(GridSaves, bytes);
                }

            });
        }

        public Task RestoreGrid(XamDataGrid grid)
        {
            return dispatcherService.InvokeAsync(() =>
            {
                byte[] bytes = File.ReadAllBytes(GridSaves);

                using (var memoryStream = new MemoryStream(bytes))
                {
                    GridCustomizations gridCustomizations =
                        xmlSerializer.Deserialize(typeof(GridCustomizations), memoryStream) as GridCustomizations;

                    if (!string.IsNullOrEmpty(gridCustomizations.GridLayout))
                    {
                        var layoutBytes = Encoding.UTF8.GetBytes(gridCustomizations.GridLayout);


                        using (var layoutStream = new MemoryStream(layoutBytes))
                        {
                            grid.LoadCustomizations(layoutStream);
                        }
                    }

                    SetGridExternalInformations(grid, gridCustomizations);
                }
            });
        }
        #endregion


        private static void SetGridExternalInformations(XamDataGrid grid, GridCustomizations gridCustomizations)
        {
            foreach (var gridCustomization in gridCustomizations.ColumnsStyle.Where(x => x.Value.HasData))
            {
                string columnName = gridCustomization.Key;

                var column = grid.FieldLayouts[0].Fields.FirstOrDefault(x => x.Name == columnName);

                var style = new Style(typeof(CellValuePresenter));

                if (!string.IsNullOrEmpty(gridCustomization.Value.ForeColor))
                {
                    style.Setters.Add(new Setter(Control.ForegroundProperty,
                        new SolidColorBrush((Color)ColorConverter.ConvertFromString(gridCustomization.Value.ForeColor))));
                }

                if (!string.IsNullOrEmpty(gridCustomization.Value.BackGroundColor))
                {
                    style.Setters.Add(new Setter(Control.BackgroundProperty,
                        new SolidColorBrush((Color)ColorConverter.ConvertFromString(gridCustomization.Value.BackGroundColor))));
                }

                column.CellValuePresenterStyle = style;
            }
        }

        public static GridCustomizations GetGridExternalInformations(XamDataGrid grid)
        {
            var gridCustomizations = new GridCustomizations();

            foreach (var field in grid.FieldLayouts[0].Fields)
            {
                if (field.CellValuePresenterStyle?.Setters?.Count > 0)
                {
                    ColumnSettings columnSettings = new ColumnSettings();

                    foreach (Setter r in field.CellValuePresenterStyle.Setters)
                    {
                        if (r.Property.Name == Constants.ForegroundKey)
                        {
                            columnSettings.ForeColor = r.Value.ToString();
                        }
                        else if (r.Property.Name == Constants.BackgroundKey)
                        {
                            columnSettings.BackGroundColor = r.Value.ToString();
                        }
                    }

                    if (columnSettings.HasData)
                    {
                        gridCustomizations.ColumnsStyle[field.Name] = columnSettings;
                    }
                }
            }

            return gridCustomizations;
        }
    }
}
