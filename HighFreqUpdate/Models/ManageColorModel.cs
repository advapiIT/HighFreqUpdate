using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Windows.DataPresenter;

namespace HighFreqUpdate.Models
{
    public class ManageColorModel
    {
        public ObservableCollection<ColumnItem> Columns { get; }

        private XamDataGrid grid;
        public XamDataGrid Grid
        {
            get => grid;
            set
            {
                grid = value;
                SetColumnInformation();
            }
        }

        public string SelectedColumn { get; set; }

        public ManageColorModel()
        {
            Columns = new ObservableCollection<ColumnItem>();
        }

        protected void SetColumnInformation()
        {
            if (Grid == null) return;

            if (Grid.FieldLayouts.FirstOrDefault() != null)
            {
                var fields = Grid.FieldLayouts.First().Fields;

                foreach (var field in fields)
                {
                    if (field.CellValuePresenterStyle != null)
                    {
                        var setters = field.CellValuePresenterStyle.Setters;

                        var foreground = GetSetterValue(setters, Control.ForegroundProperty);
                        var background = GetSetterValue(setters, Control.BackgroundProperty);

                        Columns.Add(new ColumnItem
                        {
                            Name = (string)field.Label,
                            ColumnUniqueName = field.Name,
                            ForeColor = foreground?.Color ?? Colors.Black,
                            BackColor = background?.Color ?? Colors.Transparent,
                        });
                    }
                    else
                    {
                        Columns.Add(new ColumnItem
                        {
                            Name = (string)field.Label,
                            ColumnUniqueName = field.Name,
                            ForeColor = Colors.Black,
                            BackColor = Colors.Transparent
                        });
                    }
                }
            }
        }

        private SolidColorBrush GetSetterValue(SetterBaseCollection setters, DependencyProperty dependencyProperty)
        {
            var setter = setters.OfType<Setter>().FirstOrDefault(x => x.Property == dependencyProperty);

            return (SolidColorBrush)setter?.Value;
        }
    }
}