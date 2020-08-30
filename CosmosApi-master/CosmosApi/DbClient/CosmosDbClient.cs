using CosmosApi.DataSetup;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace CosmosApi.DbClient
{
    public class CosmosDbClient : ICosmosDbClient
    {
        private readonly IDbConfiguration _dbConfiguration;
        private readonly IDataService _dataService;

        public CosmosDbClient(IOptions<DbConfiguration> dbConfigurationOptions, IDataService dataService)
        {
            _dbConfiguration = dbConfigurationOptions.Value;
            _dataService = dataService;

            CreateClient();            
        }

        public CosmosClient CosmosClient { get; private set; }

        public Database CosmosDatabase { get; private set; }

        private void CreateClient()
        {
            this.CosmosClient = new CosmosClient(
            _dbConfiguration.DbConnection,
            new CosmosClientOptions()
            {
                ApplicationRegion = Regions.EastUS2,
            });

            var dbResponse = this.CosmosClient.CreateDatabaseIfNotExistsAsync(_dbConfiguration.DbName).GetAwaiter().GetResult();

            if(dbResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                _dataService.InitializeData(dbResponse.Database);
            }

            this.CosmosDatabase = dbResponse.Database;
        }
    }
}
