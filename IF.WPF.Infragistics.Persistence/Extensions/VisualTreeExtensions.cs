using Catel;
using Catel.IoC;
using Catel.MVVM;
using Catel.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace IF.WPF.Infragistics.Persistence.Extensions
{
    public static class VisualTreeExtensions
    {
        /// <summary>
        /// Gets all child elements recursively from the visual tree by given type.
        /// </summary>
        public static IEnumerable<T> ChildrenOfType<T>(this DependencyObject element) where T : DependencyObject
        {
            return element.GetChildrenRecursive().OfType<T>();
        }

        /// <summary>
        /// Finds child element of the specified type. Uses breadth-first search.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the child that will be searched in the object hierarchy. The type should be <see cref="T:System.Windows.DependencyObject" />.
        /// </typeparam>
        /// <param name="element">The target <see cref="T:System.Windows.DependencyObject" /> which children will be traversed.</param>
        /// <returns>The first child element that is of the specified type.</returns>
        public static T FindChildByType<T>(this DependencyObject element) where T : DependencyObject
        {
            return element.ChildrenOfType<T>().FirstOrDefault<T>();
        }

        internal static IEnumerable<T> FindChildrenByType<T>(this DependencyObject element)
            where T : DependencyObject
        {
            return element.ChildrenOfType<T>();
        }

        internal static FrameworkElement GetChildByName(this FrameworkElement element, string name)
        {
            return (FrameworkElement)element.FindName(name) ?? element.ChildrenOfType<FrameworkElement>()
                       .FirstOrDefault<FrameworkElement>((Func<FrameworkElement, bool>)(c => c.Name == name));
        }

        /// <summary>
        /// Does a deep search of the element tree, trying to find a descendant of the given type
        /// (including the element itself).
        /// </summary>
        /// <returns>True if the target is one of the elements.</returns>
        internal static T GetFirstDescendantOfType<T>(this DependencyObject target) where T : DependencyObject
        {
            T obj = target as T;
            if ((object)obj != null)
                return obj;
            return target.ChildrenOfType<T>().FirstOrDefault<T>();
        }

        internal static IEnumerable<T> GetChildren<T>(this DependencyObject parent) where T : FrameworkElement
        {
            return parent.GetChildrenRecursive().OfType<T>();
        }

        /// <summary>
        /// Enumerates through element's children in the visual tree.
        /// </summary>
        private static IEnumerable<DependencyObject> GetChildrenRecursive(this DependencyObject element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(element, i);
                if (child != null)
                {
                    yield return child;
                    foreach (DependencyObject dependencyObject in child.GetChildrenRecursive())
                        yield return dependencyObject;
                }
            }
        }

        internal static IEnumerable<T> ChildrenOfType<T>(this DependencyObject element,
            Type typeWhichChildrenShouldBeSkipped)
        {
            return element.GetChildrenOfType(typeWhichChildrenShouldBeSkipped).OfType<T>();
        }

        private static IEnumerable<DependencyObject> GetChildrenOfType(this DependencyObject element,
            Type typeWhichChildrenShouldBeSkipped)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(element, i);
                yield return child;
                if (!typeWhichChildrenShouldBeSkipped.IsAssignableFrom(child.GetType()))
                {
                    foreach (DependencyObject dependencyObject in child.GetChildrenOfType(
                        typeWhichChildrenShouldBeSkipped))
                        yield return dependencyObject;
                }
            }
        }

        /// <summary>

        /// Gets the parent element from the visual tree by given type.

        /// </summary>

        public static T ParentOfType<T>(this DependencyObject element) where T : DependencyObject

        {

            if (element == null)

                return default(T);

            return element.GetParents().OfType<T>().FirstOrDefault<T>();

        }



        /// <summary>

        ///  Determines whether the element is an ancestor of the descendant.

        /// </summary>

        /// <returns>true if the visual object is an ancestor of descendant; otherwise, false.</returns>

        public static bool IsAncestorOf(this DependencyObject element, DependencyObject descendant)
        {
            if (descendant != element)

                return descendant.GetParents().Contains<DependencyObject>(element);

            return true;
        }



        /// <summary>

        /// Searches up in the visual tree for parent element of the specified type.

        /// </summary>

        /// <typeparam name="T">

        /// The type of the parent that will be searched up in the visual object hierarchy.

        /// The type should be <see cref="T:System.Windows.DependencyObject" />.

        /// </typeparam>

        /// <param name="element">The target <see cref="T:System.Windows.DependencyObject" /> which visual parents will be traversed.</param>

        /// <returns>Visual parent of the specified type if there is any, otherwise null.</returns>

        public static T GetVisualParent<T>(this DependencyObject element) where T : DependencyObject

        {

            return element.ParentOfType<T>();

        }



        /// <summary>

        /// This recurse the visual tree for ancestors of a specific type.

        /// </summary>

        internal static IEnumerable<T> GetAncestors<T>(this DependencyObject element) where T : class

        {

            return element.GetParents().OfType<T>();

        }



        /// <summary>

        /// This recurse the visual tree for a parent of a specific type.

        /// </summary>

        internal static T GetParent<T>(this DependencyObject element) where T : FrameworkElement

        {

            return element.ParentOfType<T>();

        }



        /// <summary>

        /// Enumerates through element's parents in the visual tree.

        /// </summary>

        public static IEnumerable<DependencyObject> GetParents(this DependencyObject element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            while ((element = element.GetParent()) != null)
                yield return element;
        }

        private static DependencyObject GetParent(this DependencyObject element)
        {
            DependencyObject dependencyObject;
            try
            {
                dependencyObject = VisualTreeHelper.GetParent(element);
            }
            catch (InvalidOperationException ex)
            {
                dependencyObject = null;
            }

            if (dependencyObject == null)
            {
                FrameworkElement frameworkElement = element as FrameworkElement;

                if (frameworkElement != null)
                    dependencyObject = frameworkElement.Parent;

                FrameworkContentElement frameworkContentElement = element as FrameworkContentElement;

                if (frameworkContentElement != null)
                    dependencyObject = frameworkContentElement.Parent;
            }
            return dependencyObject;
        }



        public static T GetUIElement<T>(this IViewModel viewModel, string name = "")
            where T : UIElement
        {
            Argument.IsNotNull(() => viewModel);
            var viewManager = ServiceLocator.Default.ResolveType<IViewManager>();
            var views = viewManager.GetViewsOfViewModel(viewModel);


            var view = views[0] as FrameworkElement;
            T result = !string.IsNullOrEmpty(name) ? view?.FindName(name) as T : view.FindChildByType<T>();
            return result;
        }
    }

}
