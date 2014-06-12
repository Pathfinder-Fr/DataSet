﻿namespace Pathfinder.DataSet
{
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlType("spellCastingTime")]
    public class SpellCastingTime
    {
        public SpellCastingTime()
        {
            this.Unit = TimeUnit.SimpleAction;
        }

        [XmlAttribute("unit")]
        [DefaultValue(typeof(TimeUnit), "simpleAction")]
        public TimeUnit Unit { get; set; }

        [XmlAttribute("value")]
        [DefaultValue(0)]
        public int Value { get; set; }

        [XmlText]
        public string Text { get; set; }

        public override string ToString()
        {
            switch (this.Unit)
            {
                case TimeUnit.SimpleAction:
                case TimeUnit.ImmediateAction:
                case TimeUnit.SwiftAction:
                case TimeUnit.FullRoundAction:
                    return this.Unit.ToString();

                case TimeUnit.Round:
                case TimeUnit.Minute:
                case TimeUnit.Hour:
                case TimeUnit.Day:
                    return string.Format("{0} {1}", this.Value, this.Unit);

                default:
                case TimeUnit.Special:
                    return this.Text;
            }
        }
    }
}