using System;
using System.Collections.Generic;
using System.Collections;
using System.Security;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace ThomasITServices
{
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
        public ConcurrentDictionary<string, string> GetEnvironmentVariables()
        {
            try
            {
                //dictionary object to hold the key/value pairs
                ConcurrentDictionary<string, string> variables = new ConcurrentDictionary<string, string>();

                //loop through the list and add to our dictionary list
                Parallel.ForEach<DictionaryEntry>(Environment.GetEnvironmentVariables().OfType<DictionaryEntry>(), entry =>
                {

                    variables.GetOrAdd(entry.Key.ToString(), entry.Value.ToString());
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
        public ConcurrentDictionary<string, string> GetEnvironmentVariablesByTarget(EnvironmentVariableTarget target)
        {
            try
            {
                ConcurrentDictionary<string, string> variables = new ConcurrentDictionary<string, string>();

                Parallel.ForEach<DictionaryEntry>(Environment.GetEnvironmentVariables(target).OfType<DictionaryEntry>(), entry =>
                {
                    variables.GetOrAdd(entry.Key.ToString(), entry.Value.ToString());
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

                Console.WriteLine(string.Format("The environment variable \"{0}\" at {1} level has been updated", variable, target.ToString()));

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

    class SetTechAppsENV
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
                    editor.SetEnvironmentVariableValue("path", string.Join(";", subPaths.ToArray()), EnvironmentVariableTarget.Machine);
                }

            string afterMachinePath = editor.GetEnvironmentVariableByName("path", EnvironmentVariableTarget.Machine);
            ArrayList afterSubPaths = new ArrayList(afterMachinePath.Split(delimiter));
            afterSubPaths.Sort();
            foreach (var path in afterSubPaths)
            {
                Console.WriteLine(path);
            }
            
        }


    }
}
