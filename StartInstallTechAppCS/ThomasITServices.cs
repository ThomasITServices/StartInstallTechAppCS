using System;
using System.Diagnostics;
using System.Media;
using System.Threading;
using System.IO;
using Microsoft.Win32;
using IWshRuntimeLibrary;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

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

    }

    class Installer : VarForInstaller
    {
        public Installer() { }
        public Installer(string AppName, string FileName, string Arguments)
        {
            this.AppName = AppName;
            this.FileName = FileName;
            this.Arguments = Arguments;
            this.Start();
        }
        public Installer(string AppName,string SourcePath, string FileName, string Arguments)
        {
            this.AppName = AppName;
            this.CurrentPath = SourcePath;
            this.FileName = FileName;
            this.Arguments = Arguments;
            this.Start();
        }

        public void Start()
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = this.CurrentPath + @"\" + this.FileName;
                p.StartInfo.Arguments = this.Arguments;
                Console.WriteLine("Installing {0}", this.AppName);
                p.Start();
                p.WaitForExit();
                Console.WriteLine("     {0} is now installed!!", this.AppName);
                SystemSounds.Exclamation.Play();
                Thread.Sleep(5000);
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
        public DeleteFrom(string SourcePath,string FileName)
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

    class VarForRegistry
    {
        string subKeyPath;
        string keyName;
        string keyValue;
        public string SubKeyPath
        {
            set
            {
                subKeyPath = value;
            }
            get
            {
                return subKeyPath;
            }
        }
        public string KeyName
        {
            set
            {
                keyName = value;
            }
            get
            {
                return keyName;
            }
        }
        public string KeyValue
        {
            set
            {
                keyValue = value;
            }
            get
            {
                return keyValue;
            }
        }
    }

    class HKLMEditor : VarForRegistry
    {
        HKLMEditor(string LMSubKey, string KeyName, string KeyValue)
        {
            this.SubKeyPath = LMSubKey;
            this.KeyName = KeyName;
            this.KeyValue = KeyValue;
        }
        public void Start()
        {
            RegistryKey subKey = Registry.LocalMachine.OpenSubKey(this.SubKeyPath, true);
            subKey.SetValue(this.KeyName, this.KeyValue);

        }
        public void GetKeyValue()
        {
            RegistryKey subKey = Registry.LocalMachine.OpenSubKey(this.SubKeyPath, false);
            this.KeyValue = subKey.GetValue(this.KeyName).ToString();
            Console.WriteLine("Key: {0} Value: {1}", this.KeyName, this.KeyValue);

        }
    }

    class AddShortCut
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

    class EVEditor
    {
        private string _message;
        private bool _successful;

        public string Message
        {
            get { return _message; }
            private set { _message = value; }
        }

        public bool IsSuccessful
        {
            get { return _successful; }
            private set { _successful = value; }
        }

        #region System.Environment Namespace Version

        /// <summary>
        /// method for getting all available environment variables
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetEnvironmentVariables()
        {
            try
            {
                //dictionary object to hold the key/value pairs
                Dictionary<string, string> variables = new Dictionary<string, string>();

                //loop through the list and add to our dictionary list
                Parallel.ForEach<DictionaryEntry>(Environment.GetEnvironmentVariables().OfType<DictionaryEntry>(), entry =>
                {
                    variables.Add(entry.Key.ToString(), entry.Value.ToString());
                });

                return variables;
            }
            catch (SecurityException ex)
            {
                Console.WriteLine("Error retrieving environment variables: {0}", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// method for getting all environment variables by target:
        /// EnvironmentVariableTarget.User
        /// EnvironmentVariableTarget.Machine
        /// EnvironmentVariableTarget.Process
        /// </summary>
        /// <param name="target">the EnvironmentVariableTarget we want the variables for</param>
        /// <returns></returns>
        public Dictionary<string, string> GetEnvironmentVariablesByTarget(EnvironmentVariableTarget target)
        {
            try
            {
                Dictionary<string, string> variables = new Dictionary<string, string>();

                Parallel.ForEach<DictionaryEntry>(Environment.GetEnvironmentVariables(target).OfType<DictionaryEntry>(), entry =>
                {
                    variables.Add(entry.Key.ToString(), entry.Value.ToString());
                });

                return variables;
            }
            catch (SecurityException ex)
            {
                Console.WriteLine("Error retrieving environment variables: {0}", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// method for retrieving an environment variable by it's name
        /// </summary>
        /// <param name="name">the name of the environment variable we're looking for</param>
        /// <returns></returns>
        public string GetEnvironmentVariableByName(string name)
        {
            try
            {
                //get the variable
                string variable = Environment.GetEnvironmentVariable(name);

                //check for a value, if nothing is returned let the application know
                if (!string.IsNullOrEmpty(variable))
                    return variable;
                else
                    return "The requested environment variable could not be found";
            }
            catch (SecurityException ex)
            {
                Console.WriteLine(string.Format("Error searching for environment variable: {0}", ex.Message));
                return string.Empty;
            }
        }
        public string GetEnvironmentVariableByName(string name, EnvironmentVariableTarget target)
        {
            try
            {
                //get the variable
                string variable = Environment.GetEnvironmentVariable(name, target);

                //check for a value, if nothing is returned let the application know
                if (!string.IsNullOrEmpty(variable))
                    return variable;
                else
                    return "The requested environment variable could not be found";
            }
            catch (SecurityException ex)
            {
                Console.WriteLine(string.Format("Error searching for environment variable: {0}", ex.Message));
                return string.Empty;
            }
        }

        /// <summary>
        /// method for handling environment variable values. Will:
        /// Update
        /// Create
        /// Delete
        /// </summary>
        /// <param name="variable">the variable we're looking for</param>
        /// <param name="value">the value to set it to (null for deleting a variable)</param>
        /// <param name="target">the targer, defaults to User</param>
        /// <returns></returns>
        public void SetEnvironmentVariableValue(string variable, string value = null, EnvironmentVariableTarget target = EnvironmentVariableTarget.User)
        {
            try
            {
                //first make sure a name is provided
                if (string.IsNullOrEmpty(variable))
                    throw new ArgumentException("Variable names cannot be empty or null", "variable");

                if (!DoesEnvironmentVariableExist(variable, target))
                    throw new Exception(string.Format("The environment variable {0} was not found", variable));
                else
                    Environment.SetEnvironmentVariable(variable, value, target);

                Console.WriteLine(string.Format("The environment variable {0} has been deleted", variable));

            }
            catch (SecurityException ex)
            {
                Console.WriteLine(string.Format("Error setting environment variable {0}: {1}", variable, ex.Message));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(string.Format("Error setting environment variable {0}: {1}", variable, ex.Message));
            }
        }

        private bool DoesEnvironmentVariableExist(string variable, EnvironmentVariableTarget target = EnvironmentVariableTarget.User)
        {
            try
            {
                return string.IsNullOrEmpty(Environment.GetEnvironmentVariable(variable, target)) ? false : true;
            }
            catch (SecurityException)
            {
                return false;
            }
        }
        #endregion
    }

}

