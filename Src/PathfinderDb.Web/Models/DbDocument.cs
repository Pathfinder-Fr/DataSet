// -----------------------------------------------------------------------
// <copyright file="DbDocument.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    public class DbDocument
    {
        public int DocId { get; set; }

        [Index("IX_DbDocument_RefByTypeByLang", IsUnique = true, Order = 0)]
        public DbDocumentType Type { get; set; }

        [Index("IX_DbDocument_RefByTypeByLang", IsUnique = true, Order = 1)]
        [MaxLength(6)]
        public string Lang { get; set; }

        [Index("IX_DbDocument_RefByTypeByLang", IsUnique = true, Order = 2)]
        [MaxLength(100)]
        public string Id { get; set; }

        public string Content { get; set; }

        public void SerializeContent<T>(T contentObject) where T : Schema.Element, Schema.IElementWithId
        {

            var serializer = new XmlSerializer(typeof(T), Schema.Namespaces.PathfinderDb);

            var ns = new XmlSerializerNamespaces();
            ns.Add("", Schema.Namespaces.PathfinderDb);

            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { OmitXmlDeclaration = true }))
            {
                serializer.Serialize(xmlWriter, contentObject, ns);
                this.Content = stringWriter.ToString();
            }
        }

        public static DbDocument From<T>(DbDocumentType type, T source) where T : Schema.Element, Schema.IElementWithId
        {
            var result = new DbDocument
            {
                Type = type,
                Lang = Schema.DataSetLanguages.French,
                Id = Schema.Ids.Normalize(source.Id)
            };

            result.SerializeContent(source);

            return result;
        }

        public object As(Type type)
        {
            var serializer = new XmlSerializer(type);
            using (var stringReader = new StringReader(this.Content))
            {
                return serializer.Deserialize(stringReader);
            }
        }

        public T As<T>() where T : Schema.Element, Schema.IElementWithId
        {
            var serializer = new XmlSerializer(typeof(T), Schema.Namespaces.PathfinderDb);

            using (var stringReader = new StringReader(this.Content))
            {
                T item = (T)serializer.Deserialize(stringReader);
                item.Id = this.Id;
                return item;
            }
        }
    }
}