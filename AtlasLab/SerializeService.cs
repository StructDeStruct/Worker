using Newtonsoft.Json;

namespace Project.AtlasLab
{
    public class SerializeService
    {
        public string Serialize(Message message)
        {
            return JsonConvert.SerializeObject(message);
        }
    }
}