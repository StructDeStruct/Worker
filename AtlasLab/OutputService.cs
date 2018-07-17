using System;
using Microsoft.Extensions.Logging;

namespace Project.AtlasLab
{
    public class OutputService
    {
        public void Write(string value)
        {
            Console.WriteLine(value);
        }
    }
}