using System;
using System.Collections.ObjectModel;
using System.Linq;
using HighFreqUpdate.Helpers;
using Infragistics.Windows.DataPresenter;

namespace HighFreqUpdate.Models
{
    public class ManagePropertiesModel
    {
        private XamDataGrid grid;

        internal class Resources
        {
            public const string DateFormatddMMYYYY = "dd/MM/yyyy";
            public const string DateFormatMMddYYYY = "MM/dd/yyyy";
            public const string DateFormatMMYYYY = "MM/yyyy";
            public const string DateFormatYYYYMM = "yyyy/MM";
            public const string DateFormatddMMyyyyHHmmss = "dd/MM/yyyy HH:mm:ss";
            public const string DateFormatMMddyyyyHHmmss = "MM/dd/yyyy HH:mm:ss";
            public const string HHmmss = "HH:mm:ss";

            public const string LeftAlignment = "Sinistra";
            public const string CenterAlignment = "Centro";
            public const string RightAlignment = "Destra";
            public const string JustifyAlignment = "Giustificato";

            public const string SumAggregator = "Somma";
            public const string SumAggregatorType = "Sum";
            public const string AverageAggregator = "Media";
            public const string AverageAggregatorType = "Average";
            public const string TotalRowsAggregator = "Totale righe";
            public const string TotalRowsAggregatorType = "Count";
        }
        public ObservableCollection<ColumnItem> Columns { get; set; }

        public ObservableCollection<FormatItem> Formats { get; set; }

        public ObservableCollection<AlignItem> Alignments { get; set; }

        public ObservableCollection<AggregatorItem> Aggregators { get; set; }

        public XamDataGrid Grid
        {
            get => grid;
            set
            {
                grid = value;
                SetColumnsInformation();
            }
        }

        public string SelectedColumn { get; set; }

        public ManagePropertiesModel()
        {
            Columns = new ObservableCollection<ColumnItem>();

            Formats = new ObservableCollection<FormatItem>
            {
                new FormatItem {FormatId = 0, FormatName = null},
                new FormatItem {FormatId = 1, FormatName = Resources.DateFormatddMMYYYY},
                new FormatItem {FormatId = 2, FormatName = Resources.DateFormatMMddYYYY},
                new FormatItem {FormatId = 3, FormatName = Resources.DateFormatMMYYYY},
                new FormatItem {FormatId = 4, FormatName = Resources.DateFormatYYYYMM},
                new FormatItem {FormatId = 5, FormatName = Resources.DateFormatddMMyyyyHHmmss},
                new FormatItem {FormatId = 6, FormatName = Resources.DateFormatMMddyyyyHHmmss},
                new FormatItem {FormatId = 7, FormatName = Resources.HHmmss},
            };

            Alignments = new ObservableCollection<AlignItem>
            {
                new AlignItem {AlignId = 0, AlignName = Resources.LeftAlignment},
                new AlignItem {AlignId = 1, AlignName = Resources.CenterAlignment},
                new AlignItem {AlignId = 2, AlignName = Resources.RightAlignment}
            };

            Aggregators = new ObservableCollection<AggregatorItem>
            {
                new AggregatorItem {AggregatorId = 0, AggregatorName = null, AggregatorType = null},
                new AggregatorItem {AggregatorId = 1, AggregatorName = Resources.SumAggregator, AggregatorType = Resources.SumAggregatorType},
                new AggregatorItem {AggregatorId = 2, AggregatorName = Resources.TotalRowsAggregator, AggregatorType = Resources.TotalRowsAggregatorType}
            };
        }

        protected void SetColumnsInformation()
        {
            if (Grid?.FieldLayouts != null && Grid?.FieldLayouts.FirstOrDefault() != null)
            {
                var fields = Grid.FieldLayouts.First().Fields;

                var summaryDefinition = Grid.FieldLayouts.First().SummaryDefinitions;

                foreach (var field in fields)
                {
                    Columns.Add(
                        new ColumnItem
                        {
                            Name = (string)field.Label,
                            Type = field.DataType,
                            Decimals = GetDecimals(field),
                            EnableThousandSeparator = field.Format?.Contains("N") ?? false,
                            EnableCheckThousandSeparator = CheckThousandSeparator(field),
                            Format = GetFormatType(field),
                            Align = GetAligment(field),
                            EnableAggregator = summaryDefinition.FirstOrDefault(x => x.SourceFieldName == field.Name) != null && summaryDefinition.First(x => x.SourceFieldName == field.Name) != null,
                            Aggregator = GetAggregator(Grid, field.Name),
                            EnableFreezeColumn = field.FixedLocation != FixedFieldLocation.Scrollable,
                            //EnableLockColumn = !field.IsReorderable,
                            CanBlink = field.DataType != typeof(string) && !GridCustomizationHelpers.DateTimeTypes.Contains(field.DataType),
                            //CanChangeBlinkColor = (string)field.Tag == TagBlinkable,
                            //IsBlinking = GridColumnProperties.GetIsBlinking(field),
                            //BlinkColor = GridColumnProperties.GetBlinkColor(field),
                            //BlinkingFrequency = GridColumnProperties.GetBlinkFreq(field),
                            //IsForeColorNegativeEnabled = GridColumnProperties.GetIsFontColorNegativeEnabled(field),
                            //ForeColorNegative = GridColumnProperties.GettForeColorNegative(field)
                        });
                }
            }
        }

        private int GetDecimals(Field column)
        {
            int res = !string.IsNullOrEmpty(column.Format) && IsNumberType(column.DataType) ? Convert.ToInt32(column.Format.Substring(1, 1)) : 0;

            return res;
        }

        private bool CheckThousandSeparator(Field column)
        {
            return !string.IsNullOrEmpty(column.Format) && (column.Format.Contains("N") || column.Format.Contains("F"));
        }

        private int GetFormatType(Field column)
        {
            return !string.IsNullOrEmpty(column.Format) && GridCustomizationHelpers.DateTimeTypes.Contains(column.DataType) ? Formats.First(x => x.FormatName == column.Format).FormatId : Formats.First().FormatId;
        }

        private int GetAligment(Field column)
        {
            var key = column.HorizontalContentAlignment.HasValue ? (int)column.HorizontalContentAlignment.Value : 0;
            if (!GridCustomizationHelpers.MappingTextAligment.ContainsKey(key)) throw new Exception();//throw new KeyNotFoundException(key.ToString());

            return GridCustomizationHelpers.MappingTextAligment[key];
        }

        private int GetAggregator(XamDataGrid grid, string columnName)
        {
            if (grid.FieldLayouts.FirstOrDefault() != null)
            {
                var summaryDefinition = grid.FieldLayouts.First().SummaryDefinitions;

                if (summaryDefinition.FirstOrDefault(x => x.SourceFieldName == columnName) != null)
                {
                    if (summaryDefinition.First(x => x.SourceFieldName == columnName).Calculator != null)
                        return Aggregators.FirstOrDefault(x => x.AggregatorType == summaryDefinition.First(y => y.SourceFieldName == columnName).Calculator.Name) != null
                            ? Aggregators.First(x => x.AggregatorType == summaryDefinition.First(y => y.SourceFieldName == columnName).Calculator.Name).AggregatorId
                            : 0;
                }
            }

            return 0;
        }

        private bool IsNumberType(Type type)
        {
            return GridCustomizationHelpers.NumericTypes.Contains(type);
        }
    }
}