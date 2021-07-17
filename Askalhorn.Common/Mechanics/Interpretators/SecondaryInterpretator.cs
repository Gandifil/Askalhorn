namespace Askalhorn.Common.Mechanics.Interpretators
{
    internal class SecondaryInterpretator: IInterpretator
    {
        public SecondaryTypes Type { get; set; }

        public string Description => ToString();

        public override string ToString()
        {
            return Type.ToString();
        }

        public float Calculate(Character character)
        {
            return character.Secondary[Type].Value;
        }
    }
}