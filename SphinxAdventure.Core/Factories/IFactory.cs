namespace SphinxAdventure.Core.Factories
{
    public interface IFactory<T>
    {
        T Create();
    }
}
