using System.Drawing.Text;
using System.Windows;
using Catel.Collections;
using Catel.Data;
using Infragistics.Windows.DataPresenter;

namespace HighFreqUpdate.Models
{
    public class ManageFontModel : ModelBase
    {
        #region Static Fields and Constants
        private const int MinFontSize = 6;
        private const int MaxFontSize = 79;
        private const int FontSizeStep = 2;
        #endregion

        #region Properties
        public FastObservableCollection<string> Fonts { get; set; }

        public FastObservableCollection<string> FontStyles { get; set; }

        public FastObservableCollection<string> FontSizes { get; set; }

        public XamDataGrid Grid { get; set; }
        #endregion

        #region Constructors

        public ManageFontModel()
        {
            Fonts = new FastObservableCollection<string>();
            FontStyles = new FastObservableCollection<string>();
            FontSizes = new FastObservableCollection<string>();

            using (var installedFontCollection = new InstalledFontCollection())
            {
                var fonts = installedFontCollection.Families;

                foreach (var font in fonts)
                    Fonts.Add(font.Name);
            }

            using (FontStyles.SuspendChangeNotifications())
            {
                foreach (var propertyInfo in typeof(FontStyles).GetProperties())
                    FontStyles.Add(propertyInfo.Name);
            }

            using (FontSizes.SuspendChangeNotifications())
            {
                for (var i = MinFontSize; i < MaxFontSize; i = i + FontSizeStep)
                    FontSizes.Add($"{i}");
            }
        }
        #endregion
    }
}