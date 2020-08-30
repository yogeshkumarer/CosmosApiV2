using CosmosApi.Interfaces;

namespace CosmosApi.Dto
{
    public abstract class BaseAddress : IAddress
    {
        public BaseAddress()
        {
        }

        public BaseAddress(IAddress data)
        {
            this.id = data.id;
            this.HouseNumber = data.HouseNumber;
            this.AddressLine1 = data.AddressLine1;
            this.AddressLine2 = data.AddressLine2;
            this.City = data.City;
            this.ZipCode = data.ZipCode;
            this.Deleted = data.Deleted;
        }

        public string id { get; set; }

        public string HouseNumber { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public bool Deleted { get; set; }
    }
}
