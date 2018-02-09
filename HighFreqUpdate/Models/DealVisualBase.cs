using Catel.Data;
using Catel.Windows.Threading;
using HighFreqUpdate.Interfaces;
using System;

namespace HighFreqUpdate.Models
{
    public class DealVisualBase : ModelBase, IStatus
    {
        public int Id { get; set; }

        public virtual string Type => string.Empty;
        public string Institute { get; set; }
        public DateTime Data { get; set; }
        public DateTime Ora { get; set; }
        public string Trader { get; set; }
        public int IdPortfolio { get; set; }
        public string Portfolio { get; set; }
        public int IdCtp { get; set; }
        public string CounterpartCode { get; set; }
        public string Counterpart { get; set; }
        public bool IsCounterpartSkipSettlement { get; set; }

        #region Instrument

        public int? IdCross { get; set; }
        public string InstrumentCross { get; set; }
        public string InstrumentCurrency1Description { get; set; }
        public string InstrumentCurrency2Description { get; set; }
        public string InstrumentCurrencyDescription { get; set; }

        #endregion

        #region Alias

        public string AliasSwift { get; set; }
        public string AliasAbi { get; set; }
        public string AliasCed { get; set; }

        #endregion

        #region Split

        public int? SplitCurrency { get; set; }
        public string SplitDescription { get; set; }
        public double? SplitFx { get; set; }

        #endregion

        public string Provenance { get; set; }

        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime? InsertDate { get; set; }

        //public bool IsChanged { get; set; }
        private bool isChanged;

        public bool IsChanged
        {
            get { return isChanged; }
            set
            {
                if (value)
                {
                    isChanged = value;

                    RaisePropertyChanged(() => IsChanged);
                }
                else
                {
                    isChanged = value;
                    RaisePropertyChanged(() => IsChanged);
                }
            }
        }

        //public void UpdateIsChangedStatus()
        //{
        //    DispatcherHelper.CurrentDispatcher.Invoke(() => IsChanged = true);
        //    ////DispatcherHelper.CurrentDispatcher.BeginInvokeIfRequired(() =>
        //    ////{
        //    ////       IsChanged = false;
        //    ////    IsChanged = true;
        //    ////});
        //}

        private int idStatus;

        public int IdStatus
        {
            get => idStatus;
            set
            {
                if (value != idStatus)
                {
                    idStatus = value;

                    RaisePropertyChanged(() => IdStatus);
                }
            }
        }

    }

    public class DealVisualWithSettlementBase : DealVisualBase
    {
        #region Settlements

        public int? SettlementInstructions_ID_Tesoriera_IN_OUR_Leg0 { get; set; }
        public string SettlementInstructions_Tesoriera_IN_OUR_Leg0 { get; set; }
        public string SettlementInstructions_Tesoriera_IN_OUR_Leg0_Description { get; set; }
        public string SettlementInstructions_Tesoriera_IN_OUR_Leg0_SWIFT { get; set; }
        public string SettlementInstructions_Tesoriera_IBAN_IN_OUR_Leg0 { get; set; }

        public int? SettlementInstructions_ID_Tesoriera_OUT_OUR_Leg0 { get; set; }
        public string SettlementInstructions_Tesoriera_OUT_OUR_Leg0 { get; set; }
        public string SettlementInstructions_Tesoriera_OUT_OUR_Leg0_Description { get; set; }
        public string SettlementInstructions_Tesoriera_OUT_OUR_Leg0_SWIFT { get; set; }
        public string SettlementInstructions_Tesoriera_IBAN_OUT_OUR_Leg0 { get; set; }

        public int? SettlementInstructions_ID_Tesoriera_OUT_THEIR_Leg0 { get; set; }
        public string SettlementInstructions_Tesoriera_OUT_THEIR_Leg0 { get; set; }
        public string SettlementInstructions_Tesoriera_OUT_THEIR_Leg0_Description { get; set; }
        public string SettlementInstructions_Tesoriera_OUT_THEIR_Leg0_SWIFT { get; set; }
        public string SettlementInstructions_Tesoriera_IBAN_OUT_THEIR_Leg0 { get; set; }

        #endregion
    }
}


