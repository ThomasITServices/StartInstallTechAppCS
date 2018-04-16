using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ThomasITServices
{
    public class SetTechAppsENV
    {
        internal string Target { get; private set; }
        internal string[] Paths { get; set; }
        internal EnvironmentVariableTarget Machine { get; private set; }
        

        internal SetTechAppsENV(string Target, string[] Paths, EnvironmentVariableTarget Machine)
        {
            this.Target = Target;
            this.Paths = Paths;
            this.Machine = Machine;
            Start();
        }

        public static void Add(string Target, string[] Paths, EnvironmentVariableTarget Machine)
        {
            SetTechAppsENV s = new SetTechAppsENV(Target, Paths, Machine);
        }

        internal void Start()
        {
            

            EVEditor editor = new EVEditor();
            string machinePath = editor.GetEnvironmentVariableByName(this.Target, this.Machine);
            Char delimiter = ';';
            ArrayList beforeSubPaths = new ArrayList(machinePath.Split(delimiter));
            ArrayList subPaths = new ArrayList(machinePath.Split(delimiter));
            foreach (var path in this.Paths)
            {
                if (!subPaths.Contains(path))
                {
                    Console.WriteLine("Adding to machine path: {0}", path);
                    subPaths.Add(path);
                }
            }
                if (!(beforeSubPaths.ToArray() as IStructuralEquatable).Equals(subPaths.ToArray(), EqualityComparer<string>.Default))
                {
                    //Console.WriteLine((beforeSubPaths.ToArray() as IStructuralEquatable).Equals(subPaths.ToArray(), EqualityComparer<string>.Default));
                    editor.SetEnvironmentVariableValue("path", string.Join(";", subPaths.ToArray()), this.Machine);
                }

            string afterMachinePath = editor.GetEnvironmentVariableByName("path", this.Machine);
            ArrayList afterSubPaths = new ArrayList(afterMachinePath.Split(delimiter));
            afterSubPaths.Sort();
            foreach (var path in afterSubPaths)
            {
                Console.WriteLine(path);
            }
            
        }


    }
}
