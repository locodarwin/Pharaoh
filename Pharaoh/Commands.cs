
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
                case "movetest":
                    DoMoveTest(sName, iType, iSession, cmd);
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


        // Command VERSION
        private void DoMoveTest(string sName, int iType, int iSess, string[] cmd)
        {
            int iCitnum = GetCitnum(sName);
            Globals.LogStat = Logging.Command;
            Status("Command: movetest (requested by " + sName + " " + iCitnum.ToString() + ")");

            // Check permissions
            if (CheckPerms("movetest", iCitnum) == false)
            {
                Response(iSess, iType, "Sorry, " + sName + ", but you do not have permission to use the " + cmd[0] + " command.");
                return;
            }

            // get bot current location and add to globals positioning
            Globals.xx = _instance.Attributes.MyX;
            Globals.yy = _instance.Attributes.MyY;
            Globals.zz = _instance.Attributes.MyZ;

            // Move 4 meters east
            Globals.xx = Globals.xx - 2000;
            _instance.Attributes.MyX = Globals.xx;
            _instance.StateChange();

            // Move 4 meters north
            Globals.zz = Globals.zz + 2000;
            _instance.Attributes.MyZ = Globals.zz;
            _instance.StateChange();

            // Move 4 meters west
            // Move 4 meters north
            Globals.xx = Globals.xx + 2000;
            _instance.Attributes.MyX = Globals.xx;
            _instance.StateChange();

            // Move 4 meters south
            Globals.zz = Globals.zz - 2000;
            _instance.Attributes.MyZ = Globals.zz;
            _instance.StateChange();



            Response(iSess, iType, Globals.sAppName + " " + Globals.sVersion + " - " + Globals.sByline);
        }



    }
}
