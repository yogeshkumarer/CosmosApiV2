using CosmosApi.Dto;
using CosmosApi.Interfaces;

namespace CosmosApi.Model
{
    public class AddressModel : BaseAddress
    {
        public AddressModel() : base()
        {
        }

        public AddressModel(IAddress data) : base(data)
        {
        }
    }
}
