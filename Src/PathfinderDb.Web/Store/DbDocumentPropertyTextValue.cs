using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PathfinderDb.Datas
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class DbDocumentPropertyTextValue
    {
        public int Id { get; set; }

        public int DocumentId { get; set; }

        [ForeignKey("DocumentId")]
        public DbDocument Document { get; set; }

        public int PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public DbDocumentProperty Property { get; set; }

        public int Value { get; set; }
    }
}