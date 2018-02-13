using HighFreqUpdate.Helpers;
using Infragistics.Controls.Menus;
using Infragistics.Windows.DataPresenter;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interactivity;
using Infragistics.Windows.Controls;
using System.Text.RegularExpressions;

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

            var contextMenu = ContextMenuService.GetManager(AssociatedObject);

            if (contextMenu != null)
                contextMenu.ContextMenu.Opening += ContextMenu_Opening;

            AssociatedObject.Loaded -= OnMenuItemLoaded;
        }

        private void ContextMenu_Opening(object sender, OpeningEventArgs e)
        {
            var contextMenuManager = Infragistics.Controls.Menus.ContextMenuService.GetManager(AssociatedObject);
            var contextMenu = contextMenuManager.ContextMenu;

            List<CellValuePresenter> rows = e.GetClickedElements<CellValuePresenter>();
            List<LabelPresenter> rowsHeader = e.GetClickedElements<LabelPresenter>();

            if (rowsHeader != null && rowsHeader.Any())
            {
                var row = rowsHeader.First();

                foreach (var item in headerMenuItems.OfType<XamMenuItem>())
                {
                    item.Header = new Regex(@"([""'])(\\?.)*?\1").Replace(item.Header.ToString(), $"\"{row.Content.ToString()}\"");
                    item.CommandParameter = new HeaderCommandParameter
                    {
                        Grid = AssociatedObject,
                        Column = row.Content.ToString(),
                        ViewModel = ContextMenuHelper.GetViewModelName(AssociatedObject)
                    };
                }

                contextMenu.ItemsSource = headerMenuItems;
            }
            else if (rows != null && rows.Any())
            {
                contextMenu.ItemsSource = rowMenuItems;
            }
            else
            {
                var lst = headerMenuItems.Where(y => (string)y.Tag == ForceKey).ToList();

                if (rowMenuItems != null)
                {
                    if (lst.Any())
                        lst.Add(new XamMenuSeparator());

                    var items = rowMenuItems.Where(y => y.Tag != null && (string)y.Tag == ForceKey).ToList();

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

            var contextMenu = ContextMenuService.GetManager(AssociatedObject);

            if (contextMenu == null)
            {
                contextMenu = ContextMenuHelper.CreateNewContextMenuWithDefaults();
                ContextMenuService.SetManager(AssociatedObject, contextMenu);
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
                    Header = "Imposta come layout di default",
                    Command = DefaultRadGridContextMenuCommands.SetDefaultCommand,
                    CommandParameter = AssociatedObject,
                };

                lst.Add(setDefault);

                XamMenuItem clearSettings = new XamMenuItem
                {
                    Header = "Ripristina layout della griglia",
                    Command = DefaultRadGridContextMenuCommands.ClearSettingsCommand,
                    CommandParameter = AssociatedObject,
                };

                lst.Add(clearSettings);

                lst.Add(new XamMenuSeparator());
            }

            XamMenuItem sortAscItem = new XamMenuItem
            {
                Header = "Ordina A - Z per \"{0}\"",
                Command = DefaultRadGridContextMenuCommands.SortAscendingCommand
            };

            lst.Add(sortAscItem);

            XamMenuItem sortDescItem = new XamMenuItem
            {
                Header = "Ordina Z - A per \"{0}\"",
                Command = DefaultRadGridContextMenuCommands.SortDescendingCommand
            };

            lst.Add(sortDescItem);

            XamMenuItem clearSortItem = new XamMenuItem
            {
                Header = "Cancella ordinamento per \"{0}\"",
                Command = DefaultRadGridContextMenuCommands.SortClearingCommand
            };

            lst.Add(new XamMenuSeparator());

            lst.Add(clearSortItem);

            XamMenuItem groupItem = new XamMenuItem
            {
                Header = "Raggruppa per \"{0}\"",
                Command = DefaultRadGridContextMenuCommands.GroupByCommand
            };

            lst.Add(groupItem);

            XamMenuItem ungroupItem = new XamMenuItem
            {
                Header = "Cancella reggruppamento per \"{0}\"",
                Command = DefaultRadGridContextMenuCommands.UnGroupByCommand
            };

            lst.Add(ungroupItem);

            lst.Add(new XamMenuSeparator());
            XamMenuItem colVisibleItem = new XamMenuItem
            {
                Header = "Colonne visibili : "
            };

            lst.Add(colVisibleItem);

            XamDataGrid grid = AssociatedObject;

            foreach (Field column in grid.FieldLayouts[0].Fields)
            {
                XamMenuItem subMenu = new XamMenuItem
                {
                    Header = column.Name,
                    IsCheckable = true,
                    IsChecked = true,
                    StaysOpenOnClick = true
                };

                Binding isCheckedBinding = new Binding("Visibility")
                {
                    Mode = BindingMode.TwoWay,
                    Source = column,
                    Converter = new VisibilityToBooleanConverter()
                };

                // bind IsChecked menu item property to IsVisible column property
                subMenu.SetBinding(XamMenuItem.IsCheckedProperty, isCheckedBinding);
                colVisibleItem.Items.Add(subMenu);
            }

            lst.Add(new XamMenuSeparator());
            XamMenuItem colDelFilterItem = new XamMenuItem
            {
                Header = "Elimina filtri",
                Command = DefaultRadGridContextMenuCommands.DeleteFiltersCommand
            };

            lst.Add(colDelFilterItem);

            if (ShowManageSettings)
            {
                var menuItem2 = new XamMenuItem
                {
                    Header = "Gestione colori",
                    Command = DefaultRadGridContextMenuCommands.ChangeColorCommand
                };

                lst.Add(menuItem2);

                var menuItem1 = new XamMenuItem
                {
                    Header = "Gestione font",
                    Command = DefaultRadGridContextMenuCommands.ChangeFontCommand
                };

                lst.Add(menuItem1);
            }

            return lst;
        }

        private IEnumerable<FrameworkElement> GetRowMenuItems(XamContextMenu contextMenu)
        {
            var rowItems = new List<FrameworkElement>();

            bool areThereSpecificContextItems = contextMenu.Items.Count > 0;

            var menuItem = new XamMenuItem
            {
                Header = "Export",
                Command = DefaultRadGridContextMenuCommands.ExportGridCommand,
                CommandParameter = AssociatedObject
            };

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