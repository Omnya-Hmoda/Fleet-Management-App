using FPro;
using managementclasses;
using System.Collections.Concurrent;


namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GVAR gVAR2 = new GVAR();
            gVAR2.DicOfDic["Tags"] = new ConcurrentDictionary<string, string>();
            gVAR2.DicOfDic["Tags"]["VehicleID"] = "";
          //  gVAR2.DicOfDic["Tags"]["VehicleNumber"] = "141414";
          //  gVAR2.DicOfDic["Tags"]["VehicleType"] = "Golve";

            Vehicle v = new Vehicle();
            //  v.UpdateVehicle(gVAR2);
            v.DeleteVehicle("1");

        }
    }
}
