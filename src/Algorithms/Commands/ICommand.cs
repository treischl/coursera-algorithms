namespace Algorithms.Commands
{
    public interface ICommand<T>
    {
        void Execute(T options);
    }
}
