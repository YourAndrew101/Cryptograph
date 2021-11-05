using CryptographWPF.Pages;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class MainWindow : Window
    {
        private enum Pages {EncryptionPage, ShorthandPage};
        private Pages _currentPage;
        private Pages _CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                ChangePage();
            }
        }

        readonly FileInfo fileSettings = new FileInfo("AppStartSettings.dat");

        public MainWindow()
        {
            InitializeComponent();
            InitializePage();

            GetSettings();
        }

        private void InitializePage()
        {
            //TODO load settings from file
            ActionFrame.Content = new EncryptionPage();
        }

        private void GetSettings()
        {
            using (BinaryReader binaryReader = new BinaryReader(fileSettings.Open(FileMode.OpenOrCreate)))
            {
                if (binaryReader.PeekChar() == -1) return;
                _CurrentPage = (Pages)Enum.Parse(typeof(Pages), binaryReader.ReadString());
            }
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(fileSettings.OpenWrite()))
            {
                binaryWriter.Write(_CurrentPage.ToString());
            }

            Close();
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;


        private void ChangePage()
        {
            if(_CurrentPage == Pages.EncryptionPage)
            {
                ActionFrame.Content = new EncryptionPage();
                SetEncryptionButtonCurrentPage();
            }
            else if(_CurrentPage == Pages.ShorthandPage)
            {
                ActionFrame.Content = new ShorthandPage();
                SetShorthandButtonCurrentPage();
            }
        }

        private void SetEncryptionButtonCurrentPage()
        {
            EncryptionPageButton.Style = Application.Current.TryFindResource("CurrentPageSideMenuButton") as Style;
            ShorthandPageButton.Style = Application.Current.TryFindResource("NotCurrentPageSideMenuButton") as Style;
        }
        private void SetShorthandButtonCurrentPage()
        {
            EncryptionPageButton.Style = Application.Current.TryFindResource("NotCurrentPageSideMenuButton") as Style;
            ShorthandPageButton.Style = Application.Current.TryFindResource("CurrentPageSideMenuButton") as Style;
        }

        private void EncryptionPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_CurrentPage != Pages.EncryptionPage) _CurrentPage = Pages.EncryptionPage;
        }
        private void ShorthandPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_CurrentPage != Pages.ShorthandPage) _CurrentPage = Pages.ShorthandPage;
        }

    }
}
