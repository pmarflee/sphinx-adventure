using Microsoft.Azure.Documents;

namespace SphinxAdventure.Core.Infrastructure
{
    public static class DocumentExtensions
    {
        public static TEntity ToEntity<TEntity>(this Document document)
        {
            return (TEntity)(dynamic)document;
        }
    }
}
