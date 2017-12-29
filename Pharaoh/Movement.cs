using System;
using AW;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharaoh
{
    public partial class Form1
    {
        private void aMove_Tick(object source, EventArgs e)
        {
            if (!Globals.iInWorld)
            {
                return;
            }


            // Since this timer fires 10 times a second, calculate each movement as 1/10th the movement value
            int iOneTenth = Globals.pBaseSpeed / 10;
            Coords Current, Temp, Final;
            Final = Globals.Dest;
            Current.x = _instance.Attributes.MyX;
            Current.y = _instance.Attributes.MyY;
            Current.z = _instance.Attributes.MyZ;
            Current.yaw = 0;
            Temp = Current;
            //Console.WriteLine("Pos: " + Current.x.ToString() + "x, " + Current.y.ToString() + "y, " + Current.z.ToString() + "z.");

            if (Globals.iRunning || Globals.iTrotting)
            {
                

                if (Current.x != Final.x)
                {
                    if (Current.x > Final.x)
                    {
                        if (Current.x - iOneTenth < Final.x)
                        {
                            Temp.x = Final.x;
                        }
                        else
                        {
                            Temp.x = Current.x - iOneTenth;
                        }
                        
                    }
                    else
                    {
                        if (Current.x + iOneTenth > Final.x)
                        {
                            Temp.x = Final.x;
                        }
                        else
                        {
                            Temp.x = Current.x + iOneTenth;
                        }
                    }
                }


                if (Current.z != Final.z)
                {
                    if (Current.z > Final.z)
                    {
                        if (Current.z - iOneTenth < Final.z)
                        {
                            Temp.z = Final.z;
                        }
                        else
                        {
                            Temp.z = Current.z - iOneTenth;
                        }

                    }
                    else
                    {
                        if (Current.z + iOneTenth > Final.z)
                        {
                            Temp.z = Final.z;
                        }
                        else
                        {
                            Temp.z = Current.z + iOneTenth;
                        }
                    }
                }

                if (Globals.iRunning)
                {
                    _instance.Attributes.MyState = 1;
                }
                else
                {
                    _instance.Attributes.MyState = 0;
                }

            }



            // Make the move
            _instance.Attributes.MyX = Temp.x;
            _instance.Attributes.MyZ = Temp.z;
            _instance.StateChange();


            // If we're at our destination, then trip off the running, walking, trotting flags and kill movement timer
            if ((Temp.x == Globals.Dest.x) && (Temp.z == Globals.Dest.z))
            {
                Globals.iWalking = false;
                Globals.iRunning = false;
                Globals.iTrotting = false;
                _instance.Attributes.MyState = 0;
                _instance.StateChange();
                aMove.Stop();
                //Console.WriteLine("I should stop if I get here.");
                Status("Movement complete.");
            }



        }
    }
}
