using System;
using System.Reflection;

namespace b_TerminalProject
{
    class StandBy
    {
        public void Add()
        {
            string num;
            double num1;
            double num2;
            bool isNum;

            Console.Write("Num1 = ");
            num = Console.ReadLine();
            isNum = double.TryParse(num, out num1);
            if (isNum != true)
            {
                Console.WriteLine("This isn't number");
                Add();
            }

            else
            {
                Console.Write("Num2 = ");
                num = Console.ReadLine();
                isNum = double.TryParse(num, out num2);
                if (isNum != true)
                {
                    Console.WriteLine("This isn't number");
                    Add();
                }

                else
                {
                    Console.Write("result is ");
                    Console.Write(num1 + num2);
                    Console.Write(" \n");
                }
            }
        }
    }

    class MainClass
    {
        // static List<string> FunctionBase = new List<string>();
        static StandBy sb = new StandBy();

        public static void Main(string[] args)
        {
            Stand();
        }

        static void Stand()
        {
            Console.Write("block.Terminal$ ");
            string key = Console.ReadLine();
            if (key != "quit")
            {
                Search(key);
                Stand();
            }

            else
            {

            }
        }

        static void Search(string value)
        {
            try
            {
                Type type = sb.GetType();
                MethodInfo callFunc = type.GetMethod(value, BindingFlags.Instance | BindingFlags.Public);
                callFunc.Invoke(sb, null);
            }
            catch
            {
                Console.WriteLine("There is no function with the same name as the name you entered");
            }

        }

    }
}
