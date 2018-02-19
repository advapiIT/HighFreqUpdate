using System;
using HighFreqUpdate.Interfaces;

namespace HighFreqUpdate.Models
{
    public class DealSpotVisual : DealVisualWithSettlementBase, ICheckable
    {
        private double? qtaDiv1;
        public override string Type => "FxSpot";
        public DateTime? ValutaSpot { get; set; }
        public string Sign { get; set; }

        public double? QtaDiv1 { get; set; }


        public double? Cambio { get; set; }
        public double? QtaDiv2 { get; set; }

        public string CurInSettlInfo { get; set; }
        public string CurOutSettlInfo { get; set; }

        public int? DealRevertedId { get; set; }

        public bool IsChecked { get; set; }
    }
}
