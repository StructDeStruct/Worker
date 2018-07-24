using System;
using AtlasLab.Abstract;

namespace AtlasLab.Data
{
    public class InputService : IInputService, IService
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}