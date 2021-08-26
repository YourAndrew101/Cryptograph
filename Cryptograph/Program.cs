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
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form startForm;
            if (GetSettings() == Forms.Encryption.ToString()) startForm = new EncryptionForm();
            else startForm = new ShorthandForm();
            
            Application.Run(startForm);
        }

        private static string GetSettings()
        {
            FileInfo file = new FileInfo("AppStartSettings.dat");
            file.IsReadOnly = false;

            using (BinaryReader binaryReader = new BinaryReader(file.OpenRead())) return binaryReader.ReadString(); 
        }
    }
}
