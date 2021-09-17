using Askalhorn.Characters.Actions;
using Askalhorn.Characters.Impacts;
using Askalhorn.Common;
using Askalhorn.Inventory;
using Askalhorn.Map;
using Askalhorn.Map.Actions;
using Askalhorn.Map.Builds;
using Askalhorn.Map.Local;

namespace Askalhorn.Characters.Builds
{
    internal class Container: GameObject, IBuild, IActionable
    {

        public IBuild.Types Type => IBuild.Types.Chest;

        public readonly Bag Bag;

        public readonly bool IsOneTime;

        public readonly string Name;

        public Container(string name, bool isOneTime, Bag bag)
        {
            Name = name;

            IsOneTime = isOneTime;

            Bag = bag;
        }

        public IAction Action => new OpenAction(Bag);
    }
}