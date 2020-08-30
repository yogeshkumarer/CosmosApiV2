using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace CosmosApi.DataSetup
{
    public interface IDataService
    {
        Task InitializeData(Database database);
    }
}
