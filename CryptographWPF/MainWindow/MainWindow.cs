using CryptographWPF.Encryption;
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
        private string _inputPlaceholder = "Текст для шифрування...";
        private string _outputPlaceholder = "Зашифрований текст";

        private readonly string[] _encryptionTypes = {"ROT1", "ROT13", "Шифр Цезаря", "Транспозиція", "Двійковий код",
            "Вісімковий код", "Шістнадцятковий код", "Шифр Віженера", "RSA шифрування", "AES шифрування"};
        private string _encryptionType;

        private enum Acts { Crypto, Decrypto};
        private Acts _act;


        private enum Pages {EncryptionPage, ShorthandPage};
        private Pages _currentPage;

        public MainWindow()
        {
            InitializeComponent();
            InitializeComponentsSettings();
            InitializeWindowControlButtons();
        }

        private void InitializeComponentsSettings()
        {
            /*InputTextBox.Text = _inputPlaceholder;
            OutputTextBox.Text = _outputPlaceholder;

            CryptoRadioButton.IsChecked = true;

            EncryptionsComboBox.SelectedIndex = 0;

            SetPageSettings();*/
        }
        private void SetPageSettings()
        {
            _currentPage = Pages.EncryptionPage;
            EncryptionPageButton.Style = Application.Current.TryFindResource("CurrentPageSideMenuButton") as Style;
            ShorthandPageButton.Style = Application.Current.TryFindResource("NotCurrentPageSideMenuButton") as Style;
        }
        private void InitializeWindowControlButtons()
        {
            MinimizeButton.Click += (s, e) => WindowState = WindowState.Minimized;
            CloseButton.Click += (s, e) => Close();
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
            /*InputTextBox.Text = _inputPlaceholder;
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

            SetPanels();*/
        }

        private void EncryptionPageButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = Pages.EncryptionPage;
            EncryptionPageButton.Style = Application.Current.TryFindResource("CurrentPageSideMenuButton") as Style;
            ShorthandPageButton.Style = Application.Current.TryFindResource("NotCurrentPageSideMenuButton") as Style;

            EncryptionFrame.Content = new Page1();
        }
        private void ShorthandPageButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = Pages.ShorthandPage;
            EncryptionPageButton.Style = Application.Current.TryFindResource("NotCurrentPageSideMenuButton") as Style;
            ShorthandPageButton.Style = Application.Current.TryFindResource("CurrentPageSideMenuButton") as Style;
        }
    }
}
