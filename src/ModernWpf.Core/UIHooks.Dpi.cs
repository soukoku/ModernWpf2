using ModernWpf.Native;
using ModernWpf.Native.Api;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;

namespace ModernWpf
{
    // Contains attached properties and routed event for DPI-related things.

    static partial class UIHooks
    {
        ///// <summary>
        ///// Tries to enable per-monitor high dpi support if possible (win8.1+), or general high dpi support (vista+).
        ///// This can only be set once at process startup. To use this method <see cref="System.Windows.Media.DisableDpiAwarenessAttribute"/>
        ///// must be set on the assembly.
        ///// </summary>
        ///// <returns></returns>
        //public static bool EnableHighDpiSupport()
        //{
        //    if (Shcore.IsPlatformSupported)
        //    {
        //        return Shcore.SetProcessDpiAwareness(PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE);
        //    }
        //    return User32.SetProcessDPIAware();
        //}

        //static bool? __perMonitor;
        ///// <summary>
        ///// Gets a value indicating whether this process is per-monitor dpi-aware.
        ///// </summary>
        ///// <value>
        ///// <c>true</c> if this instance is per-monitor dpi-aware; otherwise, <c>false</c>.
        ///// </value>
        //public static bool IsPerMonitorAware
        //{
        //    get
        //    {
        //        if (!__perMonitor.HasValue)
        //        {
        //            __perMonitor = Shcore.GetProcessDpiAwareness(IntPtr.Zero) == PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE;
        //        }
        //        return __perMonitor.Value;
        //    }
        //}


        ///// <summary>
        ///// Identifies the DpiChange event. This can only be listened to by a <see cref="Window"/>.
        ///// </summary>
        //public static readonly RoutedEvent DpiChangeEvent =
        //    EventManager.RegisterRoutedEvent("DpiChange", RoutingStrategy.Direct, typeof(EventHandler<DpiChangeEventArgs>), typeof(UIHooks));

        ///// <summary>
        ///// Adds a handler to the DpiChange event.
        ///// </summary>
        ///// <param name="window">The window.</param>
        ///// <param name="handler">The handler.</param>
        ///// <exception cref="ArgumentNullException">window</exception>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        //public static void AddDpiChangeHandler(Window window, EventHandler<DpiChangeEventArgs> handler)
        //{
        //    if (window == null) { throw new ArgumentNullException("window"); }
        //    window.AddHandler(DpiChangeEvent, handler);
        //}

        ///// <summary>
        ///// Removes a handler to the DpiChange event.
        ///// </summary>
        ///// <param name="window">The window.</param>
        ///// <param name="handler">The handler.</param>
        ///// <exception cref="ArgumentNullException">window</exception>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        //public static void RemoveDpiChangeHandler(Window window, EventHandler<DpiChangeEventArgs> handler)
        //{
        //    if (window == null) { throw new ArgumentNullException("window"); }
        //    window.RemoveHandler(DpiChangeEvent, handler);
        //}


        #region DPI attached prop

        ///// <summary>
        ///// Attached property on a window to store its current DPI scale value.
        ///// </summary>
        //private static readonly DependencyProperty WindowDpiScaleProperty =
        //    DependencyProperty.RegisterAttached("WindowDpiScale", typeof(double), typeof(UIHooks),
        //    new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.Inherits));

        ///// <summary>
        ///// Gets the dpi scale value for the object contained in a window using <see cref="Chrome" />.
        ///// </summary>
        ///// <param name="obj">The object.</param>
        ///// <returns></returns>
        ///// <exception cref="System.ArgumentNullException">obj</exception>
        //public static double GetWindowDpiScale(DependencyObject obj)
        //{
        //    if (obj == null) { throw new ArgumentNullException("obj"); }
        //    return (double)obj.GetValue(WindowDpiScaleProperty);
        //}

        //static void SetWindowDpiScale(DependencyObject obj, double dpiScale)
        //{
        //    obj.SetValue(WindowDpiScaleProperty, dpiScale);
        //}

        ///// <summary>
        ///// Attached property on a window to store its current DPI value.
        ///// </summary>
        //internal static readonly DependencyProperty WindowDpiProperty =
        //    DependencyProperty.RegisterAttached("WindowDpi", typeof(int), typeof(UIHooks),
        //    new FrameworkPropertyMetadata(96, FrameworkPropertyMetadataOptions.Inherits));

