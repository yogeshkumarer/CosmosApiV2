using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosApi.Dto;
using CosmosApi.Interfaces;
using CosmosApi.Model;
using CosmosApi.Repository;
using CosmosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CosmosApi.Controllers
{    
    [ApiVersion("1.0")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICosmosRepository _cosmosRepository;
        private readonly IContainerNameService _containerNameService;

        public CustomerController(ICosmosRepository cosmosRepository, IContainerNameService containerNameService)
        {
            _cosmosRepository = cosmosRepository;
            _containerNameService = containerNameService;
        }

        [HttpGet]
        [Route("api/v{version:apiversion}/[controller]")]
        public ActionResult<CustomerModel> Get()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("api/v{version:apiversion}/[controller]/[action]")]
        public async Task<ActionResult<CustomerModel>> GetById(string customerId)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "id", customerId }
            };

            var data = await this._cosmosRepository.QueryItems<CustomerDto>(_containerNameService.GetContainerName(typeof(ICustomer)), queryParams);
            if (data != null && data.Any())
            {
                return Ok(data.Select(d => new CustomerModel(d)).First());
            }

            return BadRequest("Result for the given id does not exist.");
        }

        [HttpGet]
        [Route("api/v{version:apiversion}/[controller]/[action]")]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> GetByName(string customerName)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "Name", customerName }
            };

            var data = await this._cosmosRepository.QueryItems<CustomerDto>(_containerNameService.GetContainerName(typeof(ICustomer)), queryParams);
            return Ok(data.Select(d => new CustomerModel(d)));
        }

        [HttpPost]
        [Route("api/v{version:apiversion}/[controller]")]
        public async Task<ActionResult<bool>> Post(CustomerModel customerModel)
        {
            var result = await this._cosmosRepository.CreateOrUpdateItemAsync(_containerNameService.GetContainerName(typeof(ICustomer)), new CustomerDto(customerModel));

            return Ok(result);
        }

        [HttpDelete]
        [Route("api/v{version:apiversion}/[controller]")]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            var result = await this._cosmosRepository.DeleteItemAsync<CustomerDto>(_containerNameService.GetContainerName(typeof(ICustomer)), id);

            return Ok(result);
        }
    }
}
