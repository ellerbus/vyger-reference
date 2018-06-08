using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace vyger.Database
{
    class CommandLineArguments
    {
        #region Members

        public static CommandLineArguments Default { get; } = new CommandLineArguments();

        private Dictionary<string, string> _arguments = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Constructors

        private CommandLineArguments()
        {
            List<string> args = new List<string>(Environment.GetCommandLineArgs());

            if (args.Count > 0 && args[0].StartsWith(Environment.CommandLine))
            {
                args.RemoveAt(0);
            }

            string namePattern = @"^[-/]?(?<name>[a-z0-9][a-z0-9\-_]*)";

            RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase;

            Regex nameRegex = new Regex(namePattern + "[=:]", options);

            foreach (string text in args)
            {
                Match m = nameRegex.Match(text);

                if (m.Success)
                {
                    string name = GetName(m.Groups["name"].Value);
                    string value = text.Substring(m.Length);

                    _arguments.Add(name, value);
                }
                else
                {
                    _arguments.Add(m.Groups["name"].Value, null);
                }
            }
        }

        public bool Contains(string name)
        {
            return _arguments.ContainsKey(name);
        }

        private string GetName(string name)
        {
            if (name.StartsWith("-"))
            {
                name = name.Substring(1);
            }
            if (name.StartsWith("/"))
            {
                name = name.Substring(1);
            }

            return name;
        }

        #endregion

        #region Methods

        #endregion

        #region Properties

        public string this[string key]
        {
            get
            {
                if (_arguments.ContainsKey(key))
                {
                    return _arguments[key];
                }
                return null;
            }
        }

        #endregion
    }
}
