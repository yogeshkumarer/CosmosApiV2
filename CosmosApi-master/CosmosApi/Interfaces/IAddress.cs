namespace CosmosApi.Interfaces
{
    public interface IAddress : IBaseDto
    {
        string HouseNumber { get; set; }

        string AddressLine1 { get; set; }

        string AddressLine2 { get; set; }

        string City { get; set; }

        string ZipCode { get; set; }
    }
}
