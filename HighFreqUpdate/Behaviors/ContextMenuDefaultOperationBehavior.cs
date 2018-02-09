using HighFreqUpdate.Helpers;
using Infragistics.Controls.Menus;
using Infragistics.Windows.DataPresenter;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace HighFreqUpdate.Behaviors
{
    public class ContextMenuDefaultOperationBehavior : Behavior<XamDataGrid>
    {
        private static string ForceKey = "force";

        #region Variables

        private IEnumerable<FrameworkElement> headerMenuItems;
        private IEnumerable<FrameworkElement> rowMenuItems;
        private bool isFirstLoad = true;

        #endregion

        #region Properties

        public bool ShowSaveSettings { get; set; } = true;
        public bool ShowManageSettings { get; set; } = false;

        #endregion

        #region Override

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += OnMenuItemLoaded;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            var contextMenu = Infragistics.Controls.Menus.ContextMenuService.GetManager(AssociatedObject);

            if (contextMenu != null)
                contextMenu.ContextMenu.Opening += ContextMenu_Opening;

            AssociatedObject.Loaded -= OnMenuItemLoaded;
        }

        private void ContextMenu_Opening(object sender, OpeningEventArgs e)
        {
            var contextMenuManager = Infragistics.Controls.Menus.ContextMenuService.GetManager(AssociatedObject);
            var contextMenu = contextMenuManager.ContextMenu;

            var row = e.GetClickedElements<DataRecordPresenter>();

            if (row.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            var x = e.GetClickedElements<DataRecordPresenter>()[0].Record;

            if (x is HeaderRecord)
            {
                foreach (var item in headerMenuItems.OfType<XamMenuItem>())
                {
                    item.Header = string.Format(item.Header.ToString(), x.Description);
                    //    headerCell.Content != null ? ((TextBlock)headerCell.Content).Text : "");
                    item.CommandParameter = new HeaderCommandParameter
                    {
                        Grid = this.AssociatedObject,
                        Column = x.Description,
                        ViewModel = ContextMenuHelper.GetViewModelName(AssociatedObject)
                    };
                }

                contextMenu.ItemsSource = headerMenuItems;
            }
            else if (row != null)
            {
                contextMenu.ItemsSource = rowMenuItems;
            }
            else
            {
                var lst = headerMenuItems.OfType<FrameworkElement>().Where(y => (string)y.Tag == ForceKey).ToList();

                if (rowMenuItems != null)
                {
                    if (lst.Any())
                        lst.Add(new XamMenuSeparator());

                    var items = rowMenuItems.Where(y => x.Tag != null && (string)y.Tag == ForceKey).ToList();

                    if (items.Any())
                    {
                        lst.AddRange(items);
                    }

                    if (lst.Any())
                    {
                        contextMenu.ItemsSource = lst;
                    }
                    else contextMenu.IsOpen = false;
                }
                else contextMenu.IsOpen = false;
            }


        }


        #endregion

        private void OnMenuItemLoaded(object sender, RoutedEventArgs e)
        {
            if (!isFirstLoad) return;

            var contextMenu = Infragistics.Controls.Menus.ContextMenuService.GetManager(AssociatedObject);

            if (contextMenu == null)
            {
                contextMenu = ContextMenuHelper.CreateNewContextMenuWithDefaults();
                Infragistics.Controls.Menus.ContextMenuService.SetManager(AssociatedObject, contextMenu);

            }

            ContextMenuHelper.SetDefaultValues(contextMenu.ContextMenu);

            ////In the startup phase I preapare the radmnenuItem and the row-context item

            headerMenuItems = GetGenericHeaderMenuItems();
            rowMenuItems = GetRowMenuItems(contextMenu.ContextMenu);

            contextMenu.ContextMenu.Opening += ContextMenu_Opening;

            isFirstLoad = false;
        }

        private IEnumerable<FrameworkElement> GetGenericHeaderMenuItems()
        {
            var lst = new List<FrameworkElement>();

            if (ShowSaveSettings)
            {
                XamMenuItem saveSettings = new XamMenuItem
                {
                    Tag = ForceKey,
                    Header = "Save",
                    Command = DefaultRadGridContextMenuCommands.SaveSettingsCommand,
                    CommandParameter = AssociatedObject,
                };

                lst.Add(saveSettings);

                XamMenuItem loadSettings = new XamMenuItem
                {
                    Tag = ForceKey,
                    Header = "Load",
                    Command = DefaultRadGridContextMenuCommands.LoadSettingsCommand,
                    CommandParameter = AssociatedObject,

                };

                lst.Add(loadSettings);

                XamMenuItem setDefault = new XamMenuItem
                {
                    Tag = ForceKey,
                    Header = "Set default",
                    Command = DefaultRadGridContextMenuCommands.SetDefaultCommand,
                    CommandParameter = AssociatedObject,

                };

                lst.Add(setDefault);


                XamMenuItem clearSettings = new XamMenuItem
                {
                    /*  Tag = "Clear",*/
                    Header = "Clear settings",
                    Command = DefaultRadGridContextMenuCommands.ClearSettingsCommand,
                    CommandParameter = AssociatedObject,

                };

                lst.Add(clearSettings);

                lst.Add(new XamMenuSeparator());
            }

            XamMenuItem sortAscItem = new XamMenuItem
            {
                /* Tag = "SortAscending",*/
                Header = "Ordina A - Z per \"{0}\"",
                Command = DefaultRadGridContextMenuCommands.SortAscendingCommand,


            };

            lst.Add(sortAscItem);

            XamMenuItem sortDescItem = new XamMenuItem
            {
                /* Tag = "SortDescending",*/
                Header = "Ordina Z - A per \"{0}\"",
                Command = DefaultRadGridContextMenuCommands.SortDescendingCommand,

            };

            lst.Add(sortDescItem);

            XamMenuItem clearSortItem = new XamMenuItem
            {
                /*Tag = "ClearSorting",*/
                Header = "Cancella ordinamento per \"{0}\"",
                Command = DefaultRadGridContextMenuCommands.SortClearingCommand,

            };

            lst.Add(new XamMenuSeparator());

            lst.Add(clearSortItem);

            XamMenuItem groupItem = new XamMenuItem
            {
                /* Tag = "Groupby",*/
                Header = "Raggruppa per \"{0}\"",
                Command = DefaultRadGridContextMenuCommands.GroupByCommand,

            };


            lst.Add(groupItem);

            XamMenuItem ungroupItem = new XamMenuItem
            {
                /* Tag = "Ungroup",*/
                Header = "Ungroup",
                Command = DefaultRadGridContextMenuCommands.UnGroupByCommand,

            };


            lst.Add(ungroupItem);

            lst.Add(new XamMenuSeparator());
            XamMenuItem colVisibleItem = new XamMenuItem
            {
                /* Tag = "colVisibleItem",*/
                Header = "Visible columns",

            };


            lst.Add(colVisibleItem);

            XamDataGrid grid = AssociatedObject;

            // create menu items
            foreach (Field column in grid.FieldLayouts[0].Fields)
            {
                XamMenuItem subMenu = new XamMenuItem();
                subMenu.Header = column.Name;
                subMenu.IsCheckable = true;
                subMenu.IsChecked = true;
                subMenu.StaysOpenOnClick = true;

                Binding isCheckedBinding = new Binding("IsVisible");
                isCheckedBinding.Mode = BindingMode.TwoWay;
                isCheckedBinding.Source = column;

                // bind IsChecked menu item property to IsVisible column property
                subMenu.SetBinding(XamMenuItem.IsCheckedProperty, isCheckedBinding);
                colVisibleItem.Items.Add(subMenu);
            }

            lst.Add(new XamMenuSeparator());
            XamMenuItem colDelFilterItem = new XamMenuItem
            {
                /*   Tag = "DeleteFilters",*/
                Header = "Delete filters",
                Command = DefaultRadGridContextMenuCommands.DeleteFiltersCommand,

            };

            lst.Add(colDelFilterItem);

            if (ShowManageSettings)
            {
                var menuItem2 = new XamMenuItem
                {
                    Header = "Colore",
                    Command = DefaultRadGridContextMenuCommands.ChangeColorCommand,

                };

                lst.Add(menuItem2);

                var menuItem1 = new XamMenuItem
                {
                    Header = "Font",
                    Command = DefaultRadGridContextMenuCommands.ChangeFontCommand,

                };

                lst.Add(menuItem1);
            }

            return lst;
        }


        private IEnumerable<FrameworkElement> GetRowMenuItems(XamContextMenu contextMenu)
        {
            var rowItems = new List<FrameworkElement>();

            bool areThereSpecificContextItems = contextMenu.Items.Count > 0;

            var menuItem = new XamMenuItem { Header = "Export" };

            menuItem.Command = DefaultRadGridContextMenuCommands.ExportGridCommand;
            menuItem.CommandParameter = AssociatedObject;


            rowItems.Add(menuItem);

            if (areThereSpecificContextItems)
            {
                rowItems.Add(new XamMenuSeparator());

                rowItems.AddRange(contextMenu.Items.Cast<FrameworkElement>());
            }

            contextMenu.Items.Clear();

            return rowItems;
        }
    }
}
