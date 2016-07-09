using System;
using ThomasITServices;

namespace StartInstallTechAppCS
{
    class Program
    {
        static void Main(string[] args)
        {
            //EVEditor editor = new EVEditor();

            //foreach (KeyValuePair<string, string> entry in editor.GetEnvironmentVariables())
            //{
            //    Console.WriteLine(entry.Key + " - " + entry.Value);
            //}

            //Console.ReadLine();
            //EVEditor editor = new EVEditor();

            //foreach (KeyValuePair<string, string> entry in editor.GetEnvironmentVariablesByTarget(EnvironmentVariableTarget.Machine))
            //{
            //    Console.WriteLine(entry.Key + " - " + entry.Value);
            //}

            //Console.ReadLine();
            EVEditor editor = new EVEditor();
            string machinePath = editor.GetEnvironmentVariableByName("path", EnvironmentVariableTarget.Machine);
            Char delimiter = ';';
            string[] subPaths = machinePath.Split(delimiter);
            foreach (var path in subPaths)
            {
                Console.WriteLine(path);
            }
            
            Console.ReadLine();
        }
    }
}
