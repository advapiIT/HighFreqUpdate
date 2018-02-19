using System.Collections.Generic;
using System.Threading.Tasks;
using Catel.MVVM;
using DockingInfragistics.Helpers;
using DockingInfragistics.Interfaces;
using Newtonsoft.Json;

namespace DockingInfragistics.ViewModels
{
    public class DummyViewModel : ViewModelBase, IStateAware
    {
        public  string Value { get; set; }

        public override string Title { get; protected set; } 

        public string PersistenceBag
        {
            get
            {
                var dictionary = new Dictionary<string, object> {{"VM", this.GetType().FullName}, {"Value", Value}};

                return StringBase64Helper.Base64Encode(JsonConvert.SerializeObject(dictionary));
            }
        }

        public IDictionary<string, object> SavedPersistenceBag
        {
            set => savedPersistenceBag = value;
        }

        private IDictionary<string, object> savedPersistenceBag;

        public void LoadState(IDictionary<string, object> state)
        {
            Value = (string)state["Value"];
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            if (savedPersistenceBag != null)
            {
                Value = (string) savedPersistenceBag["Value"];
            }

            Title = "test 123";
        }
    }
}
