// -----------------------------------------------------------------------
// <copyright file="DbDocument.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Datas
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using Models;
    using Schema;

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

        public string Name { get; set; }

        public string Source { get; set; }

        public string Category { get; set; }

        public bool HasEnglishName { get; set; }

        public string Content { get; set; }

        public void SerializeContent(Element contentObject)
        {
            var adapter = this.GetAdapter();
            this.Source = contentObject.Source.Id ?? Schema.Source.Ids.PathfinderRpg;
            this.Name = adapter.GetName(contentObject);
            this.HasEnglishName = contentObject.HasEnglishNameFor(adapter);
            this.Category = adapter.GetCategory(contentObject);

            var serializer = new XmlSerializer(adapter.SchemaType, Namespaces.PathfinderDb);

            var ns = new XmlSerializerNamespaces();
            ns.Add("", Namespaces.PathfinderDb);

            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { OmitXmlDeclaration = true }))
            {
                serializer.Serialize(xmlWriter, contentObject, ns);
                this.Content = stringWriter.ToString();
            }
        }

        public static DbDocument From<T>(DbDocumentType type, T source) where T : Element, IElementWithId
        {
            var adapter = GetAdapterFor(type);

            var result = new DbDocument
            {
                Type = type,
                Lang = DataSetLanguages.French,
                Id = source.Id,
                HasEnglishName = source.HasEnglishNameFor(adapter),
                Source = source.Source.Id,
                Category = adapter.GetCategory(source),
            };

            result.SerializeContent(source);

            return result;
        }

        public object As(Type type)
        {
            var serializer = new XmlSerializer(type, Namespaces.PathfinderDb);
            using (var stringReader = new StringReader(this.Content))
            {
                return serializer.Deserialize(stringReader);
            }
        }

        public T As<T>() where T : Element, IElementWithId
        {
            var serializer = new XmlSerializer(typeof(T), Namespaces.PathfinderDb);

            using (var stringReader = new StringReader(this.Content))
            {
                T item = (T)serializer.Deserialize(stringReader);
                item.Id = this.Id;
                return item;
            }
        }

        public static ISchemaAdapter GetAdapterFor(DbDocumentType type)
        {
            switch (type)
            {
                case DbDocumentType.Spells:
                    return Models.Spell.SpellAdapter.Instance;

                case DbDocumentType.Gear:
                    return Models.Gear.GearAdapter.Instance;

                default:
                    throw new NotSupportedException(type.ToString());
            }
        }

        public ISchemaAdapter GetAdapter()
        {
            return GetAdapterFor(this.Type);
        }
    }
}