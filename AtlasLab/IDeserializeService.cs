namespace Project.AtlasLab
{
    public interface IDeserializeService
    {
        IMessage Deserialize(string value);
    }
}