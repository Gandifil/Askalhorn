namespace Askalhorn.Text
{
    public class MockTextPointer: TextPointer
    {
        private readonly string _value;
        
        public MockTextPointer(string value) : base("name", "index")
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}