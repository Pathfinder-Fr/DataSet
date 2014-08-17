// -----------------------------------------------------------------------
// <copyright file="HomeIndexViewModel.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    public class HomeIndexViewModel
    {
        public List<DataGroup> Groups { get; set; }

        public void CreateGroups(IDictionary<Datas.DbDocumentType, int> counts)
        {
            this.Groups = Datas.DbDocumentTypeDescription
                .All
                .Select(f => new DataGroup
                {
                    Controller = f.ControllerName,
                    DisplayName = f.Name,
                    Count = GetOrDefault(counts, f.Type)
                })
                .ToList();
        }

        private static TValue GetOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dict, TKey key, TValue @default = default(TValue))
        {
            TValue value;
            if (!dict.TryGetValue(key, out value))
            {
                return @default;
            }

            return value;
        }

        public class DataGroup
        {
            public string Controller { get; set; }

            public string DisplayName { get; set; }

            public int Count { get; set; }
        }
    }
}