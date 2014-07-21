// -----------------------------------------------------------------------
// <copyright file="DbDocument.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    public class DbDocument
    {
        public int Id { get; set; }

        public DbDocumentType Type { get; set; }

        public string Content { get; set; }

        public object As(Type type)
        {
            var serializer = new XmlSerializer(type);
            using (var stringReader = new StringReader(this.Content))
            {
                return serializer.Deserialize(stringReader);
            }
        }

        public T As<T>()
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stringReader = new StringReader(this.Content))
            {
                return (T)serializer.Deserialize(stringReader);
            }
        }
    }
}