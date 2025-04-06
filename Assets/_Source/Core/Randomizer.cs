using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class Randomizer
    {
        private static readonly System.Random RND = new();
        public static float RandomFloat(int min, int max)
        {
            return RND.Next(min, max);
        }
        public static bool Determiner(float chance)
        {
            int roulette = RND.Next(0, 101);
            if (roulette <= chance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}