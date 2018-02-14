using Catel.MVVM;
using HighFreqUpdate.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Infragistics.Windows.DataPresenter;
using System.Windows.Media;
using Catel.Data;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows;

namespace HighFreqUpdate.ViewModels
{
    public class ManageColorViewModel : ViewModelBase
    {
        public ObservableCollection<ColumnItem> Columns { get; set; }
        public ColumnItem SelectedColumn { get; set; }

        public XamDataGrid Grid { get; protected set; }

        protected ManageColorModel ManageColorModel { get; }

        public override string Title => "Gestione Colori";

        public Color? ForeColor { get; set; }
        public Color? BackColor { get; set; }

        public ManageColorViewModel(ManageColorModel manageColorModel)
        {
            ManageColorModel = manageColorModel;
        }

        protected override async Task InitializeAsync()
        {
            try
            {
                SetProperties();

                await base.InitializeAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //ServiceLocator.Default.ResolveType<IExceptionService>().Process(() => throw new DefaultException(ex));
            }
        }

        protected void SetProperties()
        {
            Grid = ManageColorModel.Grid;
            Columns = ManageColorModel.Columns;
            SelectedColumn = ManageColorModel.SelectedColumn == null ? Columns.First() : Columns.FirstOrDefault(x => x.Name == ManageColorModel.SelectedColumn);
        }

        protected override Task<bool> SaveAsync()
        {
            return Task.FromResult(true);
        }

        protected override void OnPropertyChanged(AdvancedPropertyChangedEventArgs e)
        {
            if (e.HasPropertyChanged(() => SelectedColumn))
            {
                SetColorPickers();
            }
            else if (e.HasPropertyChanged(() => Grid))
            {
                SetColorPickers();
            }
            else if (e.HasPropertyChanged(() => ForeColor))
            {
                if (Columns.FirstOrDefault(x => x.Name == SelectedColumn.Name) != null)
                {
                    var column = Columns.First(x => x.Name == SelectedColumn.Name);
                    column.IsChangedColor = !ForeColor.Equals(column.ForeColor);
                    column.ForeColor = ForeColor;
                }
                SelectedColumn.ForeColor = ForeColor;
            }
            else if (e.HasPropertyChanged(() => BackColor))
            {
                if (Columns.FirstOrDefault(x => x.Name == SelectedColumn.Name) != null)
                {
                    var column = Columns.First(x => x.Name == SelectedColumn.Name);
                    column.IsChangedColor = !BackColor.Equals(column.BackColor);
                    column.BackColor = BackColor;
                }
                SelectedColumn.BackColor = BackColor;
            }
            base.OnPropertyChanged(e);
        }

        private void SetColorPickers()
        {
            if (SelectedColumn == null || Grid?.FieldLayouts.FirstOrDefault() == null) return;

            var fields = Grid.FieldLayouts.First().Fields;
            var field = fields[SelectedColumn.ColumnUniqueName];

            if (field == null) return;

            if (Columns.FirstOrDefault(x => x.Name == SelectedColumn.Name) != null && !Columns.First(x => x.Name == SelectedColumn.Name).ForeColor.Equals(Colors.Black))
                ForeColor = Columns.First(x => x.Name == SelectedColumn.Name).ForeColor;
            else ForeColor = GetColor(field, Control.ForegroundProperty) ?? Colors.Black;

            if (Columns.FirstOrDefault(x => x.Name == SelectedColumn.Name) != null && !Columns.First(x => x.Name == SelectedColumn.Name).BackColor.Equals(Colors.Transparent))
                BackColor = Columns.First(x => x.Name == SelectedColumn.Name).BackColor;
            else BackColor = GetColor(field, Control.BackgroundProperty) ?? Colors.Transparent;
        }

        private Color? GetColor(Field field, DependencyProperty dependencyProperty)
        {
            if (field.CellValuePresenterStyle != null)
            {
                var cellValuePresenterStyle = field.CellValuePresenterStyle;

                var property = cellValuePresenterStyle.Setters.OfType<Setter>().FirstOrDefault(x => x.Property == dependencyProperty);

                var brush = property?.Value as SolidColorBrush;
                return brush?.Color;
            }

            return null;
        }
    }
}