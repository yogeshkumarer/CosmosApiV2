using System.Collections.Generic;
using System.Linq;

namespace CosmosApi.DbClient
{
    public class QueryBuilder : IQueryBuilder
    {
        public string BuildSelectQuery(string containerId, Dictionary<string, string> parameters = null)
        {
            var queryBuilder = new System.Text.StringBuilder($"select * from {containerId} c");
            if (parameters != null && parameters.Any())
            {
                queryBuilder.Append(" where ");

                foreach (var parameter in parameters)
                {
                    queryBuilder.Append($"c.{parameter.Key} = \"{parameter.Value}\"");
                }
            }

            return queryBuilder.ToString();
        }
    }
}
