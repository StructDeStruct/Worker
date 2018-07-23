using System;

namespace Project.AtlasLab
{
    public class OutputService : IOutputService, IService
    {
        public void Write(string value)
        {
            Console.WriteLine(value);
        }
    }
}