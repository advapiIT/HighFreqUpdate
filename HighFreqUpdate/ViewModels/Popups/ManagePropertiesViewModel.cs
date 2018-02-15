using Catel.Data;
using HighFreqUpdate.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using Infragistics.Windows.DataPresenter;

namespace HighFreqUpdate.ViewModels
{
    public class ManagePropertiesViewModel : Catel.MVVM.ViewModelBase
    {
        public ObservableCollection<ColumnItem> Columns
        {
            get => GetValue<ObservableCollection<ColumnItem>>(ColumnsProperty);
            set => SetValue(ColumnsProperty, value);
        }
        public static readonly PropertyData ColumnsProperty = RegisterProperty("Columns", typeof(ObservableCollection<ColumnItem>));

        public ColumnItem SelectedColumn
        {
            get => GetValue<ColumnItem>(SelectedColumnProperty);
            set => SetValue(SelectedColumnProperty, value);
        }
        public static readonly PropertyData SelectedColumnProperty = RegisterProperty("SelectedColumn", typeof(ColumnItem));

        public int DecimalNumber
        {
            get => GetValue<int>(DecimalNumberProperty);
            set => SetValue(DecimalNumberProperty, value);
        }
        public static readonly PropertyData DecimalNumberProperty = RegisterProperty("DecimalNumber", typeof(int));

        public bool EnableThousandSeparator
        {
            get => GetValue<bool>(EnableThousandSeparatorProperty);
            set => SetValue(EnableThousandSeparatorProperty, value);
        }
        public static readonly PropertyData EnableThousandSeparatorProperty = RegisterProperty("EnableThousandSeparator", typeof(bool));

        public bool EnableCheckThousandSeparator
        {
            get => GetValue<bool>(EnableCheckThousandSeparatorProperty);
            set => SetValue(EnableCheckThousandSeparatorProperty, value);
        }
        public static readonly PropertyData EnableCheckThousandSeparatorProperty = RegisterProperty("EnableCheckThousandSeparator", typeof(bool));

        public ObservableCollection<FormatItem> Formats
        {
            get => GetValue<ObservableCollection<FormatItem>>(FormatsProperty);
            set => SetValue(FormatsProperty, value);
        }
        public static readonly PropertyData FormatsProperty = RegisterProperty("Formats", typeof(ObservableCollection<FormatItem>));

        public FormatItem SelectedFormat
        {
            get => GetValue<FormatItem>(SelectedFormatProperty);
            set => SetValue(SelectedFormatProperty, value);
        }
        public static readonly PropertyData SelectedFormatProperty = RegisterProperty("SelectedFormat", typeof(FormatItem));

        public ObservableCollection<AlignItem> Alignments
        {
            get => GetValue<ObservableCollection<AlignItem>>(AlignmentsProperty);
            set => SetValue(AlignmentsProperty, value);
        }
        public static readonly PropertyData AlignmentsProperty = RegisterProperty("Alignments", typeof(ObservableCollection<AlignItem>));

        public AlignItem SelectedAlign
        {
            get => GetValue<AlignItem>(SelectedAlignProperty);
            set => SetValue(SelectedAlignProperty, value);
        }
        public static readonly PropertyData SelectedAlignProperty = RegisterProperty("SelectedAlign", typeof(AlignItem));

        public bool EnableNotSignificantZero
        {
            get => GetValue<bool>(EnableNotSignificantZeroProperty);
            set => SetValue(EnableNotSignificantZeroProperty, value);
        }
        public static readonly PropertyData EnableNotSignificantZeroProperty = RegisterProperty("EnableNotSignificantZero", typeof(bool));

        public bool IsBlinking
        {
            get => GetValue<bool>(IsBlinkingProperty);
            set => SetValue(IsBlinkingProperty, value);
        }
        public static readonly PropertyData IsBlinkingProperty = RegisterProperty("IsBlinking", typeof(bool));

        public Color BlinkColor
        {
            get => GetValue<Color>(BlinkColorProperty);
            set => SetValue(BlinkColorProperty, value);
        }
        public static readonly PropertyData BlinkColorProperty = RegisterProperty("BlinkColor", typeof(Color));

        public int BlinkTime
        {
            get => GetValue<int>(BlinkTimeProperty);
            set => SetValue(BlinkTimeProperty, value);
        }
        public static readonly PropertyData BlinkTimeProperty = RegisterProperty("BlinkTime", typeof(int));

        public bool IsForeColorNegativeEnabled
        {
            get => GetValue<bool>(IsForeColorNegativeEnabledProperty);
            set => SetValue(IsForeColorNegativeEnabledProperty, value);
        }
        public static readonly PropertyData IsForeColorNegativeEnabledProperty = RegisterProperty("IsForeColorNegativeEnabled", typeof(bool));

