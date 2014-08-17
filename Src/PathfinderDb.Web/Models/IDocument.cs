// -----------------------------------------------------------------------
// <copyright file="IDocument.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using Schema;

    public interface IDocument
    {
        int DocId { get; set; }

        string XmlContent { get; set; }
    }

    public interface IDocument<T> : IDocument
    {
    }

    public static class DocumentExtensions
    {
        public static void Serialize<T>(this IDocument @this, T value)
        {
            var serializer = new XmlSerializer(typeof(T), Namespaces.PathfinderDb);

            var ns = new XmlSerializerNamespaces();
            ns.Add("", Namespaces.PathfinderDb);

            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { OmitXmlDeclaration = true }))
            {
                serializer.Serialize(xmlWriter, value, ns);
                @this.XmlContent = stringWriter.ToString();
            }
        }

        public static void Serialize<T>(this IDocument<T> @this, T value)
        {
            var serializer = new XmlSerializer(typeof(T), Namespaces.PathfinderDb);

            var ns = new XmlSerializerNamespaces();
            ns.Add("", Namespaces.PathfinderDb);

            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { OmitXmlDeclaration = true }))
            {
                serializer.Serialize(xmlWriter, value, ns);
                @this.XmlContent = stringWriter.ToString();
            }
        }

        public static T Deserialize<T>(this IDocument @this)
        {
            var serializer = new XmlSerializer(typeof(T), Namespaces.PathfinderDb);
            using (var stringReader = new StringReader(@this.XmlContent))
            {
                return (T)serializer.Deserialize(stringReader);
            }
        }

        public static T Deserialize<T>(this IDocument<T> @this)
        {
            var serializer = new XmlSerializer(typeof(T), Namespaces.PathfinderDb);
            using (var stringReader = new StringReader(@this.XmlContent))
            {
                return (T)serializer.Deserialize(stringReader);
            }
        }
    }
}