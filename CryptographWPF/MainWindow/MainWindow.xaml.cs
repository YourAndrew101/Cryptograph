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
            InitializeFrame();
        }
        
        private void InitializeWindowControlButtons()
        {
            MinimizeButton.Click += (s, e) => WindowState = WindowState.Minimized;
            CloseButton.Click += (s, e) => Close();
        }

        private void InitializeFrame()
        {
            ActionFrame.Content = new EncryptionPage();
            SetEncryptionButtonCurrentPage();
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
                //TODO change page
                ActionFrame.Content = new EncryptionPage();
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

        /*private void InitializeComponentsSettings()
        {
            InputTextBox.Text = _inputPlaceholder;
            OutputTextBox.Text = _outputPlaceholder;

            CryptoRadioButton.IsChecked = true;

            EncryptionsComboBox.SelectedIndex = 0;

            SetPageSettings();
        }
        private void SetPageSettings()
        {
            _currentPage = Pages.EncryptionPage;
            EncryptionPageButton.Style = Application.Current.TryFindResource("CurrentPageSideMenuButton") as Style;
            ShorthandPageButton.Style = Application.Current.TryFindResource("NotCurrentPageSideMenuButton") as Style;
        }
        

        private void InputTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == _inputPlaceholder) ((TextBox)sender).Text = "";
        }
        private void InputTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "" || ((TextBox)sender).Text == null) ((TextBox)sender).Text = _inputPlaceholder;
        }


        private void EncryptionsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            _encryptionType = ((TextBlock)selectedItem.Content).Text;

            UpdatePanels();
        }

        private void CryptoRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _act = Acts.Crypto;
            _inputPlaceholder = "Текст для шифрування...";
            _outputPlaceholder = "Зашифрований текст";

            UpdatePanels();
        }
        private void DecryptoRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _act = Acts.Decrypto;
            _inputPlaceholder = "Текст для дешифрування...";
            _outputPlaceholder = "Розшифрований текст";

            UpdatePanels();
        }


        private void UpdatePanels()
        {
            InputTextBox.Text = _inputPlaceholder;
            OutputTextBox.Text = _outputPlaceholder;

            RemovePanels();
            ClearPanels();

            if (_act == Acts.Crypto)
            {
                ActionButton.Content = "Шифрувати";

                if (_encryptionType == _encryptionTypes[2] || _encryptionType == _encryptionTypes[7]) GetSimpleKeyCryptoStackPanel();
                if (_encryptionType == _encryptionTypes[8]) GetPairKeyCryptoStackPanel();
                if (_encryptionType == _encryptionTypes[9])
                {
                    GetSimpleKeyCryptoStackPanel();
                    GetCryptoEncodingDockPanels();
                }
            }
            if (_act == Acts.Decrypto)
            {
                ActionButton.Content = "Дешифрувати";

                if (_encryptionType == _encryptionTypes[2] || _encryptionType == _encryptionTypes[7]) GetSimpleKeyDecryptoStackPanel();
                if (_encryptionType == _encryptionTypes[8]) GetPairKeyDecryptoStackPanel();
                if (_encryptionType == _encryptionTypes[9])
                {
                    GetSimpleKeyDecryptoStackPanel();
                    GetCryptoEncodingDockPanels();
                    SwapEncodingsDockPanels();
                }

            }

            SetPanels();
        }

        */
    }
}
