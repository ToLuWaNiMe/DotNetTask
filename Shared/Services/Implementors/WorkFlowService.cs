using Domain.Models;
using Domain.Repositories.Interfaces;
using Microsoft.Azure.Cosmos;
using Shared.DTOs;
using Shared.Services.Interfaces;

namespace Shared.Services.Implementors
{
    public class WorkFlowService : IWorkFlowService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;
        private readonly IRepository<WorkFlow> _repository;

        public WorkFlowService(CosmosClient cosmosClient, string databaseName, string containerName, IRepository<WorkFlow> repository)
        {

            _cosmosClient = cosmosClient;
            _container = _cosmosClient.GetContainer(databaseName, containerName);
            _repository = repository;
        }

        public async Task<bool> AddAsync(WorkFlowDto entity)
        {
            try
            {
                var workFlow = new WorkFlow
                {
                    Name = entity.Name,
                    Stages = new List<WorkFlowStage>()
                };

                foreach (var stageDto in entity.Stages)
                {
                    var stage = new WorkFlowStage
                    {
                        Questions = stageDto.Questions,
                        AdditionalInfo = stageDto.AdditionalInfo,
                        DeadLine = stageDto.DeadLine,
                        Duration = stageDto.Duration,

                    };

                    workFlow.Stages.Add(stage);
                }

                var response = await _container.CreateItemAsync(workFlow);

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var response = await _container.DeleteItemAsync<WorkFlow>(id, new PartitionKey(id));

                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<WorkFlow>> GetAllAsync()
        {
            var query = new QueryDefinition("SELECT * FROM c");
            var workFlows = new List<WorkFlow>();

            var iterator = _container.GetItemQueryIterator<WorkFlow>(query);

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                workFlows.AddRange(response);
            }

            return workFlows;
        }

        public async Task<WorkFlow> GetByIdAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<WorkFlow>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(string id, WorkFlowDto updateDto)
        {
            try
            {
                var existingWorkFlow = await GetByIdAsync(id);
                if (existingWorkFlow == null)
                {
                    return false;
                }


                existingWorkFlow.Name = updateDto.Name;


                existingWorkFlow.Stages.Clear();
                foreach (var stageDto in updateDto.Stages)
                {
                    var stage = new WorkFlowStage
                    {
                        Questions = stageDto.Questions,
                        AdditionalInfo = stageDto.AdditionalInfo,
                        DeadLine = stageDto.DeadLine,
                        Duration = stageDto.Duration,
                    };
                    existingWorkFlow.Stages.Add(stage);
                }

                await _container.ReplaceItemAsync(existingWorkFlow, id, new PartitionKey(id));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
