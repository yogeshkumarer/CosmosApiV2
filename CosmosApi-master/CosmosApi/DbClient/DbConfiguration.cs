namespace CosmosApi.DbClient
{
    public class DbConfiguration : IDbConfiguration
    {
        public string DbName { get; set; }

        public string DbConnection { get; set; }
    }
}
