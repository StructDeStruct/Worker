using AtlasLab.Abstract;
using Newtonsoft.Json;

namespace AtlasLab.Data
{
    public class SerializeService : ISerializeService, IService
    {
        public string Serialize(Message message)
        {
            return JsonConvert.SerializeObject(message);
        }
    }
}