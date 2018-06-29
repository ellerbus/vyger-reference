Converting to XML
- working serialization / services / models

```cs
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Yamler
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Parent parent = new Parent();

            for (var x = 0; x < 5; x++)
            {
                parent.Children.Add(new Child() { Id = x + 1 });
            }

            //string yaml = new Serializer().Serialize(parent);

            //Console.WriteLine(yaml);

            //Parent clone = new Deserializer().Deserialize<Parent>(yaml);

            //string json = JsonConvert.SerializeObject(parent, Formatting.Indented);

            //Console.WriteLine(json);

            //Parent clone = JsonConvert.DeserializeObject<Parent>(json);

            string xml = parent.Serialize();

            Console.WriteLine(xml);

            //Parent clone = Deserialize<Parent>(xml);
        }

        public static T Deserialize<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringReader rdr = new StringReader(xml))
            {
                T results = (T)serializer.Deserialize(rdr);

                return results;
            }
        }

        public static string Serialize<T>(this T value)
        {
            if (value == null) return string.Empty;

            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
                {
                    xmlSerializer.Serialize(xmlWriter, value);
                    return stringWriter.ToString();
                }
            }
        }
    }

    [XmlRoot("parent")]
    public class Parent
    {
        public Parent()
        {
            Children = new ChildCollection(this);
        }

        [XmlArray("children"), XmlArrayItem("child")]
        public ChildCollection Children { get; private set; }
    }

    [XmlRoot("child`")]
    public class Child
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
    }

    [XmlRoot("children")]
    public class ChildCollection : Collection<Child>
    {
        public ChildCollection(Parent parent)
        {
        }
    }
}
```