using System;
using System.Data;
using System.Windows.Media.Animation;
using Catel.Windows;
using Infragistics.Windows.DataPresenter.Events;

namespace HighFreqUpdate.Views
{
    /// <summary>
    /// Interaction logic for SampleView.xaml
    /// </summary>
    public partial class SampleView :DataWindow
    {
        public SampleView()
        {
            InitializeComponent();
        }


        //private void Timeline_OnCompleted(object sender, EventArgs e)
        //{
        //    ColorAnimation c = sender as ColorAnimation;
        //    ;

            
         
        //}

        private void Storyboard_OnCompleted(object sender, EventArgs e)
        {
           
        }
    }
}
