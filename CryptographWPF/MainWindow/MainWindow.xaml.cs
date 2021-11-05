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
        private Pages CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                ChangePage();
            }
        }

        internal readonly FileInfo fileSettings = new FileInfo("AppStartSettings.dat");

        public MainWindow()
        {
            InitializeComponent();
            LoadSettings();      
        }


        private void LoadSettings()
        {
            using (BinaryReader binaryReader = new BinaryReader(fileSettings.Open(FileMode.OpenOrCreate)))
            {
                if (binaryReader.PeekChar() == -1) return;
                CurrentPage = (Pages)Enum.Parse(typeof(Pages), binaryReader.ReadString());
            }
        }
        

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            using (BinaryWriter binaryWrite = new BinaryWriter(fileSettings.OpenWrite())) binaryWrite.Write(CurrentPage.ToString());

            Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;


        private void ChangePage()
        {
            if(CurrentPage == Pages.EncryptionPage)
            {
                ActionFrame.Content = new EncryptionPage();
                SetEncryptionButtonCurrentPage();
            }
            else if(CurrentPage == Pages.ShorthandPage)
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
            if (CurrentPage != Pages.EncryptionPage) CurrentPage = Pages.EncryptionPage;
        }
        private void ShorthandPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage != Pages.ShorthandPage) CurrentPage = Pages.ShorthandPage;
        }

    }
}
