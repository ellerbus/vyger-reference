using System.Collections.Generic;
using System.IO;
using EnsureThat;
using vyger.Common;
using YamlDotNet.Serialization;

namespace vyger.Services
{
    public class BaseService<T>
    {
        #region Constructor

        protected BaseService(ISecurityActor actor)
        {
            Actor = actor;
        }

        #endregion

        #region Properties

        protected ISecurityActor Actor { get; private set; }

        #endregion

        #region Loaders/Savers

        protected virtual T LoadOne()
        {
            if (File.Exists(FileName))
            {
                using (StreamReader sr = new StreamReader(FileName))
                {
                    Deserializer deserializer = new DeserializerBuilder().Build();

                    return deserializer.Deserialize<T>(sr.ReadToEnd());
                }
            }

            return default(T);
        }


        protected virtual IEnumerable<T> LoadAll()
        {
            if (File.Exists(FileName))
            {
                using (StreamReader sr = new StreamReader(FileName))
                {
                    Deserializer deserializer = new DeserializerBuilder().Build();

                    return deserializer.Deserialize<IEnumerable<T>>(sr.ReadToEnd());
                }
            }

            return new T[0];
        }

        protected virtual void SaveOne(T one)
        {
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                Serializer serializer = new SerializerBuilder().Build();

                sw.Write(serializer.Serialize(one));
            }
        }

        protected virtual void SaveAll(IEnumerable<T> all)
        {
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                Serializer serializer = new SerializerBuilder().Build();

                sw.Write(serializer.Serialize(all));
            }
        }

        private string FileName
        {
            get
            {
                Ensure.That(Actor, nameof(Actor)).IsNotNull();
                Ensure.That(Actor.Email, nameof(Actor.Email)).IsNotEmpty();

                string path = System.Web.Hosting.HostingEnvironment.MapPath($"~/App_Data/{Actor.Email}");

                return Path.Combine(path, typeof(T).Name + ".yaml");
            }
        }

        #endregion
    }
}