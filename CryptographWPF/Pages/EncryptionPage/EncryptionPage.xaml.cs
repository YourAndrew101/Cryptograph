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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptographWPF.Pages
{
    public partial class EncryptionPage : Page
    {
        private string _inputPlaceholder = "Текст для шифрування...";
        private string _outputPlaceholder = "Зашифрований текст";

        private enum EncryptionTypes
        {
            ROT1 = 0,
            ROT13 = 1,
            Cesar = 2,
            Transposition = 3,
            Binary = 4,
            Octane = 5,
            Hex = 6,
            Vigener = 7,
            RSA = 8,
            AES = 9
        }

        private EncryptionTypes _encryptionType;
        private EncryptionTypes _EncryptionType
        {
            get => _encryptionType;
            set
            {
                _encryptionType = value;
                UpdatePanels();
            }
        }

        private enum Acts { Crypto, Decrypto };
        private Acts _act;
        private Acts _Act
        {
            get => _act;
            set
            {
                _act = value;
                UpdatePanels();
            }
        }

        public EncryptionPage()
        {
            InitializeComponent();
            InitializeComponentsSettings();
        }

        private void InitializeComponentsSettings()
        {
            InputTextBox.Text = _inputPlaceholder;
            OutputTextBox.Text = _outputPlaceholder;

            CryptoRadioButton.IsChecked = true;

            EncryptionsComboBox.SelectedIndex = 0;
        }

        private void InputTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == _inputPlaceholder) ((TextBox)sender).Text = "";
        }
        private void InputTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "" || ((TextBox)sender).Text == null) ((TextBox)sender).Text = _inputPlaceholder;
        }


        private void EncryptionsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => _EncryptionType = (EncryptionTypes)((ComboBox)sender).SelectedIndex;

        private void CryptoRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _Act = Acts.Crypto;
            _inputPlaceholder = "Текст для шифрування...";
            _outputPlaceholder = "Зашифрований текст";
        }
        private void DecryptoRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _Act = Acts.Decrypto;
            _inputPlaceholder = "Текст для дешифрування...";
            _outputPlaceholder = "Розшифрований текст";
        }


        private void UpdatePanels()
        {
            InputTextBox.Text = _inputPlaceholder;
            OutputTextBox.Text = _outputPlaceholder;

            RemovePanels();
            ClearPanels();

            if (_Act == Acts.Crypto)
            {
                ActionButton.Content = "Шифрувати";

                if (_encryptionType == EncryptionTypes.Cesar || _encryptionType == EncryptionTypes.Vigener || _encryptionType == EncryptionTypes.AES) GetSimpleKeyCryptoStackPanel();
                if (_encryptionType == EncryptionTypes.RSA) GetPairKeyCryptoStackPanel();
                if (_encryptionType == EncryptionTypes.AES) GetCryptoEncodingDockPanels();
            }
            if (_Act == Acts.Decrypto)
            {
                ActionButton.Content = "Дешифрувати";

                if (_encryptionType == EncryptionTypes.Cesar || _encryptionType == EncryptionTypes.Vigener || _encryptionType == EncryptionTypes.AES) GetSimpleKeyDecryptoStackPanel();
                if (_encryptionType == EncryptionTypes.RSA) GetPairKeyDecryptoStackPanel();
                if (_encryptionType == EncryptionTypes.AES)
                {
                    GetCryptoEncodingDockPanels();
                    SwapEncodingsDockPanels();
                }

            }

            SetPanels();
        }
    }
}
