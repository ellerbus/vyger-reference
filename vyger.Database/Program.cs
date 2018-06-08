using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using Augment.SqlServer;

namespace vyger.Database
{
    class Program
    {
        static void Main(string[] args)
        {
            var exporter = new Exporter();

            exporter.Export();

            using (DbConnection connection = GetOpenConnection())
            {
                SqlConnection con = connection as SqlConnection;

                Installer installer = new Installer(typeof(Program).Assembly, con);

                installer.Install();
            }
        }

        public static DbConnection GetOpenConnection()
        {
            ConnectionStringSettings css = GetConnectionStringSettings();

            DbProviderFactory factory = DbProviderFactories.GetFactory(css.ProviderName);

            DbConnection con = factory.CreateConnection();

            con.ConnectionString = css.ConnectionString;

            con.Open();

            return con;
        }

        private static ConnectionStringSettings GetConnectionStringSettings()
        {
            string key = "DB.local";

            if (CommandLineArguments.Default.Contains("build"))
            {
                key = "DB." + CommandLineArguments.Default["environment"];
            }
            else
            {
                string path = GetSolutionPath(@"vyger.Web\App_Data");

                AppDomain.CurrentDomain.SetData("DataDirectory", path);
            }

            Console.WriteLine($"Key: {key}");

            ConnectionStringSettings css = ConfigurationManager.ConnectionStrings[key];

            Console.WriteLine();

            return css;
        }

        private static string GetSolutionPath(string target)
        {
            string me = AppDomain.CurrentDomain.BaseDirectory;

            string path = Path.Combine(me, @"..\..\..\");

            path = Path.GetFullPath(path);

            path = Path.Combine(path, target);

            return path;
        }

        public static string GetProjectPath(string target)
        {
            string me = AppDomain.CurrentDomain.BaseDirectory;

            string path = Path.Combine(me, @"..\..\");

            path = Path.GetFullPath(path);

            path = Path.Combine(path, target);

            return path;
        }
    }
}
