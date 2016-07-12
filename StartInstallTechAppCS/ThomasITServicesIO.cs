using System;
using System.IO;

namespace ThomasITServices
{
    class VarFromTo
    {
        string sourcePath;
        string destinationPath;
        string fileName;
        public bool IsDir;
        public string SourcePath
        {
            get
            {
                return sourcePath;
            }
            set
            {
                sourcePath = value;
            }

        }
        public string DestinationPath
        {
            get
            {
                return destinationPath;
            }
            set
            {
                destinationPath = value;
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
    }

    class CopyTo : VarFromTo
    {
        public CopyTo() { }
        public CopyTo(string SourcePath, string DestinationPath)
        {
            this.IsDir = true;
            this.SourcePath = SourcePath;
            this.DestinationPath = DestinationPath;
            this.Start();
        }
        public CopyTo(string SourcePath, string DestinationPath, string FileName)
        {
            this.IsDir = false;
            this.SourcePath = SourcePath;
            this.DestinationPath = DestinationPath;
            this.FileName = FileName;
            this.Start();
        }

        public void Start()
        {
            try
            {
                if (!Directory.Exists(this.DestinationPath))
                {
                    Directory.CreateDirectory(this.DestinationPath);
                }

                Console.WriteLine("Trying To Copy Files From: {0}", this.SourcePath);
                if (IsDir)
                {
                    if (Directory.Exists(this.SourcePath))
                    {
                        string[] fileNames = Directory.GetFiles(this.SourcePath);
                        foreach (string s in fileNames)
                        {
                            this.FileName = Path.GetFileName(s);
                            string destFile = Path.Combine(this.DestinationPath, this.FileName);
                            System.IO.File.Copy(s, destFile, true);
                            Console.WriteLine("Copied File To: {0}", destFile);
                        }
                        Console.WriteLine("Completed Copying");
                    }
                    else
                    {
                        Console.WriteLine("Source path does not exist!");
                    }
                }
                else if (!IsDir)
                {
                    if (Directory.Exists(this.SourcePath))
                    {
                        string sourceFile = Path.Combine(this.SourcePath, this.FileName);
                        string destFile = Path.Combine(this.DestinationPath, this.FileName);
                        System.IO.File.Copy(sourceFile, destFile, true);
                        Console.WriteLine("Copied File To: {0}", destFile);
                        Console.WriteLine("Completed Copying");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: {0}!!", e.Message);
                Console.Read();
            }
        }
    }

    class DeleteFrom : VarFromTo
    {
        public DeleteFrom() { }
        public DeleteFrom(string SourcePath)
        {
            this.IsDir = true;
            this.SourcePath = SourcePath;
            this.Start();
        }
        public DeleteFrom(string SourcePath, string FileName)
        {
            this.IsDir = false;
            this.SourcePath = SourcePath;
            this.FileName = FileName;
            this.Start();
        }
        public void Start()
        {
            try
            {

                if (Directory.Exists(this.SourcePath))
                {
                    if (IsDir)
                    {

                        try
                        {
                            Console.WriteLine("Try To Delete: {0}", this.SourcePath);
                            Directory.Delete(this.SourcePath, true);
                            Console.WriteLine("Deleted Directory: {0}", this.SourcePath);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.Read();
                        }

                    }

                    else if (!IsDir)
                    {
                        try
                        {
                            string f = String.Format(@"{0}\{1}", this.SourcePath, this.FileName);

                            Console.WriteLine("Try To Delete: {0}", f);
                            if (System.IO.File.Exists(f))
                            {
                                System.IO.File.Delete(f);
                                Console.WriteLine("Deleted File: {0}", f);
                            }
                            else
                            {
                                string m = String.Format("Please Check Path: {0}", f);
                                IOException e = new IOException(m);
                                throw e;
                            }

                        }
                        catch (IOException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.Read();
                        }

                    }
                }
                else
                {
                    string m = String.Format("Please Check Path: {0}", this.SourcePath);
                    IOException e = new IOException(m);
                    throw e;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: {0}!!", e.Message);
                Console.Read();
            }
        }
    }

}
