using System;

namespace Project.AtlasLab
{
    public class OutputService : IOutputService
    {
        public void Write(string value)
        {
            Console.WriteLine(value);
        }
    }
}