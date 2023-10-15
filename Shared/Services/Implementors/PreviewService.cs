using Domain.Models;
using Domain.Repositories.Interfaces;
using Microsoft.Azure.Cosmos;
using Shared.DTOs;
using Shared.Services.Interfaces;

namespace Shared.Services.Implementors
{
    public class PreviewService : IPreviewService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;
        private readonly IRepository<Preview> _repository;

        public PreviewService(CosmosClient cosmosClient, string databaseName, string containerName, IRepository<Preview> repository)
        {
            _cosmosClient = cosmosClient;
            _container = _cosmosClient.GetContainer(databaseName, containerName);
            _repository = repository;
        }

        public async Task<bool> AddAsync(PreviewDto entity)
        {
            var preview = new Preview
            {
                Title = entity.Title,
                Summary = entity.Summary,
                Description = entity.Description,
                Skills = entity.Skills,
                Benefits = entity.Benefits,
                Criteria = entity.Criteria,
                Image = entity.Image,
                ProgramInformation = new ProgramInformation
                {
                    Type = entity.ProgramInformation.Type,
                    Duration = entity.ProgramInformation.Duration,
                    StartDate = entity.ProgramInformation.StartDate,
                    ApplicationOpen = entity.ProgramInformation.ApplicationOpen,
                    ApplicationClose = entity.ProgramInformation.ApplicationClose,
                    Location = entity.ProgramInformation.Location,
                    NumberOfApplication = entity.ProgramInformation.NumberOfApplication,
                    Qualification = entity.ProgramInformation.Qualification
                }
            };

            var response = await _container.CreateItemAsync(preview);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var response = await _container.DeleteItemAsync<Preview>(id, new PartitionKey(id));

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Preview>> GetAllAsync()
        {
            var query = new QueryDefinition("SELECT * FROM c");
            var previews = new List<Preview>();

            var iterator = _container.GetItemQueryIterator<Preview>(query);

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                previews.AddRange(response);
            }

            return previews;
        }

        public async Task<Preview> GetByIdAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Preview>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task UpdateAsync(string id, Preview entity)
        {
            await _container.ReplaceItemAsync(entity, id, new PartitionKey(id));
        }
    }
}
