using System;
using System.Collections.Generic;
using System.IO;
using EnsureThat;
using vyger.Common;
using YamlDotNet.Serialization;

namespace vyger.Services
{
    public class BaseService<T>
    {
        #region Members

        protected enum RepositoryTypes { Csv, Yaml }

        #endregion

        #region Constructor

        protected BaseService(ISecurityActor actor, RepositoryTypes repositoryType)
        {
            Actor = actor;
            RepositoryType = repositoryType;
        }

        #endregion

        #region Properties

        protected ISecurityActor Actor { get; private set; }

        protected RepositoryTypes RepositoryType { get; private set; }

        #endregion

        #region Loaders/Savers

        protected virtual T LoadOne()
        {
            if (File.Exists(FileName))
            {
                switch (RepositoryType)
                {
                    case RepositoryTypes.Yaml:
                        return GetOneFromYaml();

                    default:
                        throw new NotImplementedException($"Unhandled LoadOne ({RepositoryType})");
                }
            }

            return default(T);
        }

        private T GetOneFromYaml()
        {
            using (StreamReader sr = new StreamReader(FileName))
            {
                Deserializer deserializer = new DeserializerBuilder().Build();

                return deserializer.Deserialize<T>(sr.ReadToEnd());
            }
        }

        protected virtual IEnumerable<T> LoadAll()
        {
            if (File.Exists(FileName))
            {
                switch (RepositoryType)
                {
                    case RepositoryTypes.Yaml:
                        return GetAllFromYaml();

                    default:
                        throw new NotImplementedException($"Unhandled LoadAll ({RepositoryType})");
                }
            }

            return new T[0];
        }

        private IEnumerable<T> GetAllFromYaml()
        {
            using (StreamReader sr = new StreamReader(FileName))
            {
                Deserializer deserializer = new DeserializerBuilder().Build();

                return deserializer.Deserialize<IEnumerable<T>>(sr.ReadToEnd());
            }
        }

        protected virtual void SaveOne(T one)
        {
            switch (RepositoryType)
            {
                case RepositoryTypes.Yaml:
                    SaveOneYaml(one);
                    break;

                default:
                    throw new NotImplementedException($"Unhandled SaveOne ({RepositoryType})");
            }
        }

        private void SaveOneYaml(T one)
        {
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                Serializer serializer = new SerializerBuilder().Build();

                sw.Write(serializer.Serialize(one));
            }
        }

        protected virtual void SaveAll(IEnumerable<T> all)
        {
            switch (RepositoryType)
            {
                case RepositoryTypes.Yaml:
                    SaveAllYaml(all);
                    break;

                default:
                    throw new NotImplementedException($"Unhandled SaveAll ({RepositoryType})");
            }
        }

        private void SaveAllYaml(IEnumerable<T> all)
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

                string folder = Constants.GetMemberFolder(Actor.Email);

                string path = System.Web.Hosting.HostingEnvironment.MapPath($"~/App_Data/{folder}");

                return Path.Combine(path, typeof(T).Name + ".yaml");
            }
        }

        #endregion
    }
}