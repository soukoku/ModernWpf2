using ModernWpf.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ModernWpf
{
    // Contains attached properties for text-related things.

    static partial class UIHooks
    {
        static double __threshold = 14;

        /// <summary>
        /// Gets or sets the font size threshold for <see cref="AutoCrispProperty"/> option.
        /// </summary>
        /// <value>
        /// The font size threshold.
        /// </value>
        public static double FontSizeThreshold { get { return __threshold; } set { __threshold = value; } }

        /// <summary>
        /// Gets the AutoCrispProperty value.
        /// </summary>
        /// <param name="textBlock">The text block.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">textBlock</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static bool GetAutoCrisp(TextBlock textBlock)
        {
            if (textBlock == null) { throw new ArgumentNullException("textBlock"); }
            return (bool)textBlock.GetValue(AutoCrispProperty);
        }
        /// <summary>
        /// Gets the AutoCrispProperty value.
        /// </summary>
        /// <param name="textBlock">The text block.</param>
        /// <param name="value">if set to <c>true</c> then auto crisp is turned on.</param>
        /// <exception cref="ArgumentNullException">textBlock</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetAutoCrisp(TextBlock textBlock, bool value)
        {
            if (textBlock == null) { throw new ArgumentNullException("textBlock"); }
            textBlock.SetValue(AutoCrispProperty, value);
        }

        /// <summary>
        /// Dependency property that automatically sets the TextOptions.TextFormattingMode when set on
        /// a <see cref="TextBlock"/>. 
        /// </summary>
        public static readonly DependencyProperty AutoCrispProperty =
            DependencyProperty.RegisterAttached("AutoCrisp", typeof(bool), typeof(UIHooks), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(AutoCrisp_Changed)));

        private static void AutoCrisp_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // automatically sets TextOptions.TextFormattingMode based on current DPI and font size.
            var tb = d as TextBlock;
            if (tb != null)
            {
                var oldVal = (bool)e.OldValue;
                var newVal = (bool)e.NewValue;
                if (oldVal != newVal)
                {
                    if ((bool)e.NewValue)
                    {
                        //Debug.WriteLine("Hooking up AutoCrisp for text {0}", (object)tb.Text);

                        var bind = new MultiBinding();
                        bind.Converter = TextFormattingModeConverter.Instance;
                        // actual binding used by converter
                        bind.Bindings.Add(new Binding { Source = tb });
                        // hack to listen to dpi change
                        //bind.Bindings.Add(new Binding { Source = tb, Path = new PropertyPath(UIHooks.WindowDpiProperty) });
                        // hack to listen to font size change
                        bind.Bindings.Add(new Binding(TextBlock.FontSizeProperty.Name) { Source = tb });
                        BindingOperations.SetBinding(tb, TextOptions.TextFormattingModeProperty, bind);
                    }
                    else
                    {
                        BindingOperations.ClearBinding(tb, TextOptions.TextFormattingModeProperty);
                    }
                }
            }
        }

    }
}
