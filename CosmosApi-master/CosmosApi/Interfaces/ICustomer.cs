namespace CosmosApi.Interfaces
{
    public interface ICustomer : IBaseDto
    {       
        string Name { get; set; }

        string Phone { get; set; }

        string AddressId { get; set; }
    }
}
