using System.Dynamic;
using Askalhorn.Common.Localization;

namespace Askalhorn.Common.Plot.Quests
{
    public interface IQuestStep
    {
        TextPointer Description { get; }
    }
}