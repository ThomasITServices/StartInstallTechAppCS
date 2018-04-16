using System;
using IWshRuntimeLibrary;

namespace ThomasITServices
{
   public  class AddShortCut
    {
        string targetPath;
        string iconLocation = @"%SystemRoot%\system32\imageres.dll,3";
        string shortCutPath;
        public string TargetPath
        {
            get
            {
                return this.targetPath;
            }
            set
            {
                this.targetPath = value;
            }

        }
        public string IconLocation
        {
            get
            {
                return this.iconLocation;
            }
            set
            {
                this.iconLocation = value;
            }

        }
        public string ShortCutPath
        {
            get
            {
                return this.shortCutPath;
            }
            set
            {
                this.shortCutPath = value;
            }

        }

        public AddShortCut() { }
        public AddShortCut(string Path, string LinkTo)
        {
            this.ShortCutPath = Path;
            this.TargetPath = LinkTo;
            
            this.Start();
        }
        public AddShortCut(string Path, string LinkTo, string IconPath)
        {
            this.ShortCutPath = Path;
            this.TargetPath = LinkTo;
            this.IconLocation = IconPath;
            this.Start();
        }

        public void Start()
        {
            try
            {
                WshShell shell = new WshShell();
                IWshShortcut link = (IWshShortcut)shell.CreateShortcut(this.ShortCutPath);
                link.TargetPath = this.TargetPath;
                link.WorkingDirectory = this.TargetPath;
                link.IconLocation = this.iconLocation;
                link.Save();
                Console.WriteLine(this.shortCutPath);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        
    }
}

