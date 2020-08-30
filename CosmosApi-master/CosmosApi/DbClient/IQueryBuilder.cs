using System.Collections.Generic;

namespace CosmosApi.DbClient
{
    public interface IQueryBuilder
    {
        string BuildSelectQuery(string containerId, Dictionary<string, string> parameters = null);
    }
}
