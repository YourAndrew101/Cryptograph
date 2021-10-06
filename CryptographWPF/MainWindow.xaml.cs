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
        private string _inputPlaceholder = "Текст для шифрування...";
        private string _outputPlaceholder = "Зашифрований текст";

        readonly private string[] _encryptionTypes = {"ROT1", "ROT13", "Шифр Цезаря", "Транспозиція", "Двійковий код",
            "Вісімковий код", "Шістнадцятковий код", "Шифр Віженера", "RSA шифрування", "AES шифрування"};
        private string _encryptionType;

        private enum Acts { Crypto, Decrypto};
        private Acts _act;

        private StackPanel _keyStackPanel = new StackPanel();

        public MainWindow()
        {
            InitializeComponent();
            InitializeComponentsColors();
            InitializeComponentsSettings();
        }

        private void InitializeComponentsSettings()
        {
            InputTextBox.Text = _inputPlaceholder;
            OutputTextBox.Text = _outputPlaceholder;

            CryptoRadioButton.IsChecked = true;

            EncryptionsComboBox.SelectedIndex = 0;
        }
        private void InitializeComponentsColors()
        {
            MainGrid.Background = new SolidColorBrush(ColorSchemes.BackgroundColor);
            SideMenuButton.Background = new SolidColorBrush(ColorSchemes.ButtonColor);
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

            MainGrid.Children.Remove(_keyStackPanel);
            _keyStackPanel.Children.Clear();

            if (_act == Acts.Crypto)
            {
                ActionButton.Content = "Шифрувати";

                if (_encryptionType == _encryptionTypes[2] || _encryptionType == _encryptionTypes[7]) GetSimpleKeyCryptoStackPanel();
                if (_encryptionType == _encryptionTypes[8]) GetPairKeyCryptoPanel();
            }
            if(_act == Acts.Decrypto)
            {
                ActionButton.Content = "Дешифрувати";

                if (_encryptionType == _encryptionTypes[2] || _encryptionType == _encryptionTypes[7]) GetSimpleKeyDecryptoStackPanel();
                if (_encryptionType == _encryptionTypes[8]) GetPairKeyCryptoPanel();
            }

            SetKeyStackPanel();
        }

        private void SetKeyStackPanel()
        {
            MainGrid.Children.Add(_keyStackPanel);
            Grid.SetColumn(_keyStackPanel, 2);
            Grid.SetRow(_keyStackPanel, 4);
        }

        private void GetSimpleKeyCryptoStackPanel()
        {
            GetSimpleKeyTextBox(_keyStackPanel);                     
            GetSimpleKeyLabel(_keyStackPanel);
            GetKeyLengthTextBox(_keyStackPanel);     
        }
        private void GetSimpleKeyTextBox(StackPanel keyStackPanel)
        {
            TextBox keyTextBox = new TextBox { Style = Application.Current.TryFindResource("SimpleKeyCryptoTextBoxStyle") as Style };
            string keyTextBoxPlaceholder = "Ваш ключ...";
            keyTextBox.Text = keyTextBoxPlaceholder;
            keyTextBox.GotFocus += KeyTextBox_GotFocus;
            keyTextBox.LostFocus += KeyTextBox_LostFocus;

            void KeyTextBox_LostFocus(object sender, RoutedEventArgs e)
            {
                if (((TextBox)sender).Text == "" || ((TextBox)sender).Text == null) ((TextBox)sender).Text = keyTextBoxPlaceholder;
            }
            void KeyTextBox_GotFocus(object sender, RoutedEventArgs e)
            {
                if (((TextBox)sender).Text == keyTextBoxPlaceholder) ((TextBox)sender).Text = "";
            }

            keyStackPanel.Children.Add(keyTextBox);
        }
        private void GetSimpleKeyLabel(StackPanel keyStackPanel)
        {
            Label label = new Label { Style = Application.Current.TryFindResource("SimpleKeyCryptoLabelStyle") as Style };

            keyStackPanel.Children.Add(label);
        }
        private void GetKeyLengthTextBox(StackPanel keyStackPanel)
        {
            TextBox keyLengthTextBox = new TextBox { Style = Application.Current.TryFindResource("SimpleKeyCryptoTextBoxStyle") as Style };
            string keyLengthTextBoxPlaceholder = "Довжина ключа...";
            keyLengthTextBox.Text = keyLengthTextBoxPlaceholder;
            keyLengthTextBox.GotFocus += KeyLengthTextBox_GotFocus;
            keyLengthTextBox.LostFocus += KeyLengthTextBox_LostFocus;

            void KeyLengthTextBox_LostFocus(object sender, RoutedEventArgs e)
            {
                if (((TextBox)sender).Text == "" || ((TextBox)sender).Text == null) ((TextBox)sender).Text = keyLengthTextBoxPlaceholder;
            }
            void KeyLengthTextBox_GotFocus(object sender, RoutedEventArgs e)
            {
                if (((TextBox)sender).Text == keyLengthTextBoxPlaceholder) ((TextBox)sender).Text = "";
            }

            keyStackPanel.Children.Add(keyLengthTextBox);
        }


        private void GetSimpleKeyDecryptoStackPanel()
        {
            _keyStackPanel.Children.Clear();

            TextBox keyTextBox = new TextBox{ Style = Application.Current.TryFindResource("SimpleKeyCryptoTextBoxStyle") as Style };
            string keyTextBoxPlaceholder = "Ваш ключ...";
            keyTextBox.Text = keyTextBoxPlaceholder;
            keyTextBox.GotFocus += KeyTextBox_GotFocus;
            keyTextBox.LostFocus += KeyTextBox_LostFocus;

            void KeyTextBox_LostFocus(object sender, RoutedEventArgs e)
            {
                if (((TextBox)sender).Text == "" || ((TextBox)sender).Text == null) ((TextBox)sender).Text = keyTextBoxPlaceholder;
            }
            void KeyTextBox_GotFocus(object sender, RoutedEventArgs e)
            {
                if (((TextBox)sender).Text == keyTextBoxPlaceholder) ((TextBox)sender).Text = "";
            }

            _keyStackPanel.Children.Add(keyTextBox);
        }


        private void GetPairKeyCryptoPanel()
        {
            GetGeneralKeyTextBox(_keyStackPanel);
            GetPublicKeyTextBox(_keyStackPanel);
            GetPrivateKeyTextBox(_keyStackPanel);
            GetGenerateKeysComboBox(_keyStackPanel);
        }
        private void GetGeneralKeyTextBox(StackPanel keyStackPanel)
        {
            TextBox generalKeyTextBox = new TextBox { Style = Application.Current.TryFindResource("PairKeyCryptoTextBoxStyle") as Style };
            string generalKeyTextBoxPlaceholder = "Загальний ключ...";
            generalKeyTextBox.Text = generalKeyTextBoxPlaceholder;
            generalKeyTextBox.HorizontalAlignment = HorizontalAlignment.Center;
            generalKeyTextBox.GotFocus += GeneralKeyTextBox_GotFocus;
            generalKeyTextBox.LostFocus += GeneralKeyTextBox_LostFocus;

            void GeneralKeyTextBox_LostFocus(object sender, RoutedEventArgs e)
            {
                if (((TextBox)sender).Text == "" || ((TextBox)sender).Text == null) ((TextBox)sender).Text = generalKeyTextBoxPlaceholder;
            }
            void GeneralKeyTextBox_GotFocus(object sender, RoutedEventArgs e)
            {
                if (((TextBox)sender).Text == generalKeyTextBoxPlaceholder) ((TextBox)sender).Text = "";
            }

            keyStackPanel.Children.Add(generalKeyTextBox);
        }
        private void GetPublicKeyTextBox(StackPanel keyStackPanel)
        {
            TextBox publicKeyTextBox = new TextBox { Style = Application.Current.TryFindResource("PairKeyCryptoTextBoxStyle") as Style };
            string publicKeyTextBoxPlaceholder = "Публічний ключ...";
            publicKeyTextBox.Text = publicKeyTextBoxPlaceholder;
            publicKeyTextBox.HorizontalAlignment = HorizontalAlignment.Center;
            publicKeyTextBox.Margin = new Thickness(0, 5, 0, 0);
            publicKeyTextBox.GotFocus += PublicKeyTextBox_GotFocus;
            publicKeyTextBox.LostFocus += PublicKeyTextBox_LostFocus;

            void PublicKeyTextBox_LostFocus(object sender, RoutedEventArgs e)
            {
                if (((TextBox)sender).Text == "" || ((TextBox)sender).Text == null) ((TextBox)sender).Text = publicKeyTextBoxPlaceholder;
            }
            void PublicKeyTextBox_GotFocus(object sender, RoutedEventArgs e)
            {
                if (((TextBox)sender).Text == publicKeyTextBoxPlaceholder) ((TextBox)sender).Text = "";
            }

            keyStackPanel.Children.Add(publicKeyTextBox);
        }
        private void GetPrivateKeyTextBox(StackPanel keyStackPanel)
        {
            TextBox privateKeyTextBox = new TextBox { Style = Application.Current.TryFindResource("PairKeyCryptoTextBoxStyle") as Style };
            string privateKeyTextBoxPlaceholder = "Приватний ключ...";
            privateKeyTextBox.Text = privateKeyTextBoxPlaceholder;
            privateKeyTextBox.HorizontalAlignment = HorizontalAlignment.Center;
            privateKeyTextBox.Margin = new Thickness(0, 5, 0, 0);
            privateKeyTextBox.GotFocus += PrivateKeyTextBox_GotFocus;
            privateKeyTextBox.LostFocus += PrivateKeyTextBox_LostFocus;

            void PrivateKeyTextBox_LostFocus(object sender, RoutedEventArgs e)
            {
                if (((TextBox)sender).Text == "" || ((TextBox)sender).Text == null) ((TextBox)sender).Text = privateKeyTextBoxPlaceholder;
            }
            void PrivateKeyTextBox_GotFocus(object sender, RoutedEventArgs e)
            {
                if (((TextBox)sender).Text == privateKeyTextBoxPlaceholder) ((TextBox)sender).Text = "";
            }

            keyStackPanel.Children.Add(privateKeyTextBox);
        }
        private void GetGenerateKeysComboBox(StackPanel keyStackPanel)
        {
            CheckBox generateKeysCheckBox = new CheckBox { Style = Application.Current.TryFindResource("GenerateKeysCheckBoxStyle") as Style };

            keyStackPanel.Children.Add(generateKeysCheckBox);
        }
    }
}
