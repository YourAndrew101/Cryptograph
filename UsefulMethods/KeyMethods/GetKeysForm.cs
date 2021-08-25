using ConsoleIO;
using System;
using System.Windows.Forms;

namespace UsefulMethods
{
    public static class GetKeysForm
    {
        public static string GetSimpleKeyForCrypto(TextBox keyStringBox, NumericUpDown keyLengthUpDown, char[] alphabet)
        {
            if (keyStringBox.Text == "") keyStringBox.Text = KeyMethods.Key_Generate(alphabet, (int)keyLengthUpDown.Value);

            return keyStringBox.Text;
        }
        public static string GetSimpleKeyForDecrypto(TextBox keyStringBox) => keyStringBox.Text;

        public static (ulong, ulong, ulong) GetPairKeysForCrypto(TextBox generalKeyBox, TextBox publicKeyBox,
            TextBox privateKeyBox, CheckBox generateKeysCheckBox)
        {
            ulong generalKey, publicKey, privateKey = 0;

            if (generateKeysCheckBox.Checked)
            {
                KeyMethods.CalculateKeys(out generalKey, out publicKey, out privateKey);

                generalKeyBox.Text = KeyMethods.KeyNumberToString(generalKey);
                publicKeyBox.Text = KeyMethods.KeyNumberToString(publicKey);
                privateKeyBox.Text = KeyMethods.KeyNumberToString(privateKey);

                privateKeyBox.Visible = true;
            }
            else
            {
                generalKey = KeyMethods.KeyStringToNumber(generalKeyBox.Text);
                publicKey = KeyMethods.KeyStringToNumber(publicKeyBox.Text);
            }

            return (publicKey, generalKey, privateKey);
        }
        public static (ulong, ulong) GetPairKeysForDecrypto(TextBox generalKeyBox, TextBox privateKeyBox)
        {
            ulong generalKey = KeyMethods.KeyStringToNumber(generalKeyBox.Text);
            ulong privateKey = KeyMethods.KeyStringToNumber(privateKeyBox.Text);

            return (generalKey, privateKey);
        }
    }
}
