using System;
using System.Collections.Generic;
using System.Windows;

namespace HighFreqUpdate.Helpers
{
    public static class GridCustomizationHelpers
    {
        public static readonly Type[] DateTimeTypes = { typeof(DateTime), typeof(DateTime?) };

        public static readonly Type[] NumericTypes = { typeof(int), typeof(int?), typeof(double), typeof(double?) };

        /// <summary>
        /// Key represents the Telerik one, Value ours
        /// </summary>
        public static Dictionary<int, int> MappingTextAligment = new Dictionary<int, int>
        {
            { 0, 0 }, //Left
            { 2, 1 }, //Center
            { 1, 2 }, //Right
            { 3, 3 }  //Right
        };

        public static Dictionary<int, int> InvertedMappingTextAligment = new Dictionary<int, int>
        {
            { 0, 0 }, //Left
            { 1, 2 }, //Center
            { 2, 1 }, //Right
            { 3, 3 }  //Right
        };

        public static readonly string TagBlinkable = "NumericBlinkable";//That's used to tell if a cell is blinkable

        public static FontStyle GetFontStyleFomString(string font)
        {
            switch (font)
            {
                case "Italic":
                    return FontStyles.Italic;

                default:
                    return FontStyles.Normal;
                case "Oblique":
                    return FontStyles.Oblique;
            }
        }
    }
}