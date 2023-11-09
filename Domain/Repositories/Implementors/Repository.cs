using Domain.Repositories.Interfaces;
using Microsoft.Azure.Cosmos;
using System.Linq.Expressions;


namespace Domain.Repositories.Implementors
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly Container _container;


        public Repository(CosmosClient cosmosClient, CosmosClient databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var query = _container.GetItemQueryIterator<TEntity>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<TEntity>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response);
            }
            return results;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            var query = _container.GetItemQueryIterator<TEntity>(
                new QueryDefinition($"SELECT * FROM c WHERE {GetFilterString(filter)}"));



            var results = new List<TEntity>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response);
            }
            return results;
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            try
            {
                return await _container.ReadItemAsync<TEntity>(id, new PartitionKey(id));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }



        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));



            await _container.CreateItemAsync(entity);
            return entity;
        }



        public async Task UpdateAsync(string id, TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));



            await _container.ReplaceItemAsync(entity, id, new PartitionKey(id));
        }



        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<TEntity>(id, new PartitionKey(id));
        }



        private string GetFilterString(Expression<Func<TEntity, bool>> filter)
        {
            // Translate LINQ expression to Cosmos DB SQL query
            // This is a simplified example; you may need to enhance it based on your needs
            // Here, we assume filter is a simple Equality expression



            if (filter.Body is BinaryExpression binaryExpression)
            {
                var left = binaryExpression.Left as MemberExpression;
                var right = binaryExpression.Right as ConstantExpression;



                if (left != null && right != null)
                {
                    return $"{left.Member.Name} = {right.Value}";
                }
            }



            throw new ArgumentException("Unsupported filter expression.");
        }
    }
}
