using EncryptionMethods;
using FileIOControllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cryptograph
{
    public partial class EncryptionForm : Form
    {
        public enum Acts { Crypto, Decrypto }
        private Acts _act;
        private string _cryptoType;

        private string _stringIn;
        private string _stringOut;

        readonly FileInfo fileSettings = new FileInfo("AppStartSettings.dat");

        public EncryptionForm()
        {
            InitializeComponent();
            InitializeBackgroundWorker();

            CryptoMethodsListBox.SelectedIndex = 0;          

            OpenFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            SaveFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            GetSettings();
        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.WorkerSupportsCancellation = true;
        }

        private void GetSettings()
        {
            using (BinaryReader binaryReader = new BinaryReader(fileSettings.Open(FileMode.OpenOrCreate)))
            {
                if (binaryReader.PeekChar() == -1) return;
                binaryReader.ReadString();

                if (binaryReader.PeekChar() == -1) return;
                _act = (Acts)Enum.Parse(typeof(Acts), binaryReader.ReadString());
                _cryptoType = binaryReader.ReadString();

                ApplySettings();
            }
        }
        private void ApplySettings()
        {
            CryptoMethodsListBox.SelectedItem = _cryptoType;

            if (_act == Acts.Crypto) CryptoButton.Checked = true;
            if (_act == Acts.Decrypto) DecryptoButton.Checked = true;
        }
        private void SetSettings()
        {
            try
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(fileSettings.OpenWrite()))
                {
                    binaryWriter.Write(Program.Forms.Encryption.ToString());
                    binaryWriter.Write(_act.ToString());
                    binaryWriter.Write(_cryptoType);

                    fileSettings.IsReadOnly = true;
                }
            }
            catch (UnauthorizedAccessException) { }
        }

        private void EncryptionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetSettings();

            Application.Exit();
        }


        private void CryptoButton_CheckedChanged(object sender, EventArgs e)
        {
            _act = Acts.Crypto;
            SetLabelsAndPanels();
        }
        private void DecryptoButton_CheckedChanged(object sender, EventArgs e)
        {
            _act = Acts.Decrypto;
            SetLabelsAndPanels();
        }


        private void CryptoMethodsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _cryptoType = CryptoMethodsListBox.SelectedItem.ToString();
            SetLabelsAndPanels();
        }


        private void InputTextBox_TextChanged(object sender, EventArgs e) => OutputTextBox.Text = "";
        private void GetInputTextBox() => _stringIn = InputTextBox.Text;
        private void SetInputTextBox() => InputTextBox.Text = _stringIn;

        private void GetOutputTextBox() => _stringOut = OutputTextBox.Text;
        private void SetOutputTextBox() => OutputTextBox.Text = _stringOut;
        private void OutputTextBox_TextChanged(object sender, EventArgs e) => CopyButton.Visible = OutputTextBox.Text != "";


        private void SetLabelsAndPanels()
        {
            if (_act == Acts.Crypto)
            {
                ActionButton.Text = "Шифрувати";

                if (_cryptoType == "Шифр Цезаря" || _cryptoType == "Шифр Віженера"){ SetSimpleKeysPanelForCrypto(); return; }
                if (_cryptoType == "RSA шифрування"){ SetKeysRSAPanelForCrypto(); return; }
                if (_cryptoType == "AES шифрування"){ SetKeysAESPanelForCrypto(); return; }

                SetSimpleNoKeysPanelForCrypto();
            }
            if (_act == Acts.Decrypto)
            {
                ActionButton.Text = "Розшифрувати";

                if (_cryptoType == "Шифр Цезаря" || _cryptoType == "Шифр Віженера") { SetSimpleKeysPanelForDecrypto(); return; }
                if (_cryptoType == "RSA шифрування") { SetKeysRSAPanelForDecrypto(); return; }
                if (_cryptoType == "AES шифрування") { SetKeysAESPanelForDecrypto(); return; }
                SetSimpleNoKeysPanelForCrypto();
            }
        }

        private void SetSimpleNoKeysPanelForCrypto()
        {
            InputStringFormatComboBox.Visible = false;
            OutputStringFormatComboBox.Visible = false;

            KeysRSAPanel.Visible = false;
            KeyCryptoPanel.Visible = false;
            KeyDecryptoPanel.Visible = false;
        }

        private void SetSimpleKeysPanelForCrypto()
        {
            KeyCryptoPanel.Visible = true;
            KeyDecryptoPanel.Visible = false;
            KeysRSAPanel.Visible = false;

            SimpleKeyCryptoBox.Text = "";
            SimpleKeyLengthUpDown.Value = 0;

            InputStringFormatComboBox.Visible = false;
            OutputStringFormatComboBox.Visible = false;
        }
        private void SetSimpleKeysPanelForDecrypto()
        {
            KeyCryptoPanel.Visible = false;
            KeyDecryptoPanel.Visible = true;
            KeysRSAPanel.Visible = false;

            InputStringFormatComboBox.Visible = false;
            OutputStringFormatComboBox.Visible = false;

            SimpleKeyDecryptoBox.Text = "";
        }

        private void SetKeysRSAPanelForCrypto()
        {
            KeysRSAPanel.Visible = true;
            KeyCryptoPanel.Visible = false;
            KeyDecryptoPanel.Visible = false;

            InputStringFormatComboBox.Visible = false;
            OutputStringFormatComboBox.Visible = false;

            GeneratePairKeysCheckBox.Visible = true;
            AnotherKeyNameLabel.Text = "Публічний ключ";
        }
        private void SetKeysRSAPanelForDecrypto()
        {
            KeysRSAPanel.Visible = true;
            KeyCryptoPanel.Visible = false;
            KeyDecryptoPanel.Visible = false;

            GeneratePairKeysCheckBox.Visible = false;
            AnotherKeyNameLabel.Text = "Приватний ключ";
            AnotherKeyBox.Text = "";
            GeneralKeyBox.Text = "";

            PrivateKeyBox.Visible = false;
            PrivateKeyLabel.Visible = false;
            InputStringFormatComboBox.Visible = false;
            OutputStringFormatComboBox.Visible = false;
        }

        private void SetKeysAESPanelForCrypto()
        {
            KeyCryptoPanel.Visible = true;
            KeyDecryptoPanel.Visible = false;
            KeysRSAPanel.Visible = false;

            SimpleKeyCryptoBox.Text = "";
            SimpleKeyLengthUpDown.Value = 0;

            InputStringFormatComboBox.Visible = true;
            OutputStringFormatComboBox.Visible = true;
            
            InputStringFormatComboBox.Items.Clear();
            InputStringFormatComboBox.Items.Add("UTF8");
            InputStringFormatComboBox.Items.Add("Hex");

            OutputStringFormatComboBox.Items.Clear();
            OutputStringFormatComboBox.Items.Add("Base64");
            OutputStringFormatComboBox.Items.Add("Hex");

            InputStringFormatComboBox.SelectedItem = "UTF8";
            OutputStringFormatComboBox.SelectedItem = "Base64";
        }
        private void SetKeysAESPanelForDecrypto()
        {
            KeyCryptoPanel.Visible = false;
            KeyDecryptoPanel.Visible = true;
            KeysRSAPanel.Visible = false;

            SimpleKeyDecryptoBox.Text = "";

            InputStringFormatComboBox.Visible = true;
            OutputStringFormatComboBox.Visible = true;

            InputStringFormatComboBox.Items.Clear();
            InputStringFormatComboBox.Items.Add("Base64");
            InputStringFormatComboBox.Items.Add("Hex");

            OutputStringFormatComboBox.Items.Clear();
            OutputStringFormatComboBox.Items.Add("UTF8");
            OutputStringFormatComboBox.Items.Add("Hex");

            InputStringFormatComboBox.SelectedItem = "Base64";
            OutputStringFormatComboBox.SelectedItem = "UTF8";
        }


        private void ActionButton_Click(object sender, EventArgs e)
        {
            BackgroundWorker_Cancel();

            GetInputTextBox();
            if (_stringIn == "") { MessageBox.Show("Потрібно вказати вхідний текст"); return; }

            if (!CheckKeys()) return;
            switch (_cryptoType)
            {
                case "ROT1": Rot1Encryption(); break;
                case "ROT13": Rot13Encryption(); break;
                case "Шифр Цезаря": CesarEncryption(); break;
                case "Транспозиція": TranspositionEncryption(); break;
                case "Двійковий код": Numeral2Encryption(); break;
                case "Вісімковий код": Numeral8Encryption(); break;
                case "Шістнадцятковий код": Numeral16Encryption(); break;
                case "Шифр Віженера": VigenerEncryption(); break;
                case "RSA шифрування": RsaEncryption(); break;
                case "AES шифрування": AESEncryption(); break;
                default: _stringOut = ""; break;
            }

            SetOutputTextBox();
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            GetOutputTextBox();
            Clipboard.SetText(_stringOut);
        }


        private void LoadToolStripMenu_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.Cancel) return;
            string filename = OpenFileDialog.FileName;
            _stringIn = TxtFileController.Load(filename);
            SetInputTextBox();
        }
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackgroundWorker_Cancel();

            if (_stringOut == "" || _stringOut == null)
            {
                MessageBox.Show("Відсутній текст для збереження");
                return;
            }
            if (SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;

            string path = SaveFileDialog.FileName;
            TxtFileController.Save(path, _stringOut);
        }

        private void AppModesShorthandMenuItem_Click(object sender, EventArgs e)
        {
            BackgroundWorker_Cancel();
            SetSettings();

            ShorthandForm shorthandForm = new ShorthandForm();
            shorthandForm.Show();

            Hide();
        }
    }
}