        ///// <summary>
        ///// Gets the dpi value for the object contained in a window using this tool.
        ///// </summary>
        ///// <param name="obj">The object.</param>
        ///// <returns></returns>
        ///// <exception cref="System.ArgumentNullException">obj</exception>
        //public static int GetWindowDpi(DependencyObject obj)
        //{
        //    if (obj == null) { throw new ArgumentNullException("obj"); }
        //    return (int)obj.GetValue(WindowDpiProperty);
        //}

        //static void SetWindowDpi(DependencyObject obj, int dpi)
        //{
        //    obj.SetValue(WindowDpiProperty, dpi);
        //}


        //// marker on a transform to indicate it's created by the UIHooks
        //static bool GetIsDpiTransform(DependencyObject obj)
        //{
        //    return (bool)obj.GetValue(IsDpiTransformProperty);
        //}
        //static void SetIsDpiTransform(DependencyObject obj, bool value)
        //{
        //    obj.SetValue(IsDpiTransformProperty, value);
        //}
        //static readonly DependencyProperty IsDpiTransformProperty =
        //    DependencyProperty.RegisterAttached("IsDpiTransform", typeof(bool), typeof(UIHooks), new FrameworkPropertyMetadata(false));



        #endregion

        #region DPI per-monitor scaling logic

        ///// <summary>
        ///// Gets the automatic dpi scale value.
        ///// </summary>
        ///// <param name="obj">The object.</param>
        ///// <returns></returns>
        ///// <exception cref="ArgumentNullException">obj</exception>
        //public static bool GetAutoDpiScale(DependencyObject obj)
        //{
        //    if (obj == null) { throw new ArgumentNullException("obj"); }
        //    return (bool)obj.GetValue(AutoDpiScaleProperty);
        //}

        ///// <summary>
        ///// Sets the automatic dpi scale value.
        ///// </summary>
        ///// <param name="obj">The object.</param>
        ///// <param name="value">if set to <c>true</c> [value].</param>
        ///// <exception cref="ArgumentNullException">obj</exception>
        //public static void SetAutoDpiScale(DependencyObject obj, bool value)
        //{
        //    if (obj == null) { throw new ArgumentNullException("obj"); }
        //    obj.SetValue(AutoDpiScaleProperty, value);
        //}


        ///// <summary>
        ///// The automatic dpi scale dependency property. Can be used on a <see cref="Window"/>, <see cref="ContextMenu"/>, or <see cref="ToolTip"/>.
        ///// </summary>
        //public static readonly DependencyProperty AutoDpiScaleProperty =
        //    DependencyProperty.RegisterAttached("AutoDpiScale", typeof(bool), typeof(UIHooks), new FrameworkPropertyMetadata(false, AutoDpiScaleChanged));

        //private static void AutoDpiScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    if (IsPerMonitorAware)
        //    {
        //        var old = (bool)e.OldValue;
        //        var hookIt = (bool)e.NewValue;

        //        if (old != hookIt)
        //        {
        //            var window = d as Window;
        //            if (window != null)
        //            {
        //                if (hookIt)
        //                {
        //                    Debug.WriteLine("AutoDPI hooked for " + window.GetType().Name);
        //                    AttachWindow(window);
        //                }
        //                else
        //                {
        //                    DetatchWindow(window);
        //                }
        //            }
        //            else
        //            {
        //                var menu = d as ContextMenu;
        //                if (menu != null)
        //                {
        //                    if (hookIt)
        //                    {
        //                        menu.Opened += ContextMenu_Opened;
        //                    }
        //                    else
        //                    {
        //                        menu.Opened -= ContextMenu_Opened;
        //                    }
        //                }
        //                else
        //                {
        //                    var tip = d as ToolTip;
        //                    if (tip != null)
        //                    {
        //                        if (hookIt)
        //                        {
        //                            tip.Opened += ToolTip_Opened;
        //                        }
        //                        else
        //                        {
        //                            tip.Opened -= ToolTip_Opened;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //private static void ContextMenu_Opened(object sender, RoutedEventArgs e)
        //{
        //    var cm = sender as ContextMenu;
        //    if (cm != null && cm.PlacementTarget != null)
        //    {
        //        var scale = GetWindowDpiScale(cm.PlacementTarget);
        //        ScaleElement(cm, scale);
        //        Debug.WriteLine("ContextMenu scale = " + scale);
        //    }
        //}

        //private static void ToolTip_Opened(object sender, RoutedEventArgs e)
        //{
        //    var cm = sender as ToolTip;
        //    if (cm != null && cm.PlacementTarget != null)
        //    {
        //        var scale = GetWindowDpiScale(cm.PlacementTarget);
        //        ScaleElement(cm, scale);
        //        Debug.WriteLine("ToolTip scale = " + scale);
        //    }
        //}

