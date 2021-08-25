using Cryptograph.Properties;
using FileIOControllers;
using ShortHandMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            InputTextBox_TextChanged(new object(), new EventArgs());
            OutputPictureBoxRefresh();
        }
        private void ShorthandForm_FormClosing(object sender, FormClosingEventArgs e) => Application.Exit();


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

            if (OutputPictureBox.Image != null)
            {
                ImageCopyButton.Visible = ImageDeleteButton.Visible = true;
            }
            else
            {
                ImageCopyButton.Visible = ImageDeleteButton.Visible = false;
            }
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
    }
}
