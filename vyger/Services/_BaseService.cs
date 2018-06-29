using System.IO;
using System.Web.Hosting;
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
            string xml = GetContents();

            return Serializers.FromXml<T>(xml);
        }

        protected void SaveData(object value)
        {
            string xml = Serializers.ToXml(value);

            PutContents(xml);
        }

        private string GetContents()
        {
            string fullpath = GetFullPath();

            return File.ReadAllText(fullpath);
        }

        private void PutContents(string contents)
        {
            string fullpath = GetFullPath();

            File.WriteAllText(fullpath, contents);
        }

        private string GetFullPath()
        {
            Ensure.That(Actor, nameof(Actor)).IsNotNull();
            Ensure.That(Actor.Email, nameof(Actor.Email)).IsNotEmpty();

            string folder = Constants.GetMemberFolder(Actor.Email);

            string path = HostingEnvironment.MapPath($"~/App_Data/{folder}");

            return Path.Combine(path, FileName + ".xml");
        }

        #endregion
    }
}