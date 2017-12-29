using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharaoh
{
    public partial class Form1
    {
        
        
        
        public void ParseBotgram(string sFromName, int iFromCitnum, string sText)
        {

            // Break the received text into chunks separated by comma
            string[] line = sText.Split(',');
            if (line[0] != "all" || line[0] != Globals.sBotName)
            {
                return;
            }

            switch (line[1])
            {
                case "version":
                    GoVersion(sFromName, iFromCitnum, line);
                    break;

            }

        }



        public void GoVersion(string sFromName, int iFromCitnum, string[] line)
        {
            // Do version info 
        }


    }
}
