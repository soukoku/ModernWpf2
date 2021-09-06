using ModernWpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasicRunner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LaunchWindow : Window
    {
        public LaunchWindow()
        {
            InitializeComponent();
            Loaded += LaunchWindow_Loaded;
        }

        private void LaunchWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //btnChromeTitle_Click(null, null);
        }

        private void btnChromeOnly_Click(object sender, RoutedEventArgs e)
        {
            new ChromeOnlyWindow { Owner = this }.Show();
        }

        private void btnChromeTitle_Click(object sender, RoutedEventArgs e)
        {
            new ChromeWithTitleBarWindow { Owner = this }.Show();
        }

        private void btnMaterial_Click(object sender, RoutedEventArgs e)
        {
            new MaterialWindow { Owner = this }.Show();
        }

        private void btnMetro_Click(object sender, RoutedEventArgs e)
        {
            new ModernThemeWindow { Owner = this }.Show();
        }

        private void Window_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            Debug.WriteLine("DPI changed to " + e.NewDpi);
        }
    }
}
