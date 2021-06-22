using System;

namespace NiklasGame
{
    public static class RandomHelper
    {
        private static Random random = new ();
        
        public static int GetNext(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}