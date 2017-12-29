using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using AW;

namespace Pharaoh
{

    public partial class Form1 : Form
    {

        // Timer, BG worker, instance definitions
        public IInstance _instance;
        public Timer aTimer;
        BackgroundWorker m_Login;

        // Timer and BGW for bot movement
        public Timer aMove;
        //BackgroundWorker m_Move;


        public struct Coords
        {
            public int x;
            public int y;
            public int z;
            public int yaw;
        };

        public enum Logging
        {
            Chat,
            Command,
            Status,
        };


        public Form1()
        {
            InitializeComponent();
            this.Text = Globals.sAppName + " " + Globals.sVersion;

            LoadINI();

            Status("Ready to log into universe.");
            textBotname.Text = Globals.sBotName;
            textCitnum.Text = Convert.ToString(Globals.iCitNum);
            textPrivPass.Text = Globals.sPassword;
            textWorld.Text = Globals.sWorld;
            textCoords.Text = Globals.sCoords;
            textAvatar.Text = Convert.ToString(Globals.iAV);

            // Button starting configurations
            toolLoggedIn.BackColor = System.Drawing.Color.Green;
            butLogout.Enabled = false;

            // The AW message queue timer
            aTimer = new Timer();
            aTimer.Tick += new EventHandler(aTimer_Tick);
            aTimer.Interval = 100;
            aTimer.Start();

            // The bot movement timer (specific to bots that move)
            aMove = new Timer();
            aMove.Tick += new EventHandler(aMove_Tick);
            aTimer.Interval = 100;

            // Background tasking definitions for the universe & world login
            m_Login = new BackgroundWorker();
            m_Login.DoWork += new DoWorkEventHandler(m_LoginDoWork);
            m_Login.ProgressChanged += new ProgressChangedEventHandler(m_LoginProgress);
            m_Login.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_LoginCompleted);
            m_Login.WorkerReportsProgress = true;

            // Make the enter key act as a click on the login button
            this.AcceptButton = this.butLogin;
        }



        public static class Globals
        {
            // Application parameters
            public static string sAppName = "Pharaoh";
            public static string sVersion = "v1.0";
            public static string sByline = "Copyright © 2017 by Locodarwin";
            public static string sINI = "Pharaoh.ini";

            // Login and positioning
            public static string sUnivLogin, sBotName, sPassword, sWorld, sCoords;
            public static int iPort, iCitNum, iXPos, iYPos, iZPos, iYaw, iAV;
            public static bool iCaretaker;

            // Permissions
            public static List<string> lAdminCmds, lOwners, lAdmins;

            // Status and logging
            public static bool iChat2Stat, iChat2Log, iCmd2Stat, iCmd2Log, iStat2Log;
            public static string sLogfile;

            // Universe/world login states
            public static bool iInUniv = false;
            public static bool iInWorld = false;

            // Status markers
            public static Logging LogStat = Logging.Status;

            // Command prefix
            public static string sComPrefix;

            // Global positioning
            public static int xx, yy, zz, yyaw; 

            // Racehorse parameters
            //public static int pBaseSpeed = 1300;            // How far a horse can run in one second, in centimeters
            public static int pBaseSpeed = 900;            // How far a horse can run in one second, in centimeters
            public static int pWeight;

            // Racehorse flags
            public static bool iRunning, iTrotting, iWalking;
            public static Coords Dest;

        }

        private void butLogin_Click(object sender, EventArgs e)
        {

            // Grab the contents of the controls and put them into the globals
            Globals.sBotName = textBotname.Text;
            Globals.iCitNum = Convert.ToInt32(textCitnum.Text);
            Globals.sPassword = textPrivPass.Text;
            Globals.sWorld = textWorld.Text;
            Globals.sCoords = textCoords.Text;
            Coords coords = ConvertCoords(Globals.sCoords);
            Globals.iXPos = coords.x;
            Globals.iYPos = coords.y;
            Globals.iZPos = coords.z;
            Globals.iYaw = coords.yaw;
            Globals.iAV = Convert.ToInt32(textAvatar.Text);

            toolLoggedIn.BackColor = System.Drawing.Color.Yellow;
            toolLoggedIn.Text = "Logging In";
            butLogin.Enabled = false;
            butConfig.Enabled = false;

            m_Login.RunWorkerAsync();

        }

        private void butLogout_Click(object sender, EventArgs e)
        {
            if (Globals.iInUniv == false)
            {
                Status("Not in universe. Aborted.");
                return;
            }
            _instance.Dispose();
            Status("Logged out.");
            toolLoggedIn.BackColor = System.Drawing.Color.Green;
            toolLoggedIn.Text = "Logged Out";
            butLogin.Enabled = true;
            butConfig.Enabled = true;
            butLogout.Enabled = false;
            Globals.iInUniv = false;
            Globals.iInWorld = false;
        }

