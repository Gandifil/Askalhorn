using AmbrosiaGame.GameWorld;
using TestGame.Resources;

namespace TestGame.GameWorld
{
    sealed class Player:  Creature
    {
        public Player(int x, int y, PlayerInfo info)
            : base(x, y, info.Texture)
        {
            //Body = new PlayerBody();
        }
    }
}
