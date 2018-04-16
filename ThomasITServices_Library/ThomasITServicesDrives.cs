using IWshRuntimeLibrary;
using System;
using System.IO;

namespace ThomasITServices
{
    public class NetworkDrive
    {

        private string DriveName { set; get; }
        private string NetworkPath { set; get; }
        public NetworkDrive() { }
        internal NetworkDrive(string DriveName, string NetworkPath)
        {
            this.DriveName = DriveName;
            this.NetworkPath = NetworkPath;
            Start();
        }
        public static void SetDrive(string DriveName, string NetworkPath)
        {
            NetworkDrive d = new NetworkDrive(DriveName, NetworkPath);
        }
        internal void Start()
        {
            
            IWshNetwork_Class networkDriveK = new IWshNetwork_Class();
            DriveInfo getDrive = new DriveInfo(DriveName);
            if (!getDrive.IsReady)
            {
                networkDriveK.MapNetworkDrive(DriveName, NetworkPath, true);
                Console.WriteLine(@"Added drive {0} {1}!", DriveName, NetworkPath);
            }
            else
            {
                Console.WriteLine(@"The drive {0} {1} is already set.", DriveName, NetworkPath);
            }
        }




    }
}