        private void butConfig_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start(@"notepad.exe " + Globals.sINI);
            string appPath = Application.StartupPath;
            var p = new System.Diagnostics.Process();
            //p.StartInfo.FileName = "notepad.exe " + appPath + "\\" + Globals.sINI;
            p.StartInfo.FileName = appPath + "\\" + Globals.sINI;
            p.Start();
        }


        private void m_LoginDoWork(object sender, DoWorkEventArgs e)
        {

            // Check universe login state and abort if we're already logged in
            if (Globals.iInUniv == true)
            {
                m_Login.ReportProgress(0, "Already logged into universe!");
                return;
            }

            // Initalize the AW API?
            m_Login.ReportProgress(0, "Initializing the API instance.");
            _instance = new Instance();

            // Install events & callbacks
            m_Login.ReportProgress(0, "Installing events and callbacks.");
            //_instance.EventAvatarAdd += OnEventAvatarAdd;
            _instance.EventChat += OnEventChat;

            // Set universe login parameters
            _instance.Attributes.LoginName = Globals.sBotName;
            _instance.Attributes.LoginOwner = Globals.iCitNum;
            _instance.Attributes.LoginPrivilegePassword = Globals.sPassword;
            //_instance.Attributes.LoginApplication = Globals.sBotDesc;

            // Log into universe
            //m_Login.ReportProgress(0, "Entering universe.");
            var rc = _instance.Login();
            if (rc != Result.Success)
            {
                m_Login.ReportProgress(0, "Unable to log in to universe (reason:" + rc + ").");
                return;
            }
            else
            {
                m_Login.ReportProgress(0, "Universe entry successful.");
                Globals.iInUniv = true;
            }

            // Enter world

            // Prepare for Caretaker mode if the option has been enabled
            if (Globals.iCaretaker == true)
            {
                _instance.Attributes.EnterGlobal = true;
                m_Login.ReportProgress(0, "Caretaker mode requested.");
            }

            //m_Login.ReportProgress(0, "Logging into world " + Globals.sWorld + ".");
            rc = _instance.Enter(Globals.sWorld);
            if (rc != Result.Success)
            {
                m_Login.ReportProgress(0, "Failed to log into world" + Globals.sWorld + " (reason:" + rc + ").");
                _instance.Dispose();
                Globals.iInUniv = false;
                return;
            }
            else
            {
                m_Login.ReportProgress(0, "Entered world " + Globals.sWorld + ".");
                Globals.iInWorld = true;
            }

            // Test caretaker mode (if requested)
            if (Globals.iCaretaker == true)
            {
                if (_instance.Attributes.WorldCaretakerCapability == true)
                {
                    m_Login.ReportProgress(0, "Caretaker mode confirmed.");
                }
                else
                {
                    m_Login.ReportProgress(0, "Caretaker mode denied.");
                }
            }

            // Commit the positioning and become visible
            m_Login.ReportProgress(0, "Changing position in world.");
            _instance.Attributes.MyX = Globals.iXPos;
            _instance.Attributes.MyY = Globals.iYPos;
            _instance.Attributes.MyZ = Globals.iZPos;
            _instance.Attributes.MyYaw = Globals.iYaw;
            _instance.Attributes.MyType = Globals.iAV;
            Globals.Dest.x = Globals.iXPos;
            Globals.Dest.y = Globals.iYPos;
            Globals.Dest.z = Globals.iZPos;
            Globals.Dest.yaw = Globals.iYaw;

            rc = _instance.StateChange();
            if (rc == Result.Success)
            {
                m_Login.ReportProgress(0, "Movement successful.");
            }
            else
            {
                m_Login.ReportProgress(0, "Movement aborted (reason: " + rc + ").");
            }


        }

        private void m_LoginProgress(object serder, ProgressChangedEventArgs e)
        {
            Status(e.UserState.ToString());
        }


        private void m_LoginCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Status("Error while performing background operation!");
            }

            if (Globals.iInWorld == true)
            {
                toolLoggedIn.BackColor = System.Drawing.Color.Red;
                toolLoggedIn.Text = "Logged In";
                butLogout.Enabled = true;
                Status("Logged in.");
            }
            else
            {
                toolLoggedIn.BackColor = System.Drawing.Color.Green;
                toolLoggedIn.Text = "Logged Out";
                butLogin.Enabled = true;
                butConfig.Enabled = true;
                Status("Failed to log in.");
            }

        }



        // Timer fires this method to perform AW_WAIT()
        private void aTimer_Tick(object source, EventArgs e)
        {
            if (Globals.iInWorld)
            {
                Utility.Wait(0);
            }

        }


        // Dispose of the bot instance before exiting the main application form
        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _instance.Dispose();
            Status("The bot application was closed.");
        }




        // The status update member
        public void Status(string sText)
        {

            bool iShouldStat = false;
            bool iShouldLog = false;

            // Decide if we should send the status to the status window, then do so as applicable
            if (Globals.LogStat == Logging.Status) { iShouldStat = true; }
            if (Globals.LogStat == Logging.Chat && Globals.iChat2Stat == true) { iShouldStat = true; }
            if (Globals.LogStat == Logging.Command && Globals.iCmd2Stat == true) { iShouldStat = true; }

            if (iShouldStat == true)
            {
                // Keep the status window contents trimmed as needed
                if (lisStatus.Items.Count > 500)
                {
                    lisStatus.Items.RemoveAt(0);
                }
                // Update status listview & make sure the latest addition is visible
                lisStatus.Items.Add(sText);
                lisStatus.TopIndex = lisStatus.Items.Count - 1;
            }


            // Decide if we should send the status to the logfile, then do so as applicable
            if (Globals.LogStat == Logging.Status && Globals.iStat2Log == true) { iShouldLog = true; }
            if (Globals.LogStat == Logging.Chat && Globals.iChat2Log == true) { iShouldLog = true; }
            if (Globals.LogStat == Logging.Command && Globals.iCmd2Log == true) { iShouldLog = true; }


            if (iShouldLog == true)
            {
                string sDump = string.Format("{0:G} -- {1}\r\n", DateTime.Now, sText);
                File.AppendAllText(Globals.sLogfile, sDump);

            }

            Globals.LogStat = Logging.Status;

        }


    }
}
