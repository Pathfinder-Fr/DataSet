// -----------------------------------------------------------------------
// <copyright file="SpellController.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Controllers
{
    using System.Web.Mvc;
    using System.Xml.Serialization;
    using Datas;
    using Models;
    using Models.Spell;
    using Schema;

    public class SpellController : ItemController<Spell, ItemViewModel, EditViewModel>
    {
        public SpellController() :
            base(Datas.DbDocumentType.Spells)
        {
        }

        [HttpPost]
        public ActionResult Import()
        {
            var serializer = new XmlSerializer(typeof(DataSet));
            var dataSet = (DataSet)serializer.Deserialize(this.Request.InputStream);

            using (var db = this.OpenDb())
            {
                foreach (var spell in dataSet.Spells)
                {
                    // force id update
                    spell.Id = Ids.Normalize(spell.Name);

                    var dbDoc = DbDocument.From(this.DocType, spell);
                    db.Documents.Add(dbDoc);
                }

                db.SaveChanges();
            }

            return null;
        }
    }
}