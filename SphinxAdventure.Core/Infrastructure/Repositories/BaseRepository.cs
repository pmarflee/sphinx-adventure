using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
    {
        protected readonly IDocumentClient DocumentClient;
        private readonly CosmosDbConfiguration _config;
        protected readonly string CollectionId;
        protected readonly Uri DocumentCollectionUri;

        protected BaseRepository(
            IDocumentClient documentClient, CosmosDbConfiguration config, string collectionId)
        {
            DocumentClient = documentClient;
            _config = config;
            CollectionId = collectionId;
            DocumentCollectionUri = UriFactory.CreateDocumentCollectionUri(
                _config.Database, CollectionId);

            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return (await DocumentClient.ReadDocumentAsync(CreateDocumentUri(id)))
                .Resource.ToEntity<TEntity>();
        }
                
        public async Task<TEntity> SaveAsync(TEntity entity)
        {
            return (await DocumentClient.UpsertDocumentAsync(DocumentCollectionUri, entity))
                .Resource.ToEntity<TEntity>();
        }

        public IOrderedQueryable<TEntity> CreateQuery()
        {
            return DocumentClient.CreateDocumentQuery<TEntity>(DocumentCollectionUri);
        }

        private Uri CreateDocumentUri(Guid id)
        {
            return UriFactory.CreateDocumentUri(
                _config.Database, CollectionId, id.ToString());
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await DocumentClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(
                    _config.Database));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await DocumentClient.CreateDatabaseAsync(new Database
                    { Id = _config.Database });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await DocumentClient.ReadDocumentCollectionAsync(DocumentCollectionUri);
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await DocumentClient.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(_config.Database),
                        new DocumentCollection { Id = CollectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
