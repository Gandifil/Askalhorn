using System;

namespace Askalhorn.Mechanics.CUITests
{
    class Program
    {
        static void Main(string[] args)
        {
            var character = CharacterBuilder.CreateTest("Test CUI Character");
            Console.WriteLine(character);
        }
    }
}
