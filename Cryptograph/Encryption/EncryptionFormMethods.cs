using EncryptionMethods;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsefulMethods;

namespace Cryptograph
{
    public partial class EncryptionForm : Form
    {
        private readonly BackgroundWorker backgroundWorker = new BackgroundWorker();
        private bool backgroundWorkerIsCancelled = false;

        private bool CheckKeys()
        {
            switch (_cryptoType)
            {
                case "Шифр Цезаря":
                    return CheckSimpleKey();
                case "RSA шифрування":
                    return CheckPairKeys();
                case "Шифр Віженера":
                    return CheckSimpleKey();
                case "AES шифрування":
                    return CheckSimpleKey();
                default:
                    return true;
            }
        }
        private bool CheckSimpleKey()
        {
            if (_act == Acts.Crypto)
            {
                if (SimpleKeyCryptoBox.Text == "" && SimpleKeyLengthUpDown.Value <= 0)
                {
                    MessageBox.Show("Потрібно вказати ключ шифрування або \nвказати довжину для генерування ключа");
                    return false;
                }
                else return true;
            }
            else
            {
                if (SimpleKeyDecryptoBox.Text == "")
                {
                    MessageBox.Show("Потрібно вказати ключ шифрування");
                    return false;
                }
                else return true;
            }
        }
        private bool CheckPairKeys()
        {
            if (_act == Acts.Crypto)
            {
                if ((AnotherKeyBox.Text == "" || GeneralKeyBox.Text == "") && !GeneratePairKeysCheckBox.Checked)
                {
                    MessageBox.Show("Потрібно вказати ключі шифрування або \nвказати необхідність генерування ключа");
                    return false;
                }
                if (AnotherKeyBox.Text != "" || GeneralKeyBox.Text != "")
                {
                    try
                    {
                        KeyMethods.KeyStringToNumber(GeneralKeyBox.Text);
                        KeyMethods.KeyStringToNumber(AnotherKeyBox.Text);

                        GetKeysForm.GetPairKeysForCrypto(GeneralKeyBox, AnotherKeyBox, PrivateKeyBox, GeneratePairKeysCheckBox);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Неправильний формат ключа");

                        GeneralKeyBox.Text = "";
                        AnotherKeyBox.Text = "";
                        PrivateKeyBox.Text = "";

                        return false;
                    }
                }
            }
            if (_act == Acts.Decrypto)
            {
                if (AnotherKeyBox.Text == "" || GeneralKeyBox.Text == "")
                {
                    MessageBox.Show("Потрібно вказати ключі шифрування");
                    return false;
                }
                try
                {
                    KeyMethods.KeyStringToNumber(GeneralKeyBox.Text);
                    KeyMethods.KeyStringToNumber(AnotherKeyBox.Text);

                    GetKeysForm.GetSimpleKeyForDecrypto(SimpleKeyDecryptoBox);
                }
                catch (Exception)
                {
                    MessageBox.Show("Неправильний формат ключа");

                    GeneralKeyBox.Text = "";
                    AnotherKeyBox.Text = "";
                    PrivateKeyBox.Text = "";

                    return false;
                }
            }

            return true;
        }


        private void Rot1Encryption()
        {
            RotEncryption rotEncryption = new RotEncryption(_stringIn, RotEncryption.ROT1_SHIFT);

            if (_act == Acts.Crypto) rotEncryption.Crypto();
            else rotEncryption.Decrypto();

            _stringOut = rotEncryption.StringOut;
        }
        private void Rot13Encryption()
        {
            RotEncryption rotEncryption = new RotEncryption(_stringIn, RotEncryption.ROT13_SHIFT);

            if (_act == Acts.Crypto) rotEncryption.Crypto();
            else rotEncryption.Decrypto();

            _stringOut = rotEncryption.StringOut;
        }
        private void CesarEncryption()
        {
            CesarEncryption cesarEncryption = new CesarEncryption(_stringIn);

            if (_act == Acts.Crypto)
            {
                cesarEncryption = new CesarEncryption(_stringIn, GetKeysForm.GetSimpleKeyForCrypto(SimpleKeyCryptoBox, SimpleKeyLengthUpDown, cesarEncryption.Alphabet));
                cesarEncryption.Crypto();
            }
            else
            {
                cesarEncryption = new CesarEncryption(_stringIn, GetKeysForm.GetSimpleKeyForDecrypto(SimpleKeyDecryptoBox));
                cesarEncryption.Decrypto();
            }

            _stringOut = cesarEncryption.StringOut;
        }


        private void TranspositionEncryption()
        {
            TranspositionEncryption transpositionEncryption = new TranspositionEncryption(_stringIn);

            if (_act == Acts.Crypto) transpositionEncryption.Crypto();
            else transpositionEncryption.Decrypto();

            _stringOut = transpositionEncryption.StringOut;
        }


