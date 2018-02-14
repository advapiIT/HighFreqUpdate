using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;
using Catel;
using Catel.Collections;
using Catel.MVVM;
using Catel.Services;
using FizzWare.NBuilder;
using HighFreqUpdate.Models;
using Infragistics.Persistence;
using Infragistics.Persistence.Primitives;
using Infragistics.Windows.DataPresenter;
using ServiceStack;


namespace HighFreqUpdate.ViewModels
{
    public class SampleViewModel : ViewModelBase
    {
        public FastObservableCollection<DealSpotVisual> DataItems { get; set; }
        private IDictionary<int, DealSpotVisual> mappingDummyItems { get; set; }

        public TaskCommand<XamDataGrid> SaveLayoutCommand { get; private set; }
        public TaskCommand<XamDataGrid> LoadLayoutCommand { get; private set; }

        private IDispatcherService dispatcherService;
        private object mappingDummyItemsLock = new object();
        private IDisposable IsGenerating()
        {
            return new DisposableToken<SampleViewModel>(this,
                x => x.Instance.IsBusy = true,
                x => x.Instance.IsBusy = false);
        }

        private bool IsBusy { get; set; }

        private bool hasToUpdate;
        private object hasToUpdateLock = new object();


        private Random random = new Random();


        private ConcurrentQueue<DealSpotVisual> queue = new ConcurrentQueue<DealSpotVisual>();
        private Timer timer;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private const double dispatcherInterval = 250;

        public SampleViewModel(IDispatcherService dispatcherService)
        {
            this.dispatcherService = dispatcherService;

            SaveLayoutCommand = new TaskCommand<XamDataGrid>(OnSaveCommandExecute);
            LoadLayoutCommand = new TaskCommand<XamDataGrid>(OnLoadCommandExecute);

            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(dispatcherInterval);
            dispatcherTimer.Tick += DispatcherTimer_Tick1;
            timer = new Timer(5000);
            timer.Elapsed += Timer_Elapsed;

            timer.Start();

            DataItems = new FastObservableCollection<DealSpotVisual>();

            lock (mappingDummyItemsLock)
            {
                mappingDummyItems = DataItems.ToDictionary(x => x.Id, x => x);
            }

        }

        private Task OnLoadCommandExecute(XamDataGrid grid)
        {

            timer.Stop();
            dispatcherTimer.Stop();

            var str = File.ReadAllText("c:\\temp\\prova123.xml");

            byte[] bytes = Encoding.UTF8.GetBytes(str);

            return dispatcherService.InvokeAsync(() =>
            {
                using (var memoryStream = new MemoryStream(bytes))
                {
                    grid.LoadCustomizations(memoryStream);
                    timer.Start();
                    dispatcherTimer.Start();
                }
            });
        }




        private Task OnSaveCommandExecute(XamDataGrid grid)
        {

            timer.Stop();
            dispatcherTimer.Stop();

            return dispatcherService.InvokeAsync(() =>
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    grid.SaveCustomizations(memoryStream);

                    byte[] bytes = memoryStream.ToArray();

                    var str = Encoding.UTF8.GetString(bytes);

                    File.WriteAllText("c:\\temp\\prova123.xml", str);

                    timer.Start();
                    dispatcherTimer.Start();

                }
            });
        }


        private void DispatcherTimer_Tick1(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();

            if (hasToUpdate)
            {
                using (DataItems.SuspendChangeNotifications())
                {
                    while (queue.TryDequeue(out DealSpotVisual item))
                    {
                        if (mappingDummyItems.ContainsKey(item.Id))
                        {
                            mappingDummyItems[item.Id].PopulateWith(item);
                            mappingDummyItems[item.Id].IsChanged = true;
                        }
                        else
                        {
                            DataItems.Add(item);

                            lock (mappingDummyItemsLock)
                            {
                                mappingDummyItems = DataItems.ToDictionary(x => x.Id, x => x);
                            }
                        }
                    }
                }
            }
        }

        private void Timer_Elapsed(object sender, EventArgs e)
        {
            if (IsBusy) return;


            using (IsGenerating())
            {
                var priceGenerator = new RandomGenerator();
                var itemsToGenerate = random.Next(0, 20);

                for (int i = 0; i < itemsToGenerate; i++)
                {
                    var dummyItem = Builder<DealSpotVisual>.CreateNew()
                        .With(x => x.IdStatus = priceGenerator.Next(0, 4)).Build();

                    var index = random.Next(-20, 20);

                    dummyItem.Id = Math.Abs(index);

                    var cross = random.Next(1, 3);

                    dummyItem.IdCross = cross;
                    //dummyItem.Value = index * DateTime.Now.Ticks;

                    queue.Enqueue(dummyItem);
                }

                lock (hasToUpdateLock)
                {
                    hasToUpdate = true;
                }

                dispatcherTimer.Start();
            }
        }
    }
}


