namespace Core
{
    public interface ICommandHandler<T>: IHandler<ICommand> where T:ICommand
    {
        
    }
}