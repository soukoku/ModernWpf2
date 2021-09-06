using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ModernWpf.Converters
{
    class TextFormattingModeConverter : IMultiValueConverter
    {
        static readonly TextFormattingModeConverter _instance = new TextFormattingModeConverter();

        /// <summary>
        /// Gets the singleton instance for this converter.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static TextFormattingModeConverter Instance { get { return _instance; } }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var mode = TextFormattingMode.Ideal;
            if (values != null)
            {
                foreach (var v in values)
                {
                    // only the TextBlock is useful
                    var tb = v as TextBlock;
                    if (tb != null)
                    {
                        var dpi = VisualTreeHelper.GetDpi(tb).PixelsPerDip;// UIHooks.GetWindowDpi(tb);
                        if (dpi <= 96 && tb.FontSize <= UIHooks.FontSizeThreshold)
                        {
                            mode = TextFormattingMode.Display;
                        }
                        //Debug.WriteLine("DPI={0}, using {1} mode for text {2}", dpi, mode, tb.Text);
                        break;
                    }
                }
            }
            return mode;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
