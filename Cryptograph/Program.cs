using EncryptionMethods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cryptograph
{
    public static class Program
    {
        public enum Forms { Encryption, ShortHand }
        /// <summary>S
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form startForm;
            if (GetSettings() == Forms.ShortHand.ToString()) startForm = new ShorthandForm();
            else startForm = new EncryptionForm();

            Application.Run(startForm);
        }

        private static string GetSettings()
        {
            FileInfo file = new FileInfo("AppStartSettings.dat");
            if (file.Exists) file.IsReadOnly = false;
            else return "";

            using (BinaryReader binaryReader = new BinaryReader(file.OpenRead())) return binaryReader.ReadString();
        }
    }
}
