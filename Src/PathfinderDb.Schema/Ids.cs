// -----------------------------------------------------------------------
// <copyright file="Ids.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    public static class Ids
    {
        private const string Accents = @"èéêëàáâãäåìíîïòóôöõùúûüç";

        private const string Remplac = @"eeeeaaaaaaiiiiooooouuuuc";

        private const string Doubles = @"æœ";

        private const string DoublesReplacement = "aeoe";

        public static string Normalize(string id)
        {
            id = id ?? string.Empty;

            // 1) compute final size
            var maxSize = id.Length;
            for (var i = 0; i < id.Length; i++)
            {
                var c = id[i];
                if (Doubles.IndexOf(c) != -1)
                {
                    maxSize++;
                }
            }

            var result = new char[maxSize];
            var size = 0;
            var wordBoundary = false; // false = camelCased ; true = PascalCased

            for (var i = 0; i < id.Length; i++)
            {
                var c = id[i];
                var lc = char.ToLowerInvariant(c);
                int ci;
                if ((ci = Accents.IndexOf(lc)) != -1)
                {
                    // suppression accents
                    c = Remplac[ci];
                    if (lc != c)
                    {
                        c = char.ToUpperInvariant(c);
                    }
                }

                if ((ci = Doubles.IndexOf(c)) != -1)
                {
                    // replacement doubleletters
                    var c1 = DoublesReplacement[(ci * 2)];
                    c = DoublesReplacement[(ci * 2) + 1];

                    // add first letter
                    result[size] = wordBoundary ? char.ToUpperInvariant(c1) : char.ToLowerInvariant(c1);
                    wordBoundary = false;
                    size++;

                    // the second letter will be added automatically
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