// -----------------------------------------------------------------------
// <copyright file="JsonSpell.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace Xml2Json
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using PathfinderDb.Schema;

    public class JsonSpell
    {
        public string Name { get; set; }

        public string School { get; set; }

        public string[] Descriptors { get; set; }

        public Dictionary<string, int> Levels { get; set; }

        public JsonSpellComponents Components { get; set; }

        public JsonSpellRange Range { get; set; }

        public string Target { get; set; }

        public JsonSpellCastingTime CastingTime { get; set; }

        public JsonSpellResistance SpellResistance { get; set; }

        public static JsonSpell FromXml(Spell spell)
        {
            return new JsonSpell
            {
                Name = spell.Name,
                School = spell.School.ToString().ToCamelCase(),
                Levels = spell.Levels.ToDictionary(l => l.List, l => l.Level),
                Descriptors = spell.Descriptor.ToFlagsArray(),
                Components = JsonSpellComponents.FromXml(spell.Components),
                Range = JsonSpellRange.FromXml(spell.Range),
                Target = spell.Target != null ? spell.Target.Value : null,
                CastingTime = JsonSpellCastingTime.FromXml(spell.CastingTime),
                SpellResistance = JsonSpellResistance.FromXml(spell.SpellResistance),
            };
        }
    }

    public class JsonSpellComponents
    {
        public string[] Kinds { get; set; }

        public string Description { get; set; }

        public static JsonSpellComponents FromXml(SpellComponents components)
        {
            if (components == null)
                return null;

            return new JsonSpellComponents
            {
                Kinds = components.Kinds.ToFlagsArray(),
                Description = components.Description.EmptyAsNull()
            };
        }
    }

    public class JsonSpellRange
    {
        public string Unit { get; set; }

        public string Value { get; set; }

        public static JsonSpellRange FromXml(SpellRange range)
        {
            if (range == null)
            {
                return null;
            }

            return new JsonSpellRange
            {
                Unit = range.Unit.ToString().ToCamelCase(),
                Value = range.SpecificValue,
            };
        }
    }

    public class JsonSpellCastingTime
    {
        [DefaultValue("round")]
        public string Unit { get; set; }

        [DefaultValue(0)]
        public int Value { get; set; }

        public string Text { get; set; }

        public static JsonSpellCastingTime FromXml(SpellCastingTime castingTime)
        {
            if (castingTime == null)
            {
                return null;
            }

            return new JsonSpellCastingTime
            {
                Text = castingTime.Text,
                Unit = castingTime.Unit.ToString().ToCamelCase(),
                Value = castingTime.Value,
            };
        }
    }

    public class JsonSpellResistance
    {
        public bool? Resist { get; set; }

        [DefaultValue(false)]
        public bool Objects { get; set; }

        [DefaultValue(false)]
        public bool Harmless { get; set; }

        public string Text { get; set; }

        public static JsonSpellResistance FromXml(SpellResistance resistance)
        {
            if (resistance == null)
                return null;

            return new JsonSpellResistance
            {
                Resist = resistance.Resist == SpecialBoolean.Special ? (bool?)null : resistance.Resist == SpecialBoolean.Yes,
                Objects = resistance.Objects,
                Harmless = resistance.Harmless,
                Text = resistance.Text
            };
        }
    }
}