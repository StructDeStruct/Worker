using System;
using AtlasLab.Abstract;

namespace AtlasLab.Data
{
    public class OutputService : IOutputService, IService
    {
        public void Write(string value)
        {
            Console.WriteLine(value);
        }
    }
}