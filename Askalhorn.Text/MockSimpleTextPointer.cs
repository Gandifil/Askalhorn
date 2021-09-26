namespace Askalhorn.Text
{
    public class MockSimpleTextPointer: TextPointer
    {
        private readonly string _value;
        
        public MockSimpleTextPointer(string value) : base("name", "index")
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}