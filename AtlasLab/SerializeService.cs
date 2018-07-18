using Newtonsoft.Json;

namespace Project.AtlasLab
{
    public class SerializeService : ISerializeService
    {
        public string Serialize(IMessage message)
        {
            return JsonConvert.SerializeObject(message);
        }
    }
}