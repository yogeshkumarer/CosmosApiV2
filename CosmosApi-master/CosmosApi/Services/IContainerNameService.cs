using System;

namespace CosmosApi.Services
{
    public interface IContainerNameService
    {
        string GetContainerName(Type type);
    }
}