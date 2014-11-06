// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace Xml2Json
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using PathfinderDb.Schema;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var inDir = args.Length != 0 ? args[0] : Environment.CurrentDirectory;
            var outDir = args.Length > 1 ? args[1] : Environment.CurrentDirectory;

            var serializer = new XmlSerializer(typeof(DataSet));

            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
            };

            using (var reader = new FileStream(Path.Combine(inDir, "spells.xml"), FileMode.Open, FileAccess.Read))
            {
                var spells = (DataSet)serializer.Deserialize(reader);

                foreach (var spell in spells.Spells)
                {
                    var spellDir = Path.Combine(outDir, "fr", spell.Source.Id ?? Source.Ids.PathfinderRpg, "spells");
                    if (!Directory.Exists(spellDir))
                    {
                        Directory.CreateDirectory(spellDir);
                    }
                    File.WriteAllText(Path.Combine(spellDir, JsonNormalize(spell.Name) + ".json"), JsonConvert.SerializeObject(JsonSpell.FromXml(spell), jsonSettings));
                }
            }
        }

        private static string JsonNormalize(string name)
        {
            var result = new char[name.Length];
            var resultLength = 0;

            var newWord = true;
            for (int i = 0; i < name.Length; i++)
            {
                var c = name[i];
                var n = i < name.Length - 1 ? name[i + 1] : ' ';

                if (char.IsLetterOrDigit(c))
                {
                    if (newWord && !char.IsLetterOrDigit(n))
                    {
                        // skip single letter words
                    }
                    else
                    {
                        if (newWord)
                        {
                            if (i != 0)
                            {
                                c = char.ToUpper(c);
                            }
                            else
                            {
                                c = char.ToLower(c);
                            }
                        }

                        newWord = false;
                        result[resultLength] = c;
                        resultLength++;
                    }
                }
                else
                {
                    newWord = true;
                }
            }

            return new string(result, 0, resultLength);
        }
    }
}