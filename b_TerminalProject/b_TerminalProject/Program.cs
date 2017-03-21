using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace b_TerminalProject
{
    [Serializable]
    class SerializableClass
    {
        string _name;
        double _value;

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public double Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }

        public override string ToString()
        {
            return string.Format("[SerilizableClass: Name={0}, Value={1}]", Name, Value);
        }

        public SerializableClass(string Value1, double Value2)
        {
            Name = Value1;
            Value = Value2;
        }

    }


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

        public void Minus()
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
                Minus();
            }

            else
            {
                Console.Write("Num2 = ");
                num = Console.ReadLine();
                isNum = double.TryParse(num, out num2);
                if (isNum != true)
                {
                    Console.WriteLine("This isn't number");
                    Minus();
                }

                else
                {
                    Console.Write("result is ");
                    Console.Write(num1 - num2);
                    Console.Write(" \n");
                }
            }
        }

        public void Convent()
        {
            Console.Write("Enter the string : ");
            byte[] ascii = Encoding.ASCII.GetBytes(Console.ReadLine());

            foreach (byte b in ascii)
            {
                Console.Write(string.Format("0x{0:x8}", b));
                Console.Write(" ");
                Console.Write(string.Format("0x{0:X}", b));
                Console.Write(" ");
                Console.WriteLine(((Char)b).ToString());
            }

            foreach (byte b in ascii)
            {
                Console.Write(string.Format("0x{0:X}", b));
            }

            Console.WriteLine("");
            Console.WriteLine(Encoding.ASCII.GetString(ascii));

        }
   
        public void Serialization()
        {
            Console.WriteLine("Serialization Section Start");

            Console.Write("Enter the string : ");
            string value1 = Console.ReadLine();

            Console.Write("Enter the Number : ");
            double value2 = double.Parse(Console.ReadLine());

            SerializableClass sc = new SerializableClass(value1, value2);
            Console.Write("Original type : " + sc.ToString());


            Stream stream = File.Open("data.block", FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, sc);

            stream.Close();

            Console.Write("\nEnter new Name : ");
            sc.Name = Console.ReadLine();

            Console.Write("Convented type : " + sc.ToString());
            sc = null;

            stream = File.Open("data.block", FileMode.Open);

            SerializableClass d_sc = (SerializableClass)formatter.Deserialize(stream);
            stream.Close();

            Console.WriteLine("\nRestored type : " + d_sc.ToString());

        }
    
    
    }

  

    class MainClass
    {

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
                StandBy sb = new StandBy();
                Type type = sb.GetType();
                MethodInfo callFunc = type.GetMethod(value, BindingFlags.Instance | BindingFlags.Public);
                callFunc.Invoke(sb, null);
            }
            catch
            {
                Console.WriteLine("*There is no function with the same name as the name you entered");
            }

        }

    }
}
