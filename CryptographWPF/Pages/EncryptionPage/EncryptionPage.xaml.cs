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
        private const string _inputPlaceholderForCrypto = "Текст для шифрування...";
        private const string _inputPlaceholderForDecrypto = "Текст для дешифрування...";

        private string _inputPlaceholder;
        private string InputPlaceholder 
        {
            get => _inputPlaceholder;
            set 
            {
                _inputPlaceholder = value;
                if (CheckForEmptyInputTextBox()) InputTextBox.Text = InputPlaceholder;
            }
        }
        private const string _outputPlaceholderForCrypto = "Зашифрований текст";
        private const string _outputPlaceholderForDecrypto = "Розшифрований текст";
        private string _outputPlaceholder;
        private string OutputPlaceholder
        {
            get => _outputPlaceholder;
            set
            {
                _outputPlaceholder = value;
                if (CheckForEmptyOutoutTextBox()) OutputTextBox.Text = OutputPlaceholder;
            }
        }
        private string InputText { get; set; }

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
        private EncryptionTypes EncryptionType
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
        private Acts Act 
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
            InitializeBackgroundWorker();
        }

        private void InitializeComponentsSettings()
        {
            InputPlaceholder = _inputPlaceholderForCrypto;
            OutputPlaceholder = _outputPlaceholderForCrypto;

            InputTextBox.Text = InputPlaceholder;
            OutputTextBox.Text = OutputPlaceholder;

            CryptoRadioButton.IsChecked = true;

            EncryptionsComboBox.SelectedIndex = 0;
        }

        private void InitializeBackgroundWorker()
        {
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            _backgroundWorker.WorkerSupportsCancellation = true;
        }

        private void InputTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == _inputPlaceholder) ((TextBox)sender).Text = "";
        }
        private void InputTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "" || ((TextBox)sender).Text == null) ((TextBox)sender).Text = _inputPlaceholder;
        }
        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            InputText = InputTextBox.Text;
            if (!CheckForEmptyInputTextBox()) OutputTextBox.Text = OutputPlaceholder;
        }


        private void EncryptionsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => EncryptionType = (EncryptionTypes)((ComboBox)sender).SelectedIndex;

        private void CryptoRadioButton_Checked(object sender, RoutedEventArgs e)
        {  
            Act = Acts.Crypto;
            InputPlaceholder = _inputPlaceholderForCrypto;
            OutputPlaceholder =_outputPlaceholderForCrypto;
        }
        private void DecryptoRadioButton_Checked(object sender, RoutedEventArgs e)
        {  
            Act = Acts.Decrypto;
            InputPlaceholder = _inputPlaceholderForDecrypto;
            OutputPlaceholder = _outputPlaceholderForDecrypto;
        }


        private bool CheckForEmptyInputTextBox() => (InputText == "" || InputText == null || InputText == _inputPlaceholderForCrypto || InputText == _inputPlaceholderForDecrypto);
        private bool CheckForEmptyOutoutTextBox() => (OutputText == "" || OutputText == null || OutputText == _outputPlaceholderForCrypto || OutputText == _outputPlaceholderForDecrypto);


        private void UpdatePanels()
        {
            RemovePanels();
            ClearPanels();

            if (Act == Acts.Crypto)
            {
                ActionButton.Content = "Шифрувати";

                if (EncryptionType == EncryptionTypes.Cesar || _encryptionType == EncryptionTypes.Vigener || _encryptionType == EncryptionTypes.AES) SetSimpleKeyCryptoStackPanel();
                if (EncryptionType == EncryptionTypes.RSA) GetPairKeyCryptoStackPanel();
                if (EncryptionType == EncryptionTypes.AES) GetCryptoEncodingDockPanels();
            }
            if (Act == Acts.Decrypto)
            {
                ActionButton.Content = "Дешифрувати";

                if (EncryptionType == EncryptionTypes.Cesar || _encryptionType == EncryptionTypes.Vigener || _encryptionType == EncryptionTypes.AES) GetSimpleKeyDecryptoStackPanel();
                if (EncryptionType == EncryptionTypes.RSA) GetPairKeyDecryptoStackPanel();
                if (EncryptionType == EncryptionTypes.AES)
                {
                    GetCryptoEncodingDockPanels();
                    SwapEncodingsDockPanels();
                }

            }

            SetPanels();
        }
    }
}
