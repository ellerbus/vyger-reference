using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Augment;

namespace vyger.Web
{
    public class ApplicationDetails
    {
        #region Members

        public static readonly ApplicationDetails Default = new ApplicationDetails();

        #endregion

        #region Constructors

        private ApplicationDetails()
        {
            Version v = typeof(Global).Assembly.GetName().Version;

            Date = new DateTime(2000, 1, 1)
                .AddDays(v.Build)
                .AddSeconds(v.Revision * 2);

            Version = "{0}.{1}.{2}.{3}".FormatArgs(v.Major, v.Minor, v.Build, v.Revision);

#if !DEBUG
            Scripts = GetScripts();
#endif
        }

        #endregion

        #region Methods

        protected string GetScripts()
        {
            StringBuilder sb = new StringBuilder();

            //if (HostingEnvironment.IsDevelopmentEnvironment)
            //{
            //    DirectoryInfo root = new DirectoryInfo(HostingEnvironment.MapPath("~/ClientApp"));

            //    foreach (string file in GetScripts(root))
            //    {
            //        string js = file.Replace(root.FullName, "").Replace('\\', '/');

            //        sb.AppendFormat("  <script type='text/javascript' src='/ClientApp{0}'></script>", js).AppendLine();
            //    }
            //}
            //else
            //{
            //    string v = typeof(Global).Assembly.GetName().Version.ToString();

            //    sb.AppendFormat("  <script type='text/javascript' src='/Content/Sniper.min.js?v={0}'></script>", v).AppendLine();
            //}

            return sb.ToString();
        }

        private IEnumerable<string> GetScripts(DirectoryInfo dir)
        {
            foreach (DirectoryInfo d in dir.GetDirectories().OrderBy(x => x.Name))
            {
                foreach (string s in GetScripts(d))
                {
                    yield return s;
                }
            }

            foreach (FileInfo f in dir.GetFiles("*.js"))
            {
                //if (!f.Name.EndsWith("Tests.js") && !f.Name.EndsWith("Templates.js"))
                {
                    yield return f.FullName;
                }
            }
        }

        #endregion

        #region Properties

        public string Version { get; private set; }

        public DateTime Date { get; private set; }

        public string Scripts
        {
            get
            {
#if DEBUG
                _scripts = GetScripts();
#endif
                return _scripts;
            }
            private set { _scripts = value; }
        }
        private string _scripts;

        #endregion
    }
}