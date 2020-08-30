using System;
using System.Collections.Generic;
using CosmosApi.Interfaces;

namespace CosmosApi.Services
{
    public class ContainerNameService : IContainerNameService
    {
        private Dictionary<Type, string> _containerNames;

        public ContainerNameService()
        {
            _containerNames = new Dictionary<Type, string>
            {
                {typeof(ICustomer), Constants.CustomerContainer },
                {typeof(IAddress), Constants.AddressContainer }
            };
        }

        public string GetContainerName(Type type)
        {
            if (_containerNames.ContainsKey(type))
            {
                return _containerNames[type];
            }

            return null;
        }
    }
}
