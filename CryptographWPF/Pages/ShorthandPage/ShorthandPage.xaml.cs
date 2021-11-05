using Microsoft.Win32;
using ShortHandMethods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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

        private BitmapImage _image;
        private BitmapImage _Image
        {
            get => _image;
            set
            {
                _image = value;
                RefreshImageDockPanel();
            }
        }

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
            _Image = new BitmapImage(new Uri(_openImageFileDialog.FileName));
        }

        private void RefreshImageDockPanel()
        {
            ActionImage.Source = _Image;
            ImageDragNDropImage.Visibility = Visibility.Hidden;
            ImageDragNDropText.Visibility = Visibility.Hidden;
            ImageBorder.BorderThickness = new Thickness(0);
        }

        private void ToImageActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (In_OutTextBox.Text == "")
            {
                MessageBox.Show("Вкажіть вхідний текст");
                return;
            }
            if (ActionImage.Source == null)
            {
                MessageBox.Show("Вкажіть вихідне зображення");
                return;
            }

            ShortHand shortHand = new ShortHand(In_OutTextBox.Text, BitmapImage2Bitmap(_Image));
            shortHand.Crypto();
            //_Image = BitmapToImageSource(shortHand.OutputImage);

            MessageBox.Show("Текст записано");
        }

        private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }
    }
}
