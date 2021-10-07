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

        private readonly string[] _encryptionTypes = {"ROT1", "ROT13", "Шифр Цезаря", "Транспозиція", "Двійковий код",
            "Вісімковий код", "Шістнадцятковий код", "Шифр Віженера", "RSA шифрування", "AES шифрування"};
        private string _encryptionType;

        private enum Acts { Crypto, Decrypto};
        private Acts _act;

        private DockPanel _keyDockPanel = new DockPanel();
        private DockPanel _encodingInDockPanel = new DockPanel();
        private DockPanel _encodingOutDockPanel = new DockPanel();


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

            RemovePanels();
            ClearPanels();

            if (_act == Acts.Crypto)
            {
                ActionButton.Content = "Шифрувати";

                if (_encryptionType == _encryptionTypes[2] || _encryptionType == _encryptionTypes[7]) GetSimpleKeyCryptoStackPanel();
                if (_encryptionType == _encryptionTypes[8]) GetPairKeyCryptoDockPanel();
                if (_encryptionType == _encryptionTypes[9])
                {
                    GetSimpleKeyCryptoStackPanel();
                    GetEncodingDockPanels();
                }
            }
            if (_act == Acts.Decrypto)
            {
                ActionButton.Content = "Дешифрувати";

                if (_encryptionType == _encryptionTypes[2] || _encryptionType == _encryptionTypes[7]) GetSimpleKeyDecryptoStackPanel();
                if (_encryptionType == _encryptionTypes[8]) GetPairKeyDecryptoDockPanel();
                if (_encryptionType == _encryptionTypes[9])
                {
                    GetSimpleKeyDecryptoStackPanel();
                    GetEncodingDockPanels();
                }

            }

            SetPanels();
        }

        private void SetPanels()
        {
            SetKeyStackPanel();
            SetEncodingStackPanel();
        }
        private void RemovePanels()
        {
            MainGrid.Children.Remove(_keyDockPanel);
            MainGrid.Children.Remove(_encodingInDockPanel);
            MainGrid.Children.Remove(_encodingOutDockPanel);
        }
        private void ClearPanels()
        {
            _keyDockPanel.Children.Clear();
            _encodingInDockPanel.Children.Clear();
            _encodingOutDockPanel.Children.Clear();
        }

        private void SetKeyStackPanel()
        {
            _ = MainGrid.Children.Add(_keyDockPanel);
            Grid.SetColumn(_keyDockPanel, 2);
            Grid.SetRow(_keyDockPanel, 4);
        }
        private void SetEncodingStackPanel()
        {
            _ = MainGrid.Children.Add(_encodingInDockPanel);
            Grid.SetColumn(_encodingInDockPanel, 1);
            Grid.SetRow(_encodingInDockPanel, 0);

            _ = MainGrid.Children.Add(_encodingOutDockPanel);
            Grid.SetColumn(_encodingOutDockPanel, 3);
            Grid.SetRow(_encodingOutDockPanel, 0);
        }


        private void GetSimpleKeyCryptoStackPanel()
        {
            GetSimpleKeyTextBox(_keyDockPanel);
            GetSimpleKeyLabel(_keyDockPanel);
            GetKeyLengthTextBox(_keyDockPanel);
        }
        private void GetSimpleKeyTextBox(DockPanel keyDockPanel)
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

            keyDockPanel.Children.Add(keyTextBox);
        }
        private void GetSimpleKeyLabel(DockPanel keyDockPanel)
        {
            Label label = new Label { Style = Application.Current.TryFindResource("SimpleKeyCryptoLabelStyle") as Style };

            keyDockPanel.Children.Add(label);
        }
        private void GetKeyLengthTextBox(DockPanel keyDockPanel)
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

            keyDockPanel.Children.Add(keyLengthTextBox);
        }

        private void GetSimpleKeyDecryptoStackPanel()
        {
            _keyDockPanel.Children.Clear();

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

            _keyDockPanel.Children.Add(keyTextBox);
        }


        private void GetPairKeyCryptoDockPanel()
        {
            GetGeneralKeyTextBox(_keyDockPanel);
            GetPublicKeyTextBox(_keyDockPanel);
            GetPrivateKeyTextBox(_keyDockPanel);
            GetGenerateKeysComboBox(_keyDockPanel);
        }
        private void GetGeneralKeyTextBox(DockPanel keyDockPanel)
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

            keyDockPanel.Children.Add(generalKeyTextBox);
        }
        private void GetPublicKeyTextBox(DockPanel keyDockPanel)
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

            keyDockPanel.Children.Add(publicKeyTextBox);
        }
        private void GetPrivateKeyTextBox(DockPanel keyDockPanel)
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

            keyDockPanel.Children.Add(privateKeyTextBox);
        }
        private void GetGenerateKeysComboBox(DockPanel keyDockPanel)
        {
            CheckBox generateKeysCheckBox = new CheckBox { Style = Application.Current.TryFindResource("GenerateKeysCheckBoxStyle") as Style };

            keyDockPanel.Children.Add(generateKeysCheckBox);
        }

        private void GetPairKeyDecryptoDockPanel()
        {
            GetGeneralKeyTextBox(_keyDockPanel);
            GetPrivateKeyTextBox(_keyDockPanel);
        }


        private void GetEncodingDockPanels()
        {
            GetInEncodingDockPanel();
            GetOutEncodingDockPanel();
        }

        private void GetInEncodingDockPanel()
        {
            ComboBox comboBox = new ComboBox { Style = Application.Current.TryFindResource("EncodingComboBoxStyle") as Style };
            comboBox.HorizontalAlignment = HorizontalAlignment.Right;
            comboBox.VerticalAlignment = VerticalAlignment.Bottom;
            ComboBoxItem itemUTF8 = new ComboBoxItem
            {
                Style = Application.Current.TryFindResource("OddEncryptionTypeComboBoxItemStyle") as Style,
                Content = "UTF-8"
            };
            ComboBoxItem itemHex = new ComboBoxItem
            {
                Style = Application.Current.TryFindResource("OddEncryptionTypeComboBoxItemStyle") as Style,
                Content = "Hex"
            };
            comboBox.Items.Add(itemUTF8);
            comboBox.Items.Add(itemHex);

            _encodingInDockPanel.Children.Add(comboBox);
        }

        private void GetOutEncodingDockPanel()
        {

        }
    }
}
