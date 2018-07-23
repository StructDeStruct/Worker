using System;

namespace Project.AtlasLab
{
    public class InputService : IInputService, IService
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}