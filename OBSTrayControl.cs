using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace OBStray
{
    public class OBSTrayControl : WebSocketBehavior
    {
        private TrayApp trayControl;

        public OBSTrayControl(TrayApp app)
        {
            trayControl = app;    
        }
        protected override void OnOpen()
        {
            //connected w/ client
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            //mensagem recebe a url, path, monitor e bitrate
            Message incomingMsg = JsonConvert.DeserializeObject<Message>(e.Data);
            if (trayControl.showDebugInfo)
              MessageBox.Show(e.Data.ToString());
            
            if (incomingMsg.type == "SetStreamPath")          
                trayControl.setStreamPath(incomingMsg.streampath);           
            if (incomingMsg.type == "SetStreamUrl")
                trayControl.setStreamUrl(incomingMsg.streamurl);
            if (incomingMsg.type == "SetStreamDisplay")
                trayControl.setStreamDisplay(Convert.ToInt32(incomingMsg.displayid));
            if (incomingMsg.type == "StartStopStreaming")
                trayControl.startStopStreamToOBS();
            if (incomingMsg.type == "SetSizeCapture")
                trayControl.setCaptureSize(Convert.ToInt32(incomingMsg.height), Convert.ToInt32(incomingMsg.width));

        }
    }
}
