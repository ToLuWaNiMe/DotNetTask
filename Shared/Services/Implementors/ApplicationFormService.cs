using Domain.Models;
using Domain.Repositories.Interfaces;
using Microsoft.Azure.Cosmos;
using Shared.DTOs;
using Shared.Services.Interfaces;

namespace Shared.Services.Implementors
{
    public class ApplicationFormService : IApplicationFormService
    {

        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;
        private readonly IRepository<ApplicationForm> _repository;

        public ApplicationFormService(CosmosClient cosmosClient, string databaseName, string containerName, IRepository<ApplicationForm> repository)
        {
            _cosmosClient = cosmosClient;
            _container = _cosmosClient.GetContainer(databaseName, containerName);
            _repository = repository;
        }

        public async Task<bool> AddAsync(ApplicationFormDto entity)
        {
            var appForm = new ApplicationForm
            {
                Image = entity.Image,
                PersonalInformation = new PersonalInformation
                {
                    FirstName = entity.PersonalInformation.FirstName,
                    LastName = entity.PersonalInformation.LastName,
                    Email = entity.PersonalInformation.Email,
                    DateOfBirth = entity.PersonalInformation.DateOfBirth,
                    Gender = entity.PersonalInformation.Gender,
                    Phone = entity.PersonalInformation.Phone,
                    CurrentResidence = entity.PersonalInformation.CurrentResidence,
                    Nationality = entity.PersonalInformation.Nationality,
                },
                Profile = new Profile
                {
                    Education = entity.Profile.Education,
                    Experience = entity.Profile.Experience,
                    Resume = entity.Profile.Resume,
                },
                AdditionalQuestions = new AdditionalQuestions
                {
                    SelfDescription = entity.AdditionalQuestions.SelfDescription,
                    YearOfGraduation = entity.AdditionalQuestions.YearOfGraduation,
                    Question = entity.AdditionalQuestions.Question,
                    Choice = entity.AdditionalQuestions.Choice,
                    Rejected = entity.AdditionalQuestions.Rejected
                }
            };

            var response = await _container.CreateItemAsync(appForm);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                await _container.DeleteItemAsync<ApplicationForm>(id, new PartitionKey(id));
                return true;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ApplicationForm>> GetAllAsync()
        {
            var query = new QueryDefinition("SELECT * FROM c");
            var results = new List<ApplicationForm>();

            var iterator = _container.GetItemQueryIterator<ApplicationForm>(query);

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response.Resource);
            }

            return results;
        }

        public async Task<ApplicationForm> GetByIdAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<ApplicationForm>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(string id, ApplicationFormDto updateDto)
        {
            try
            {
                var existingForm = await GetByIdAsync(id);

                if (existingForm == null)
                {
                    return false;
                }


                existingForm.Image = updateDto.Image;
                existingForm.PersonalInformation = updateDto.PersonalInformation;
                existingForm.Profile = updateDto.Profile;
                existingForm.AdditionalQuestions = updateDto.AdditionalQuestions;


                var response = await _container.ReplaceItemAsync(existingForm, existingForm.Id);

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
