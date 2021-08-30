
namespace Cryptograph
{
    partial class EncryptionForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.InputTextBox = new System.Windows.Forms.RichTextBox();
            this.OutputTextBox = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.CryptoButton = new System.Windows.Forms.RadioButton();
            this.DecryptoButton = new System.Windows.Forms.RadioButton();
            this.ActionButton = new System.Windows.Forms.Button();
            this.CryptoMethodsListBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.KeysRSAPanel = new System.Windows.Forms.Panel();
            this.GeneratePairKeysCheckBox = new System.Windows.Forms.CheckBox();
            this.PrivateKeyLabel = new System.Windows.Forms.Label();
            this.PrivateKeyBox = new System.Windows.Forms.TextBox();
            this.GeneralKeyName = new System.Windows.Forms.Label();
            this.AnotherKeyBox = new System.Windows.Forms.TextBox();
            this.AnotherKeyNameLabel = new System.Windows.Forms.Label();
            this.GeneralKeyBox = new System.Windows.Forms.TextBox();
            this.KeyCryptoPanel = new System.Windows.Forms.Panel();
            this.SimpleKeyLengthUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SimpleKeyCryptoBox = new System.Windows.Forms.TextBox();
            this.OR_label = new System.Windows.Forms.Label();
            this.KeyDecryptoPanel = new System.Windows.Forms.Panel();
            this.SimpleKeyDecryptoBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RsaWaitLabel = new System.Windows.Forms.Label();
            this.GeneralMenuStrip = new System.Windows.Forms.MenuStrip();
            this.AppModesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AppModesShorthandMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyButton = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.KeysRSAPanel.SuspendLayout();
            this.KeyCryptoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SimpleKeyLengthUpDown)).BeginInit();
            this.KeyDecryptoPanel.SuspendLayout();
            this.GeneralMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 461F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.InputTextBox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.OutputTextBox, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.RsaWaitLabel, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.GeneralMenuStrip, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CopyButton, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 218F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.39069F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 395F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1246, 1055);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // InputTextBox
            // 
            this.InputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputTextBox.Location = new System.Drawing.Point(3, 308);
            this.InputTextBox.Name = "InputTextBox";
            this.InputTextBox.Size = new System.Drawing.Size(386, 349);
            this.InputTextBox.TabIndex = 0;
            this.InputTextBox.Text = "";
            this.InputTextBox.TextChanged += new System.EventHandler(this.InputTextBox_TextChanged);
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputTextBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.OutputTextBox.Location = new System.Drawing.Point(856, 308);
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.Size = new System.Drawing.Size(387, 349);
            this.OutputTextBox.TabIndex = 1;
            this.OutputTextBox.Text = "";
            this.OutputTextBox.TextChanged += new System.EventHandler(this.OutputTextBox_TextChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.52381F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel2.Controls.Add(this.CryptoButton, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.DecryptoButton, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.ActionButton, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.CryptoMethodsListBox, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(395, 308);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.01087F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.67391F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.20652F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.782609F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.1087F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.67391F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(455, 349);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // CryptoButton
            // 
            this.CryptoButton.AutoSize = true;
            this.CryptoButton.Checked = true;
            this.CryptoButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CryptoButton.Location = new System.Drawing.Point(65, 164);
            this.CryptoButton.Name = "CryptoButton";
            this.CryptoButton.Size = new System.Drawing.Size(341, 24);
            this.CryptoButton.TabIndex = 0;
            this.CryptoButton.TabStop = true;
            this.CryptoButton.Text = "Шифрувати";
            this.CryptoButton.UseVisualStyleBackColor = true;
            this.CryptoButton.CheckedChanged += new System.EventHandler(this.CryptoButton_CheckedChanged);
            // 
            // DecryptoButton
            // 
            this.DecryptoButton.AutoSize = true;
            this.DecryptoButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DecryptoButton.Location = new System.Drawing.Point(65, 198);
            this.DecryptoButton.Name = "DecryptoButton";
            this.DecryptoButton.Size = new System.Drawing.Size(341, 24);
            this.DecryptoButton.TabIndex = 1;
            this.DecryptoButton.Text = "Розшифрувати";
            this.DecryptoButton.UseVisualStyleBackColor = true;
            this.DecryptoButton.CheckedChanged += new System.EventHandler(this.DecryptoButton_CheckedChanged);
            // 
            // ActionButton
            // 
            this.ActionButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ActionButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ActionButton.Location = new System.Drawing.Point(65, 257);
            this.ActionButton.Name = "ActionButton";
            this.ActionButton.Size = new System.Drawing.Size(341, 35);
            this.ActionButton.TabIndex = 2;
            this.ActionButton.Text = "Шифрувати";
            this.ActionButton.UseVisualStyleBackColor = true;
            this.ActionButton.Click += new System.EventHandler(this.ActionButton_Click);
            // 
            // CryptoMethodsListBox
            // 
            this.CryptoMethodsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CryptoMethodsListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CryptoMethodsListBox.FormattingEnabled = true;
            this.CryptoMethodsListBox.Items.AddRange(new object[] {
            "ROT1",
            "ROT13",
            "Шифр Цезаря",
            "Транспозиція",
            "Двійковий код",
            "Вісімковий код",
            "Шістнадцятковий код",
            "Шифр Віженера",
            "RSA шифрування",
            "AES шифрування"});
            this.CryptoMethodsListBox.Location = new System.Drawing.Point(65, 80);
            this.CryptoMethodsListBox.Name = "CryptoMethodsListBox";
            this.CryptoMethodsListBox.Size = new System.Drawing.Size(341, 28);
            this.CryptoMethodsListBox.TabIndex = 3;
            this.CryptoMethodsListBox.SelectedIndexChanged += new System.EventHandler(this.CryptoMethodsListBox_SelectedIndexChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 455F));
            this.tableLayoutPanel3.Controls.Add(this.KeysRSAPanel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.KeyCryptoPanel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.KeyDecryptoPanel, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(395, 663);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.20513F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 89F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 154F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(455, 389);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // KeysRSAPanel
            // 
            this.KeysRSAPanel.Controls.Add(this.GeneratePairKeysCheckBox);
            this.KeysRSAPanel.Controls.Add(this.PrivateKeyLabel);
            this.KeysRSAPanel.Controls.Add(this.PrivateKeyBox);
            this.KeysRSAPanel.Controls.Add(this.GeneralKeyName);
            this.KeysRSAPanel.Controls.Add(this.AnotherKeyBox);
            this.KeysRSAPanel.Controls.Add(this.AnotherKeyNameLabel);
            this.KeysRSAPanel.Controls.Add(this.GeneralKeyBox);
            this.KeysRSAPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.KeysRSAPanel.Location = new System.Drawing.Point(3, 238);
            this.KeysRSAPanel.Name = "KeysRSAPanel";
            this.KeysRSAPanel.Size = new System.Drawing.Size(449, 148);
            this.KeysRSAPanel.TabIndex = 7;
            this.KeysRSAPanel.Visible = false;
            // 
            // GeneratePairKeysCheckBox
            // 
            this.GeneratePairKeysCheckBox.AutoSize = true;
            this.GeneratePairKeysCheckBox.Location = new System.Drawing.Point(81, 118);
            this.GeneratePairKeysCheckBox.Name = "GeneratePairKeysCheckBox";
            this.GeneratePairKeysCheckBox.Size = new System.Drawing.Size(264, 24);
            this.GeneratePairKeysCheckBox.TabIndex = 7;
            this.GeneratePairKeysCheckBox.Text = "Згенерувати ключі випадково";
            this.GeneratePairKeysCheckBox.UseVisualStyleBackColor = true;
            // 
            // PrivateKeyLabel
            // 
            this.PrivateKeyLabel.Location = new System.Drawing.Point(255, 83);
            this.PrivateKeyLabel.Margin = new System.Windows.Forms.Padding(0);
            this.PrivateKeyLabel.Name = "PrivateKeyLabel";
            this.PrivateKeyLabel.Size = new System.Drawing.Size(148, 25);
            this.PrivateKeyLabel.TabIndex = 6;
            this.PrivateKeyLabel.Text = "Приватний ключ";
            this.PrivateKeyLabel.Visible = false;
            // 
            // PrivateKeyBox
            // 
            this.PrivateKeyBox.Location = new System.Drawing.Point(62, 80);
            this.PrivateKeyBox.Name = "PrivateKeyBox";
            this.PrivateKeyBox.Size = new System.Drawing.Size(174, 26);
            this.PrivateKeyBox.TabIndex = 5;
            this.PrivateKeyBox.Visible = false;
            // 
            // GeneralKeyName
            // 
            this.GeneralKeyName.Location = new System.Drawing.Point(42, 45);
            this.GeneralKeyName.Margin = new System.Windows.Forms.Padding(0);
            this.GeneralKeyName.Name = "GeneralKeyName";
            this.GeneralKeyName.Size = new System.Drawing.Size(134, 25);
            this.GeneralKeyName.TabIndex = 3;
            this.GeneralKeyName.Text = "Загальний ключ";
            // 
            // AnotherKeyBox
            // 
            this.AnotherKeyBox.Location = new System.Drawing.Point(259, 9);
            this.AnotherKeyBox.Name = "AnotherKeyBox";
            this.AnotherKeyBox.Size = new System.Drawing.Size(162, 26);
            this.AnotherKeyBox.TabIndex = 2;
            // 
            // AnotherKeyNameLabel
            // 
            this.AnotherKeyNameLabel.AutoSize = true;
            this.AnotherKeyNameLabel.Location = new System.Drawing.Point(274, 45);
            this.AnotherKeyNameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.AnotherKeyNameLabel.Name = "AnotherKeyNameLabel";
            this.AnotherKeyNameLabel.Size = new System.Drawing.Size(129, 20);
            this.AnotherKeyNameLabel.TabIndex = 1;
            this.AnotherKeyNameLabel.Text = "Публічний ключ";
            // 
            // GeneralKeyBox
            // 
            this.GeneralKeyBox.Location = new System.Drawing.Point(19, 9);
            this.GeneralKeyBox.Name = "GeneralKeyBox";
            this.GeneralKeyBox.Size = new System.Drawing.Size(183, 26);
            this.GeneralKeyBox.TabIndex = 0;
            // 
            // KeyCryptoPanel
            // 
            this.KeyCryptoPanel.Controls.Add(this.SimpleKeyLengthUpDown);
            this.KeyCryptoPanel.Controls.Add(this.label2);
            this.KeyCryptoPanel.Controls.Add(this.label1);
            this.KeyCryptoPanel.Controls.Add(this.SimpleKeyCryptoBox);
            this.KeyCryptoPanel.Controls.Add(this.OR_label);
            this.KeyCryptoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.KeyCryptoPanel.Location = new System.Drawing.Point(3, 149);
            this.KeyCryptoPanel.Name = "KeyCryptoPanel";
            this.KeyCryptoPanel.Size = new System.Drawing.Size(449, 83);
            this.KeyCryptoPanel.TabIndex = 4;
            this.KeyCryptoPanel.Visible = false;
            // 
            // SimpleKeyLengthUpDown
            // 
            this.SimpleKeyLengthUpDown.Location = new System.Drawing.Point(244, 9);
            this.SimpleKeyLengthUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.SimpleKeyLengthUpDown.Name = "SimpleKeyLengthUpDown";
            this.SimpleKeyLengthUpDown.Size = new System.Drawing.Size(150, 26);
            this.SimpleKeyLengthUpDown.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(242, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 65);
            this.label2.TabIndex = 1;
            this.label2.Text = "Довжина ключа для генерації ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ключ шифрування";
            // 
            // SimpleKeyCryptoBox
            // 
            this.SimpleKeyCryptoBox.Location = new System.Drawing.Point(0, 9);
            this.SimpleKeyCryptoBox.Name = "SimpleKeyCryptoBox";
            this.SimpleKeyCryptoBox.Size = new System.Drawing.Size(139, 26);
            this.SimpleKeyCryptoBox.TabIndex = 0;
            // 
            // OR_label
            // 
            this.OR_label.AutoSize = true;
            this.OR_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OR_label.Location = new System.Drawing.Point(162, 9);
            this.OR_label.Margin = new System.Windows.Forms.Padding(10, 20, 0, 0);
            this.OR_label.Name = "OR_label";
            this.OR_label.Size = new System.Drawing.Size(65, 29);
            this.OR_label.TabIndex = 6;
            this.OR_label.Text = "АБО";
            // 
            // KeyDecryptoPanel
            // 
            this.KeyDecryptoPanel.Controls.Add(this.SimpleKeyDecryptoBox);
            this.KeyDecryptoPanel.Controls.Add(this.label3);
            this.KeyDecryptoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.KeyDecryptoPanel.Location = new System.Drawing.Point(3, 3);
            this.KeyDecryptoPanel.Name = "KeyDecryptoPanel";
            this.KeyDecryptoPanel.Size = new System.Drawing.Size(449, 108);
            this.KeyDecryptoPanel.TabIndex = 6;
            this.KeyDecryptoPanel.Visible = false;
            // 
            // SimpleKeyDecryptoBox
            // 
            this.SimpleKeyDecryptoBox.Location = new System.Drawing.Point(130, 11);
            this.SimpleKeyDecryptoBox.Name = "SimpleKeyDecryptoBox";
            this.SimpleKeyDecryptoBox.Size = new System.Drawing.Size(139, 26);
            this.SimpleKeyDecryptoBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(128, 40);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ключ шифрування";
            // 
            // RsaWaitLabel
            // 
            this.RsaWaitLabel.AutoSize = true;
            this.RsaWaitLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.RsaWaitLabel.Font = new System.Drawing.Font("Perpetua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RsaWaitLabel.Location = new System.Drawing.Point(856, 660);
            this.RsaWaitLabel.Name = "RsaWaitLabel";
            this.RsaWaitLabel.Size = new System.Drawing.Size(387, 28);
            this.RsaWaitLabel.TabIndex = 4;
            this.RsaWaitLabel.Text = "Дані розшифровуються...";
            this.RsaWaitLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.RsaWaitLabel.Visible = false;
            // 
            // GeneralMenuStrip
            // 
            this.GeneralMenuStrip.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.GeneralMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.GeneralMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AppModesMenu,
            this.FileToolStripMenu});
            this.GeneralMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.GeneralMenuStrip.Name = "GeneralMenuStrip";
            this.GeneralMenuStrip.Size = new System.Drawing.Size(392, 33);
            this.GeneralMenuStrip.TabIndex = 5;
            this.GeneralMenuStrip.Text = "menuStrip1";
            // 
            // AppModesMenu
            // 
            this.AppModesMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AppModesShorthandMenuItem});
            this.AppModesMenu.Name = "AppModesMenu";
            this.AppModesMenu.Size = new System.Drawing.Size(180, 29);
            this.AppModesMenu.Text = "Режими програми";
            // 
            // AppModesShorthandMenuItem
            // 
            this.AppModesShorthandMenuItem.Name = "AppModesShorthandMenuItem";
            this.AppModesShorthandMenuItem.Size = new System.Drawing.Size(209, 34);
            this.AppModesShorthandMenuItem.Text = "Стінографія";
            this.AppModesShorthandMenuItem.Click += new System.EventHandler(this.AppModesShorthandMenuItem_Click);
            // 
            // FileToolStripMenu
            // 
            this.FileToolStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadToolStripMenu,
            this.SaveToolStripMenuItem});
            this.FileToolStripMenu.Name = "FileToolStripMenu";
            this.FileToolStripMenu.Size = new System.Drawing.Size(69, 29);
            this.FileToolStripMenu.Text = "Файл";
            // 
            // LoadToolStripMenu
            // 
            this.LoadToolStripMenu.Name = "LoadToolStripMenu";
            this.LoadToolStripMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.LoadToolStripMenu.Size = new System.Drawing.Size(250, 34);
            this.LoadToolStripMenu.Text = "Відкрити";
            this.LoadToolStripMenu.Click += new System.EventHandler(this.LoadToolStripMenu_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.SaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(250, 34);
            this.SaveToolStripMenuItem.Text = "Зберегти";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.BackColor = System.Drawing.SystemColors.Control;
            this.CopyButton.BackgroundImage = global::Cryptograph.Properties.Resources.copy;
            this.CopyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CopyButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CopyButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.CopyButton.FlatAppearance.BorderSize = 0;
            this.CopyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyButton.Location = new System.Drawing.Point(1161, 221);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(82, 81);
            this.CopyButton.TabIndex = 6;
            this.CopyButton.UseVisualStyleBackColor = false;
            this.CopyButton.Visible = false;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.InitialDirectory = "Y:\\";
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.InitialDirectory = "Y:\\";
            // 
            // EncryptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 1055);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.GeneralMenuStrip;
            this.Name = "EncryptionForm";
            this.Text = "Шифрування";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EncryptionForm_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.KeysRSAPanel.ResumeLayout(false);
            this.KeysRSAPanel.PerformLayout();
            this.KeyCryptoPanel.ResumeLayout(false);
            this.KeyCryptoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SimpleKeyLengthUpDown)).EndInit();
            this.KeyDecryptoPanel.ResumeLayout(false);
            this.KeyDecryptoPanel.PerformLayout();
            this.GeneralMenuStrip.ResumeLayout(false);
            this.GeneralMenuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox InputTextBox;
        private System.Windows.Forms.RichTextBox OutputTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RadioButton CryptoButton;
        private System.Windows.Forms.RadioButton DecryptoButton;
        private System.Windows.Forms.Button ActionButton;
        private System.Windows.Forms.ComboBox CryptoMethodsListBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox SimpleKeyCryptoBox;
        private System.Windows.Forms.Panel KeyCryptoPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown SimpleKeyLengthUpDown;
        private System.Windows.Forms.Label OR_label;
        private System.Windows.Forms.Panel KeyDecryptoPanel;
        private System.Windows.Forms.TextBox SimpleKeyDecryptoBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel KeysRSAPanel;
        private System.Windows.Forms.Label GeneralKeyName;
        private System.Windows.Forms.TextBox AnotherKeyBox;
        private System.Windows.Forms.Label AnotherKeyNameLabel;
        private System.Windows.Forms.TextBox GeneralKeyBox;
        private System.Windows.Forms.Label PrivateKeyLabel;
        private System.Windows.Forms.TextBox PrivateKeyBox;
        private System.Windows.Forms.CheckBox GeneratePairKeysCheckBox;
        private System.Windows.Forms.Label RsaWaitLabel;
        private System.Windows.Forms.MenuStrip GeneralMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem LoadToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.ToolStripMenuItem AppModesMenu;
        private System.Windows.Forms.ToolStripMenuItem AppModesShorthandMenuItem;
    }
}

