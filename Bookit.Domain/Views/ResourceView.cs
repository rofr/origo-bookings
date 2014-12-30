using System;

namespace Bookit.Domain
{
    [Serializable]
    public class ResourceView
    {
        public readonly int Id;
        public readonly string Name;
        public readonly int PricePerDay;

        public ResourceView(Resource resource)
        {
            Id = resource.Id;
            Name = resource.Name;
            PricePerDay = resource.PricePerDay;
        }
    }
}