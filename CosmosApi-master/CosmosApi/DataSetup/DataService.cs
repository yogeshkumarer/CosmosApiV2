using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CosmosApi.Model;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

namespace CosmosApi.DataSetup
{
    public class DataService : IDataService
    {
        public async Task InitializeData(Database database)
        {
            var containerResponse = database.CreateContainerIfNotExistsAsync(Constants.CustomerContainer, "/Name").GetAwaiter().GetResult();

            var tasks = new List<Task>();

            if (containerResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                tasks.Add(CreateData<CustomerModel>(containerResponse.Container, "TestData.json"));
            }

            var addressContainerResponse = database.CreateContainerIfNotExistsAsync(Constants.AddressContainer, "/ZipCode").GetAwaiter().GetResult();

            if (addressContainerResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                tasks.Add(CreateData<AddressModel>(addressContainerResponse.Container, "AddressData.json"));
            }

            await Task.WhenAll(tasks);
        }

        private async Task CreateData<T>(Container container, string dataFileName)
        {
            string fileName;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + dataFileName))
            {
                fileName = AppDomain.CurrentDomain.BaseDirectory + dataFileName;
            }
            else
            {
                fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin") + dataFileName;
            }

            var strData = File.ReadAllText(fileName);
            var data = JsonConvert.DeserializeObject<List<T>>(strData);

            var tasks = data.Select(x => container.CreateItemAsync(x));
            await Task.WhenAll(tasks);
        }
    }
}
