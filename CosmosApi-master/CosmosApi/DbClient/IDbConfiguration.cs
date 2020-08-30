namespace CosmosApi.DbClient
{
    public interface IDbConfiguration
    {
        string DbName { get; set; }

        string DbConnection { get; set; }
    }
}
