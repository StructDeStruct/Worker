using AtlasLab.Abstract;
using Newtonsoft.Json;

namespace AtlasLab.Data
{
    public class DeserializeService : IDeserializeService, IService
    {
        public Message Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<Message>(value);
        }
    }
}