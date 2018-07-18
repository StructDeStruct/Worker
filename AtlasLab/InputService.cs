using System;

namespace Project.AtlasLab
{
    public class InputService : IInputService
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}