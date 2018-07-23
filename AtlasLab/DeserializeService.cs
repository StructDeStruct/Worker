using Newtonsoft.Json;

namespace Project.AtlasLab
{
    public class DeserializeService : IDeserializeService, IService
    {
        public IMessage Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<Message>(value);
        }
    }
}