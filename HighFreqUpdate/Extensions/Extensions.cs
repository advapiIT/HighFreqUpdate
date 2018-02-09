using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HighFreqUpdate.Extensions
{
    public static class ParentOfTypeExtensions

    {

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

                dependencyObject = (DependencyObject)null;

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

    }


}
