namespace Mario.Services
{
    public interface IObserver<TState>
    {
        void Update(TState state);
    }
}