        private void Numeral2Encryption()
        {
            NumeralEncryption numeral2Encryption = new NumeralEncryption(_stringIn, 2);

            try
            {
                if (_act == Acts.Crypto) numeral2Encryption.Crypto();
                else numeral2Encryption.Decrypto();
            }
            catch (Exception) { MessageBox.Show("Невірна система числення!!!"); return; }

            _stringOut = numeral2Encryption.StringOut;
        }
        private void Numeral8Encryption()
        {
            NumeralEncryption numeral8Encryption = new NumeralEncryption(_stringIn, 8);

            try
            {
                if (_act == Acts.Crypto) numeral8Encryption.Crypto();
                else numeral8Encryption.Decrypto();
            }
            catch (Exception) { MessageBox.Show("Невірна система числення!!!"); return; }

            _stringOut = numeral8Encryption.StringOut;
        }
        private void Numeral16Encryption()
        {
            NumeralEncryption numeral16Encryption = new NumeralEncryption(_stringIn, 16);

            try
            {
                if (_act == Acts.Crypto) numeral16Encryption.Crypto();
                else numeral16Encryption.Decrypto();
            }
            catch (Exception) { MessageBox.Show("Невірна система числення!!!"); return; }

            _stringOut = numeral16Encryption.StringOut;
        }


        private void VigenerEncryption()
        {
            VigenerEncryption vigenerEncryption = new VigenerEncryption(_stringIn);

            if (_act == Acts.Crypto)
            {
                vigenerEncryption = new VigenerEncryption(_stringIn, GetKeysForm.GetSimpleKeyForCrypto(SimpleKeyCryptoBox, SimpleKeyLengthUpDown, vigenerEncryption.Alphabet));
                vigenerEncryption.Crypto();
            }
            else
            {
                vigenerEncryption = new VigenerEncryption(_stringIn, GetKeysForm.GetSimpleKeyForDecrypto(SimpleKeyDecryptoBox));
                vigenerEncryption.Decrypto();
            }

            _stringOut = vigenerEncryption.StringOut;
        }


        private void RsaEncryption()
        {
            RsaEncryption rsaEncryption;

            SetRSAKeyPanel();

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

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!backgroundWorkerIsCancelled)
            {
                _stringOut = (string)e.Result;
                SetOutputTextBox();

                RsaWaitLabel.Visible = false;
            }
            else backgroundWorkerIsCancelled = false;
        } 
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            RsaEncryption rsaEncryption;

            var (generalKey, privateKey) = GetKeysForm.GetPairKeysForDecrypto(GeneralKeyBox, AnotherKeyBox);
            rsaEncryption = new RsaEncryption(_stringIn, generalKey, privateKey);
            Task task = Task.Run(() => rsaEncryption.Decrypto());
            task.Wait();

            e.Result = rsaEncryption.StringOut;           
        }
        private void BackgroundWorker_Cancel()
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorkerIsCancelled = true;
                backgroundWorker.CancelAsync();
                backgroundWorker.Dispose();
                RsaWaitLabel.Visible = false;
            }
        }

        private void SetRSAKeyPanel()
        {
            if (_act == Acts.Crypto)
            {
                AnotherKeyNameLabel.Text = "Публічний ключ";
                if (GeneratePairKeysCheckBox.Checked)
                {
                    PrivateKeyBox.Visible = true;
                    PrivateKeyLabel.Visible = true;
                }
            }
        }


        private void AESEncryption()
        {
            AesEncryption aesEncryption = new AesEncryption(_stringIn);

            if (_act == Acts.Crypto)
            {
                var (inputStringFormat, outputStringFormat) = GetStringsFormats();
                try
                {
                    aesEncryption = new AesEncryption(_stringIn, GetKeysForm.GetSimpleKeyForCrypto(SimpleKeyCryptoBox, SimpleKeyLengthUpDown, aesEncryption.Alphabet),
                                                        inputStringFormat, outputStringFormat);
                }
                catch (Exception) { MessageBox.Show("Неправильний формат вхідного тексту"); return; }
                aesEncryption.Crypto();
            }
            if (_act == Acts.Decrypto)
            {
                var (inputStringFormat, outputStringFormat) = GetStringsFormats();
                try
                {
                    aesEncryption = new AesEncryption(_stringIn, GetKeysForm.GetSimpleKeyForDecrypto(SimpleKeyDecryptoBox),
                    inputStringFormat, outputStringFormat);
                }
                catch (Exception) { MessageBox.Show("Неправильний формат вхідного тексту"); _stringOut = ""; return; }
                aesEncryption.Decrypto();
            }

            _stringOut = aesEncryption.StringOut;
        }

        private (AesEncryption.TypesOfInputs, AesEncryption.TypesOfInputs) GetStringsFormats()
        {
            return ((AesEncryption.TypesOfInputs)Enum.Parse(typeof(AesEncryption.TypesOfInputs), InputStringFormatComboBox.SelectedItem.ToString()),
                (AesEncryption.TypesOfInputs)Enum.Parse(typeof(AesEncryption.TypesOfInputs), OutputStringFormatComboBox.SelectedItem.ToString()));
        }
    }
}