using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Splitwise.Options
{
    public static class JsonOptions
    {
        public static readonly JsonSerializerSettings JsonSerializerSettings = new ()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };
    }
}
