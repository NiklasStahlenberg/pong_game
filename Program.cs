using System;
using Microsoft.Xna.Framework;

namespace NiklasGame
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            using var game = new Game1();
            game.Run();
        }
    }
}