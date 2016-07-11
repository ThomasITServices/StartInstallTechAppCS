using System;
using System.Diagnostics;
using System.Media;
using System.Threading;

namespace ThomasITServices
{
    class VarForInstaller
    {

        private string currentPath = Environment.CurrentDirectory;
        private string fileName;
        private string arguments;
        private string appName;

        public string CurrentPath
        {
            get
            {
                return currentPath;
            }
            set
            {
                currentPath = value;
            }
        }
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }
        public string Arguments
        {
            get
            {
                return arguments;
            }
            set
            {
                arguments = value;
            }
        }
        public string AppName
        {
            get
            {
                return appName;
            }
            set
            {
                appName = value;
            }
        }
        public string WorkingDirectory { get;  set; }

    }

    class Installer : VarForInstaller
    {
        public Installer() { }
        internal Installer(string AppName, string FileName)
        {
            this.AppName = AppName;
            this.FileName = FileName;
            this.StartInstaller();
        }
        internal Installer(string AppName, string FileName, string WorkingDirectory)
        {
            this.AppName = AppName;
            this.FileName = FileName;
            this.WorkingDirectory = WorkingDirectory;
            this.StartInstaller();
        }
        internal Installer(string AppName, string FileName, string Arguments, string WorkingDirectory)
        {
            this.AppName = AppName;
            this.WorkingDirectory = WorkingDirectory;
            this.FileName = FileName;
            this.Arguments = Arguments;
            this.StartInstaller();
        }


        public static void Start(string AppName, string FileName)
        {
            Installer i = new Installer(AppName, FileName);
        }

        public static void Start(string AppName, string FileName, string WorkingDirectory)
        {
            Installer i = new Installer(AppName, FileName, WorkingDirectory);
        }
        public static void Start(string AppName, string FileName, string Arguments, string WorkingDirectory)
        {
            Installer i = new Installer(AppName, FileName, Arguments, WorkingDirectory);
        }


        internal void StartInstaller()
        {
            try
            {
                Process p = new Process();
               
                p.StartInfo.FileName = this.FileName;
                p.StartInfo.WorkingDirectory = this.WorkingDirectory;
                p.StartInfo.Arguments = this.Arguments;
                Console.WriteLine("Installing {0}", this.AppName);
                p.Start();
                p.WaitForExit();
                Console.WriteLine("     {0} is now installed!!", this.AppName);
                SystemSounds.Exclamation.Play();
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                SystemSounds.Hand.Play();
                Console.WriteLine("Exception caught: {0}!!", e.Message);
                Console.WriteLine("Current Path: {0}", this.CurrentPath);
                Console.WriteLine("File Name Needed: {0}", this.FileName);
                Console.WriteLine("Make sure {0} is in the same directory as this app.", this.FileName);
                Console.Read();
            }

        }
    }

}
