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

        private List<CommandInfo> commandInfoList = new List<CommandInfo>() {
                new CommandInfo(EncryptionTypes.ROT1.ToString(), Rot1Encryption),
                new CommandInfo(EncryptionTypes.ROT13.ToString(), Rot13Encryption),
                new CommandInfo(EncryptionTypes.Cesar.ToString(), CesarEncryptio),
                new CommandInfo(EncryptionTypes.Transposition.ToString(), TranspositionEncryption),
                new CommandInfo(EncryptionTypes.Binary.ToString(), Numeral2Encryption),
                new CommandInfo(EncryptionTypes.Octane.ToString(), Numeral8Encryption),
                new CommandInfo(EncryptionTypes.Hex.ToString(), Numeral16Encryption),
                new CommandInfo(EncryptionTypes.Vigener.ToString(), VigenerEncryption),
                new CommandInfo(EncryptionTypes.RSA.ToString(), RsaEncryption),
                new CommandInfo(EncryptionTypes.AES.ToString(), AesEncryption),
            };

        private static Acts StaticAct;
        private static string StaticInputText;
        private static string StaticOutputText;

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            StaticAct = Act;
            StaticInputText = InputText;         
            GetCommand().Invoke();
            OutputText = StaticOutputText;
        }
        private Command GetCommand() => commandInfoList.First(e => e.Name == EncryptionType.ToString()).Command;

        private void SetOutPutTextBox() => OutputTextBox.Text = OutputText;

        private static void Rot1Encryption()
        {
            RotEncryption rotEncryption = new RotEncryption(StaticInputText, RotEncryption.ROT1_SHIFT);

            if (StaticAct == Acts.Crypto) rotEncryption.Crypto();
            else rotEncryption.Decrypto();

            StaticOutputText = rotEncryption.StringOut;
        }
        private static void Rot13Encryption()
        {

        }
        private static void CesarEncryptio()
        {

        }
        private static void TranspositionEncryption()
        {

        }
        private static void Numeral2Encryption()
        {

        }
        private static void Numeral8Encryption()
        {

        }
        private static void Numeral16Encryption()
        {

        }
        private static void VigenerEncryption()
        {

        }
        private static void RsaEncryption()
        {

        }
        private static void AesEncryption()
        {

        }
    }
}
