﻿using System;
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
        private StackPanel _keyStackPanel = new StackPanel { Name = "KeyStackPanel" };
        private DockPanel _encodingInDockPanel = new DockPanel { Name = "EncodingInDockPanel" };
        private DockPanel _encodingOutDockPanel = new DockPanel { Name = "EncodingOutDockPanel" };
  

        private void SetPanels()
        {
            SetKeyStackPanel();
            SetEncodingDockPanel();
        }
        private void RemovePanels()
        {
            MainGrid.Children.Remove(_keyStackPanel);
            MainGrid.Children.Remove(_encodingInDockPanel);
            MainGrid.Children.Remove(_encodingOutDockPanel);
        }
        private void ClearPanels()
        {
            _keyStackPanel.Children.Clear();
            _encodingInDockPanel.Children.Clear();
            _encodingOutDockPanel.Children.Clear();
        }

        private void SetKeyStackPanel()
        {
            _ = MainGrid.Children.Add(_keyStackPanel);
            Grid.SetColumn(_keyStackPanel, 2);
            Grid.SetRow(_keyStackPanel, 4);
        }
        private void SetEncodingDockPanel()
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
            GetSimpleKeyTextBox(_keyStackPanel);
            GetSimpleKeyLabel(_keyStackPanel);
            GetKeyLengthTextBox(_keyStackPanel);
        }
        private void GetSimpleKeyTextBox(StackPanel keyStackPanel)
        {
            TextBox keyTextBox = new TextBox
            {
                Style = Application.Current.TryFindResource("SimpleKeyCryptoTextBoxStyle") as Style,
                Name = "SimpleKeyTextBox"
            };
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
            TextBox keyLengthTextBox = new TextBox
            {
                Style = Application.Current.TryFindResource("SimpleKeyCryptoTextBoxStyle") as Style,
                Name = "SimpleKeyLengthTextBox"
            };
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

            TextBox keyTextBox = new TextBox
            {
                Style = Application.Current.TryFindResource("SimpleKeyCryptoTextBoxStyle") as Style,
                Name = "SimpleKeyTextBox"
            };
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


        private void GetPairKeyCryptoStackPanel()
        {
            GetGeneralKeyTextBox(_keyStackPanel);
            GetPublicKeyTextBox(_keyStackPanel);
            GetPrivateKeyTextBox(_keyStackPanel);
            GetGenerateKeysComboBox(_keyStackPanel);
        }
        private void GetGeneralKeyTextBox(StackPanel keyStackPanel)
        {
            TextBox generalKeyTextBox = new TextBox
            {
                Style = Application.Current.TryFindResource("PairKeyCryptoTextBoxStyle") as Style,
                Name = "GeneralKeyTextBox"
            };
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

        private void GetPairKeyDecryptoStackPanel()
        {
            GetGeneralKeyTextBox(_keyStackPanel);
            GetPrivateKeyTextBox(_keyStackPanel);
        }


        private void GetCryptoEncodingDockPanels()
        {
            GetCryptoInEncodingDockPanel();
            GetCryptoOutEncodingDockPanel();
        }
        private void GetCryptoInEncodingDockPanel()
        {
            ComboBox comboBox = new ComboBox { Style = Application.Current.TryFindResource("EncodingComboBoxStyle") as Style };
            comboBox.HorizontalAlignment = HorizontalAlignment.Right;
            ComboBoxItem itemUTF8 = new ComboBoxItem
            {
                Style = Application.Current.TryFindResource("EncodingComboBoxItemStyle") as Style,
                Content = "UTF-8"
            };
            ComboBoxItem itemHex = new ComboBoxItem
            {
                Style = Application.Current.TryFindResource("EncodingComboBoxItemStyle") as Style,
                Content = "Hex"
            };
            comboBox.Items.Add(itemUTF8);
            comboBox.Items.Add(itemHex);

            _encodingInDockPanel.Children.Add(comboBox);
        }
        private void GetCryptoOutEncodingDockPanel()
        {
            ComboBox comboBox = new ComboBox { Style = Application.Current.TryFindResource("EncodingComboBoxStyle") as Style };
            comboBox.HorizontalAlignment = HorizontalAlignment.Left;
            ComboBoxItem itemBase64 = new ComboBoxItem
            {
                Style = Application.Current.TryFindResource("OddEncryptionTypeComboBoxItemStyle") as Style,
                Content = "Base64"
            };
            ComboBoxItem itemHex = new ComboBoxItem
            {
                Style = Application.Current.TryFindResource("OddEncryptionTypeComboBoxItemStyle") as Style,
                Content = "Hex"
            };
            comboBox.Items.Add(itemBase64);
            comboBox.Items.Add(itemHex);

            _encodingOutDockPanel.Children.Add(comboBox);
        }

        private void SwapEncodingsDockPanels()
        {
            ComboBoxItem cbi1 = (ComboBoxItem)((ComboBox)_encodingInDockPanel.Children[0]).Items[0];
            cbi1.Content = "Base64";
            ComboBoxItem cbi2 = (ComboBoxItem)((ComboBox)_encodingOutDockPanel.Children[0]).Items[0];
            cbi2.Content = "UTF-8";
        }
    }
}