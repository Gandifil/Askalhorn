namespace Askalhorn.Characters.Interpretators
{
    internal class SecondaryInterpretator: IInterpretator
    {
        public SecondaryTypes Type { get; set; }

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