        //static void AttachWindow(Window window)
        //{
        //    window.Closed += Window_Closed;
        //    var dpd = DependencyPropertyDescriptor.FromProperty(FrameworkElement.FlowDirectionProperty, typeof(Window));
        //    if (dpd != null)
        //    {
        //        dpd.AddValueChanged(window, HandleFlowDirChange);
        //    }
        //    var hwnd = new WindowInteropHelper(window).Handle;
        //    if (hwnd == IntPtr.Zero)
        //    {
        //        window.SourceInitialized += Window_SourceInitialized;
        //    }
        //    else
        //    {
        //        HookupDpiMessage(window, hwnd);
        //    }
        //}

        //static void DetatchWindow(Window win)
        //{
        //    win.Closed -= Window_Closed;
        //    win.SourceInitialized -= Window_SourceInitialized;
        //    var dpd = DependencyPropertyDescriptor.FromProperty(FrameworkElement.FlowDirectionProperty, typeof(Window));
        //    if (dpd != null)
        //    {
        //        dpd.RemoveValueChanged(win, HandleFlowDirChange);
        //    }
        //    var hSrc = (HwndSource)HwndSource.FromVisual(win);
        //    if (hSrc != null)
        //    {
        //        hSrc.RemoveHook(WndProc);
        //    }
        //}

        //private static void Window_Closed(object sender, EventArgs e)
        //{
        //    DetatchWindow((Window)sender);
        //}

        //private static void Window_SourceInitialized(object sender, EventArgs e)
        //{
        //    Window win = (Window)sender;
        //    win.SourceInitialized -= Window_SourceInitialized;
        //    var hwnd = new WindowInteropHelper(win).Handle;
        //    HookupDpiMessage(win, hwnd);
        //}

        //private static void HandleFlowDirChange(object sender, EventArgs e)
        //{
        //    RescaleForDpi((Window)sender);
        //}

        //private static void HookupDpiMessage(Window win, IntPtr hwnd)
        //{
        //    var hSrc = HwndSource.FromHwnd(hwnd);
        //    hSrc.AddHook(WndProc);

        //    //Calculate the effective DPI used by WPF;
        //    var wpfDPI = 96.0 * hSrc.CompositionTarget.TransformToDevice.M11;
        //    //Get the Current DPI of the monitor of the window. 
        //    var monitorDPI = Shcore.GetDpiForWindow(hSrc.Handle);
        //    SetWindowDpi(win, monitorDPI);
        //    //Calculate the scale factor used to modify window size, graphics and text
        //    var dpiScaleFactor = monitorDPI / wpfDPI;
        //    SetWindowDpiScale(win, dpiScaleFactor);
        //    win.Width *= dpiScaleFactor;
        //    win.Height *= dpiScaleFactor;
        //    RescaleForDpi(win);
        //}

        //static void RescaleForDpi(Window window)
        //{
        //    if (window != null)
        //    {
        //        var child = VisualTreeHelper.GetChild(window, 0) as FrameworkElement;
        //        if (child != null)
        //        {
        //            ScaleElement(child, GetWindowDpiScale(window), true);
        //        }

        //        var dpiArgs = new DpiChangeEventArgs(window, GetWindowDpi(window), GetWindowDpiScale(window));
        //        window.RaiseEvent(dpiArgs);
        //    }
        //}

        //private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        //{
        //    if (!handled && (WindowMessage)msg == WindowMessage.WM_DPICHANGED)
        //    {
        //        try
        //        {
        //            var hSrc = HwndSource.FromHwnd(hwnd);
        //            var win = hSrc.RootVisual as Window;
        //            if (win != null)
        //            {
        //                var curDpi = GetWindowDpi(win);

        //                var newMonDpi = wParam.ToInt32() & 0xffff;
        //                if (curDpi != newMonDpi)
        //                {
        //                    SetWindowDpi(win, newMonDpi);

        //                    var wpfDPI = 96.0 * hSrc.CompositionTarget.TransformToDevice.M11;
        //                    var scale = newMonDpi / wpfDPI;
        //                    SetWindowDpiScale(win, scale);
        //                    // update window size as well
        //                    RECT winRect = (RECT)Marshal.PtrToStructure(lParam, typeof(RECT));
        //                    User32.SetWindowPos(hwnd, IntPtr.Zero, winRect.left, winRect.top, winRect.right - winRect.left, winRect.bottom - winRect.top,
        //                         SetWindowPosOptions.SWP_NOZORDER |
        //                         SetWindowPosOptions.SWP_NOOWNERZORDER |
        //                         SetWindowPosOptions.SWP_NOACTIVATE);

