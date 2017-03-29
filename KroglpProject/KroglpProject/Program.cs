using System;
using System.Collections.Generic;

namespace KroglpProject
{

    class Kernel
    {
        List<List<string>> Memory = new List<List<string>>();

        public List<string> Compare(string[] value)
        {
            List<string> target = new List<string>();

            foreach (string element in value)
                target.Add(element);

            Memory.Add(target);


        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
