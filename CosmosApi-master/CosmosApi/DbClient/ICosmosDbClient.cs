using Microsoft.Azure.Cosmos;

namespace CosmosApi.DbClient
{
    public interface ICosmosDbClient
    {
        CosmosClient CosmosClient { get; }

        Database CosmosDatabase
        {
            get;
        }
    }
}