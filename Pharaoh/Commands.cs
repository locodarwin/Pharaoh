
using System;

namespace Pharaoh
{
    public partial class Form1
    {

        private void Commands(string sName, int iType, int iSession, string sMsg)
        {
            // Get first letter of what was said command; if not a command, abort command mode
            string sPrefix = sMsg.Substring(0, Globals.sComPrefix.Length);
            if (sPrefix != Globals.sComPrefix)
            {
                return;
            }

            // Strip all unneeded parts off the command and split into parameters
            string strip = sMsg.Substring(Globals.sComPrefix.Length);
            strip = strip.Trim();
            string[] cmd = strip.Split(' ');

            switch (cmd[0])
            {
                case "version":
                    DoVersion(sName, iType, iSession, cmd);
                    break;
                case "ver":
                    DoVersion(sName, iType, iSession, cmd);
                    break;
                case "speed":
                    DoSpeed(sName, iType, iSession, cmd);
                    break;
                case "gallop":
                    DoGallop(sName, iType, iSession, cmd);
                    break;
                case "gohome":
                    DoGoHome(sName, iType, iSession, cmd);
                    break;

            }

        }

        // Method to respond back on the results of the command
        private void Response(int iSess, int iType, string sMsg)
        {
            if (iType == 2)
            {
                _instance.Whisper(iSess, sMsg);
                Globals.LogStat = Logging.Chat;
                Status("(whispered): " + sMsg);
            }
            else
            {
                _instance.Say(sMsg);
                Globals.LogStat = Logging.Chat;
                Status(sMsg);
            }

        }


        // Command VERSION
        private void DoVersion(string sName, int iType, int iSess, string[] cmd)
        {
            int iCitnum = GetCitnum(sName);
            Globals.LogStat = Logging.Command;
            Status("Command: version (requested by " + sName + " " + iCitnum.ToString() + ")");

            // Check permissions
            if (CheckPerms("version", iCitnum) == false)
            {
                Response(iSess, iType, "Sorry, " + sName + ", but you do not have permission to use the " + cmd[0] + " command.");
                return;
            }
            Response(iSess, iType, Globals.sAppName + " " + Globals.sVersion + " - " + Globals.sByline);
        }


        // Command SPEED
        private void DoSpeed(string sName, int iType, int iSess, string[] cmd)
        {
            int iCitnum = GetCitnum(sName);
            Globals.LogStat = Logging.Command;
            Status("Command: speed (requested by " + sName + " " + iCitnum.ToString() + ")");

            // Check permissions
            if (CheckPerms("speed", iCitnum) == false)
            {
                Response(iSess, iType, "Sorry, " + sName + ", but you do not have permission to use the " + cmd[0] + " command.");
                return;
            }

            if (cmd.Length != 2)
            {
                Response(iSess, iType, "Command must have 1 parameter: speed (in centimeters).");
            }

            Globals.pBaseSpeed = Convert.ToInt32(cmd[1]);
            Response(iSess, iType, "Speed (in centimeters) now set to " + Globals.pBaseSpeed.ToString());


        }


        // Command GoHome
        private void DoGoHome(string sName, int iType, int iSess, string[] cmd)
        {
            int iCitnum = GetCitnum(sName);
            Globals.LogStat = Logging.Command;
            Status("Command: gohome (requested by " + sName + " " + iCitnum.ToString() + ")");

            // Check permissions
            if (CheckPerms("gohome", iCitnum) == false)
            {
                Response(iSess, iType, "Sorry, " + sName + ", but you do not have permission to use the " + cmd[0] + " command.");
                return;
            }

            Response(iSess, iType, "Warping to login location.");

            _instance.Attributes.MyX = Globals.iXPos;
            _instance.Attributes.MyY = Globals.iYPos;
            _instance.Attributes.MyZ = Globals.iZPos;
            _instance.Attributes.MyYaw = Globals.iYaw;
            _instance.StateChange();

        }



        // Command GALLOP
        private void DoGallop(string sName, int iType, int iSess, string[] cmd)
        {
            int iCitnum = GetCitnum(sName);
            Globals.LogStat = Logging.Command;
            Status("Command: gallop (requested by " + sName + " " + iCitnum.ToString() + ")");

            // Check permissions
            if (CheckPerms("gallop", iCitnum) == false)
            {
                Response(iSess, iType, "Sorry, " + sName + ", but you do not have permission to use the " + cmd[0] + " command.");
                return;
            }

            if (cmd.Length != 3)
            {
                Response(iSess, iType, "Command must have 2 parameters: direction and distance.");
            }

            // Store current coords
            Coords Current, Final;
            Current.x = _instance.Attributes.MyX;
            Current.y = _instance.Attributes.MyY;
            Current.z = _instance.Attributes.MyZ;
            Current.yaw = 0;
            Final = Current;

            // Galloping north test
            if (cmd[1].ToLower() == "north")
            {
                Globals.iRunning = true;
                Globals.Dest.z = Globals.Dest.z + Convert.ToInt32(cmd[2]);

                Response(iSess, iType, "Moving " + cmd[2] + " centimeters north.");
                aMove.Start();
            }

            // Galloping south test
            if (cmd[1].ToLower() == "south")
            {
                Globals.iRunning = true;
                Globals.Dest.z = Globals.Dest.z + (-1 * Convert.ToInt32(cmd[2]));

                Response(iSess, iType, "Moving " + cmd[2] + " centimeters south.");
                aMove.Start();
            }

            // Galloping west test
            if (cmd[1].ToLower() == "west")
            {
                Globals.iRunning = true;
                Globals.Dest.x = Globals.Dest.x + Convert.ToInt32(cmd[2]);

                Response(iSess, iType, "Moving " + cmd[2] + " centimeters west.");
                aMove.Start();
            }


            // Galloping northwest test
            if (cmd[1].ToLower() == "nw")
            {
                Globals.iRunning = true;
                Globals.Dest.x = Globals.Dest.x + Convert.ToInt32(cmd[2]);
                Globals.Dest.z = Globals.Dest.z + Convert.ToInt32(cmd[2]);

                Response(iSess, iType, "Moving " + cmd[2] + " centimeters northwest.");
                aMove.Start();
            }



        }


    }
}
