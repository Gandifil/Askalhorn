namespace Askalhorn.Common
{
    public interface ICommand
    {
        void Run(object target, string[] args);
    }
}