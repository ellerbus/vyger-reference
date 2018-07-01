using System.IO;
using System.Web.Hosting;
using Augment;
using EnsureThat;
using vyger.Common;

namespace vyger.Services
{
    public abstract class BaseService
    {
        #region Constructor

        protected BaseService(ISecurityActor actor)
        {
            Actor = actor;
        }

        #endregion

        #region Properties

        protected ISecurityActor Actor { get; private set; }

        protected abstract string FileName { get; }

        #endregion

        #region Loaders/Savers

        protected T ReadData<T>()
        {
            if (Actor.IsAuthenticated)
            {

                string fullpath = GetFullPath();

                if (File.Exists(GetFullPath()))
                {
                    string xml = File.ReadAllText(fullpath);

                    return Serializers.FromXml<T>(xml);
                }
            }

            return default(T);
        }

        protected void SaveData(object value)
        {
            string xml = Serializers.ToXml(value);

            string fullpath = GetFullPath();

            File.WriteAllText(fullpath, xml);
        }

        private string GetFullPath()
        {
            Ensure.That(Actor, nameof(Actor)).IsNotNull();
            Ensure.That(Actor.Email, nameof(Actor.Email)).IsNotEmpty();

            string folder = Constants.GetMemberFolder(Actor.Email);

            string path = HostingEnvironment.MapPath($"~/App_Data/{folder}");

            if (HostingEnvironment.IsDevelopmentEnvironment)
            {
                path += "@local";
            }

            return Path.Combine(path, FileName + ".xml");
        }

        #endregion
    }
}