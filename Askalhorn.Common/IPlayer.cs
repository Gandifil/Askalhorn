using Askalhorn.Common.Plot.Quests;

namespace Askalhorn.Common
{
    public interface IPlayer: ICharacter
    {
        IJournal Journal { get; }
    }
}