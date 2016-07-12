using System;
using ThomasITServices;
using System.Threading;

namespace StartInstallTechAppCS
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] paths = { @"K:\PROD\WNT\LIB", @"C:\PERL\V510\BIN", @"C:\LOCALAPPS\BIN", @"K:\PROD\SHARE\BIN", @"K:\PROD\WNT\BIN" };
            SetTechAppsENV.Add("path", paths, EnvironmentVariableTarget.Machine);

            NetworkDrive.SetDrive("K:", @"\\utcapp.com\utas_apps");
            
            //Installer.Start("Goodrich", "goodrich",@"K:\");
            //Installer.Start("BTA", "bta", @"K:\");
            Installer.Start("IP config", "ipconfig", @"K:\");


            //Thread.Sleep(2000);
            Console.Read();
            
        }
    }
}
