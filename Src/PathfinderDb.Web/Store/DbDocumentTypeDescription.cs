// -----------------------------------------------------------------------
// <copyright file="DbDocumentTypeDescription.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Datas
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public class DbDocumentTypeDescription
    {
        private static IEnumerable<DbDocumentTypeDescription> all;

        public static IEnumerable<DbDocumentTypeDescription> All
        {
            get { return all ?? (all = GetAll()); }
        }

        public DbDocumentType Type { get; set; }

        public string Name { get; set; }

        public string ControllerName { get; set; }

        private static IEnumerable<DbDocumentTypeDescription> GetAll()
        {
            return typeof(DbDocumentType)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(f => new
                {
                    Field = f,
                    Type = (DbDocumentType)f.GetValue(null),
                    Display = (DisplayAttribute)Attribute.GetCustomAttribute(f, typeof(DisplayAttribute))
                })
                .Where(f => f.Display != null)
                .OrderBy(f => f.Display.Name)
                .Select(f => new DbDocumentTypeDescription
                {
                    Type = f.Type,
                    Name = f.Display.Name,
                    ControllerName = ControllerFromField(f.Type)
                })
                .ToList();
        }

        private static string ControllerFromField(DbDocumentType type)
        {
            switch (type)
            {
                default:
                    return type.ToString();
            }
        }
    }
}