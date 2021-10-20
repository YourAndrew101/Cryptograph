using EncryptionMethods;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UsefulMethods;

namespace CryptographWPF.Pages
{
    public partial class EncryptionPage : Page
    {
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();
        private bool _backgroundWorkerIsCancelled = false;

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
                    AESEncryption();
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
        private Control GetControl(string name, DockPanel parentPanel)
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
            //TODO RSAWaitLabel
            RsaEncryption rsaEncryption;

            if (_act == Acts.Crypto)
            {
                var (publicKey, generalKey, privateKey) = GetPairKeysCrypto();

                rsaEncryption = new RsaEncryption(InputText, publicKey, generalKey, privateKey);
                rsaEncryption.Crypto();
                OutputText = rsaEncryption.StringOut;
            }
            else
            {
                var (_generalKey, _privateKey) = GetPairKeysDecrypto();
                privateKey = _privateKey;
                generalKey = _generalKey;
                OutputText = "";

                try { _backgroundWorker.RunWorkerAsync(); }
                catch (InvalidOperationException) { BackgroundWorker_Cancel(); }
            }
        }
        private ulong privateKey, generalKey;

        private (ulong, ulong, ulong) GetPairKeysCrypto()
        {
            //TODO make notify
            ulong _publicKey = 0, _generalKey = 0, _privateKey = 0;

            TextBox generalKeyTextBox = (TextBox)GetControl("GeneralKeyTextBox", _keyStackPanel);
            TextBox publicKeyTextBox = (TextBox)GetControl("PublicKeyTextBox", _keyStackPanel);
            TextBox privateKeyTextBox = (TextBox)GetControl("PrivateKeyTextBox", _keyStackPanel);

            CheckBox generateKeysCheckBox = (CheckBox)GetControl("GenerateKeysCheckBox", _keyStackPanel);

            if (generateKeysCheckBox.IsChecked == true)
            {
                KeyMethods.CalculateKeys(out _generalKey, out _publicKey, out _privateKey);

                generalKeyTextBox.Text = KeyMethods.KeyNumberToString(_generalKey);
                publicKeyTextBox.Text = KeyMethods.KeyNumberToString(_generalKey);
                privateKeyTextBox.Text = KeyMethods.KeyNumberToString(_privateKey);
            }
            else
            {
                if (!CheckKeyBoxForNull("Загальний ключ...", generalKeyTextBox)) _publicKey = Convert.ToUInt64(generalKeyTextBox.Text);
                if (!CheckKeyBoxForNull("Публічний ключ...", publicKeyTextBox)) _generalKey = Convert.ToUInt64(generalKeyTextBox.Text);
                if (!CheckKeyBoxForNull("Приватний ключ...", privateKeyTextBox)) _privateKey = Convert.ToUInt64(generalKeyTextBox.Text);
            }

            return (_publicKey, _generalKey, _privateKey);
        }
        private (ulong, ulong) GetPairKeysDecrypto()
        {
            //TODO make notify
            ulong _generalKey = 0, _privateKey = 0;

            TextBox generalKeyTextBox = (TextBox)GetControl("GeneralKeyTextBox", _keyStackPanel);
            TextBox privateKeyTextBox = (TextBox)GetControl("PrivateKeyTextBox", _keyStackPanel);


            if (!CheckKeyBoxForNull("Загальний ключ...", generalKeyTextBox)) _generalKey = KeyMethods.KeyStringToNumber(generalKeyTextBox.Text);
            if (!CheckKeyBoxForNull("Приватний ключ...", privateKeyTextBox)) _privateKey = KeyMethods.KeyStringToNumber(privateKeyTextBox.Text);

            return (_generalKey, _privateKey);
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!_backgroundWorkerIsCancelled)
            {
                OutputText = (string)e.Result;

                //RsaWaitLabel.Visible = false;
            }
            else _backgroundWorkerIsCancelled = false;
        }
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            RsaEncryption rsaEncryption = new RsaEncryption(InputText, generalKey, privateKey);
            Task task = Task.Run(() => rsaEncryption.Decrypto());
            task.Wait();

            e.Result = rsaEncryption.StringOut;
        }
        private void BackgroundWorker_Cancel()
        {
            if (_backgroundWorker.IsBusy)
            {
                _backgroundWorkerIsCancelled = true;
                _backgroundWorker.CancelAsync();
                _backgroundWorker.Dispose();
                //RsaWaitLabel.Visible = false;
            }
        }


        private void AESEncryption()
        {
            AesEncryption aesEncryption = new AesEncryption(InputText);

            if (_act == Acts.Crypto)
            {
                var (inputStringFormat, outputStringFormat) = GetStringsFormats();
                try
                {
                    string key = GetSimpleKeyCrypto(aesEncryption.Alphabet);
                    aesEncryption = new AesEncryption(InputText, key, inputStringFormat, outputStringFormat);
                }
                catch (Exception) { MessageBox.Show("Неправильний формат вхідного тексту"); return; }
                aesEncryption.Crypto();
            }
            if (_act == Acts.Decrypto)
            {
                var (inputStringFormat, outputStringFormat) = GetStringsFormats();
                try
                {
                    string key = GetSimpleKeyDecrypto();
                    aesEncryption = new AesEncryption(InputText, key, inputStringFormat, outputStringFormat);
                }
                catch (Exception) { return; }
                aesEncryption.Decrypto();
            }

            OutputText = aesEncryption.StringOut;
        }

        private (AesEncryption.TypesOfInputs, AesEncryption.TypesOfInputs) GetStringsFormats()
        {
            ComboBox inputEncodingcomboBox = (ComboBox)GetControl("EncodingComboBox", _encodingInDockPanel);
            ComboBox outputEncodingcomboBox = (ComboBox)GetControl("EncodingComboBox", _encodingOutDockPanel);

            ComboBoxItem selectedItemInputEncoding = (ComboBoxItem)inputEncodingcomboBox.SelectedItem;
            ComboBoxItem selectedItemOutputEncoding = (ComboBoxItem)outputEncodingcomboBox.SelectedItem;


            return ((AesEncryption.TypesOfInputs)Enum.Parse(typeof(AesEncryption.TypesOfInputs), selectedItemInputEncoding.Content.ToString()),
                (AesEncryption.TypesOfInputs)Enum.Parse(typeof(AesEncryption.TypesOfInputs), selectedItemOutputEncoding.Content.ToString()));
        }


    }
}
