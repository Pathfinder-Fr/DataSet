// -----------------------------------------------------------------------
// <copyright file="HomeIndexViewModel.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public class HomeIndexViewModel
    {
        public List<DataGroup> Groups { get; set; }

        public void CreateGroups(IDictionary<Models.DbDocumentType, int> counts)
        {
            this.Groups = typeof(Models.DbDocumentType)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(f => new {
                    Field = f,
                    Type = (Models.DbDocumentType)f.GetValue(null),
                    Display = (DisplayAttribute)Attribute.GetCustomAttribute(f, typeof(DisplayAttribute)) })
                .Where(f => f.Display != null)
                .OrderBy(f => f.Display.Name)
                .Select(f => new DataGroup
                {
                    Controller = ControllerFromField(f.Type),
                    DisplayName = f.Display.Name,
                    Count = GetOrDefault(counts, f.Type)
                })
                .ToList();
        }

        private static TValue GetOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dict, TKey key, TValue @default = default(TValue))
        {
            TValue value;
            if(!dict.TryGetValue(key, out value))
            {
                return @default;
            }

            return value;

        }

        private static string ControllerFromField(Models.DbDocumentType type)
        {
            switch(type)
            {
                default:
                    return type.ToString();
            }
        }

        public class DataGroup
        {
            public string Controller { get; set; }

            public string DisplayName { get; set; }

            public int Count { get; set; }
        }
    }
}