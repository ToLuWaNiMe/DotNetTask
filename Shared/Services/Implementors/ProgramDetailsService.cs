using Domain.Models;
using Domain.Repositories.Interfaces;
using Microsoft.Azure.Cosmos;
using Shared.DTOs;
using Shared.Services.Interfaces;

namespace Shared.Services.Implementors
{
    public class ProgramDetailsService : IProgramDetailsService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;
        private readonly IRepository<ProgramDetails> _repository;

        public ProgramDetailsService(CosmosClient cosmosClient, string databaseName, string containerName, IRepository<ProgramDetails> repository)
        {
            _cosmosClient = cosmosClient;
            _container = _cosmosClient.GetContainer(databaseName, containerName);
            _repository = repository;
        }

        public async Task<bool> AddAsync(ProgramDto entity)
        {
            var programDetails = new ProgramDetails
            {
                Title = entity.Title,
                Summary = entity.Summary,
                Description = entity.Description,
                Skills = entity.Skills,
                Benefits = entity.Benefits,
                Criteria = entity.Criteria,
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

            var response = await _container.CreateItemAsync(programDetails);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var response = await _container.DeleteItemAsync<ProgramDetails>(id, new PartitionKey(id));

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<ProgramDetails>> GetAllAsync()
        {
            var query = new QueryDefinition("SELECT * FROM c");
            var programDetails = new List<ProgramDetails>();

            var iterator = _container.GetItemQueryIterator<ProgramDetails>(query);

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                programDetails.AddRange(response);
            }

            return programDetails;
        }

        public async Task<ProgramDetails> GetByIdAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<ProgramDetails>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(string id, ProgramDto updateDto)
        {
            try
            {
                var existingProgram = await GetByIdAsync(id);

                if (existingProgram == null)
                {
                    return false;
                }


                existingProgram.Title = updateDto.Title;
                existingProgram.Summary = updateDto.Summary;
                existingProgram.Description = updateDto.Description;
                existingProgram.Skills = updateDto.Skills;
                existingProgram.Benefits = updateDto.Benefits;
                existingProgram.Criteria = updateDto.Criteria;
                existingProgram.ProgramInformation.Type = updateDto.ProgramInformation.Type;
                existingProgram.ProgramInformation.Duration = updateDto.ProgramInformation.Duration;
                existingProgram.ProgramInformation.StartDate = updateDto.ProgramInformation.StartDate;
                existingProgram.ProgramInformation.ApplicationOpen = updateDto.ProgramInformation.ApplicationOpen;
                existingProgram.ProgramInformation.ApplicationClose = updateDto.ProgramInformation.ApplicationClose;
                existingProgram.ProgramInformation.Location = updateDto.ProgramInformation.Location;
                existingProgram.ProgramInformation.NumberOfApplication = updateDto.ProgramInformation.NumberOfApplication;
                existingProgram.ProgramInformation.Qualification = updateDto.ProgramInformation.Qualification;


                var response = await _container.ReplaceItemAsync(existingProgram, existingProgram.Id);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (CosmosException ex)
            {

                Console.WriteLine($"Cosmos DB Exception: {ex}");
                return false;
            }
        }
    }
}
