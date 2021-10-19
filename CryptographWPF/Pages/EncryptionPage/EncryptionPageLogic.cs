using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
using Managers.CommandManagers;
using EncryptionMethods;
using UsefulMethods;

namespace CryptographWPF.Pages
{
    public partial class EncryptionPage : Page
    {
        private string _outputText;
        private string OutputText
        {
            get => _outputText;
            set 
            {
                _outputText = value;
                SetOutPutTextBox();
            }
        }       
        private void SetOutPutTextBox() => OutputTextBox.Text = OutputText;

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            switch (EncryptionType)
            {
                case EncryptionTypes.ROT1:
                    Rot1Encryption();
                    break;
                case EncryptionTypes.ROT13:
                    Rot13Encryption();
                    break;
                case EncryptionTypes.Cesar:
                    CesarEncryption();
                    break;
                case EncryptionTypes.Transposition:
                    TranspositionEncryption();
                    break;
                case EncryptionTypes.Binary:
                    Numeral2Encryption();
                    break;
                case EncryptionTypes.Octane:
                    Numeral8Encryption();
                    break;
                case EncryptionTypes.Hex:
                    Numeral16Encryption();
                    break;
                case EncryptionTypes.Vigener:
                    VigenerEncryption();
                    break;
                case EncryptionTypes.RSA:
                    RsaEncryption();
                    break;
                case EncryptionTypes.AES:
                    AesEncryption();
                    break;
                default:
                    OutputText = "";
                    break;
            }
        }


        private bool CheckKeyBoxForNull(string placeholder, Control keyTextBox) => (((TextBox)keyTextBox).Text == "" || ((TextBox)keyTextBox).Text == null || ((TextBox)keyTextBox).Text == placeholder);

        private Control GetControl(string name, StackPanel parentPanel)
        {
            foreach (Control control in parentPanel.Children) if (control.Name == name) return control;
            return null;
        }

        private void Rot1Encryption()
        {
            RotEncryption rotEncryption = new RotEncryption(InputText, RotEncryption.ROT1_SHIFT);

            if (Act == Acts.Crypto) rotEncryption.Crypto();
            else rotEncryption.Decrypto();

            OutputText = rotEncryption.StringOut;
        }
        private void Rot13Encryption()
        {
            RotEncryption rotEncryption = new RotEncryption(InputText, RotEncryption.ROT13_SHIFT);

            if (Act == Acts.Crypto) rotEncryption.Crypto();
            else rotEncryption.Decrypto();

            OutputText = rotEncryption.StringOut;
        }

        private void CesarEncryption()
        {
            CesarEncryption cesarEncryption = new CesarEncryption(InputText);

            if (Act == Acts.Crypto)
            {
                string key = GetSimpleKeyCrypto(cesarEncryption.Alphabet);
                cesarEncryption = new CesarEncryption(InputText, key);
                cesarEncryption.Crypto();
            }
            else
            {
                string key = GetSimpleKeyDecrypto();
                cesarEncryption = new CesarEncryption(InputText, key);
                cesarEncryption.Decrypto();
            }

            OutputText = cesarEncryption.StringOut;
        }

        private string GetSimpleKeyCrypto(char[] alphabet)
        {
            //TODO make notify
            TextBox keyTextBox = (TextBox)GetControl("SimpleKeyTextBox", _keyStackPanel);
            TextBox keyLengthTextBox = (TextBox)GetControl("SimpleKeyLengthTextBox", _keyStackPanel);
            if (!CheckKeyBoxForNull("Ваш ключ...", keyTextBox)) return keyTextBox.Text;
            else
            {
                string key = KeyMethods.KeyGenerate(alphabet, Convert.ToInt32(keyLengthTextBox.Text));
                keyTextBox.Text = key;
                return key;
            }
        }
        private string GetSimpleKeyDecrypto()
        {
            //TODO make notify
            TextBox keyTextBox = (TextBox)GetControl("SimpleKeyTextBox", _keyStackPanel);

            if (!CheckKeyBoxForNull("Ваш ключ...", keyTextBox)) return keyTextBox.Text;
            else return null;
        }


        private void TranspositionEncryption()
        {
            TranspositionEncryption transpositionEncryption = new TranspositionEncryption(InputText);

            if (Act == Acts.Crypto) transpositionEncryption.Crypto();
            else transpositionEncryption.Decrypto();

            OutputText = transpositionEncryption.StringOut;
        }

        private void Numeral2Encryption()
        {
            NumeralEncryption numeral2Encryption = new NumeralEncryption(InputText, 2);

            try
            {
                if (Act == Acts.Crypto) numeral2Encryption.Crypto();
                else numeral2Encryption.Decrypto();
            }
            //TODO change message box to notification
            catch (Exception) { MessageBox.Show("Невірна система числення!!!"); return; }

            OutputText = numeral2Encryption.StringOut;
        }
        private void Numeral8Encryption()
        {
            NumeralEncryption numeral8Encryption = new NumeralEncryption(InputText, 8);

            try
            {
                if (Act == Acts.Crypto) numeral8Encryption.Crypto();
                else numeral8Encryption.Decrypto();
            }
            catch (Exception) { MessageBox.Show("Невірна система числення!!!"); return; }

            OutputText = numeral8Encryption.StringOut;
        }
        private void Numeral16Encryption()
        {
            NumeralEncryption numeral16Encryption = new NumeralEncryption(InputText, 16);

            try
            {
                if (Act == Acts.Crypto) numeral16Encryption.Crypto();
                else numeral16Encryption.Decrypto();
            }
            catch (Exception) { MessageBox.Show("Невірна система числення!!!"); return; }

            OutputText = numeral16Encryption.StringOut;
        }

        private void VigenerEncryption()
        {
            VigenerEncryption vigenerEncryption = new VigenerEncryption(InputText);

            if (Act == Acts.Crypto)
            {
                string key = GetSimpleKeyCrypto(vigenerEncryption.Alphabet);
                vigenerEncryption = new VigenerEncryption(InputText, key);
                vigenerEncryption.Crypto();
            }
            else
            {
                string key = GetSimpleKeyDecrypto();
                vigenerEncryption = new VigenerEncryption(InputText, key);
                vigenerEncryption.Decrypto();
            }

            OutputText = vigenerEncryption.StringOut;
        }

        private void RsaEncryption()
        {
            RsaEncryption rsaEncryption;

            if (_act == Acts.Crypto)
            {
                var (publicKey, generalKey, privateKey) = GetKeysForm.GetPairKeysForCrypto(GeneralKeyBox, AnotherKeyBox, PrivateKeyBox, GeneratePairKeysCheckBox);

                rsaEncryption = new RsaEncryption(_stringIn, publicKey, generalKey, privateKey);
                rsaEncryption.Crypto();
                _stringOut = rsaEncryption.StringOut;
            }
            else
            {
                RsaWaitLabel.Visible = true;

                _stringOut = "";
                try { backgroundWorker.RunWorkerAsync(); }
                catch (InvalidOperationException) { BackgroundWorker_Cancel(); }
            }
        }



        private void AesEncryption()
        {

        }
    }
}
