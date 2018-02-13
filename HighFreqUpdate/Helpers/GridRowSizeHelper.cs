namespace HighFreqUpdate.Helpers
{
    public static class GridRowSizeHelper
    {
        private const int DefaultRowHeight = 25;
        private const double DefaultFontSize = 12;
        private static double Ratio = DefaultRowHeight / DefaultFontSize;

        public static double GetRowHeightFromFontSize(double fontSize)
        {
            if (fontSize == DefaultFontSize) return DefaultRowHeight;

            return fontSize * Ratio;
        }
    }
}