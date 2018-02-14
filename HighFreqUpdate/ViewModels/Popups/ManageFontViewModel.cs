using Catel.MVVM;
using HighFreqUpdate.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Infragistics.Windows.DataPresenter;
using System.Globalization;

namespace HighFreqUpdate.ViewModels
{
    public class ManageFontViewModel : ViewModelBase
    {
        #region Properties
        public ObservableCollection<string> Fonts { get; set; }

        public string SelectedFont { get; set; }

        public ObservableCollection<string> FontStyles { get; set; }

        //[TypeConverter(typeof(FontStyleTypeConverter))]
        public string SelectedFontStyle { get; set; }

        public ObservableCollection<string> FontSizes { get; set; }

        public string SelectedFontSize { get; set; }

        public XamDataGrid Grid { get; set; }

        public ManageFontModel ManageFontModel { get; set; }

        public override string Title => "Gestione font";

        public string PreviewText => "Esempio di stile";
        #endregion

        #region Constructors
        public ManageFontViewModel(ManageFontModel manageFontModel)
        {
            ManageFontModel = manageFontModel;
        }
        #endregion

        #region Override
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
                //ServiceLocator.Default.ResolveType<IExceptionService>().Process(() => throw new Exception(ex));
            }
        }

        public void SetProperties()
        {
            Fonts = ManageFontModel.Fonts;
            FontStyles = ManageFontModel.FontStyles;
            FontSizes = ManageFontModel.FontSizes;
            Grid = ManageFontModel.Grid;

            if (Grid.FontFamily != null)
                SelectedFont = Grid.FontFamily.ToString();

            SelectedFontStyle = Grid.FontStyle.ToString();
            SelectedFontSize = Grid.FontSize.ToString(CultureInfo.InvariantCulture);
        }

        protected override Task<bool> SaveAsync()
        {
            return Task.FromResult(true);
        }
        #endregion
    }
}