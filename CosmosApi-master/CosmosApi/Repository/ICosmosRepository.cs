using System.Collections.Generic;
using System.Threading.Tasks;
using CosmosApi.Interfaces;

namespace CosmosApi.Repository
{
    public interface ICosmosRepository
    {
        Task<bool> CreateItemAsync<T>(string containerId, T data);

        Task<bool> CreateOrUpdateItemAsync<T>(string containerId, T data);

        Task<bool> UpdateItemAsync<T>(string containerId, T data) where T : IBaseDto;

        List<T> GetItems<T>(string containerId);

        Task<List<T>> QueryItems<T>(string containerId, Dictionary<string, string> parameters = null);

        Task<bool> DeleteItemAsync<T>(string containerId, string id) where T : IBaseDto;
    }
}
