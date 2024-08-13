using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace catxapi
{
    public static class maincourse
    {
        // agent with power! (and CAS)
        private static string executorsex = "agent | powered by the cas";

        // httpClient for all your HTTP needs
        private static readonly HttpClient httpClient = new HttpClient();

        // version control (keepin' it fresh)
        private static string currentVersion = "3.0.0";
        private static bool isVersionValid = true;

        // check if the agent is already working hard
        public static bool isinj()
        {
            Process[] processes = Process.GetProcessesByName("agent");
            return processes.Length > 0;
        }

        // inject some process magic
        public static async void inj()
            
        {

            //PLEASE JUST PLEASE KEEP THIS HERE IF YOU ARE USING THE API :pray:
            MessageBox.Show("Made with the an open source exploit api:  https://github.com/casprivate/agent-api", "GNU AGPL 3.0 LICENSE");
            // get the path to the executable
            string executablePath = AppDomain.CurrentDomain.BaseDirectory;

            // folders needed to keep things tidy
            string[] foldersToCreate = { "workspace", "autoexec", "scripts", "bin", "pysploit" };

            // make the folders if they don't already exist (cleaning crew on duty)
            foreach (var folder in foldersToCreate)
            {
                string folderPath = Path.Combine(executablePath, folder);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
            }

            // because ascii art never gets old
            string asciiArt = @"

 ________  ________  ________      
|\   ____\|\   __  \|\   ____\     
\ \  \___|\ \  \|\  \ \  \___|_    
 \ \  \    \ \   __  \ \_____  \   
  \ \  \____\ \  \ \  \|____|\  \  
   \ \_______\ \__\ \__\____\_\  \ 
    \|_______|\|__|\|__|\_________\
                       \|_________|
                                   
                                   
 https://github.com/casprivate/agent-api

            ";

            // let's start cmd with some flair (ascii style)
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/K title Cat CAS Injector && color 0C && echo " + asciiArt.Replace("\n", " && echo "),
                CreateNoWindow = false,
                UseShellExecute = true,
                RedirectStandardInput = false,
                RedirectStandardOutput = false,
                RedirectStandardError = false
            };

            // start the cmd process
            Process cmdProcess = Process.Start(startInfo);

            // wait for 2 seconds (because patience is a virtue)
            await Task.Delay(2000);

            // and now... start the agent (drumroll please)
            Process.Start(Path.Combine(executablePath, "agent.exe"));

            // give the user a friendly heads up
            MessageBox.Show("This API is powered by the CAS", "CAS");
        }

        // execute some sneaky scripts
        public static async void exec(string script)
        {
            ClientWebSocket ws = new ClientWebSocket();
            Uri serverUri = new Uri("ws://localhost:8050/ws");

            // connect to the websocket server (knock knock, it's the script)
            await ws.ConnectAsync(serverUri, CancellationToken.None);

            // send the script over (no peeking!)
            ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(script));
            await ws.SendAsync(bytesToSend, WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None);
        }
    }
}
