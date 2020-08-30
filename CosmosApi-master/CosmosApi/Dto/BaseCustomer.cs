using CosmosApi.Interfaces;

namespace CosmosApi.Dto
{
    public abstract class BaseCustomer : ICustomer
    {
        public BaseCustomer()
        {
        }

        public BaseCustomer(ICustomer data)
        {
            this.id = data.id;
            this.Name = data.Name;
            this.Phone = data.Phone;
            this.AddressId = data.AddressId;
            this.Deleted = data.Deleted;
        }

        public string id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string AddressId { get; set; }

        public bool Deleted { get; set; }
    }
}
