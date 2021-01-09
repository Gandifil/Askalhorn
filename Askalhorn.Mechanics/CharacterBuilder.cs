using System;
using System.Collections.Generic;
using System.Text;

namespace Askalhorn.Mechanics
{
    public static class CharacterBuilder
    {
        public static ICharacter CreateTest(string name)
        {
            return new Character(name);
        }
    }
}
