namespace CosmosApi.Interfaces
{
    public interface IBaseDto
    {
        string id { get; set; }

        bool Deleted { get; set; }
    }
}
