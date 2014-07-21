using System;

namespace PathfinderDb.Models
{
    public class DbDocument
    {
        public DbDocument()
        {
        }

        public int Id { get; set; }

        public DbDocumentType Type { get; set; }

        public string XmlContent { get; set; }
    }
}