namespace OBStray
{
    partial class FormSetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSaveSetup = new System.Windows.Forms.Button();
            this.txtObsApp = new System.Windows.Forms.TextBox();
            this.txtObsProfile = new System.Windows.Forms.TextBox();
            this.txtObsSettings = new System.Windows.Forms.TextBox();
            this.lstMonitors = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.bObsApp = new System.Windows.Forms.Button();
            this.bObsProfile = new System.Windows.Forms.Button();
            this.bObsSettings = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDownscaleFactor = new System.Windows.Forms.ComboBox();
            this.numObsMaxBitRate = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numObsMaxBitRate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveSetup
            // 
            this.btnSaveSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSetup.Location = new System.Drawing.Point(375, 137);
            this.btnSaveSetup.Name = "btnSaveSetup";
            this.btnSaveSetup.Size = new System.Drawing.Size(69, 28);
            this.btnSaveSetup.TabIndex = 2;
            this.btnSaveSetup.Text = "Salvar";
            this.btnSaveSetup.UseVisualStyleBackColor = true;
            this.btnSaveSetup.Click += new System.EventHandler(this.btnSaveSetup_Click);
            // 
            // txtObsApp
            // 
            this.txtObsApp.Location = new System.Drawing.Point(129, 38);
            this.txtObsApp.Name = "txtObsApp";
            this.txtObsApp.Size = new System.Drawing.Size(315, 20);
            this.txtObsApp.TabIndex = 5;
            // 
            // txtObsProfile
            // 
            this.txtObsProfile.Location = new System.Drawing.Point(129, 64);
            this.txtObsProfile.Name = "txtObsProfile";
            this.txtObsProfile.Size = new System.Drawing.Size(315, 20);
            this.txtObsProfile.TabIndex = 7;
            // 
            // txtObsSettings
            // 
            this.txtObsSettings.Location = new System.Drawing.Point(129, 90);
            this.txtObsSettings.Name = "txtObsSettings";
            this.txtObsSettings.Size = new System.Drawing.Size(315, 20);
            this.txtObsSettings.TabIndex = 9;
            // 
            // lstMonitors
            // 
            this.lstMonitors.FormattingEnabled = true;
            this.lstMonitors.Location = new System.Drawing.Point(73, 13);
            this.lstMonitors.Name = "lstMonitors";
            this.lstMonitors.Size = new System.Drawing.Size(151, 21);
            this.lstMonitors.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Monitor:";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Applications|*.exe|All files|*.*";
            this.openFileDialog.Title = "Selecione o app OBS";
            // 
            // bObsApp
            // 
            this.bObsApp.Location = new System.Drawing.Point(9, 38);
            this.bObsApp.Name = "bObsApp";
            this.bObsApp.Size = new System.Drawing.Size(114, 23);
            this.bObsApp.TabIndex = 13;
            this.bObsApp.Text = "OBS App";
            this.bObsApp.UseVisualStyleBackColor = true;
            this.bObsApp.Click += new System.EventHandler(this.bObsApp_Click);
            // 
            // bObsProfile
            // 
            this.bObsProfile.Location = new System.Drawing.Point(9, 66);
            this.bObsProfile.Name = "bObsProfile";
            this.bObsProfile.Size = new System.Drawing.Size(114, 23);
            this.bObsProfile.TabIndex = 14;
            this.bObsProfile.Text = "OBS Profile";
            this.bObsProfile.UseVisualStyleBackColor = true;
            this.bObsProfile.Click += new System.EventHandler(this.bObsProfile_Click);
            // 
            // bObsSettings
            // 
            this.bObsSettings.Location = new System.Drawing.Point(9, 92);
            this.bObsSettings.Name = "bObsSettings";
            this.bObsSettings.Size = new System.Drawing.Size(114, 23);
            this.bObsSettings.TabIndex = 15;
            this.bObsSettings.Text = "OBS Settings";
            this.bObsSettings.UseVisualStyleBackColor = true;
            this.bObsSettings.Click += new System.EventHandler(this.bObsSettings_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.ApplicationData;
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Max bitrate (kb/s):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Downscale factor:";
            // 
            // cmbDownscaleFactor
            // 
            this.cmbDownscaleFactor.FormattingEnabled = true;
            this.cmbDownscaleFactor.Location = new System.Drawing.Point(105, 152);
            this.cmbDownscaleFactor.Name = "cmbDownscaleFactor";
            this.cmbDownscaleFactor.Size = new System.Drawing.Size(109, 21);
            this.cmbDownscaleFactor.TabIndex = 18;
            // 
            // numObsMaxBitRate
            // 
            this.numObsMaxBitRate.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numObsMaxBitRate.Location = new System.Drawing.Point(105, 121);
            this.numObsMaxBitRate.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numObsMaxBitRate.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numObsMaxBitRate.Name = "numObsMaxBitRate";
            this.numObsMaxBitRate.Size = new System.Drawing.Size(67, 20);
            this.numObsMaxBitRate.TabIndex = 20;
            this.numObsMaxBitRate.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "OBS profile:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "OBS settings:";
            // 
            // FormSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 177);
            this.Controls.Add(this.bObsSettings);
            this.Controls.Add(this.bObsProfile);
            this.Controls.Add(this.bObsApp);
            this.Controls.Add(this.cmbDownscaleFactor);
            this.Controls.Add(this.numObsMaxBitRate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstMonitors);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtObsSettings);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtObsProfile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtObsApp);
            this.Controls.Add(this.btnSaveSetup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Configuração";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.numObsMaxBitRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveSetup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtObsApp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtObsProfile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtObsSettings;
        private System.Windows.Forms.ComboBox lstMonitors;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numObsMaxBitRate;
        private System.Windows.Forms.ComboBox cmbDownscaleFactor;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button bObsApp;
        private System.Windows.Forms.Button bObsProfile;
        private System.Windows.Forms.Button bObsSettings;
    }
}