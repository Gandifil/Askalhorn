namespace Askalhorn.Utils
{
    public interface ILimitedValue<out T>
    {
        T Current { get; }
        
        T Max { get; }
        
        float Percent { get; }
    }
}