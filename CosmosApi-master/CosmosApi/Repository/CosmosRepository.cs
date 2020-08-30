using CosmosApi.DbClient;
using CosmosApi.Interfaces;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosApi.Repository
{
    public class CosmosRepository : ICosmosRepository
    {
        private readonly ICosmosDbClient _cosmosDbClient;
        private readonly IQueryBuilder _queryBuilder;

        public CosmosRepository(ICosmosDbClient cosmosDbClient, IQueryBuilder queryBuilder)
        {
            _cosmosDbClient = cosmosDbClient;
            _queryBuilder = queryBuilder;
        }

        public async Task<bool> CreateItemAsync<T>(string containerId, T data)
        {
            var container = _cosmosDbClient.CosmosDatabase.GetContainer(containerId);
            if(container != null)
            {
                var response = await container.CreateItemAsync<T>(data);

                return response.StatusCode == System.Net.HttpStatusCode.Created;
            }

            return false;
        }

        public async Task<bool> CreateOrUpdateItemAsync<T>(string containerId, T data)
        {
            var container = _cosmosDbClient.CosmosDatabase.GetContainer(containerId);
            if (container != null)
            {
                var response = await container.UpsertItemAsync<T>(data);

                return response.StatusCode == System.Net.HttpStatusCode.Created 
                    || response.StatusCode == System.Net.HttpStatusCode.OK
                    || response.StatusCode == System.Net.HttpStatusCode.Accepted;
            }

            return false;
        }

        public List<T> GetItems<T>(string containerId)
        {
            var container = _cosmosDbClient.CosmosDatabase.GetContainer(containerId);
            if (container != null)
            {
                var response = container.GetItemLinqQueryable<T>();

                return response.ToList();
            }

            return null;
        }

        public async Task<List<T>> QueryItems<T>(string containerId, Dictionary<string, string> parameters = null)
        {
            var container = _cosmosDbClient.CosmosDatabase.GetContainer(containerId);
            if (container != null)
            {
                var query = _queryBuilder.BuildSelectQuery(containerId, parameters);

                var queryResultSetIterator = container.GetItemQueryIterator<T>(query);

                var response = new List<T>();

                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<T> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (var data in currentResultSet)
                    {
                        response.Add(data);
                    }
                }

                return response;
            }

            return null;
        }

        public async Task<bool> UpdateItemAsync<T>(string containerId, T data) where T : IBaseDto
        {
            return await ReplaceItemAsync(containerId, data);
        }        

        public async Task<bool> DeleteItemAsync<T>(string containerId, string id) where T : IBaseDto
        {
            var data = (await this.QueryItems<T>(containerId, new Dictionary<string, string> { { "id", id } })).FirstOrDefault();
            if(data != null)
            {
                data.Deleted = true;
                return await ReplaceItemAsync(containerId, data);
            }

            return false;
        }

        private async Task<bool> ReplaceItemAsync<T>(string containerId, T data) where T : IBaseDto
        {
            var container = _cosmosDbClient.CosmosDatabase.GetContainer(containerId);
            if (container != null)
            {
                var response = await container.ReplaceItemAsync<T>(data, data.id);

                return response.StatusCode == System.Net.HttpStatusCode.Created
                    || response.StatusCode == System.Net.HttpStatusCode.OK
                    || response.StatusCode == System.Net.HttpStatusCode.Accepted;
            }

            return false;
        }
    }
}