        //                    RescaleForDpi(win);
        //                }
        //            }
        //        }
        //        catch { }
        //    }
        //    return IntPtr.Zero;
        //}


        #endregion

        #region scaling calls

        ///// <summary>
        ///// Scales the element based on some factor.
        ///// </summary>
        ///// <param name="child">The child.</param>
        ///// <param name="scaleFactor">The scale factor.</param>
        //public static void ScaleElement(FrameworkElement child, double scaleFactor)
        //{
        //    ScaleElement(child, scaleFactor, false);
        //}

        ///// <summary>
        ///// Scales the element based on some factor.
        ///// </summary>
        ///// <param name="child">The child.</param>
        ///// <param name="scaleFactor">The scale factor.</param>
        ///// <param name="compensateRender">if set to <c>true</c> to compensate RTL bug with render transform.</param>
        ///// <exception cref="System.ArgumentNullException">child</exception>
        //public static void ScaleElement(FrameworkElement child, double scaleFactor, bool compensateRender)
        //{
        //    if (child == null) { throw new ArgumentNullException("child"); }

        //    var flow = child.FlowDirection;
        //    var origLayout = UnwrapDpiTransform((Transform)child.GetValue(FrameworkElement.LayoutTransformProperty));
        //    var origRender = UnwrapDpiTransform((Transform)child.GetValue(UIElement.RenderTransformProperty));

        //    if (scaleFactor != 1.0)
        //    {
        //        child.SetValue(FrameworkElement.LayoutTransformProperty, WrapDpiTransform(origLayout, scaleFactor));
        //        if (compensateRender)
        //        {
        //            // weird wpf bug when using RTL so compensate again in render xform
        //            if (flow == FlowDirection.RightToLeft)
        //            {
        //                child.SetValue(UIElement.RenderTransformProperty, WrapDpiTransform(origRender, scaleFactor));
        //            }
        //            else
        //            {
        //                child.SetValue(UIElement.RenderTransformProperty, origRender);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        child.SetValue(FrameworkElement.LayoutTransformProperty, origLayout);
        //        if (compensateRender)
        //        {
        //            child.SetValue(UIElement.RenderTransformProperty, origRender);
        //        }
        //    }
        //}

        //static Transform WrapDpiTransform(Transform origTransform, double dpiScaleFactor)
        //{
        //    var group = new TransformGroup();
        //    if (origTransform != null)
        //    {
        //        group.Children.Add(origTransform);
        //    }
        //    group.Children.Add(new ScaleTransform(dpiScaleFactor, dpiScaleFactor));
        //    UIHooks.SetIsDpiTransform(group, true);
        //    return group;
        //}

        //static Transform UnwrapDpiTransform(Transform currentTransform)
        //{
        //    if (currentTransform != null && UIHooks.GetIsDpiTransform(currentTransform))
        //    {
        //        var group = currentTransform as TransformGroup;
        //        if (group != null && group.Children.Count > 1)
        //        {
        //            return group.Children[0];
        //        }
        //        return null;
        //    }
        //    return currentTransform;
        //}

        #endregion

    }

    ///// <summary>
    ///// Contains information on DPI changes.
    ///// </summary>
    ///// <seealso cref="System.Windows.RoutedEventArgs" />
    //public class DpiChangeEventArgs : RoutedEventArgs
    //{
    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="DpiChangeEventArgs" /> class.
    //    /// </summary>
    //    /// <param name="window">The window.</param>
    //    /// <param name="newDpi">The new dpi.</param>
    //    /// <param name="scale">The scale.</param>
    //    public DpiChangeEventArgs(Window window, int newDpi, double scale) : base(UIHooks.DpiChangeEvent, window)
    //    {
    //        NewDpi = newDpi;
    //        Scale = scale;
    //    }

    //    /// <summary>
    //    /// Gets the new dpi value.
    //    /// </summary>
    //    /// <value>
    //    /// The new dpi.
    //    /// </value>
    //    public int NewDpi { get; private set; }

    //    /// <summary>
    //    /// Gets the scale for the new dpi value.
    //    /// </summary>
    //    /// <value>
    //    /// The scale.
    //    /// </value>
    //    public double Scale { get; private set; }
    //}
}
