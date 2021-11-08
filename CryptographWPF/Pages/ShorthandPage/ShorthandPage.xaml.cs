using FileIOControllers;
using Microsoft.Win32;
using ShortHandMethods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace CryptographWPF.Pages
{
    public partial class ShorthandPage : Page
    {
        private string _textPlaceholder = "Ваш текст...";

        private OpenFileDialog _openImageFileDialog = new OpenFileDialog();
        private OpenFileDialog _openTextFileDialog = new OpenFileDialog();
        private SaveFileDialog _saveImageFileDialog = new SaveFileDialog();
        private SaveFileDialog _saveTextFileDialog = new SaveFileDialog();

        private Bitmap _image;
        private Bitmap _Image
        {
            get => _image;
            set
            {
                _image = value;
                RefreshImageDockPanel();
            }
        }

        private string _cryptoString;
        private string CryptoString
        {
            get => (_cryptoString == _textPlaceholder || _cryptoString == null) ? "" : _cryptoString;
            set
            {
                _cryptoString = value;
                RefreshTextBox();
            }
        }


        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteObject([In] IntPtr hObject);



        private bool CheckImageNull(Bitmap image) => image == null || image.Width == 1 || image.Height == 1;


        public ShorthandPage()
        {
            InitializeComponent();
            InitializeFileDialoges();
        }

        private void InitializeFileDialoges()
        {
            _openImageFileDialog.Filter = "Image Files(*.BMP)| *.BMP; | All files(*.*) | *.*";
            _saveImageFileDialog.Filter = "Image FilSes(*.BMP)| *.BMP; | All files(*.*) | *.*";

            _saveTextFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            _openTextFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }


        private void In_OutTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == _textPlaceholder) ((TextBox)sender).Text = "";
        }
        private void In_OutTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "" || ((TextBox)sender).Text == null) ((TextBox)sender).Text = _textPlaceholder;
        }
        private void In_OutTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _cryptoString = In_OutTextBox.Text;
        }


        private void ImageDockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                GetPictureFromFileDialog();
            }
        }
        private void GetPictureFromFileDialog()
        {
            if (_openImageFileDialog.ShowDialog() == false) return;
            _Image = new Bitmap(_openImageFileDialog.FileName);
        }


        private void RefreshImageDockPanel()
        {
            ActionImage.Source = ImageSourceFromBitmap(_Image);
            ImageSource ImageSourceFromBitmap(Bitmap bmp)
            {
                var handle = bmp.GetHbitmap();
                try
                {
                    return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
                finally { DeleteObject(handle); }
            }

            ImageDragNDropImage.Visibility = Visibility.Hidden;
            ImageDragNDropText.Visibility = Visibility.Hidden;
            ImageBorder.BorderThickness = new Thickness(0);
        }
        private void RefreshTextBox() => In_OutTextBox.Text = CryptoString;


        private void ToImageActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (CryptoString == "")
            {
                //TODO make notify
                MessageBox.Show("Вкажіть вхідний текст");
                return;
            }
            if (CheckImageNull(_Image))
            {
                MessageBox.Show("Вкажіть вихідне зображення");
                return;
            }

            ShortHand shortHand = new ShortHand(CryptoString, _Image);
            shortHand.Crypto();
            _Image = shortHand.OutputImage;

            MessageBox.Show("Текст записано");
        }
        private void ToTextActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckImageNull(_Image))
            {
                MessageBox.Show("Вкажіть вхідне зображення");
                return;
            }

            ShortHand shortHand = new ShortHand(_Image);
            try { shortHand.Decrypto(); }
            catch (BadImageFormatException ex) { MessageBox.Show(ex.Message); }

            CryptoString = shortHand.StringOut;
        }


        private void DeleteImageButton_Click(object sender, RoutedEventArgs e)
        {
            _Image = new Bitmap(1, 1);

            ImageDragNDropImage.Visibility = Visibility.Visible;
            ImageDragNDropText.Visibility = Visibility.Visible;
            ImageBorder.BorderThickness = new Thickness(2);
        }
        private void SaveImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckImageNull(_Image))
            {
                MessageBox.Show("Відсутнє зображення для збереження");
                return;
            }

            if (_saveImageFileDialog.ShowDialog() == false) return;
            _Image.Save(_saveImageFileDialog.FileName);
        }
        private void PasteImageButton_Click(object sender, RoutedEventArgs e) => _Image = System.Windows.Forms.Clipboard.GetImage() as Bitmap;

        private void PasteTextButton_Click(object sender, RoutedEventArgs e) => CryptoString = Clipboard.GetText();
        private void SaveTextButton_Click(object sender, RoutedEventArgs e)
        {
            if (CryptoString == "")
            {
                MessageBox.Show("Відсутній текст для збереження");
                return;
            }

            if (_saveTextFileDialog.ShowDialog() == false) return;
            TxtFileController.Save(_saveTextFileDialog.FileName, CryptoString);
        }
        private void LoadTextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_openTextFileDialog.ShowDialog() == false) return;
            CryptoString = TxtFileController.Load(_openTextFileDialog.FileName);
        }


        private void ImageBorder_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                _Image = new Bitmap(files[0]);
            }
        }
    }
}
