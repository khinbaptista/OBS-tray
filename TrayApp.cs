using System;
using System.Diagnostics;
using System.Windows.Forms;
using OBStray.Properties;
using System.Drawing;
using System.Threading;
using System.Text;
using System.IO;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace OBStray
{
	public class TrayApp
	{
        WebSocketServer wsRemoteControl;

        WebSocket ws;
        bool isStreamPlaying = false;
        string obsHost = "";
        public string streamUrl = "";
        public string streamPath = "";
        string[] protocols = new string[1];
        int messageCount;
        Process obs = null;
        bool obsIsConnected = false;
        bool obsInvisible = true;
        public bool showDebugInfo = false;
        public string obsExePath = "";
        public string obsConfigPath = "";
        public string obsProfilePath = "";
        public string broadcastMonitorName = "";
        public string obsMaxBitrate = "";
        public string obsDownscale = "";
        int broadcastMonitor = 0;        
        int screenWidth = 0;
        int screenHeight = 0;

        bool setupSuccess = false;

        public TrayApp(string host)
        {
            obsExePath = Properties.obstray.Default.obsdir.ToString();
            obsConfigPath = Properties.obstray.Default.obsconfigdir.ToString();
            obsProfilePath = Properties.obstray.Default.obsprofiledir.ToString();
            obsMaxBitrate = Properties.obstray.Default.maxbitrate.ToString();
            obsDownscale = Properties.obstray.Default.downscale.ToString();

            screenWidth = Screen.PrimaryScreen.Bounds.Width;
            screenHeight = Screen.PrimaryScreen.Bounds.Height;

            isStreamPlaying = false;
            obsHost = host;
            protocols[0] = "obsapi";
            messageCount = 0;

            ParseArgsInput();
            InitializeOBS();
        }

        private void ParseArgsInput() {
            string[] args = Environment.GetCommandLineArgs();

            foreach (string arg in args) {
                if (arg.Equals("obsvisible", StringComparison.CurrentCultureIgnoreCase))
                    obsInvisible = false;
                if (arg.Equals("debug", StringComparison.CurrentCultureIgnoreCase))
                    showDebugInfo = true;
            }
        }

        private void InitializeOBS() {
            LaunchOBS(obsInvisible);
            SetupOBSConfig();

            if (!setupSuccess)
                ResetOBS(obsInvisible);

            if (!setupSuccess) {
                DialogResult result;
                string message =
                    "Primeira inicialização do aplicativo. Bem vindo ao OBStray!\r\n" +
                    "As configurações automáticas serão utilizadas.";

                result = MessageBox.Show(message, "Bem vindo", MessageBoxButtons.OK);

                ResetOBS(obsInvisible);
            }
            else
                ConnectWebsocket();
        }

        private bool LaunchOBS(bool silent) {
            if (!File.Exists(obsExePath)) return false;

            obs = new Process();
            obs.StartInfo.FileName = obsExePath;
            if (silent)
                obs.StartInfo.Arguments = "-silent";
            obs.Exited += obs_Exited;
            return obs.Start();
        }

        // Keeps OBS alive (if possible)
        void obs_Exited(object sender, EventArgs e) {
            Console.WriteLine(e.ToString());
            ResetOBS(obsInvisible);
        }

        //(re)start OBS app
        private void ResetOBS(bool invisible)
        {
            bool obsRunning = false;

            ExitOBS();

            // Try to setup OBS even if OBS is not running (in case just the executable file is not found)
            obsRunning = LaunchOBS(invisible);
            SetupOBSConfig();

            // If OBS is not running, tries relaunching it. In case it fails, send a message
            if (!obsRunning && !LaunchOBS(invisible))
                MessageBox.Show("Erro ao abrir o processo OBS.exe. Reconfigure ou reinstale o OBStray.");
            else
                ConnectWebsocket();
        }

        private void SetupOBSConfig() {
            if (!File.Exists(obsExePath)) {
                Properties.obstray.Default.obsdir = Environment.CurrentDirectory + @"\OBS\OBS.exe";
                Properties.obstray.Default.Save();

                obsExePath = Properties.obstray.Default.obsdir.ToString();
            }

            setupSuccess = CopyObsConfigProfile();

            if (setupSuccess) return;

            Properties.obstray.Default.obsconfigdir =
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\OBS\";
            Properties.obstray.Default.obsprofiledir =
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\OBS\profiles";
            Properties.obstray.Default.Save();

            obsExePath = Properties.obstray.Default.obsdir.ToString();
            obsConfigPath = Properties.obstray.Default.obsconfigdir.ToString();
            obsProfilePath = Properties.obstray.Default.obsprofiledir.ToString();

            setupSuccess = CopyObsConfigProfile();
        }

        private bool CopyObsConfigProfile() {
            string OBSConfig = "scenes.xconfig";
            string OBSProfile = "Untitled.ini";
            string TemplateConfig = "defaultobs.xconfig";
            string TemplateProfile = "defaultobs.ini";
            string destFile, sourceFile, updatedConfig = "";

            if (System.IO.Directory.Exists(obsProfilePath)) {
                //copy config
                sourceFile = Environment.CurrentDirectory + "\\" + TemplateConfig;
                destFile = System.IO.Path.Combine(obsConfigPath, OBSConfig);
                updatedConfig = replaceUserConfig(File.ReadAllText(sourceFile));
                File.WriteAllText(destFile, updatedConfig);

                //copy profile
                sourceFile = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), TemplateProfile);
                destFile = System.IO.Path.Combine(obsProfilePath, OBSProfile);
                updatedConfig = replaceUserProfile(File.ReadAllText(sourceFile));
                File.WriteAllText(destFile, updatedConfig);

                return true;
            }
            else
                return false;
        }

        private void ConnectWebsocket() {
            ws = new WebSocket(obsHost, protocols);

            if (!ConnectOBS(ws))
                return;

            if (wsRemoteControl != null && wsRemoteControl.IsListening)
                wsRemoteControl.Stop();

            //setup websocket to receive remote commands
            wsRemoteControl = new WebSocketServer(System.Net.IPAddress.Loopback, 2424);    //port 2424 local-only
            wsRemoteControl.AddWebSocketService<OBSTrayControl>("/obstraycontrol", () => new OBSTrayControl(this));

            try {
                wsRemoteControl.Start();
            }
            catch {
                MessageBox.Show("Only one instance of OBS may be running at a time.\r\nClose OBS and try again.",
                        "Failed to connect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool ConnectOBS(WebSocket webSocket) {
            webSocket.OnMessage += onMessageReceived;
            webSocket.OnOpen += onConnect;
            webSocket.OnClose += onClose;

            if (!(obsIsConnected))
                webSocket.Connect();

            DialogResult action = DialogResult.OK;

            if (!obsIsConnected)
                action = MessageBox.Show("Failed to connect to OBS", "WebSocket failed",
                                        MessageBoxButtons.RetryCancel);
            else {
                Console.WriteLine("Connected to OBS");
                obs.EnableRaisingEvents = true;
            }

            if (action == DialogResult.Retry) {
                ResetOBS(obsInvisible);
                return false;
            }
            else if (action == DialogResult.Cancel) {
                ExitOBS();
                return false;
            }

            return true;
        }

        private void ExitOBS() {
            if (ws != null) ws.Close();

            try {
                obs.EnableRaisingEvents = false;
                obs.Kill();
            }
            catch { }
        }
		
		public ContextMenuStrip Create()
		{
			// Add the default menu options.
			ContextMenuStrip menu = new ContextMenuStrip();
			ToolStripMenuItem item;
			ToolStripSeparator sep;

			// Settings
			item = new ToolStripMenuItem();
			item.Text = "Configurações";
			item.Click += new EventHandler(Settings_Click);
			item.Image = Resources.About;
			menu.Items.Add(item);

			// Separator
			sep = new ToolStripSeparator();
			menu.Items.Add(sep);

			// Exit.
			item = new ToolStripMenuItem();
			item.Text = "Sair";
			item.Click += new System.EventHandler(Exit_Click);
			item.Image = Resources.Quit;
			menu.Items.Add(item);

			return menu;
		}

        public void startStopStreamToOBS()
        {
            if (!(obsIsConnected))
            {
                MessageBox.Show("Erro de comunicação com o OBS. Tente novamente.");
                ExitOBS();
                Application.Exit();
            }

            if (!(isStreamPlaying))
            {
                sendMsgSetStreamUrl(ws, streamUrl, messageCount++);                
                sendMsgSetStreamPath(ws, streamPath, messageCount++);
                sendMsgStartStream(ws, messageCount++);
                isStreamPlaying = true;                
            }
            else
            {
                sendMsgStopStream(ws, messageCount++);
                isStreamPlaying = false;
            }
        }

        private bool setupOBSConfig()
        {
            string OBSConfig = "scenes.xconfig";
            string OBSProfile = "Untitled.ini";
            string TemplateConfig = "defaultobs.xconfig";
            string TemplateProfile = "defaultobs.ini";
            string destFile, sourceFile, updatedConfig = "";

            if (System.IO.Directory.Exists(obsConfigPath))
            {
                //copy config
                sourceFile = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), TemplateConfig);
                destFile = System.IO.Path.Combine(obsConfigPath, OBSConfig);
                updatedConfig = replaceUserConfig(File.ReadAllText(sourceFile));
                File.WriteAllText(destFile, updatedConfig);

                //copy profile
                sourceFile = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), TemplateProfile);
                destFile = System.IO.Path.Combine(obsProfilePath, OBSProfile);
                updatedConfig = replaceUserProfile(File.ReadAllText(sourceFile));
                File.WriteAllText(destFile, updatedConfig);

                return true;           
            }
            else
                return false;             
        }

        private string replaceUserConfig(string inFile)
        {            
            inFile = inFile.Replace("resXMonitor1", screenWidth.ToString());
            inFile = inFile.Replace("resYMonitor1", screenHeight.ToString());

            inFile = inFile.Replace("captureCX : x", "captureCX : " + screenWidth.ToString());
            inFile = inFile.Replace("captureCY : y", "captureCY : " + screenHeight.ToString());

            inFile = inFile.Replace("cx : x", "cx : " + screenWidth.ToString());
            inFile = inFile.Replace("cy : y", "cy : " + screenHeight.ToString());
            

            if (Screen.AllScreens.Length > 1)
            {
                inFile = inFile.Replace("resXMonitor2", Screen.AllScreens[1].Bounds.Width.ToString());  //fix [quando cliente tiver mais de um monitor
                inFile = inFile.Replace("resYMonitor2", Screen.AllScreens[1].Bounds.Height.ToString()); //verificar a resolucao da segunda tela
            }

            if (broadcastMonitor == 0)
            {
                inFile = inFile.Replace("renderMonitor1 : 0", "render : 1");
                inFile = inFile.Replace("renderMonitor2 : 0", "render : 0");
            }
            if (broadcastMonitor == 1)
            {
                inFile = inFile.Replace("renderMonitor1 : 0", "render : 0");
                inFile = inFile.Replace("renderMonitor2 : 0", "render : 1");
            }

            return inFile;
        }

        private string replaceUserProfile(string inFile)
        {
            inFile = inFile.Replace("BaseWidth=x", "BaseWidth="+screenWidth.ToString());
            inFile = inFile.Replace("BaseHeight=y", "BaseHeight="+screenHeight.ToString());
            inFile = inFile.Replace("Downscale=x", "Downscale="+obsDownscale.ToString());
            inFile = inFile.Replace("MaxBitrate=x", "MaxBitrate=" + obsMaxBitrate);

            return inFile;
        }

        void StartStop_Click(object sender, EventArgs e)
        {
            startStopStreamToOBS();
        }

        void onConnect(object sender, EventArgs e)
        {
            WebSocket wb = (WebSocket)sender;
            obsIsConnected = true;
        }

        void onMessageReceived(object sender, EventArgs e)
        {
            WebSocket wb = (WebSocket)sender;
            MessageEventArgs mensagem = (MessageEventArgs)e;            
        }

        private bool sendMsgStartStream(WebSocket webSocket, int messageId)
        {            
            webSocket.Send("{\"request-type\":\"StartStream\",\"message-id\":\"" + messageId.ToString() + "\"}");
            if (showDebugInfo)
                MessageBox.Show("Msg to OBS:" + "{\"request-type\":\"StartStream\",\"message-id\":\"" + messageId.ToString() + "\"}"); 
            return true;
        }

        private bool sendMsgStopStream(WebSocket webSocket, int messageId)
        {
            webSocket.Send("{\"request-type\":\"StopStream\",\"message-id\":\"" + messageId.ToString() + "\"}");
            if (showDebugInfo)
                MessageBox.Show("Msg to OBS:" + "{\"request-type\":\"StopStream\",\"message-id\":\"" + messageId.ToString() + "\"}");
            return true;
        }

        private bool sendMsgSetStreamUrl(WebSocket webSocket, string url, int messageId)
        {
            webSocket.Send("{\"request-type\":\"SetStreamUrl\",\"streamurl\":\""+url+"\",\"message-id\":\""+messageId.ToString()+"\"}");
            if (showDebugInfo)
                MessageBox.Show("Msg to OBS:" + "{\"request-type\":\"SetStreamUrl\",\"streamurl\":\"" + url + "\",\"message-id\":\"" + messageId.ToString() + "\"}");
            return true;
        }

        private bool sendMsgSetStreamPath(WebSocket webSocket, string path, int messageId)
        {
            webSocket.Send("{\"request-type\":\"SetStreamPath\",\"streampath\":\""+path+"\",\"message-id\":\""+messageId.ToString()+"\"}");
            if (showDebugInfo)
                MessageBox.Show("Msg to OBS:" + "{\"request-type\":\"SetStreamPath\",\"streampath\":\"" + path + "\",\"message-id\":\"" + messageId.ToString() + "\"}");
            return true;
        }

        public void setStreamUrl(string url)
        {
            streamUrl = url;            
        }

        public void setStreamPath(string path)
        {
            streamPath = path;
        }

        public void setStreamDisplay(int id)
        {
            broadcastMonitor = id;
        }

        public void setCaptureSize(int x, int y)
        {
            screenWidth = x;
            screenHeight = y;

            //reload new configs
            ResetOBS(obsInvisible);
        }

        void Settings_Click(object sender, EventArgs e)
        {
            FormSetup frm = new FormSetup(this);
            frm.FormClosed += frm_FormClosed;
            frm.Show();            
        }

        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormSetup form = (FormSetup)sender;
            if (form.save())
            {
                Properties.obstray.Default.obsdir = obsExePath;
                Properties.obstray.Default.obsconfigdir = obsConfigPath;
                Properties.obstray.Default.obsprofiledir = obsProfilePath;
                Properties.obstray.Default.downscale = obsDownscale;
                Properties.obstray.Default.maxbitrate = obsMaxBitrate;
                Properties.obstray.Default.Save();
                //TODO: set broadcast device monitor

                MessageBox.Show("Gravando configurações do OBSTray.");
                MessageBox.Show("Reinicie o OBSTray.");
                ExitOBS();
                Application.Exit();                               
            }
        }

		void Exit_Click(object sender, EventArgs e)
		{
            ExitOBS();
			Application.Exit();
		}

        private void connectOBS(WebSocket webSocket)
        {
            webSocket.OnMessage += onMessageReceived;
            webSocket.OnOpen += onConnect;
            webSocket.OnClose += onClose;
            
            while (!(obsIsConnected))
                webSocket.Connect();
        }

        private void parseArgsInput()
        {
            string[] args = Environment.GetCommandLineArgs();

            foreach (string arg in args)
            {
                if (arg.Equals("obsvisible", StringComparison.CurrentCultureIgnoreCase))
                    obsInvisible = false;
                if (arg.Equals("debug", StringComparison.CurrentCultureIgnoreCase))
                    showDebugInfo = true;                
            }
        }

        private void onClose(object sender, CloseEventArgs e)
        {
            obsIsConnected = false;
        }
    }
}