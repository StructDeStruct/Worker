using Newtonsoft.Json;

namespace Project.AtlasLab
{
    public class DeserializeService
    {
        public Message Deserialize(string message)
        {
            return JsonConvert.DeserializeObject<Message>(message);
        }
    }
}