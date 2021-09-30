using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptographWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            InitializeComponentsColors();
            Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 5 });
        }

        private void InitializeComponentsColors()
        {
            MainGrid.Background = new SolidColorBrush(ColorSchemes.BackgroundColor);
            SideMenuButton.Background = new SolidColorBrush(ColorSchemes.ButtonColor);
        }

        private void InputTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (TextBox)sender
        }

        private void InputTextBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
