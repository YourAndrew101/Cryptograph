using CryptographWPF.Pages;
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

        public MainWindow()
        {
            InitializeComponent();
            InitializeWindowControlButtons();
            InitializePage();
        }
        
        private void InitializeWindowControlButtons()
        {
            MinimizeButton.Click += (s, e) => WindowState = WindowState.Minimized;
            CloseButton.Click += (s, e) => Close();
        }

        private void InitializePage()
        {
            //TODO load settings from file
            ActionFrame.Content = new EncryptionPage();
            _CurrentPage = Pages.EncryptionPage;
        }


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

        //TODO add labels to not understendable buttons
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
