using CosmosApi.Interfaces;

namespace CosmosApi.Dto
{
    public class CustomerDto : BaseCustomer
    {
        public CustomerDto() : base()
        {
        }

        public CustomerDto(ICustomer data) : base(data)
        {
        }
    }
}