        public Color ForeColorNegative
        {
            get => GetValue<Color>(ForeColorNegativeProperty);
            set => SetValue(ForeColorNegativeProperty, value);
        }
        public static readonly PropertyData ForeColorNegativeProperty = RegisterProperty("ForeColorNegative", typeof(Color));

        public bool EnableAggregator
        {
            get => GetValue<bool>(EnableAggregatorProperty);
            set => SetValue(EnableAggregatorProperty, value);
        }
        public static readonly PropertyData EnableAggregatorProperty = RegisterProperty("EnableAggregator", typeof(bool));

        public ObservableCollection<AggregatorItem> Aggregators
        {
            get => GetValue<ObservableCollection<AggregatorItem>>(AggregatorsProperty);
            set => SetValue(AggregatorsProperty, value);
        }
        public static readonly PropertyData AggregatorsProperty = RegisterProperty("Aggregators", typeof(ObservableCollection<AggregatorItem>));

        public AggregatorItem SelectedAggregator
        {
            get => GetValue<AggregatorItem>(SelectedAggregatorProperty);
            set => SetValue(SelectedAggregatorProperty, value);
        }
        public static readonly PropertyData SelectedAggregatorProperty = RegisterProperty("SelectedAggregator", typeof(AggregatorItem));

        public bool EnableLockColumn
        {
            get => GetValue<bool>(EnableLockColumnProperty);
            set => SetValue(EnableLockColumnProperty, value);
        }
        public static readonly PropertyData EnableLockColumnProperty = RegisterProperty("EnableLockColumn", typeof(bool));

        public bool EnableFreezeColumn
        {
            get => GetValue<bool>(EnableFreezeColumnProperty);
            set => SetValue(EnableFreezeColumnProperty, value);
        }
        public static readonly PropertyData EnableFreezeColumnProperty = RegisterProperty("EnableFreezeColumn", typeof(bool));

        public bool ShowNumberOptions
        {
            get => GetValue<bool>(ShowNumberOptionsProperty);
            set => SetValue(ShowNumberOptionsProperty, value);
        }
        public static readonly PropertyData ShowNumberOptionsProperty = RegisterProperty("ShowNumberOptions", typeof(bool));

        public bool ShowDateOptions
        {
            get => GetValue<bool>(ShowDateOptionsProperty);
            set => SetValue(ShowDateOptionsProperty, value);
        }
        public static readonly PropertyData ShowDateOptionsProperty = RegisterProperty("ShowDateOptions", typeof(bool));

        public XamDataGrid Grid { get; set; }
        public ManagePropertiesModel managePropertiesModel { get; set; }
        public bool WinResult { get; set; }

        public override string Title => "Gestione Proprietà";

