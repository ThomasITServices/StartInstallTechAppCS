using System;
using System.Collections.Generic;
using ThomasITServices;
using System.Collections;

namespace StartInstallTechAppCS
{
    class Program
    {
        static void Main(string[] args)
        {
            EVEditor editor = new EVEditor();

            //foreach (KeyValuePair<string, string> entry in editor.GetEnvironmentVariables())
            //{
            //    Console.WriteLine(entry.Key + " - " + entry.Value);
            //}

            //Console.ReadLine();

            //foreach (KeyValuePair<string, string> entry in editor.GetEnvironmentVariablesByTarget(EnvironmentVariableTarget.Machine))
            //{
            //    Console.WriteLine(entry.Key + " - " + entry.Value);
            //}

            //Console.ReadLine();

            string machinePath = editor.GetEnvironmentVariableByName("path", EnvironmentVariableTarget.Machine);
            Char delimiter = ';';
            ArrayList beforeSubPaths = new ArrayList(machinePath.Split(delimiter));
            ArrayList subPaths = new ArrayList(machinePath.Split(delimiter));
            if (!subPaths.Contains(@"C:\Python27"))
            {
                subPaths.Add(@"C:\Python27");
            }
            
            if (!(beforeSubPaths.ToArray() as IStructuralEquatable).Equals(subPaths.ToArray(), EqualityComparer<string>.Default))
            {
                //Console.WriteLine((beforeSubPaths.ToArray() as IStructuralEquatable).Equals(subPaths.ToArray(), EqualityComparer<string>.Default));
                editor.SetEnvironmentVariableValue("path", string.Join(";", subPaths.ToArray()), EnvironmentVariableTarget.Machine);
            }
            
            subPaths.Sort();
            foreach (var path in subPaths)
            {
                Console.WriteLine(path);
            }
            Console.ReadLine();
            

            //Console.WriteLine(string.Join(";", subPaths.ToArray()));
            //Console.ReadLine();
        }
    }
}
