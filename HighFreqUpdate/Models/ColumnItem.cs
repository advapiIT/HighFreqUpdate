using System;
using System.Windows.Media;

namespace HighFreqUpdate.Models
{
    public class ColumnItem
    {
        public string Name { get; set; }
        public string ColumnUniqueName { get; set; }
        public Type Type { get; set; }
        public Color? ForeColor { get; set; }
        public Color? BackColor { get; set; }
        public bool IsChangedColor { get; set; }

        public int Decimals { get; set; }
        public bool EnableThousandSeparator { get; set; }
        public bool EnableCheckThousandSeparator { get; set; }
        public int? Format { get; set; }
        public int Align { get; set; }
        public bool EnableNotSignificantZero { get; set; }
        public bool IsBlinking { get; set; }
        public Color BlinkColor { get; set; }
        public bool CanBlink { get; set; }
        public bool CanChangeBlinkColor { get; set; }
        public bool IsForeColorNegativeEnabled { get; set; }
        public Color ForeColorNegative { get; set; }
        public int BlinkingFrequency { get; set; }
        public bool EnableAggregator { get; set; }
        public int Aggregator { get; set; }
        public bool EnableLockColumn { get; set; }
        public bool EnableFreezeColumn { get; set; }

        public bool CanChangeBlinkFeature => CanBlink && CanChangeBlinkColor;
    }
}