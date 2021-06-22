using System;

namespace NiklasGame
{
    public static class RandomHelper
    {
        private static Random _random = new ();
        
        public static int GetNext(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}