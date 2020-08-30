using CosmosApi.Dto;
using CosmosApi.Interfaces;

namespace CosmosApi.Model
{
    public class CustomerModel : BaseCustomer
    {
        public CustomerModel() : base()
        {
        }

        public CustomerModel(ICustomer data) : base(data)
        {
        }
    }
}
