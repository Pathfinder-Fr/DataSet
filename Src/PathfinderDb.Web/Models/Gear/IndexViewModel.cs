// -----------------------------------------------------------------------
// <copyright file="IndexViewModel.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Models.Gear
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using Datas;

    public class IndexViewModel : Page<ItemViewModel>
    {
        [UIHint("SourceNullable")]
        public string Source { get; set; }

        public GearCategoryViewModel? Category { get; set; }

        public IEnumerable<Expression<Func<DbDocument, bool>>> AsExpressions()
        {
            if (!string.IsNullOrEmpty(this.Source))
            {
                yield return x => x.Source == this.Source;
            }

            if (this.Category.HasValue)
            {
                var categoryText = this.Category.Value.ToString();
                yield return x => x.Category == categoryText;
            }
        }
    }
}