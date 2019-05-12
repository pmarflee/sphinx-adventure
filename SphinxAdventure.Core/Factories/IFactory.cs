namespace SphinxAdventure.Core.Factories
{
    public interface IFactory<TObject, TProps>
    {
        TObject Create(TProps props);
    }
}
