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

        public SerializableClass(string Value1, double Value2)
        {
            Name = Value1;
            Value = Value2;
        }

    }

    class Kernel
    {
        List<List<char>> memory = new List<List<char>>();

        List<List<char>> Event = new List<List<char>>();


        public void Input(string value)
        {
            List<char> inv = new List<char>(value.Trim().ToCharArray());
            Event.Add(inv);


        }

        public void Compare(List<char> target, List<char> mem)
        {
            for (int i = 0; i < target.Count; i++)
            {
                for (int j = 0; j < mem.Count; j++)
                {
                    
                }
            }
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
    


        // 
        public void ArrayToString()
        {
            string[] array = new string[] { "Hi", "My", "Name", "Apple" };



            StringBuilder builder = new StringBuilder();

            Console.Write("Array : ");

            foreach (string value in array)
            {
                Console.Write(value + " ");
                builder.Append(value);
                builder.Append("3");
            }

            Console.WriteLine("");
            Console.WriteLine("String : " + builder.ToString());
        }


        // 간단한 문자열 비교
        /*
        public void StringCompare()
        {
            string root = "Direct";
            string root2 = "direct";

            bool result = root.Equals(root2, StringComparison.Ordinal);

            Console.WriteLine("Ordinal comparison: {0} and {1} are {2}", root, root2,
                                result ? "equal." : "not equal.");
        }
        */


        // 데이터 두개를 입력받으면 유사도를 측정한다.
        public void Compare()
        {
            int[,] array = new int[1001, 1001];

            Console.Write("Enter the data : ");
            string value1 = Console.ReadLine();

            Console.Write("Enter the data : ");
            string value2 = Console.ReadLine();
            Console.WriteLine("");

            char[] st_arr1 = value1.Trim().ToCharArray();
            char[] st_arr2 = value2.Trim().ToCharArray();

            for (int i = 1; i <= st_arr1.Length; i++)
                array[i, 0] = i;

            for (int i = 1; i <= st_arr2.Length; i++)
                array[0, i] = i;


            for (int i = 1; i <= st_arr1.Length; i++)
            {
                for (int j = 1; j <= st_arr2.Length; j++)
                {
                    if (st_arr1[i - 1] == st_arr2[j - 1]) array[i, j] = array[i - 1, j - 1];
                    else array[i, j] = Math.Min(array[i - 1, j - 1] + 1, Math.Min(array[i, j - 1] + 1, array[i - 1, j] + 1));
                }
            }


            for (int i = 0; i <= st_arr1.Length; i++)
            {
                for (int j = 0; j <= st_arr2.Length; j++)
                {
                    Console.Write(array[i, j] + "\t");
                }
                Console.WriteLine("");
            }

            Console.WriteLine("");
            // array[st_arr1.Length - 1, st_arr2.Length - 1] == 유사도 결과값.
            Console.WriteLine("Last Num : " + array[st_arr1.Length, st_arr2.Length]);
            Console.WriteLine("Return : " + Compare(value1, value2));
        }

        int Compare(string value1, string value2)
        {
            int[,] array = new int[1001, 1001];

            char[] arr1 = value1.Trim().ToCharArray();
            char[] arr2 = value2.Trim().ToCharArray();


            for (int i = 1; i <= arr1.Length; i++)
                array[i, 0] = i;

            for (int i = 1; i <= arr2.Length; i++)
                array[0, i] = i;


            for (int i = 1; i <= arr1.Length; i++)
            {
                for (int j = 1; j <= arr2.Length; j++)
                {
                    if (arr1[i - 1] == arr2[j - 1]) array[i, j] = array[i - 1, j - 1];
                    else array[i, j] = Math.Min(array[i - 1, j - 1] + 1, Math.Min(array[i, j - 1] + 1, array[i - 1, j] + 1));
                }
            }

            return array[arr1.Length, arr2.Length];
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
