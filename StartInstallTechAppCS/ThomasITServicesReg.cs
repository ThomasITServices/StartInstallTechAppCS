using Microsoft.Win32;
using System;

namespace ThomasITServices
{
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

}
