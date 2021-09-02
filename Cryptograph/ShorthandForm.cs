using FileIOControllers;
using ShortHandMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cryptograph
{
    public partial class ShorthandForm : Form
    {
        private string _cryptoString;
        private Bitmap _image;

        public ShorthandForm()
        {
            InitializeComponent();

            OpenImageFileDialog.Filter = "Image Files(*.BMP)| *.BMP; | All files(*.*) | *.*";
            SaveImageFileDialog.Filter = "Image FilSes(*.BMP)| *.BMP; | All files(*.*) | *.*";

            OpenTextFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            SaveTextFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            InputTextBox.AllowDrop = true;
            InputTextBox.DragDrop += InputTextBox_DragDrop;
            InputTextBox.DragEnter += InputTextBox_DragEnter;

            OutputPictureBox.AllowDrop = true;
            OutputPictureBox.DragDrop += OutputPictureBox_DragDrop; ;
            OutputPictureBox.DragEnter += OutputPictureBox_DragEnter; ;

            InputTextBox_TextChanged(new object(), new EventArgs());
            OutputPictureBoxRefresh();
        }

        private void ShorthandForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetSettings();

            Application.Exit();
        }

        private static void SetSettings()
        {
            FileInfo file = new FileInfo("AppStartSettings.dat");
            try
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(file.Open(FileMode.Create)))
                {
                    binaryWriter.Write(Program.Forms.ShortHand.ToString());
                    file.IsReadOnly = true;
                }
            }
            catch (UnauthorizedAccessException) { }
        }

        private void CryptoButton_Click(object sender, EventArgs e)
        {
            if (_cryptoString == "")
            {
                MessageBox.Show("Вкажіть вхідний текст");
                return;
            }
            if (_image == null)
            {
                MessageBox.Show("Вкажіть вихідне зображення");
                return;
            }

            ShortHand shortHand = new ShortHand(_cryptoString, _image);
            shortHand.Crypto();
            _image = shortHand.OutputImage;

            OutputPictureBoxRefresh();
            MessageBox.Show("Текст записано");
        }
        private void DecryptoButton_Click(object sender, EventArgs e)
        {
            if (_image == null)
            {
                MessageBox.Show("Вкажіть вхідне зображення");
                return;
            }

            ShortHand shortHand = new ShortHand(_image);
            try { shortHand.Decrypto(); }
            catch (BadImageFormatException ex) { MessageBox.Show(ex.Message); }

            _cryptoString = shortHand.StringOut;
            InputTextBoxRefresh();
        }


        private void InputTextBox_TextChanged(object sender, EventArgs e)
        {
            _cryptoString = InputTextBox.Text;

            TextControlsTableLayoutPanel.Visible = _cryptoString != "";
        }
        private void InputTextBoxRefresh() => InputTextBox.Text = _cryptoString;

        private void OutputPictureBoxRefresh()
        {
            OutputPictureBox.Image = _image;
            ImageCopyButton.Visible = ImageDeleteButton.Visible = OutputPictureBox.Image != null;
        }
        private void OutputPictureBox_DoubleClick(object sender, EventArgs e) => OpenImageToolStripMenuItem_Click(sender, e);


        private void OpenImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenImageFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _image = new Bitmap(OpenImageFileDialog.FileName);

            OutputPictureBoxRefresh();
        }
        private void SaveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image == null)
            {
                MessageBox.Show("Відсутнє зображення для збереження");
                return;
            }

            if (SaveImageFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _image.Save(SaveImageFileDialog.FileName);
        }

        private void OpenTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenTextFileDialog.ShowDialog() == DialogResult.Cancel) return;
            string filename = OpenTextFileDialog.FileName;
            _cryptoString = TxtFileController.Load(filename);
            InputTextBoxRefresh();
        }
        private void SaveTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_cryptoString == null)
            {
                MessageBox.Show("Відсутній текст для збереження");
                return;
            }

            if (SaveTextFileDialog.ShowDialog() == DialogResult.Cancel) return;
            TxtFileController.Save(SaveTextFileDialog.FileName, _cryptoString);
        }

        private void AppModesShorthandMenuItem_Click(object sender, EventArgs e)
        {
            SetSettings();

            EncryptionForm encryptionForm = new EncryptionForm();
            encryptionForm.Show();

            Hide();
        }


        private void ImageDeleteButton_Click(object sender, EventArgs e)
        {
            _image = null;
            OutputPictureBoxRefresh();
        }
        private void ImageCopyButton_Click(object sender, EventArgs e) => Clipboard.SetImage(_image);

        private void TextDeleteButton_Click(object sender, EventArgs e) => InputTextBox.Text = "";
        private void TextCopyButton_Click(object sender, EventArgs e) => Clipboard.SetText(_cryptoString);

        private void ImagePasteButton_Click(object sender, EventArgs e)
        {
            _image = Clipboard.GetImage() as Bitmap;
            OutputPictureBoxRefresh();
        }


        private void InputTextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void InputTextBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            StringBuilder sb = new StringBuilder();

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);

                if (fileInfo.Extension == ".txt")
                    using (StreamReader sr = fileInfo.OpenText())
                        sb.Append(sr.ReadToEnd());
                else { MessageBox.Show("Неправильний формат файлу"); return; }

                sb.Append("\n");
            }

            InputTextBox.Text = sb.ToString();
        }

        private void OutputPictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void OutputPictureBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);

            FileInfo fileInfo = new FileInfo(file[0]);

            if (fileInfo.Extension == ".bmp")
            {
                _image = new Bitmap(file[0]);
                OutputPictureBoxRefresh();
            }
            else { MessageBox.Show("Неправильний формат файлу"); }
        }
    }
}
