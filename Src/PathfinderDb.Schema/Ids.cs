// -----------------------------------------------------------------------
// <copyright file="Ids.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    public static class Ids
    {
        private const string Accents = @"éèëêàäâìïîöô";

        private const string Remplac = @"eeeeaaaiiioo";

        public static string Normalize(string id)
        {
            id = id ?? string.Empty;

            var result = new char[id.Length];
            var size = 0;
            var wordBoundary = false; // false = camelCased ; true = PascalCased

            for (var i = 0; i < id.Length; i++)
            {
                var c = id[i];
                int ci;
                if ((ci = Accents.IndexOf(c)) != -1)
                {
                    // suppression accents
                    c = Remplac[ci];
                }
                
                if (char.IsLetterOrDigit(c))
                {
                    result[size] = wordBoundary ? char.ToUpperInvariant(c) : char.ToLowerInvariant(c);
                    wordBoundary = false;
                    size++;
                }
                else
                {
                    wordBoundary = true;
                }
            }

            return new string(result, 0, size);
        }
    }
}