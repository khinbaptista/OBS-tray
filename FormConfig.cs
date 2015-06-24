using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OBStray
{
    public partial class FormSetup : Form
    {
        private bool btnSaveCliked;
        private TrayApp trayControl;

        private string pathObsApp;
        private string pathObsProfile;
        private string pathConfig;

        public FormSetup(TrayApp app)
        {
            trayControl = app;

            InitializeComponent();
            btnSaveCliked = false;

            txtObsApp.Text = trayControl.obsExePath;
            txtObsProfile.Text = trayControl.obsProfilePath;
            txtObsSettings.Text = trayControl.obsConfigPath;

            float factorDefault = 0;
            for (float factor = 1.0f; factor <= 3.0f; factor += 0.25f)
            {                
                cmbDownscaleFactor.Items.Add(factor.ToString());                
                if (float.TryParse(trayControl.obsDownscale, out factorDefault))
                    if (factor == factorDefault)
                        cmbDownscaleFactor.SelectedIndex = cmbDownscaleFactor.Items.Count - 1;
            }

            foreach (var screen in System.Windows.Forms.Screen.AllScreens)            
                lstMonitors.Items.Add(screen.DeviceName);           
            lstMonitors.SelectedIndex = 0;
        }

        private void btnSaveSetup_Click(object sender, EventArgs e)
        {            
            trayControl.obsExePath = txtObsApp.Text.ToString();
            trayControl.obsProfilePath = txtObsProfile.Text.ToString();
            trayControl.obsConfigPath = txtObsSettings.Text.ToString();
            trayControl.broadcastMonitorName = lstMonitors.SelectedItem.ToString();
            trayControl.obsMaxBitrate = numObsMaxBitRate.Value.ToString();
            trayControl.obsDownscale = cmbDownscaleFactor.SelectedItem.ToString();

            btnSaveCliked = true;
            Close();
        }

        private void bObsApp_Click(object sender, EventArgs e) {
            if (File.Exists(pathObsApp))
                openFileDialog.InitialDirectory = Directory.GetDirectoryRoot(pathObsApp);
            else
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK) {
                pathObsApp = openFileDialog.FileName;
                txtObsApp.Text = pathObsApp;
            }
        }

        private void bObsProfile_Click(object sender, EventArgs e) {
            folderBrowserDialog.Description = "Selecione a paste de perfis do OBS";

            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK) {
                pathObsProfile = folderBrowserDialog.SelectedPath;
                txtObsProfile.Text = pathObsProfile;
            }
        }

        private void bObsSettings_Click(object sender, EventArgs e) {
            folderBrowserDialog.Description = "Selecione a paste de configurações do OBS";

            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK) {
                pathConfig = folderBrowserDialog.SelectedPath;
                txtObsSettings.Text = pathConfig;
            }
        }

        internal bool save()
        {
            return btnSaveCliked;
        }
    }
}