        public ManagePropertiesViewModel(ManagePropertiesModel managePropertiesModel)
        {
            this.managePropertiesModel = managePropertiesModel;
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

        protected override void OnPropertyChanged(AdvancedPropertyChangedEventArgs e)
        {
            try
            {
                if (e.HasPropertyChanged(() => SelectedColumn))
                {
                    if (SelectedColumn != null)
                    {
                        if (SelectedColumn.Type == typeof(int?) || SelectedColumn.Type == typeof(double?) || SelectedColumn.Type == typeof(int) || SelectedColumn.Type == typeof(double))
                        {
                            DecimalNumber = SelectedColumn.Decimals;
                            RaisePropertyChanged(() => DecimalNumber);

                            EnableThousandSeparator = SelectedColumn.EnableThousandSeparator;
                            RaisePropertyChanged(() => EnableThousandSeparator);

                            EnableCheckThousandSeparator = SelectedColumn.EnableCheckThousandSeparator;
                            RaisePropertyChanged(() => EnableCheckThousandSeparator);

                            EnableNotSignificantZero = SelectedColumn.EnableNotSignificantZero;
                            RaisePropertyChanged(() => EnableNotSignificantZero);

                            EnableAggregator = SelectedColumn.EnableAggregator;
                            RaisePropertyChanged(() => EnableAggregator);

                            SelectedAggregator = Aggregators.FirstOrDefault(x => x.AggregatorId == SelectedColumn.Aggregator);
                            RaisePropertyChanged(() => SelectedAggregator);

                            IsForeColorNegativeEnabled = SelectedColumn.IsForeColorNegativeEnabled;
                            RaisePropertyChanged(() => IsForeColorNegativeEnabled);

                            ForeColorNegative = SelectedColumn.ForeColorNegative;
                            RaisePropertyChanged(() => ForeColorNegative);

                            ShowNumberOptions = true;
                            RaisePropertyChanged(() => ShowNumberOptions);
                            ShowDateOptions = false;
                            RaisePropertyChanged(() => ShowDateOptions);
                        }
                        else if (SelectedColumn.Type == typeof(DateTime?) || SelectedColumn.Type == typeof(DateTime))
                        {
                            SelectedFormat = Formats.FirstOrDefault(x => x.FormatId == SelectedColumn.Format);
                            RaisePropertyChanged(() => SelectedFormat);

                            ShowDateOptions = true;
                            RaisePropertyChanged(() => ShowDateOptions);
                            ShowNumberOptions = false;
                            RaisePropertyChanged(() => ShowNumberOptions);
                        }
                        else
                        {
                            ShowNumberOptions = false;
                            RaisePropertyChanged(() => ShowNumberOptions);

                            ShowDateOptions = false;
                            RaisePropertyChanged(() => ShowDateOptions);
                        }

                        SelectedAlign = Alignments.FirstOrDefault(x => x.AlignId == SelectedColumn.Align);
                        RaisePropertyChanged(() => SelectedAlign);

                        IsBlinking = SelectedColumn.IsBlinking;
                        RaisePropertyChanged(() => IsBlinking);

                        BlinkColor = SelectedColumn.BlinkColor;
                        RaisePropertyChanged(() => BlinkColor);

                        BlinkTime = SelectedColumn.BlinkingFrequency;
                        RaisePropertyChanged(() => BlinkTime);

                        EnableLockColumn = SelectedColumn.EnableLockColumn;
                        RaisePropertyChanged(() => EnableLockColumn);

                        EnableFreezeColumn = SelectedColumn.EnableFreezeColumn;
                        RaisePropertyChanged(() => EnableFreezeColumn);
                    }
                }
                else if (e.HasPropertyChanged(() => DecimalNumber))
                {
                    if (SelectedColumn != null)
                        SelectedColumn.Decimals = DecimalNumber;
                }
                else if (e.HasPropertyChanged(() => EnableThousandSeparator))
                {
                    if (SelectedColumn != null)
                        SelectedColumn.EnableThousandSeparator = EnableThousandSeparator;
                }
                else if (e.HasPropertyChanged(() => SelectedFormat))
                {
                    if (SelectedColumn != null)
                        SelectedColumn.Format = SelectedFormat.FormatId;
                }
                else if (e.HasPropertyChanged(() => SelectedAlign))
                {
                    if (SelectedColumn != null)
                        SelectedColumn.Align = SelectedAlign.AlignId;
                }
                else if (e.HasPropertyChanged(() => EnableNotSignificantZero))
                {
                    if (SelectedColumn != null)
                        SelectedColumn.EnableNotSignificantZero = EnableNotSignificantZero;
                }
                else if (e.HasPropertyChanged(() => IsBlinking))
                {
                    if (SelectedColumn != null)
                        SelectedColumn.IsBlinking = IsBlinking;
                }
                else if (e.HasPropertyChanged(() => BlinkColor))
                {
                    if (SelectedColumn != null)
                        SelectedColumn.BlinkColor = BlinkColor;
                }
                else if (e.HasPropertyChanged(() => BlinkTime))
                {
                    if (SelectedColumn != null)
                        SelectedColumn.BlinkingFrequency = BlinkTime;
                }
                else if (e.HasPropertyChanged(() => IsForeColorNegativeEnabled))
                {
                    if (SelectedColumn != null)
                        SelectedColumn.IsForeColorNegativeEnabled = IsForeColorNegativeEnabled;
                }
                else if (e.HasPropertyChanged(() => ForeColorNegative))
                {
                    if (SelectedColumn != null)
                        SelectedColumn.ForeColorNegative = ForeColorNegative;
                }
                else if (e.HasPropertyChanged(() => EnableAggregator))
                {
                    if (SelectedColumn != null)

                        SelectedColumn.EnableAggregator = EnableAggregator;
                }
                else if (e.HasPropertyChanged(() => SelectedAggregator))
                {
                    if (SelectedColumn != null)
                        SelectedColumn.Aggregator = SelectedAggregator.AggregatorId;
                }
                else if (e.HasPropertyChanged(() => EnableLockColumn))
                {
                    if (SelectedColumn != null)
                        SelectedColumn.EnableLockColumn = EnableLockColumn;
                }
                else if (e.HasPropertyChanged(() => EnableFreezeColumn))
                {
                    if (SelectedColumn != null)
                        SelectedColumn.EnableFreezeColumn = EnableFreezeColumn;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //ServiceLocator.Default.ResolveType<IExceptionService>().Process(() => throw new DefaultException(ex));
            }

            base.OnPropertyChanged(e);
        }

        public virtual void SetProperties()
        {
            Formats = managePropertiesModel.Formats;
            Alignments = managePropertiesModel.Alignments;
            Aggregators = managePropertiesModel.Aggregators;
            ShowNumberOptions = true;
            ShowDateOptions = true;
            Columns = managePropertiesModel.Columns;
            SelectedColumn = managePropertiesModel.SelectedColumn == null ? Columns.First() : Columns.FirstOrDefault(x => x.Name == managePropertiesModel.SelectedColumn);
            Grid = managePropertiesModel.Grid;
        }

        protected override async Task<bool> SaveAsync()
        {
            WinResult = true;
            return true;
        }
    }
}